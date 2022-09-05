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
    public class VaccineCentersController : Controller
    {
        private VaccineSystemEntities2 db = new VaccineSystemEntities2();

        // GET: VaccineCenters
        public ActionResult Index()
        {
            return View(db.VaccineCenters.ToList());
        }

        // GET: VaccineCenters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineCenter vaccineCenter = db.VaccineCenters.Find(id);
            if (vaccineCenter == null)
            {
                return HttpNotFound();
            }
            return View(vaccineCenter);
        }

        // GET: VaccineCenters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VaccineCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CenterName,CenterState,CenterDistict")] VaccineCenter vaccineCenter)
        {
            if (ModelState.IsValid)
            {
                if (vaccineCenter.CenterName == null || vaccineCenter.CenterState == null
                   || vaccineCenter.CenterDistict == null)
                {
                    ModelState.AddModelError("error", "Please Fill All Details");
                    return View(vaccineCenter);
                }
                db.VaccineCenters.Add(vaccineCenter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vaccineCenter);
        }

        // GET: VaccineCenters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineCenter vaccineCenter = db.VaccineCenters.Find(id);
            if (vaccineCenter == null)
            {
                return HttpNotFound();
            }
            return View(vaccineCenter);
        }

        // POST: VaccineCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CenterName,CenterState,CenterDistict")] VaccineCenter vaccineCenter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vaccineCenter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vaccineCenter);
        }

        // GET: VaccineCenters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VaccineCenter vaccineCenter = db.VaccineCenters.Find(id);
            if (vaccineCenter == null)
            {
                return HttpNotFound();
            }
            return View(vaccineCenter);
        }

        // POST: VaccineCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VaccineCenter vaccineCenter = db.VaccineCenters.Find(id);
            db.VaccineCenters.Remove(vaccineCenter);
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
