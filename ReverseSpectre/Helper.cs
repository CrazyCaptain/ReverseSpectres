using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre
{
    public class Helper
    {
        public class StorageHelper
        {
            /// <summary>
            /// Uploads file to specified blob container.
            /// </summary>
            /// <param name="storageConnectionString">Connection string to storage account.</param>
            /// <param name="containerName">Blob container name, will create if it does not exist.</param>
            /// <param name="fileStream">MemoryStream for file upload.</param>
            /// <param name="fileName">Name of file on blob.</param>
            /// <returns>Returns the URL of the uploaded file.</returns>
            public static async Task<string> UploadToStorage(string storageConnectionString, string containerName, Stream fileStream, string fileName)
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

                // Create the Blob Client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);
                
                // Retrieve a reference to a Container.
                CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                // Create the Container if it doesn't already exist.
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                
                // Upload file
                await blob.UploadFromStreamAsync(fileStream);
                return blob.Uri.ToString();
            }
        }

        public class Email
        {
            /// <summary>
            /// Sends html email to specified email address.
            /// </summary>
            /// <param name="email">Recipient email address.</param>
            /// <param name="subject">Email subject.</param>
            /// <param name="body">Email htm body content.</param>
            /// <returns></returns>
            public static async Task SendEmailAsync(string email, string subject, string body)
            {
                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    // Set smtp credentials
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["RegistrationEmail"],
                        ConfigurationManager.AppSettings["RegistrationEmailPassword"]);
                    smtp.EnableSsl = true;
                    string emailFrom = ConfigurationManager.AppSettings["RegistrationEmail"];
                    // Create mail
                    using (MailMessage mail = new MailMessage(emailFrom, email))
                    {
                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
            }
        }

        public class LoanRequirements
        {
            public static List<LoanApplicationDocument> GetHomeLoanRequirements(LoanApplication application)
            {
                // Set required documents
                List<LoanApplicationDocument> documents = new List<LoanApplicationDocument>();
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Marriage Contract",
                    Comment = "Only if applicable",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Government Id",
                    Comment = "Requires picture and signature",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Certificate of Employment",
                    Comment = "For locally employed applicants",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Payslips",
                    Comment = "3 months latest payslips",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Business Registration",
                    Comment = "For self-employed applicants",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Bank Statements",
                    Comment = "For self-employed applicants",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Bank Statements",
                    Comment = "For self-employed applicants",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Income Tax Return",
                    Comment = "For self-employed applicants",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Statement of Account",
                    Comment = "",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                documents.Add(new LoanApplicationDocument()
                {
                    Name = "Contract to Sell or Reservation Agreement",
                    Comment = "",
                    TimestampCreated = DateTime.Now,
                    Status = LoanDocumentStatusType.Pending,
                    LoanApplication = application
                });
                return documents;
            }
        }


    }

    public static class HtmlHelpers
    {
        public static string MakeActiveClass(this UrlHelper urlHelper, string controller)
        {
            string result = "active";
            string controllerName = urlHelper.RequestContext.RouteData.Values["controller"].ToString();

            if (!controllerName.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                result = null;
            }

            return result;
        }

        public static string SetClass(this HtmlHelper htmlHelper, bool condition, string className)
        {
            if (condition)
                return className;
            else
                return null;
        }

        public static string GetControllerName(this UrlHelper urlHelper)
        {
            return urlHelper.RequestContext.RouteData.Values["controller"].ToString();
        }
    }
}