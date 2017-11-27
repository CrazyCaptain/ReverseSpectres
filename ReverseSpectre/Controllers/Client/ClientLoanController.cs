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
    [Authorize(Roles = "Client")]
    [RoutePrefix("client/loan")]
    [Route("{action=index}")]
    public class ClientLoanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var model = db.LoanApplication.FirstOrDefault(m => m.Client.User.UserName == User.Identity.Name);

            return View(model);
        }

        [Route("applications/{id}")]
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

        [Route("application/document/{id}")]
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

        [Route("application/document/{id}")]
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

        [Route("redirect")]
        public ActionResult RedirectClient(string r)
        {
            ViewBag.redirect_link = r;
            return View("Redirect");
        }

    }
}