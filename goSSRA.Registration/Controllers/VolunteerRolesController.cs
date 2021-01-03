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
    public class VolunteerRolesController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /VolunteerRoles/
        public ActionResult Index()
        {
            return View(db.VolunteerRoles.ToList());
        }

        //
        // GET: /VolunteerRoles/Details/5
        public ActionResult Details(Int32 id)
        {
            VolunteerRoles volunteerroles = db.VolunteerRoles.Find(id);
            if (volunteerroles == null)
            {
                return HttpNotFound();
            }
            return View(volunteerroles);
        }

        //
        // GET: /VolunteerRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /VolunteerRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolunteerRoles volunteerroles)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerRoles.Add(volunteerroles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(volunteerroles);
        }

        //
        // GET: /VolunteerRoles/Edit/5
        public ActionResult Edit(Int32 id)
        {
            VolunteerRoles volunteerroles = db.VolunteerRoles.Find(id);
            if (volunteerroles == null)
            {
                return HttpNotFound();
            }
            return View(volunteerroles);
        }

        //
        // POST: /VolunteerRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VolunteerRoles volunteerroles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteerroles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteerroles);
        }

        //
        // GET: /VolunteerRoles/Delete/5
        public ActionResult Delete(Int32 id)
        {
            VolunteerRoles volunteerroles = db.VolunteerRoles.Find(id);
            if (volunteerroles == null)
            {
                return HttpNotFound();
            }
            return View(volunteerroles);
        }

        //
        // POST: /VolunteerRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            VolunteerRoles volunteerroles = db.VolunteerRoles.Find(id);
            db.VolunteerRoles.Remove(volunteerroles);
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
