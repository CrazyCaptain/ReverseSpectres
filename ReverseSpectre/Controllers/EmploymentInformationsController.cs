using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;
using ReverseSpectre.Extensions;

namespace ReverseSpectre.Controllers
{
    public class EmploymentInformationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmploymentInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentInformation employmentInformation = db.EmploymentInformations.Find(id);
            if (employmentInformation == null)
            {
                return HttpNotFound();
            }
            return View(employmentInformation);
        }

        // GET: EmploymentInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmploymentInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentInformationId,SourceOfFunds,SourceOfFundsInfo,Employer,Position,FormOfBusiness,EmployerBusinessAddress,ContactNo,NatureOfJob,YrsInJob,IsOverseas,DateTimeCreated,DateTimeExpired")] EmploymentInformation employmentInformation)
        {
            string uid = User.Identity.GetUserId();
            employmentInformation.UserId = uid;

            if (ModelState.IsValid)
            {
                db.EmploymentInformations.Add(employmentInformation);
                db.SaveChanges();
                return RedirectToAction("Details", "User");
            }
            
            return View(employmentInformation);
        }

        // GET: EmploymentInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmploymentInformation employmentInformation = db.EmploymentInformations.Find(id);
            if (employmentInformation == null)
            {
                return HttpNotFound();
            }
            return View(employmentInformation);
        }

        // POST: EmploymentInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmploymentInformation employmentInformation = db.EmploymentInformations.Find(id);
            db.EmploymentInformations.Remove(employmentInformation);
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
