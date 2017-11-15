using ReverseSpectre.Extensions;
using ReverseSpectre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReverseSpectre.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Details()
        {
            string uid = User.Identity.GetUserId();
            UserViewModel uv = new UserViewModel(db.Users.FirstOrDefault(u => u.Id == uid));

            return View(uv);
        }

        // GET: MobileNumbers/Create
        public ActionResult AddMobileNumber()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMobileNumber(MobileNumberCreateModel mnc)
        {
            if (ModelState.IsValid)
            {
                mnc.UserId = User.Identity.GetUserId();

                if (db.MobileNumbers.Any(m=>m.IsPrimary == true && m.UserId == m.UserId))
                {
                    mnc.IsPrimary = false;
                }else
                {
                    mnc.IsPrimary = true;
                }

                if(db.MobileNumbers.Any(m => m.MobileNo == mnc.MobileNo && m.Token != null))
                {
                    ModelState.AddModelError(string.Empty, "A Verified Mobile number ("+mnc.MobileNo+") already exists in the database.");
                    return View(mnc);
                }

                if (db.MobileNumbers.Any(m => m.MobileNo == mnc.MobileNo && m.UserId == m.UserId))
                {
                    ModelState.AddModelError(string.Empty, "You already have this mobile number.");
                    return View(mnc);
                }

                MobileNumber mobileNumber = new MobileNumber(mnc);

                db.MobileNumbers.Add(mobileNumber);
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            
            return View(mnc);
        }
        
    }
}
