using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers.Client
{
    [RoutePrefix("Client/Profile")]
    public class ClientProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
    }
}