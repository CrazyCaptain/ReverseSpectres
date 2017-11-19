using Globe.Connect;
using Newtonsoft.Json.Linq;
using ReverseSpectre.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReverseSpectre.Api
{
    public class SmsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string short_code = ConfigurationManager.AppSettings["SmsShortCode"];

        [HttpGet]
        public IHttpActionResult Redirect(string access_token, string subscriber_number)
        {
            Trace.TraceInformation("Token: " + access_token + " Number: " + subscriber_number);
            string subscriber_number_p = "0" + subscriber_number;

            ReverseSpectre.Models.Client client = db.Clients.FirstOrDefault(c => c.MobileNumber == subscriber_number_p);

            if (client != null)
            {
                client.SmsAccessToken = access_token;
                db.SaveChanges();

                Sms sms = new Sms(short_code, access_token);

                dynamic response = sms
                    .SetReceiverAddress("+63" + subscriber_number)
                    .SetMessage("Hello, " + client.FirstName + ". Your subscription to Spectres-UnionBank app has been verified. Send 'learn more' or 'saklolo' or 'help' for list of keywords.")
                    .SendMessage()
                    .GetDynamicResponse();

                Trace.TraceInformation(subscriber_number);
            }
            else
            {
                Trace.TraceInformation("Mobile number doesn't exist.");
            }

            return Ok();
        }

        public IHttpActionResult Inquiry(InboundSms sms)
        {
            string customerMessage, customerNumber = string.Empty;

            if (!sms.InboundSmsMessageList.Any())
                return BadRequest();
            else
            {
                customerMessage = sms.InboundSmsMessageList.First().Message;
                customerNumber = sms.InboundSmsMessageList[0].BaseSenderAddress;
            }

            // convert globe api format tel:+639 to 09
            string mobileNumber = $"0{customerNumber.Substring(7)}";

            // log message
            Trace.TraceInformation($"{customerMessage} from {mobileNumber}");

            ReverseSpectre.Models.Client client = db.Clients.FirstOrDefault(c => c.MobileNumber == mobileNumber);
            // log client
            Trace.TraceInformation("Name: " + client.FirstName);

            // if match any keyword
            bool match = false;

            // explode string to array
            string[] msg = customerMessage.ToLower().Split(' ');

            // log 1st word of message
            Trace.TraceInformation(msg[0]);

            if (msg[0] == "hello" || msg[0] == "hi" || msg[0] == "test")
            {
                if (client.SmsAccessToken != null)
                {
                    try
                    {
                        sendSMS($"Hello {client.FirstName}.", client.SmsAccessToken, customerNumber.Substring(4));
                    }
                    catch(Exception ex)
                    {
                        Trace.TraceInformation($"Error: {ex.Message}");
                    }
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
                    .SetReceiverAddress("+63" + subscriber_number)
                    .SetMessage(msg)
                    .SendMessage()
                    .GetDynamicResponse();
            }
        }
    }
}
