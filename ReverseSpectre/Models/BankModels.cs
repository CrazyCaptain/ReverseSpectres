using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
            CivilStatus = client.CivilStatus;
            TIN = client.TIN;
            SSS = client.SSS;
            CurrentAddress = client.CurrentAddress;
            PermanentAddress = client.PermanentAddress;
            MobileNumber = client.MobileNumber;
            UserId = user.Id;
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
        [Required]
        public string Nationality { get; set; }
        [Required]
        public CivilStatusType CivilStatus { get; set; }

        [Required]
        public string TIN { get; set; }
        public string SSS { get; set; }
        [Required]
        public string CurrentAddress { get; set; }
        [Required]
        public string PermanentAddress { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string AccessToken { get; set; } // for sms query and alerts
        //[Required]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Must be a vaild email.")]
        //public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual EmploymentInformation EmploymentInformation { get; set; }
    }

    public enum CivilStatusType
    {
        Single = 1,
        Married = 2,
        Widowed =3,
        Seperated = 4,
        Others = 5
    }

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

    public class EmploymentInformation
    {
        public int EmploymentInformationId { get; set; }

        public byte SourceOfFunds { get; set; } //Salary,Business,Commission/Fees,Remittance,Others
        public string SourceOfFundsInfo { get; set; }

        public string Employer { get; set; }
        public string Position { get; set; }

        public byte FormOfBusiness { get; set; } //Sole Proprietor,Partnership,Corporation

        public string EmployerBusinessAddress { get; set; }
        public string ContactNo { get; set; }
        public string NatureOfJob { get; set; }
        public byte YrsInJob { get; set; }

        public bool IsOverseas { get; set; } //determine if OFW
        
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        [Required]
        public virtual Client Client { get; set; }

        public DateTime DateTimeCreated { get; set; }
        //public DateTime? DateTimeExpired { get; set; } // enable 1 to many, latest employment info

        public IEnumerable<SelectListItem> SourceOfFundsList
        {
            get
            {
                return new List<SelectListItem>{
                    new SelectListItem { Text = "Salary", Value = "0"},
                    new SelectListItem { Text = "Business", Value = "1"},
                    new SelectListItem { Text = "Commission/Fees", Value = "2"},
                    new SelectListItem { Text = "Remittances", Value = "3"},
                    new SelectListItem { Text = "Others", Value = "4"}
                };
            }
        }

        public IEnumerable<SelectListItem> FormOfBusinessList
        {
            get
            {
                return new List<SelectListItem>{
                    new SelectListItem { Text = "Sole Proprietor", Value = "0"},
                    new SelectListItem { Text = "Partnership", Value = "1"},
                    new SelectListItem { Text = "Corporation", Value = "2"}
                };
            }
        }
    }
}