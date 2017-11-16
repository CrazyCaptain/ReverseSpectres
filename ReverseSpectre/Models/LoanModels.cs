using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class LoanApplication
    {
        public LoanApplication() { }
        public LoanApplication(LoanApplicationViewModel application, Client client)
        {
            Amount = application.Amount;
            Term = application.Term;
            TimestampCreated = DateTime.Now;
            LoanStatus = LoanStatusType.Pending;
            Client = client;
        }

        public int LoanApplicationId { get; set; }

        public double Amount { get; set; }
        // Duration in months
        [DisplayName("Term(Months)")]
        public int Term { get; set; }
        [DisplayName("Date of Application")]
        public DateTime TimestampCreated { get; set; }
        [DisplayName("Status")]
        public LoanStatusType LoanStatus { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        //public int RelationshipManagerId { get; set; }
        //public virtual RelationshipManager RelationshipManager { get; set; }

        public int LoanTypeId { get; set; }
        public virtual LoanType LoanType { get; set; }

        public virtual List<LoanApplicationReference> LoanApplicationReferences { get; set; }
        public virtual List<LoanApplicationDocument> LoanApplicationDocuments { get; set; }
    }

    public class LoanApplicationPartner
    {
        public LoanApplicationPartner() { }
        public LoanApplicationPartner(LoanApplicationPartnerViewModel partner)
        {
            FirstName = partner.FirstName;
            MiddleName = partner.MiddleName;
            LastName = partner.LastName;
            IsFemale = partner.IsFemale;
            Birthdate = partner.Birthdate;
            Nationality = partner.Nationality;
            Relationship = partner.Relationship;
            MobileNumber = partner.MobileNumber;
            Email = partner.Email;
            Employer = partner.Employer;
            Position = partner.Position;
            EmployerAddress = partner.EmployerAddress;
            YearsInJob = partner.YearsInJob;
        }

        public int LoanApplicationPartnerId { get; set; }
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

        public int LoanApplicationId { get; set; }
        public virtual LoanApplication LoanApplication { get; set; }
    }

    public class LoanApplicationReference
    {
        public int LoanApplicationReferenceId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public int LoanApplicationId { get; set; }
        public virtual LoanApplication LoanApplication { get; set; }
    }

    public class LoanType
    {
        public int LoanTypeId { get; set; }
        public string Name { get; set; }
        //// Duration in Months
        //public int Term { get; set; }
        public double InterestRate { get; set; }
    }
    
    public class LoanApplicationDocument
    {
        public int LoanApplicationDocumentId { get; set; }
        public string Name { get; set; }
        public LoanDocumentStatusType Status { get; set; }
        public DateTime TimestampCreated { get; set; }
        public string Comment { get; set; }

        public int LoanApplicationId { get; set; }
        public virtual LoanApplication LoanApplication { get; set; }

        public List<LoanApplicationDocumentFile> Files { get; set; }
    }
    public class LoanApplicationDocumentFile
    {
        public int LoanApplicationDocumentFileId { get; set; }
        [MaxLength(5)]
        public string FileType { get; set; }
        public string Url { get; set; }
        public DateTime TimestampCreated { get; set; }

        public int LoanApplicationDocumentId { get; set; }
        public virtual LoanApplicationDocument LoanApplicationDocument { get; set; }
    }

    public enum LoanStatusType
    {
        [Display(Name = "Pending for Review")]
        Pending = 0,
        [Display(Name = "Processing")]
        Processing = 1,
        [Display(Name = "Approved")]
        Approved = 2,
        [Display(Name = "Denied")]
        Denied = 3,
    }

    public enum LoanDocumentStatusType
    {
        [Display(Name = "Pending for Review")]
        Pending = 0,
        [Display(Name = "Requires Revision")]
        Revision = 1,
        [Display(Name = "Approved")]
        Approved = 2,
        [Display(Name = "Denied")]
        Denied = 3,
    }

    public enum MonthlyIncomeType
    {
        [Display(Name = "Less than 50,000")]
        PHP49999 = 0,
        [Display(Name = "50,000 - 69,999")]
        PHP50000_69999 = 1,
        [Display(Name = "70,000 - 99,999")]
        PHP70000_99999 = 2,
        [Display(Name = "100,000 - 199,999")]
        PHP100000_199999 = 3,
        [Display(Name = "200,000 - 299,999")]
        PHP200000_299999 = 4,
        [Display(Name = "300,000 above")]
        PHP300000 = 5,
    }
}