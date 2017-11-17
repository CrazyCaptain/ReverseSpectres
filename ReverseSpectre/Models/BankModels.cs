using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ReverseSpectre.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BranchName { get; set; }
    }

    public class RelationshipManager
    {
        public int RelationshipManagerId { get; set; }
        public bool IsDisabled { get; set; }

        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
    }

    public class Client
    {
        public Client() { }
        public Client(ClientRegistrationModel client, ApplicationUser user)
        {
            FirstName = client.FirstName;
            MiddleName = client.MiddleName;
            LastName = client.LastName;
            Birthdate = client.Birthdate;
            Nationality = client.Nationality;
            UserId = user.Id;
            MobileNumber = client.MobileNumber;
        }

        public int ClientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName {
            get
            {
                return $"{FirstName} {MiddleName} {LastName}";
            }
        }

        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; }
        public CivilStatusType CivilStatus { get; set; }

        public string TIN { get; set; }
        public string SSS { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string MobileNumber { get; set; }
        public string SmsAccessToken { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Must be a vaild email.")]
        //public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<EmploymentInformation> EmploymentInformation { get; set; }
    }

    public class EmploymentInformation
    {
        public EmploymentInformation() { }
        public EmploymentInformation(EmploymentInformationViewModel employment, Client client)
        {
            SourceOfFunds = employment.SourceOfFunds;
            SourceOfFundsInfo = employment.SourceOfFundsInfo;
            Employer = employment.Employer;
            Position = employment.Position;
            FormOfBusiness = employment.FormOfBusiness;
            EmployerBusinessAddress = employment.EmployerBusinessAddress;
            ContactNumber = employment.ContactNumber;
            NatureOfJob = employment.NatureOfJob;
            YearsInJob = employment.YearsInJob;
            IsOFW = employment.IsOFW;

            TimestampCreated = DateTime.Now;
            Client = client;
        }

        public int EmploymentInformationId { get; set; }

        [Required]
        public SourceOfFundsType SourceOfFunds { get; set; }
        [Required]
        public string SourceOfFundsInfo { get; set; }

        [Required]
        public string Employer { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public byte FormOfBusiness { get; set; }
        [Required]
        public string EmployerBusinessAddress { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string NatureOfJob { get; set; }
        [Required]
        public byte YearsInJob { get; set; }

        public bool IsOFW { get; set; }

        public DateTime TimestampCreated { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

    public enum CivilStatusType
    {
        Single = 1,
        Married = 2,
        Widowed =3,
        Seperated = 4,
        Others = 5
    }

    public enum SourceOfFundsType
    {
        [Display(Name = "Salary")]
        Salary = 0,
        [Display(Name = "Business")]
        Business = 1,
        [Display(Name = "Commision/Fees")]
        Commission_Fees = 2,
        [Display(Name = "Remittances")]
        Remittances = 3,
        [Display(Name = "Others")]
        Others = 4
    }

    public enum FormOfBusinessType
    {
        [Display(Name = "Sole Proprietor")]
        SoleProprietor = 0,
        [Display(Name = "Partnership")]
        Partnership = 1,
        [Display(Name = "Corporation")]
        Corporation = 2
    }

    
}