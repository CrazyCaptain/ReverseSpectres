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
    public class RelationshipManagerLoanController : Controller
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

        public ActionResult Loan(int? id)
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

        public ActionResult CreateLoan()
        {
            return View();
        }

        public async Task<ActionResult> InviteClient(ClientInvitationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get RelationshipManager
                RelationshipManager rm = db.RelationshipManagers.FirstOrDefault(m => m.User.UserName == User.Identity.Name);

                // Add entry
                db.ClientInvitations.Add(new ClientInvitation(model, rm));

                // Save changes
                await db.SaveChangesAsync();
            }

            return View(model);
        }
    }
}