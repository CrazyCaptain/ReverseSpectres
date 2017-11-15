using Globe.Connect;
using Newtonsoft.Json.Linq;
using ReverseSpectre.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReverseSpectre.Controllers
{
    public class SMSController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string short_code = "21587087";

        public ActionResult Redirect(string access_token, string subscriber_number)
        {
            Trace.TraceInformation("Token: "+access_token+" Number: "+subscriber_number);
            string subscriber_number_p = "0" + subscriber_number;

            MobileNumber MobileNo = db.MobileNumbers.FirstOrDefault(m => m.MobileNo == subscriber_number_p);

            if (MobileNo != null)
            {
                MobileNo.Token = access_token;
                db.SaveChanges();

                Sms sms = new Sms(short_code, access_token);

                dynamic response = sms
                    .SetReceiverAddress("+63" + subscriber_number)
                    .SetMessage("Hello, " + MobileNo.User.FirstName + ". Your subscription to Spectres-UnionBank app has been verified. Send 'learn more' or 'saklolo' or 'help' for list of keywords.")
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

            //Console.WriteLine(result);
            Trace.TraceInformation(customer_msg + " from " + mobile_number);

            var pm = db.MobileNumbers.FirstOrDefault(m => m.MobileNo == mobile_number);
            Trace.TraceInformation("Name: " + pm.User.FirstName);

            bool match = false;
            string[] msg = customer_msg.ToLower().Split(' ');

            Trace.TraceInformation(msg[0]);

            return null;
        }
    }
}
