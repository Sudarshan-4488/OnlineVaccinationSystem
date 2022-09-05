using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineVaccineSystem.data;
using OnlineVaccineSystem.Models;

namespace OnlineVaccineSystem.Controllers
{
    public class AdminDashboardController : Controller
    {
        private VaccineSystemEntities2 db = new VaccineSystemEntities2();

        // GET: AdminDashboard
        public ActionResult Index()
        {
            return View(db.UserMasters.ToList());
        }

        // GET: AdminDashboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // GET: AdminDashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminDashboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MobileNo,Password,EmailId,Address,BirthDate,CreatedDate,ModifiedDate,IsAdmin,IsDeleted")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                if (userMaster.EmailId == null || userMaster.Name == null
                   || userMaster.MobileNo == null || userMaster.Address == null
                   || userMaster.Password == null || userMaster.BirthDate == null)
                {
                    ModelState.AddModelError("error", "Please Fill All Details");
                    return View(userMaster);
                }
                userMaster.CreatedDate = DateTime.Now;
                userMaster.ModifiedDate = DateTime.Now;
                UserMaster user = db.UserMasters.FirstOrDefault(x => x.EmailId == userMaster.EmailId);
                if (user == null)
                {
                    db.UserMasters.Add(userMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    ModelState.AddModelError("error", "User Already Register ");
                    return View(userMaster);
                }
            }

            return View(userMaster);
        }

        // GET: AdminDashboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
           
            return View(userMaster);
        }


        public ActionResult ShowAppoiment(int Id)
        {
           
            UserDashBoardModel model = new UserDashBoardModel();
            User user = new User();
            user._user = db.UserMasters.FirstOrDefault(x=>x.Id==Id);
            user.Id = Id;
            user.AppoimentMasters = new List<AppoimentMaster>(db.AppoimentMasters.Where(x => x.UserId == Id).ToList());
            model.UserMaster = new List<User>();
            model.UserMaster.Add(user);

            var memberList = db.UserMasters.Where(x => x.UserId == Id);
            foreach (var item in memberList)
            {

                var appoimentMasters = new List<AppoimentMaster>(db.AppoimentMasters.Where(x => x.UserId == item.Id).ToList());
                model.UserMaster.Add(new Models.User
                {
                    _user = item,
                    Id = item.Id,
                    AppoimentMasters = appoimentMasters
                });
            }


            return View("ShowAppoiment", model);
        }
        // POST: AdminDashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MobileNo,Password,EmailId,Address,BirthDate,CreatedDate,ModifiedDate,IsAdmin,IsDeleted")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                UserMaster user = db.UserMasters.FirstOrDefault(x => x.EmailId == userMaster.EmailId);
                if (user == null)
                {
                    userMaster.ModifiedDate = DateTime.Now;
                db.Entry(userMaster).State = EntityState.Modified;
              
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("error", "User Already Register ");
                    return View(userMaster);
                }
            }
            return View(userMaster);
        }

        // GET: AdminDashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: AdminDashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMaster userMaster = db.UserMasters.Find(id);
            db.UserMasters.Remove(userMaster);
            db.SaveChanges();
            return RedirectToAction("Index");
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
