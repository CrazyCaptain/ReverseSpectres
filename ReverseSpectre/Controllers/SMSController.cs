using Globe.Connect;
using Newtonsoft.Json.Linq;
using ReverseSpectre.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
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
            Trace.TraceInformation("Token: "+access_token+" Number: "+subscriber_number);
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

            return null;
        }

        public ActionResult Inquiry()
        {
            String data = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            JObject result = JObject.Parse(data);

            Trace.TraceInformation(data.ToString());

            string customer_msg = result["inboundSMSMessageList"]["inboundSMSMessage"][0]["message"].ToString();
            string customer_number = result["inboundSMSMessageList"]["inboundSMSMessage"][0]["senderAddress"].ToString();

            // convert globe api format tel:+639 to 09
            string mobile_number = "0" + customer_number.Substring(7);
            
            // log message
            Trace.TraceInformation(customer_msg + " from " + mobile_number);

            ReverseSpectre.Models.Client client = db.Clients.FirstOrDefault(c=>c.MobileNumber == mobile_number);
            // log client
            Trace.TraceInformation("Name: " + client.FirstName);

            // if match any keyword
            bool match = false;

            // explode string to array
            string[] msg = customer_msg.ToLower().Split(' ');

            // log 1st word of message
            Trace.TraceInformation(msg[0]);

            return null;
        }
    }
}
