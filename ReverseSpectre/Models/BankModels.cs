using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public class BusinessManager
    {
        public int BusinessManagerId { get; set; }
        public DateTime TimestampCreated { get; set; }

        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public List<RelationshipManager> RelationshipManagers { get; set; }
    }

    public class RelationshipManager
    {
        public int RelationshipManagerId { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime TimestampCreated { get; set; }

        public int BusinessManagerId { get; set; }
        public virtual BusinessManager BusinessManager { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    public class Client
    {
        public Client() { }
        public Client(ClientRegistrationModel client, ApplicationUser user, ClientInvitation invitation)
        {
            BusinessName = invitation.BusinessName;
            BusinessAddress = client.BusinessAddress;
            FormOfBusiness = invitation.FormOfBusiness;
            TelephoneNumber = client.TelephoneNumber;

            UserId = user.Id;
        }

        public int ClientId { get; set; }
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
        [DisplayName("Business Address")]
        public string BusinessAddress { get; set; }
        [DisplayName("Nature of Business")]
        public FormOfBusinessType FormOfBusiness { get; set; }
        [DisplayName("Telephone Number")]
        public string TelephoneNumber { get; set; }
        public string SmsAccessToken { get; set; }
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public int RelationshipManagerId { get; set; }
        public virtual RelationshipManager RelationshipManager { get; set; }

        public virtual List<ContactInformation> ContactInformation { get; set; }
    }

    public class ContactInformation
    {
        public ContactInformation() { }
        public ContactInformation(ContactInformationViewModel contact, Client client)
        {
            FirstName = contact.FirstName;
            MiddleName = contact.MiddleName;
            LastName = contact.LastName;
            IsFemale = contact.IsFemale;
            Position = contact.Position;
            Email = contact.Email;
            MobileNumber = contact.MobileNumber;

            TimestampCreated = DateTime.Now;
            Client = client;
        }

        public int ContactInformationId { get; set; }

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

        public DateTime TimestampCreated { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

    public class ClientInvitation
    {
        public int ClientInvitationId { get; set; }
        public string Email { get; set; }
        public string BusinessName { get; set; }
        public FormOfBusinessType FormOfBusiness { get; set; }
        public Guid Token { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum CivilStatusType
    {
        Single = 0,
        Married = 1,
        Widowed = 2,
        Seperated = 3,
        Other = 4
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