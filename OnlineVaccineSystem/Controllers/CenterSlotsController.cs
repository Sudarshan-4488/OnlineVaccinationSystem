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
    public class CenterSlotsController : Controller
    {
        private VaccineSystemEntities2 db = new VaccineSystemEntities2();

        // GET: CenterSlots
        public ActionResult Index()
        {
            var centerSlots = db.CenterSlots.Include(c => c.VaccineCenter).Include(c => c.Vaccine);
            return View(centerSlots.ToList());
        }

        // GET: CenterSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CenterSlot centerSlot = db.CenterSlots.Find(id);
            if (centerSlot == null)
            {
                return HttpNotFound();
            }
            return View(centerSlot);
        }

        // GET: CenterSlots/Create
        public ActionResult Create()
        {
            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName");
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName");
            return View();
        }

        // POST: CenterSlots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StartDate,EndDate,VaccineId,CenterId,SlotCount,SlotTime")] CenterSlot centerSlot)
        {
            if (ModelState.IsValid)
            {
                if (centerSlot.StartDate == null || centerSlot.EndDate == null
                  || centerSlot.SlotTime == null || centerSlot.SlotCount == null)
                {
                    ModelState.AddModelError("error", "Please Fill All Details");
                    return View(centerSlot);
                }
                db.CenterSlots.Add(centerSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName", centerSlot.CenterId);
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName", centerSlot.VaccineId);
            return View(centerSlot);
        }

        // GET: CenterSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CenterSlot centerSlot = db.CenterSlots.Find(id);
            if (centerSlot == null)
            {
                return HttpNotFound();
            }
            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName", centerSlot.CenterId);
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName", centerSlot.VaccineId);
            return View(centerSlot);
        }

        // POST: CenterSlots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartDate,EndDate,VaccineId,CenterId,SlotCount,SlotTime")] CenterSlot centerSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(centerSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CenterId = new SelectList(db.VaccineCenters, "Id", "CenterName", centerSlot.CenterId);
            ViewBag.VaccineId = new SelectList(db.Vaccines, "Id", "VaccineName", centerSlot.VaccineId);
            return View(centerSlot);
        }

        // GET: CenterSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CenterSlot centerSlot = db.CenterSlots.Find(id);
            if (centerSlot == null)
            {
                return HttpNotFound();
            }
            return View(centerSlot);
        }

        // POST: CenterSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CenterSlot centerSlot = db.CenterSlots.Find(id);
            db.CenterSlots.Remove(centerSlot);
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
