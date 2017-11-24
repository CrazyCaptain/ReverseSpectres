using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class ClientInvitationViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
        [Required]
        [DisplayName("Form of Business")]
        public FormOfBusinessType FormOfBusiness { get; set; }
        [Required]
        [DisplayName("Accounting Officer")]
        public int AccountingOfficerId { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int Term { get; set; }
    }

    public class LoanApplicationViewModel
    {
        public double Amount { get; set; }
        /// <summary>
        /// Duration in months.
        /// </summary>
        public int Term { get; set; }
    }

    public class LoanApplicationDocumentFileViewModel
    {
        public string Name { get; set; }
        [Required]
        public HttpPostedFileBase File { get; set; }
        [Required]
        public int LoanApplicationDocumentId { get; set; }
    }

}