using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;
using System.Net;
using System.Threading.Tasks;

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
            return View();
        }

        public ActionResult Edit()
        {
            // Get client
            Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
            if (client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(new ClientViewModel(client));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel model)
        {
            // Get client
            Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
            if (client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(client);
        }

        public ActionResult EditEmployementInfo()
        {
            // Get client
            Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);
            if (client == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            // Convert latest entry to Viewmodel
            EmploymentInformationViewModel model;
            var employementInfo = client.EmploymentInformation.LastOrDefault();
            if (employementInfo == null)
                model = new EmploymentInformationViewModel();
            else
                model = new EmploymentInformationViewModel(employementInfo);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditEmployementInfo(EmploymentInformationViewModel model)
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