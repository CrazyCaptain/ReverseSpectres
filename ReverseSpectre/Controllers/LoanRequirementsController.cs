using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers
{
    public class LoanRequirementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LoanRequirements
        public ActionResult Index()
        {
            return View(db.LoanRequirements.ToList());
        }

        // GET: LoanRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRequirement loanRequirement = db.LoanRequirements.Find(id);
            if (loanRequirement == null)
            {
                return HttpNotFound();
            }
            return View(loanRequirement);
        }

        // GET: LoanRequirements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoanRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoanRequirementId,Name,Link,Description,DateTimeCreated")] LoanRequirement loanRequirement)
        {
            if (ModelState.IsValid)
            {
                db.LoanRequirements.Add(loanRequirement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loanRequirement);
        }

        // GET: LoanRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRequirement loanRequirement = db.LoanRequirements.Find(id);
            if (loanRequirement == null)
            {
                return HttpNotFound();
            }
            return View(loanRequirement);
        }

        // POST: LoanRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoanRequirementId,Name,Link,Description,DateTimeCreated")] LoanRequirement loanRequirement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loanRequirement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loanRequirement);
        }

        // GET: LoanRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanRequirement loanRequirement = db.LoanRequirements.Find(id);
            if (loanRequirement == null)
            {
                return HttpNotFound();
            }
            return View(loanRequirement);
        }

        // POST: LoanRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanRequirement loanRequirement = db.LoanRequirements.Find(id);
            db.LoanRequirements.Remove(loanRequirement);
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
