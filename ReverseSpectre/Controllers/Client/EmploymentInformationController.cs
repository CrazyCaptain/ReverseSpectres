using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers.Client
{
    public class EmploymentInformationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmploymentInformation
        public ActionResult Index()
        {
            string uid = User.Identity.GetUserId();

            ReverseSpectre.Models.Client client = db.Clients.FirstOrDefault(c => c.UserId == uid);
            var ei = db.EmploymentInformations.Where(e => e.Client == client).ToList();
            return View(ei);
        }

        // GET: EmploymentInformation/Create
        public ActionResult Create()
        {
            string uid = User.Identity.GetUserId();

            EmploymentInformation ei = new EmploymentInformation();
            ei.Client = db.Clients.FirstOrDefault(c => c.UserId == uid);

            return View(ei);
        }

        // POST: EmploymentInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmploymentInformationId,SourceOfFunds,SourceOfFundsInfo,Employer,Position,FormOfBusiness,EmployerBusinessAddress,ContactNo,NatureOfJob,YrsInJob,IsOverseas")] EmploymentInformation employmentInformation)
        {
            employmentInformation.DateTimeCreated = DateTime.UtcNow.AddHours(8);

            if (ModelState.IsValid)
            {
                db.EmploymentInformations.Add(employmentInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(employmentInformation);
        }

        // GET: EmploymentInformation/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: EmploymentInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmploymentInformationId,SourceOfFunds,SourceOfFundsInfo,Employer,Position,FormOfBusiness,EmployerBusinessAddress,ContactNo,NatureOfJob,YrsInJob,IsOverseas,ClientId,DateTimeCreated")] EmploymentInformation employmentInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employmentInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employmentInformation);
        }

        // GET: EmploymentInformation/Delete/5
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

        // POST: EmploymentInformation/Delete/5
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
