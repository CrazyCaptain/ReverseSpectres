using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers
{
    public class SalesDirectorDashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var loans = db.Banks;

            return View();
        }
    }
}