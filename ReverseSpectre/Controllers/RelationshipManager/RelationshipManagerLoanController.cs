using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers
{
    public class RelationshipManagerLoanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
           
        public ActionResult Index()
        {
            //RelationshipManager rm = db.RelationshipManagers.FirstOrDefault(m => m.)
            return View();
        }
    }
}