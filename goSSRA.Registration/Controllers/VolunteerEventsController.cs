using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goSSRA.Registration.Models;

namespace goSSRA.Registration.Controllers
{
    public class VolunteerEventsController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /VolunteerEvents/
        public ActionResult Index()
        {
            return View(db.VolunteerEvents.ToList());
        }

        //
        // GET: /VolunteerEvents/Details/5
        public ActionResult Details(Int32 id)
        {
            VolunteerEvents volunteerevents = db.VolunteerEvents.Find(id);
            if (volunteerevents == null)
            {
                return HttpNotFound();
            }
            return View(volunteerevents);
        }

        //
        // GET: /VolunteerEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VolunteerEvents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolunteerEvents volunteerevents)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerEvents.Add(volunteerevents);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteerevents);
        }

        //
        // GET: /VolunteerEvents/Edit/5
        public ActionResult Edit(Int32 id)
        {
            VolunteerEvents volunteerevents = db.VolunteerEvents.Find(id);
            if (volunteerevents == null)
            {
                return HttpNotFound();
            }
            return View(volunteerevents);
        }

        //
        // POST: /VolunteerEvents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VolunteerEvents volunteerevents)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerevents).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteerevents);
        }

        //
        // GET: /VolunteerEvents/Delete/5
        public ActionResult Delete(Int32 id)
        {
            VolunteerEvents volunteerevents = db.VolunteerEvents.Find(id);
            if (volunteerevents == null)
            {
                return HttpNotFound();
            }
            return View(volunteerevents);
        }

        //
        // POST: /VolunteerEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            VolunteerEvents volunteerevents = db.VolunteerEvents.Find(id);
            db.VolunteerEvents.Remove(volunteerevents);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
