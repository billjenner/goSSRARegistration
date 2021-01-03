using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcMembership;
using MvcMembership.Settings;
using goSSRA.Registration.Models;

namespace goSSRA.Registration.Controllers
{
    public class ProgramController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /Program/
        public ActionResult Index()
        {
            // only active program list for non-admins
            if (User.IsInRole("Admin"))
            {
               var _programs = from p in db.Programs
                            orderby p.Active
                            select p;

                return View(_programs.ToList());
            }
            else
            {
               var _programs = from p in db.Programs
                            where p.Active == true
                            select p;

                return View(_programs.ToList());
            }
        }

        //
        // GET: /Program/Details/5
        public ActionResult Details(Int32 id)
        {
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        //
        // GET: /Program/Create
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Program/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Create(Program program)
        {
            if (ModelState.IsValid)
            {
                db.Programs.Add(program);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(program);
        }

        //
        // GET: /Program/Edit/5
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Edit(Int32 id)
        {
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        //
        // POST: /Program/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Edit(Program program)
        {
            if (ModelState.IsValid)
            {
                db.Entry(program).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        //
        // GET: /Program/Delete/5
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Delete(Int32 id)
        {
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        //
        // POST: /Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult DeleteConfirmed(Int32 id)
        {
           // test to make sure record not in use before allowing a delete
            var query = from e in db.Enrollment
                        where (e.Program.ProgramID == id)
                        select e;

            if (query.Count() < 1)
            {
                Program program = db.Programs.Find(id);

                db.Programs.Remove(program);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
