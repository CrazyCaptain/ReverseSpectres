using Globe.Connect;
using Newtonsoft.Json.Linq;
using ReverseSpectre.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ReverseSpectre.Controllers
{
    public class SmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string short_code = ConfigurationManager.AppSettings["SmsShortCode"];

        public ActionResult Redirect(string access_token, string subscriber_number)
        {
            if (access_token == null && subscriber_number == null)
            {
                Trace.TraceInformation("Subscription cancelled.");
                return null;
            }else
            {
                Trace.TraceInformation("Token: " + access_token + " Number: " + subscriber_number);
                string subscriber_number_p = "0" + subscriber_number;

                ContactInformation ci = db.EmploymentInformations.FirstOrDefault(e => e.MobileNumber == subscriber_number_p);
                Client client = db.Clients.FirstOrDefault(c => c.ClientId == ci.ClientId);

                if (client != null)
                {
                    Trace.TraceInformation($"{ci.FirstName} found.");

                    client.SmsAccessToken = access_token;
                    db.SaveChanges();

                    Sms sms = new Sms(short_code, access_token);

                    dynamic response = sms
                        .SetReceiverAddress("+63" + subscriber_number)
                        .SetMessage("Hello, " + ci.FirstName + ". Your subscription to Spectres-UnionBank app has been verified. Send 'learn more' or 'saklolo' or 'help' for list of keywords.")
                        .SendMessage()
                        .GetDynamicResponse();

                    Trace.TraceInformation(subscriber_number);
                }
                else
                {
                    Trace.TraceInformation("Mobile number doesn't exist.");
                }

                return null;
            }
        }

        public ActionResult Inquiry()
        {
            String data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            JObject result = JObject.Parse(data);

            Trace.TraceInformation(data.ToString());

            string customer_msg = result["inboundSMSMessageList"]["inboundSMSMessage"][0]["message"].ToString();
            string customer_number = result["inboundSMSMessageList"]["inboundSMSMessage"][0]["senderAddress"].ToString();

            // convert globe api format tel:+639 to 09
            string mobileNumber = $"0{customer_number.Substring(7)}";

            // log message
            Trace.TraceInformation($"{customer_msg} from {mobileNumber}");

            ContactInformation ci = db.EmploymentInformations.FirstOrDefault(e => e.MobileNumber == mobileNumber);
            Client client = db.Clients.FirstOrDefault(c => c.ClientId == ci.ClientId);

            // log client
            Trace.TraceInformation("Name: " + ci.FirstName);

            // if match any keyword
            bool match = false;

            // explode string to array
            string[] msg = customer_msg.ToLower().Split(' ');

            // log 1st word of message
            Trace.TraceInformation(msg[0]);
            // log access token
            Trace.TraceInformation("Access Token: "+client.SmsAccessToken);
            // log sent to number
            Trace.TraceInformation("Client Number: " + customer_number.Substring(4));

            #region hello hi test
            if (msg[0] == "hello" || msg[0] == "hi" || msg[0] == "test")
            {
                match = true;
                if (client.SmsAccessToken != null)
                {
                    try
                    {
                        sendSMS($"Hello {ci.FirstName}.", client.SmsAccessToken, customer_number.Substring(4));
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceInformation($"Error: {ex.Message}");
                    }
                }
            }
            #endregion

            #region loans
            if (msg[0] == "loans")
            {
                match = true;
                if (client.SmsAccessToken != null)
                {
                    try
                    {
                        StringBuilder s_msg = new StringBuilder();
                        int loan_count = client.LoanApplications.Count();

                        s_msg.Append($"Hello {ci.FirstName}, you have {loan_count} registered loans in the system.");

                        if(loan_count > 0)
                        {
                            s_msg.Append(" Loan ID(s): ");
                            client.LoanApplications.ForEach(l => s_msg.Append($"{l.LoanApplicationId} "));
                            s_msg.Append(". To inquire about Loan Status or Details, send 'Loan <ID>'.");
                        }
                        
                        sendSMS(s_msg.ToString(), client.SmsAccessToken, customer_number.Substring(4));
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceInformation($"Error: {ex.Message}");
                    }
                }
            }
            #endregion

            #region loan
            if (msg[0] == "loan")
            {
                match = true;
                if (client.SmsAccessToken != null)
                {
                    int loan_id = Convert.ToInt16(msg[1]);
                    LoanApplication loan = client.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == loan_id);

                    if (loan == null)
                    {
                        try
                        {
                            sendSMS($"Hello {ci.FirstName}, you don't have loans registered with this ID.", client.SmsAccessToken, customer_number.Substring(4));
                        }
                        catch(Exception ex)
                        {
                            Trace.TraceInformation($"Error: {ex.Message}");
                        }
                    }
                    else
                    {
                        Trace.TraceInformation($"Loan with ID {loan.LoanApplicationId} found.");
                        Trace.TraceInformation($"Message length: {msg.Length}");

                        if (msg.Length > 2)
                        {
                            if (msg[2] == "docs")
                            {
                                Trace.TraceInformation($"docs keyword recognized.");

                                try
                                {
                                    StringBuilder s_msg = new StringBuilder();

                                    loan.LoanApplicationDocuments.ForEach(l => s_msg.Append($"{l.Name}, {l.Status.ToString()}. "));
                                    sendSMS(s_msg.ToString(), client.SmsAccessToken, customer_number.Substring(4));
                                }
                                catch (Exception ex)
                                {
                                    Trace.TraceInformation($"Error: {ex.Message}");
                                }

                            }
                        }
                        else
                        {
                            Trace.TraceInformation($"no docs keyword.");
                            Trace.TraceInformation($"{loan.LoanStatus}");

                            try
                            {
                                StringBuilder s_msg = new StringBuilder();

                                s_msg.Append($" Loan ID {loan.LoanApplicationId} with {loan.LoanStatus.ToString()} status and {loan.LoanApplicationDocuments.Count(ld => ld.Status == LoanDocumentStatusType.Approved)}/{loan.LoanApplicationDocuments.Count()} approved documents.");
                                sendSMS(s_msg.ToString(), client.SmsAccessToken, customer_number.Substring(4));
                            }
                            catch (Exception ex)
                            {
                                Trace.TraceInformation($"Error: {ex.Message}");
                            }
                        }
                    }
                }
            }
            #endregion

            if (match == false)
            {
                try
                {
                    sendSMS($"Here are the list of available keywords; 'Loans', 'Loan <ID>', 'Loan <ID> Docs'.", client.SmsAccessToken, customer_number.Substring(4));
                }
                catch(Exception ex)
                {
                    Trace.TraceInformation($"Error: {ex.Message}");
                }
            }
            return null;
        }

        private void sendSMS(string msg, string access_token, string subscriber_number)
        {
            if (access_token != null && msg != null)
            {
                Sms sms = new Sms(short_code, access_token);

                dynamic response = sms
                    .SetReceiverAddress(subscriber_number)
                    .SetMessage(msg)
                    .SendMessage()
                    .GetDynamicResponse();
            }
        }
    }
}
