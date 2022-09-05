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
    public class AppoimentMastersController : Controller
    {
        private VaccineSystemEntities2 db = new VaccineSystemEntities2();

        // GET: AppoimentMasters
        public ActionResult Index()
        {
            var appoimentMasters = db.AppoimentMasters.Include(a => a.CenterSlot).Include(a => a.UserMaster).Include(a => a.Vaccine).Include(a => a.VaccineCenter);
            return View(appoimentMasters.ToList());
        }

        // GET: AppoimentMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppoimentMaster appoimentMaster = db.AppoimentMasters.Find(id);
            if (appoimentMaster == null)
            {
                return HttpNotFound();
            }
            return View(appoimentMaster);
        }

        // GET: AppoimentMasters/Create
        public ActionResult Create(int Id)
        {
            ViewBag.StateList= new SelectList(db.VaccineCenters, "Id", "CenterState");
            ViewBag.DistictList = new SelectList(db.VaccineCenters, "Id", "CenterDistict");
            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName");
            ViewBag.SlotID = new SelectList(db.CenterSlots, "Id", "SlotTime");
            ViewBag.UserId = new SelectList(db.UserMasters.Where(x=>x.Id==Id), "Id", "Name");
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName");
            return View();
        }

        // POST: AppoimentMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SlotID,CenterId,UserId,VaccineId,CreatedDate,ModifiedDate,AppoimentDate")] AppoimentMaster appoimentMaster)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserMasters.FirstOrDefault(x => x.Id == appoimentMaster.UserId);
                var prev = db.AppoimentMasters.Where(x => x.UserId == appoimentMaster.UserId).OrderBy(x => x.AppoimentDate);
                int days = 0;
                if(prev!=null)
                    try
                    {
                        days = (appoimentMaster.AppoimentDate - prev.FirstOrDefault().AppoimentDate).Value.Days;

                    }
                    catch (Exception)
                    {
                        days = 0;


                    }
                  
                var nextday = db.Vaccines.FirstOrDefault(x => x.Id == appoimentMaster.VaccineId).VaccineNextDays;

                if (days!=0 ||days > nextday)
                {
                    ViewBag.StateList = new SelectList(db.VaccineCenters, "Id", "CenterState");
                    ViewBag.DistictList = new SelectList(db.VaccineCenters, "Id", "CenterDistict");
                    ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName");
                    ViewBag.SlotID = new SelectList(db.CenterSlots, "Id", "SlotTime");
                    ViewBag.UserId = new SelectList(db.UserMasters.Where(x => x.Id == appoimentMaster.UserId), "Id", "Name");
                    ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName");
                    ModelState.AddModelError("error_msg", "You are not Eligible for Appoiment Now Please try After days : "+(days-nextday));
                    return View(appoimentMaster);
                }
                appoimentMaster.CreatedDate = DateTime.Now;
                appoimentMaster.ModifiedDate = DateTime.Now;
                db.AppoimentMasters.Add(appoimentMaster);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(appoimentMaster);
        }

        // GET: AppoimentMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppoimentMaster appoimentMaster = db.AppoimentMasters.Find(id);
            if (appoimentMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.SlotID = new SelectList(db.CenterSlots, "Id", "SlotTime", appoimentMaster.SlotID);
            ViewBag.UserId = new SelectList(db.UserMasters, "Id", "Name", appoimentMaster.UserId);
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName", appoimentMaster.VaccineId);
            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName", appoimentMaster.CenterId);
            return View(appoimentMaster);
        }

        // POST: AppoimentMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SlotID,CenterId,UserId,VaccineId,CreatedDate,ModifiedDate")] AppoimentMaster appoimentMaster)
        {
            if (ModelState.IsValid)
            {
                appoimentMaster.ModifiedDate = DateTime.Now;
                db.Entry(appoimentMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SlotID = new SelectList(db.CenterSlots, "Id", "SlotTime", appoimentMaster.SlotID);
            ViewBag.UserId = new SelectList(db.UserMasters, "Id", "Name", appoimentMaster.UserId);
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName", appoimentMaster.VaccineId);
            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName", appoimentMaster.CenterId);
            return View(appoimentMaster);
        }

        // GET: AppoimentMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppoimentMaster appoimentMaster = db.AppoimentMasters.Find(id);
            if (appoimentMaster == null)
            {
                return HttpNotFound();
            }
            return View(appoimentMaster);
        }

        // POST: AppoimentMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AppoimentMaster appoimentMaster = db.AppoimentMasters.Find(id);
            db.AppoimentMasters.Remove(appoimentMaster);
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
