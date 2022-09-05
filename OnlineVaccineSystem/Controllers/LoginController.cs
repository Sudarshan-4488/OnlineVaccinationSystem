using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineVaccineSystem.data;

namespace OnlineVaccineSystem.Controllers
{
    public class LoginController : Controller
    {
        private VaccineSystemEntities2 db = new VaccineSystemEntities2();

        // GET: Login
        public ActionResult Index()
        {
            return View(db.UserMasters.ToList());
        }
        // GET: Login/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "EmailId,Password")] UserMaster userMaster)
        {
            UserMaster user= db.UserMasters.FirstOrDefault(x=>x.EmailId==userMaster.EmailId && x.Password==userMaster.Password);
            if (user == null)
            {
                return HttpNotFound();
            }
            if (user.IsAdmin == true)
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            else
            {
                return RedirectToAction("Index", "UserDashboard", user);
            }
        }
        public ActionResult ForgotPassowrd()
        {
            return View("ForgotPassword");
        }
        public ActionResult ForgotPassowrd(string email)
        {

            UserMaster userMaster = db.UserMasters.FirstOrDefault(x => x.EmailId == email);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            else
            {
                ModelState.AddModelError("error", "Your Password Is : " + userMaster.Password);
                return View(userMaster);
            }
            return View(userMaster);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
