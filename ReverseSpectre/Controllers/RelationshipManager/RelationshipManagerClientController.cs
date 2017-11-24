using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace ReverseSpectre.Controllers
{
    [Authorize(Roles = "RelationshipManager")]
    [RoutePrefix("relationshipmanager/client")]
    [Route("{action=index}")]
    public class RelationshipManagerClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
           
        public ActionResult Index()
        {
            // Get RelationshipManager
            RelationshipManager rm = db.RelationshipManagers.FirstOrDefault(m => m.User.UserName == User.Identity.Name);

            // Get list of loans
            var loans = db.LoanApplication.Where(m => m.Client.RelationshipManagerId == rm.RelationshipManagerId).ToList();

            return View(loans);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var loan = db.LoanApplication.Find(id);
            if (loan == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(loan);
        }

        public ActionResult Add()
        {

            var query = db.AccountingOfficers.Include("User");

            List<SelectListItem> accountingOfficerList = new List<SelectListItem>();
            foreach (var item in query)
            {
                accountingOfficerList.Add(new SelectListItem()
                {
                    Text = $"{ item.User.LastName}, {item.User.FirstName} {item.User.MiddleName}",
                    Value = item.AccountingOfficerId.ToString()
                });
            }

            ViewBag.AccountingOfficerList = accountingOfficerList;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(ClientInvitationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get RelationshipManager
                RelationshipManager rm = db.RelationshipManagers.FirstOrDefault(m => m.User.UserName == User.Identity.Name);

                // Add entry
                ClientInvitation client = new ClientInvitation(model, rm);
                db.ClientInvitations.Add(client);

                // Save changes
                await db.SaveChangesAsync();

                // Send an email notification
                var callbackUrl = Url.Action("Register", "Account", new { token = client.Token }, protocol: Request.Url.Scheme);
                await Helper.Email.SendEmailAsync(
                    model.Email,
                    "Register your account",
                    RenderPartialViewToString("ClientInvitationEmail", new ClientRegistrationEmailViewModel() { Name = $"{model.BusinessName}", RedirectUrl = callbackUrl }));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        #region Helpers
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
    }
}