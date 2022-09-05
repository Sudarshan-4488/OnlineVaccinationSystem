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
    public class UserDashboardController : Controller
    {
        private VaccineSystemEntities2 db = new VaccineSystemEntities2();
        int UserId = 0;
        // GET: UserDashboard
        public ActionResult Index(UserMaster userMaster)
        {
            int id = userMaster.Id;
            if (TempData.ContainsKey("Id"))
                id = int.Parse(TempData["Id"].ToString());
            UserDashBoardModel model = new UserDashBoardModel();
            User user = new User();
            user._user = userMaster;
            user.Id = id;
            user.AppoimentMasters = new List<AppoimentMaster>(db.AppoimentMasters.Where(x => x.UserId == id).ToList());
            model.UserMaster = new List<User>();
            model.UserMaster.Add(user);

            var memberList = db.UserMasters.Where(x => x.UserId == id);
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


            return View("BookAppoiment", model);
        }

        public ActionResult Certiciate(int Id)
        {
            int id = Id; //int.Parse(TempData["Id"].ToString());
            var userMaster = db.UserMasters.FirstOrDefault(x => x.Id == id);
            return View("Certiciate", userMaster);
        }
        // GET: UserDashboard/Details/5
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

        // GET: UserDashboard/Create
        public ActionResult Create(int Id = 0)
        {
            TempData["Id"] = Id;
            return View();
        }

        // POST: UserDashboard/Create
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
                userMaster.UserId = int.Parse(TempData["Id"].ToString());
                db.UserMasters.Add(userMaster);
                db.SaveChanges();
                return RedirectToAction("Index", userMaster);
            }

            return View(userMaster);
        }

        // GET: UserDashboard/Edit/5
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

        // POST: UserDashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MobileNo,Password,EmailId,Address,BirthDate,CreatedDate,ModifiedDate,IsAdmin,IsDeleted")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userMaster);
        }

        // GET: UserDashboard/Delete/5
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

        // POST: UserDashboard/Delete/5
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
