using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class LoanApplicationViewModel
    {
        public double Amount { get; set; }
        /// <summary>
        /// Duration in months.
        /// </summary>
        public int Term { get; set; }


        public LoanApplicationPartnerViewModel Partner { get; set; }
        public List<LoanApplicationReference> References { get; set; }
    }

    public class LoanApplicationPartnerViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool IsFemale { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Relationship { get; set; }
        [Required]
        [DisplayName("Mobile Number")]
        public string MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string Employer { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string EmployerAddress { get; set; }
        [Required]
        public int YearsInJob { get; set; }
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