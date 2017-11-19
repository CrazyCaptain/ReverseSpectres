using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;
using System.Net;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ReverseSpectre.Controllers
{
    [Authorize(Roles = "Client")]
    [RoutePrefix("client/profile")]
    [Route("{action=index}")]
    public class ClientProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            // Get client
            Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);

            return View(client);
        }

        public ActionResult Edit()
        {
            // Get client
            Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
            
            if (client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Client model)
        {
            if (ModelState.IsValid)
            {
                
                // Get client
                Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
                
                // Edit client
                client.CivilStatus = model.CivilStatus;
                client.MobileNumber = model.MobileNumber;
                client.CurrentAddress = model.CurrentAddress;
                client.PermanentAddress = model.PermanentAddress;
                client.SSS = model.SSS;
                client.TIN = model.TIN;

                db.Entry(client).State = System.Data.Entity.EntityState.Modified;

                //Save changes
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult EditEmploymentInfo()
        {
            // Get client
            Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
            if (client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Convert latest entry to Viewmodel
            EmploymentInformationViewModel model;
            var employmentInfo = client.EmploymentInformation.LastOrDefault();
            if (employmentInfo == null)
                model = new EmploymentInformationViewModel();
            else
                model = new EmploymentInformationViewModel(employmentInfo);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditEmploymentInfo(EmploymentInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get client
                Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
                if (client == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }

                // Add entry
                db.EmploymentInformations.Add(new EmploymentInformation(model, client));

                // Save entry
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();
        }

    }
}