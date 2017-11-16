using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Configuration;

namespace ReverseSpectre.Controllers
{
    [Authorize]
    public class ClientLoanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = db.LoanApplication.Include("LoanType").Where(m => m.Client.User.UserName == User.Identity.Name);

            //List<LoanApplication> model = new List<Models.LoanApplication>();
            //foreach (var item in applications)
            //{
            //    model.Add(new Models.LoanApplication()
            //    {
            //        Amount = item.Amount,
            //        Term = item.Term,
            //        TimestampCreated = item.TimestampCreated,
            //        LoanStatus = item.loanst
            //    });
            //}

            return View(model.ToList());
        }

        public ActionResult LoanApplication(int? id)
        {
            // Validation
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var application = db.LoanApplication.Find(id);
            if (application == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (application.Client.User.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var documents = db.LoanApplicationDocuments.Include("Files").Where(m => m.LoanApplicationId == application.LoanApplicationId).ToList();
            application.LoanApplicationDocuments = documents;

            return View(application);
        }

        public ActionResult CreateLoanApplication()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLoanApplication(LoanApplicationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get client
                Client client = db.Clients.FirstOrDefault(m => m.User.UserName == User.Identity.Name);

                // Get loan (temporary)
                var loanType = db.LoanTypes.First();

                // Add entries
                LoanApplication application = new LoanApplication(model, client) { LoanType = loanType };
                db.LoanApplication.Add(application);
                db.LoanApplicationPartners.Add(new LoanApplicationPartner(model.Partner) { LoanApplication = application });

                foreach (var item in model.References ?? new List<LoanApplicationReference>())
                {
                    db.LoanApplicationReferences.Add(new LoanApplicationReference()
                    {
                        Address = item.Address,
                        FirstName = item.FirstName,
                        MiddleName = item.MiddleName,
                        LastName = item.LastName,
                        PhoneNumber = item.PhoneNumber,
                        LoanApplication = application
                    });
                }

                // Loan requirements
                foreach (var item in Helper.LoanRequirements.GetHomeLoanRequirements(application))
                {
                    db.LoanApplicationDocuments.Add(item);
                }

                // Save entries
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult UploadLoanApplicationDocument(int? id)
        {
            // Validation
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var document = db.LoanApplicationDocuments.Find(id);
            if (document == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (document.LoanApplication.Client.User.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(new LoanApplicationDocumentFileViewModel() { Name = document.Name, LoanApplicationDocumentId = document.LoanApplicationDocumentId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadLoanApplicationDocument(LoanApplicationDocumentFileViewModel model)
        {
            // Validation
            var document = db.LoanApplicationDocuments.Find(model.LoanApplicationDocumentId);
            if (document == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (document.LoanApplication.Client.User.UserName != User.Identity.Name)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                // Get storage strings
                string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
                string containerName = ConfigurationManager.AppSettings["LoanApplicationDocumentsContainerName"];
                string fileType = Path.GetExtension(model.File.FileName);
                string fileName = $"{document.Name.Replace(" ", string.Empty)}{DateTime.Now.ToString("yyyyMMddHHmmss")}{document.LoanApplicationDocumentId}{fileType}";

                // Upload to storage
                string url = await Helper.StorageHelper.UploadToStorage(storageConnectionString, containerName, model.File.InputStream, fileName);

                var documentFile = new LoanApplicationDocumentFile()
                {
                    LoanApplicationDocumentId = model.LoanApplicationDocumentId,
                    FileType = fileType,
                    Url = url,
                    TimestampCreated = DateTime.Now
                };

                // Add entry
                db.LoanApplicationDocumentFiles.Add(documentFile);

                // Save entry
                await db.SaveChangesAsync();

                return RedirectToAction("LoanApplication", new { id = document.LoanApplicationId });
            }
            return View();
        }


    }
}