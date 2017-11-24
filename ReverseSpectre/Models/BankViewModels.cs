using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class ClientRegistrationModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [DisplayName("Business Address")]
        public string BusinessAddress { get; set; }
        [Required]
        [DisplayName("Telephone Number")]
        public string TelephoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public ContactInformationViewModel ContactInformation { get; set; }
    }

    public class ClientViewModel
    {
        public ClientViewModel() { }
        public ClientViewModel(Client client)
        {
            BusinessAddress = client.BusinessAddress;
            BusinessName = client.BusinessName;
            FormOfBusiness = client.FormOfBusiness;
            TelephoneNumber = client.TelephoneNumber;
        }

        public int ClientId { get; set; }
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
        [DisplayName("Business Address")]
        public string BusinessAddress { get; set; }
        [Required]
        [DisplayName("Nature of Business")]
        public FormOfBusinessType FormOfBusiness { get; set; }
        [DisplayName("Telephone Number")]
        public string TelephoneNumber { get; set; }
        public string SmsAccessToken { get; set; }
        
        public List<ContactInformation> ContactInformation { get; set; }
    }

    public class ContactInformationViewModel
    {
        public ContactInformationViewModel() { }
        public ContactInformationViewModel(ContactInformation contact)
        {
            FirstName = contact.FirstName;
            MiddleName = contact.MiddleName;
            LastName = contact.LastName;
            IsFemale = contact.IsFemale;
            Position = contact.Position;
            Email = contact.Email;
            MobileNumber = contact.MobileNumber;
        }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Gender")]
        public bool IsFemale { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        [DisplayName("MobileNumber")]
        public string MobileNumber { get; set; }
    }
}
