using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;
using System.ComponentModel.DataAnnotations;
using ReverseSpectre.Extensions;
using System.Diagnostics;

namespace ReverseSpectre.Controllers
{
    [Authorize]
    public class LoansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Loans
        public ActionResult Index()
        {
            string uid = User.Identity.GetUserId();
            return View(db.Loans.Where(l=>l.UserId == uid).ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            string uid = User.Identity.GetUserId();
            ApplicationUser au = db.Users.FirstOrDefault(u => u.Id == uid);

            if (au.EmploymentInfo.FirstOrDefault() == null)
            {
                //if employment information is not yet filled, redirect to add info
                return RedirectToAction("Create", "EmploymentInformations", null);
            }

            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanId,LoanTerm,PropertyAddress,TypeOfProperty,TypeOfPropertyInfo,ContractPrice,Seller")] Loan loan)
        {
            loan.UserId = User.Identity.GetUserId();

            ApplicationUser au = db.Users.FirstOrDefault(u => u.Id == loan.UserId);
            EmploymentInformation ie = db.EmploymentInformations.FirstOrDefault(i => i.UserId == loan.UserId);

            List<LoanRequirement> lr = new List<LoanRequirement>();

            if (au.CivilStatus == 1) 
            {
                //if married or widowed, add marriage contract
                lr.Add(new LoanRequirement { Name = "Marriage Contract", Link = "", Status = 0, Description = "Photocopy of Marriage Contract", DateTimeCreated=DateTime.UtcNow.AddHours(8), Loan = loan });
            }

            if(ie.IsOverseas)
            {
                //if ofw
                lr.Add(new LoanRequirement() { Name = "SPA", Link = "", Status = 0, Description = "Original Consularized Special Power of Attorney", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
                lr.Add(new LoanRequirement() { Name = "Passport & Stamps", Link = "", Status = 0, Description = "Passport with entry and exit stamps", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
            }
            else if (ie.FormOfBusiness == 0)
            {
                //if sole proprietor
                lr.Add(new LoanRequirement() { Name = "Business Registration", Link = "", Status = 0, Description = "Business Registration", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
                lr.Add(new LoanRequirement() { Name = "Bank Statements", Link = "", Status = 0, Description = "Latest 6 months Bank Statements", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
                lr.Add(new LoanRequirement() { Name = "Financial Statements", Link = "", Status = 0, Description = "Financial Statements", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
                lr.Add(new LoanRequirement() { Name = "ITR", Link = "", Status = 0, Description = "ITR", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
                lr.Add(new LoanRequirement() { Name = "List of Customers", Link = "", Status = 0, Description = "Top 3 List of Customers/Suppliers with Contact Number (Landline)", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
            }
            else
            {
                //if locally employed
                lr.Add(new LoanRequirement() { Name = "Certificate of Employment", Link = "", Status = 0, Description = "Certificate of Employment", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
                lr.Add(new LoanRequirement() { Name = "Pay Slips", Link = "", Status = 0, Description = "3 months latest pay slips", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
            }

            //government id's
            lr.Add(new LoanRequirement() { Name = "Any Government ID", Link = "", Status = 0, Description = "Photocopy of any government-issued ID with picture and signature", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
            // collateral documents
            lr.Add(new LoanRequirement() { Name = "Statement of Account", Link = "", Status = 0, Description = "Statement of Account", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });
            lr.Add(new LoanRequirement() { Name = "Contract to Sell or Reservation Agreement", Link = "", Status = 0, Description = "Contract to Sell or Reservation Agreement", DateTimeCreated = DateTime.UtcNow.AddHours(8), Loan = loan });

            if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.LoanRequirements.AddRange(lr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loan);
        }

        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanId,LoanTerm,PropertyAddress,TypeOfProperty,TypeOfPropertyInfo,ContractPrice,Seller")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
