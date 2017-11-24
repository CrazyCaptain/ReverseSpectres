using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReverseSpectre.Models;

namespace ReverseSpectre.Controllers
{
    [Authorize(Roles = "RelationshipManager")]
    [RoutePrefix("relationshipmanager/dashboard")]
    [Route("{action=index}")]
    public class RelationshipManagerDashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }
    }
}