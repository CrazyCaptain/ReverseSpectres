using ReverseSpectre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ReverseSpectre.Controllers.CommercialBank
{
    [RoutePrefix("combank/loans")]
    [Route("{action=index}")]
    public class ComBankLoanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                // if client id is present

                Client client = db.Clients.FirstOrDefault(c => c.ClientId == id);

                if (client == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }else
                {
                    var loans = db.LoanApplications.Where(l => l.ClientId == id);
                    return View(loans);
                }

            }else
            {
                // if no client id provided

                var loans = db.LoanApplications.ToList();
                return View(loans);
            }
        }

        [Route("{id}/details")]
        public ActionResult Details(int id)
        {
            LoanApplication loan = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if(loan == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var documents = db.LoanApplicationDocuments.Include("Files").Where(m => m.LoanApplicationId == loan.LoanApplicationId).ToList();
            loan.LoanApplicationDocuments = documents;

            return View(loan);
        }

        [Route("{id}/add/requirement")]
        public ActionResult AddRequirement(int id)
        {
            LoanApplication loan = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (loan == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoanApplicationDocument lad = new LoanApplicationDocument();
            lad.LoanApplicationId = loan.LoanApplicationId;

            return View(lad);
        }

        [HttpPost]
        [Route("{id}/add/requirement")]
        public ActionResult AddRequirement([Bind(Include = "LoanApplicationDocumentId,Name,Comment,LoanApplicationId")] LoanApplicationDocument loanApplicationDocument)
        {
            loanApplicationDocument.Status = LoanDocumentStatusType.Pending;
            loanApplicationDocument.TimestampCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.LoanApplicationDocuments.Add(loanApplicationDocument);
                db.SaveChanges();
                return RedirectToAction("Details", new { id=loanApplicationDocument.LoanApplicationId });
            }

            return View(loanApplicationDocument);
        }

        //Loan Documents

        public ActionResult Approve(int id)
        {
            LoanApplicationDocument lad = db.LoanApplicationDocuments.FirstOrDefault(l => l.LoanApplicationDocumentId == id);

            if (lad == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            lad.Status = LoanDocumentStatusType.Approved;
            db.Entry(lad).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = lad.LoanApplicationId });
        }

        [Route("requirement/{id}/revise")]
        public ActionResult ReviseRequirement(int id)
        {
            LoanApplicationDocument lad = db.LoanApplicationDocuments.FirstOrDefault(l => l.LoanApplicationDocumentId == id);

            if (lad == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoanDocumentReviseModel ldrm = new LoanDocumentReviseModel(lad);

            return View(ldrm);
        }

        [Route("requirement/{id}/revise")]
        [HttpPost]
        public ActionResult Revise(LoanDocumentReviseModel ldrm)
        {
            if (ModelState.IsValid)
            {
                LoanApplicationDocument lad = db.LoanApplicationDocuments
                    .FirstOrDefault(l => l.LoanApplicationDocumentId == ldrm.LoanApplicationDocumentId);

                if (lad == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                lad.Status = LoanDocumentStatusType.Revision;
                lad.Comment = ldrm.Comment;

                db.Entry(lad).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = lad.LoanApplicationId });
            }

            return View(ldrm);
        }

        public ActionResult Deny(int id)
        {
            LoanApplicationDocument lad = db.LoanApplicationDocuments.FirstOrDefault(l => l.LoanApplicationDocumentId == id);

            if (lad == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            lad.Status = LoanDocumentStatusType.Denied;
            db.Entry(lad).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = lad.LoanApplicationId });
        }

        //Loan Status

            //Credit Investigation
        public ActionResult CreditInvestigation(int id)
        {
            LoanApplication la = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (la == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            la.LoanStatus = LoanStatusType.CreditInvestigation;
            db.Entry(la).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "ComBankLoan");
        }

        //Trade Checking
        public ActionResult TradeChecking(int id)
        {
            LoanApplication la = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (la == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            la.LoanStatus = LoanStatusType.TradeChecking;
            db.Entry(la).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "ComBankLoan");
        }

        //Credit Risk
        public ActionResult CreditRisk(int id)
        {
            LoanApplication la = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (la == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            la.LoanStatus = LoanStatusType.CreditRisk;
            db.Entry(la).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "ComBankLoan");
        }

        //Creating Proposal
        public ActionResult CreatingProposal(int id)
        {
            LoanApplication la = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (la == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            la.LoanStatus = LoanStatusType.CreatingProposal;
            db.Entry(la).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "ComBankLoan");
        }

        //Analyzing Proposal
        public ActionResult AnalyzingProposal(int id)
        {
            LoanApplication la = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (la == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            la.LoanStatus = LoanStatusType.AnalyzingProposal;
            db.Entry(la).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "ComBankLoan");
        }

        //Approve Loan
        public ActionResult ApprovedLoan(int id)
        {
            LoanApplication la = db.LoanApplications.FirstOrDefault(l => l.LoanApplicationId == id);

            if (la == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            la.LoanStatus = LoanStatusType.Approved;
            db.Entry(la).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "ComBankLoan");
        }
        

    }
}
