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
    [Authorize]
    public class AthleteController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /Athlete/
        public ActionResult Index(string sortOrder, int? sortDesc, string currentFilter, string searchString, bool excelFreindly = false)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = "First Name";
            ViewBag.LastNameSortParm = "Last Name";
            ViewBag.BirthdaySortParm = "Birthday";
            ViewBag.EmailSortParm = "Email";
            ViewBag.GenderSortParm = "Gender";
            ViewBag.adminRole = false;

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }

            // toggle sort order forwards & backwards
            int _sortDesc = sortDesc ?? 0;
            _sortDesc = (_sortDesc + 1) % 2;
            ViewBag.sortDesc = _sortDesc;

            ViewBag.CurrentFilter = searchString;

            var athletes = db.Athletes
                 .Where(a => a.AthleteID > 0);

            // only admins can see all member
            // users can see those associated with them
            if (!User.IsInRole("Admin"))
            {
                athletes = athletes
                    .Where(a => a.Email == User.Identity.Name);

                ViewBag.adminRole = false;
            } else
            {
                ViewBag.adminRole = true;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                athletes = athletes.Where(a => a.FirstName.ToUpper().Contains(searchString.ToUpper())
                                       || a.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || a.Gender.ToUpper().Contains(searchString.ToUpper())
                                       || a.Email.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (_sortDesc)
            {
                case 1:
                    switch (sortOrder)
                    {
                        case "First Name":
                            athletes = athletes.OrderBy(a => a.FirstName);
                            break;
                        case "Last Name":
                            athletes = athletes.OrderBy(a => a.LastName);
                            break;
                        case "Birthday":
                            athletes = athletes.OrderBy(a => a.Birthday);
                            break;
                        case "Email":
                            athletes = athletes.OrderBy(a => a.Email);
                            break;
                        case "Gender":
                            athletes = athletes.OrderBy(a => a.Gender);
                            break;
                        default:
                            athletes = athletes
                                .OrderBy(a => a.LastName)
                                .OrderBy(a => a.FirstName);
                            break;
                    }
                    break;
                default:
                    switch (sortOrder)
                    {
                        case "First Name":
                            athletes = athletes.OrderByDescending(a => a.FirstName);
                            break;
                        case "Last Name":
                            athletes = athletes.OrderByDescending(a => a.LastName);
                            break;
                        case "Birthday":
                            athletes = athletes.OrderByDescending(a => a.Birthday);
                            break;
                        case "Email":
                            athletes = athletes.OrderByDescending(a => a.Email);
                            break;
                        case "Gender":
                            athletes = athletes.OrderByDescending(a => a.Gender);
                            break;
                        default:
                            athletes = athletes
                                .OrderByDescending(a => a.LastName)
                                .OrderByDescending(a => a.FirstName);
                            break;
                    }
                    break;
            }
            if (excelFreindly)
            {
                return View("excelIndex", athletes.ToList());
            }

            return View(athletes.ToList());
  
        }


        //
        // GET: /Athlete/Details/5
        public ActionResult Details(Int32 id)
        {
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        //
        // GET: /Athlete/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Athlete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(athlete);
        }

        //
        // GET: /Athlete/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        //
        // POST: /Athlete/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlete).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(athlete);
        }

        //
        // GET: /Athlete/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Athlete athlete = db.Athletes.Find(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        //
        // POST: /Athlete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {

            // test to make sure record not in use before allowing a delete
            var query = from e in db.Enrollment
                        where (e.Athlete.AthleteID == id)
                        select e;

            if (query.Count() < 1)
            {
                Athlete athlete = db.Athletes.Find(id);

                db.Athletes.Remove(athlete);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // custom
        public ActionResult ExcelIndex(List<int> athleteList)
        {
            var query = from a in db.Athletes
                        from b in athleteList
                        where athleteList.Contains(a.AthleteID)
                        select a;


            return RedirectToAction("ExcelIndex", query.ToList());
        }

        [HttpPost]
        public ActionResult GetUserID()
        {
            return Json(User.Identity.Name);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
