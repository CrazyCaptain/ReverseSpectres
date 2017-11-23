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