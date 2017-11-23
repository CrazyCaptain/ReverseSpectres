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

            // Generate model
            var model = new ClientViewModel(client);
            model.ContactInformation = client.ContactInformation.ToList() ?? new List<ContactInformation>();

            return View(model);
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
        public async Task<ActionResult> Edit(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                // Get client
                Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);

                // Edit client
                client.BusinessAddress = model.BusinessAddress;
                client.TelephoneNumber = model.TelephoneNumber;

                db.Entry(client).State = System.Data.Entity.EntityState.Modified;

                //Save changes
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
    }
}