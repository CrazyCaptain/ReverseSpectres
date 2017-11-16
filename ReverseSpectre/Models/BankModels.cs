using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
        [DataType(DataType.PhoneNumber, ErrorMessage = "Must be a valid mobile number.")]
        public string MobileNumber { get; set; }
        //[Required]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Must be a vaild email.")]
        //public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

    public enum CivilStatusType
    {
        Single = 1,
        Married = 2,
        Widowed =3,
        Seperated = 4,
        Others = 5
    }
}