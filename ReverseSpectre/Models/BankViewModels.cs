using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class ClientRegistrationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName
        {
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
        public string MobileNumber { get; set; }
    }

    public class ClientViewModel
    {
        public ClientViewModel() { }
        public ClientViewModel(Client client)
        {
            FirstName = client.FirstName;
            MiddleName = client.MiddleName;
            LastName = client.LastName;
            Birthdate = client.Birthdate;
            Nationality = client.Nationality;
            MobileNumber = client.MobileNumber;
            CivilStatus = client.CivilStatus;
            TIN = client.TIN;
            SSS = client.SSS;
            CurrentAddress = client.CurrentAddress;
            PermanentAddress = client.PermanentAddress;
            MobileNumber = client.MobileNumber;
        }

        public int ClientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName
        {
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

    public class EmploymentInformationViewModel
    {
        public EmploymentInformationViewModel() { }
        public EmploymentInformationViewModel(EmploymentInformation employment)
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
        }

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
    }
}