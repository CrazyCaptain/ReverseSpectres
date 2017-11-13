using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class MobileNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MobileNumberId { get; set; }
        [Required]
        public string MobileNo { get; set; }
        public string Token { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsPrimary { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual List<Message> Messages { get; set; }
    }

    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }

        public virtual MobileNumber MobileNumber { get; set; }

        public string Body { get; set; }
        public DateTime DateTimeCreated { get; set; }
    }

    public class TwoFactorAuth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TwoFactorAuthId { get; set; }

        public int Code { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeExpiry { get; set; }

        public virtual Loan Loan { get; set; }
    }
}