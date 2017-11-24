using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class TwoFactorAuthSMS
    {
        public int TwoFactorAuthSMSId { get; set; }

        public int Code { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeExpiry { get; set; }

        public int LoanApplicationId { get; set; }
        [ForeignKey("LoanApplicationId")]
        public virtual LoanApplication LoanApplication { get; set; }
    }

    public class InboundSms
    {
        [JsonProperty("inboundSMSMessageList")]
        public List<InboundSmsMessage> InboundSmsMessageList { get; set; }
    }

    public class InboundSmsMessage
    {
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("dateTime")]
        public string Timestamp { get; set; }
        [JsonProperty("resourceURL")]
        public string ResourceUrl { get; set; }
        [JsonProperty("senderAddress")]

        public string SenderAddress { get; set; }
        public string BaseSenderAddress
        {
            get
            {
                return SenderAddress.Substring(7);
            }
        }

        [JsonProperty("destinationAddress")]
        public string DestinationAddress { get; set; }
        public string DestinationSenderAddress
        {
            get
            {
                return SenderAddress.Substring(4);
            }
        }
    }
}