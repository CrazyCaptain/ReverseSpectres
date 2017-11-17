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

}