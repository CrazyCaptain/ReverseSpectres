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
        public LoanApplication(ClientInvitation invitation, Client client)
        {
            Amount = invitation.Amount;
            Term = invitation.Term;
            TimestampCreated = DateTime.Now;
            LoanStatus = LoanStatusType.Pending;
            Client = client;
        }

        public int LoanApplicationId { get; set; }

        public double Amount { get; set; }
        // Duration in months
        [DisplayName("Term(Months)")]
        public int Term { get; set; }
        public double Interest { get; set; }
        [DisplayName("Date of Application")]
        public DateTime TimestampCreated { get; set; }
        [DisplayName("Status")]
        public LoanStatusType LoanStatus { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        
        public virtual List<LoanApplicationDocument> LoanApplicationDocuments { get; set; }
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
        public string Comment { get; set; }

        public int LoanApplicationDocumentId { get; set; }
        public virtual LoanApplicationDocument LoanApplicationDocument { get; set; }
    }

    public class LoanDocumentReviseModel
    {
        public LoanDocumentReviseModel() { }

        public LoanDocumentReviseModel(LoanApplicationDocument lad)
        {
            LoanApplicationDocumentId = lad.LoanApplicationDocumentId;
            Comment = lad.Comment;
            Name = lad.Name;
            LoanApplicationId = lad.LoanApplicationId;
        }

        public int LoanApplicationDocumentId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public int LoanApplicationId { get; set; }
    }

    public enum LoanStatusType
    {
        [Display(Name = "Pending for Review")]
        Pending = 0,
        [Display(Name = "Credit Investigation")]
        CreditInvestigation = 1,
        [Display(Name = "Trade Checking")]
        TradeChecking = 2,
        [Display(Name = "Credit Risk")]
        CreditRisk = 3,
        [Display(Name = "Creating Proposal")]
        CreatingProposal = 4,
        [Display(Name = "Analyzing Proposal")]
        AnalyzingProposal = 5,
        [Display(Name = "Approve Loan")]
        Approved = 6,
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
}