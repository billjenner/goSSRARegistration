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
    [Authorize]
    public class VolunteerCommitmentsController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /VolunteerCommitments/
        public ActionResult Index(string sortOrder, int? sortDesc, string currentFilter, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.EventNameSortParm = "Event Name";
            ViewBag.RoleNameSortParm = "Role Name";
            ViewBag.RegistersEmailSortParm = "Registers Email";
            ViewBag.FamilyNameSortParm = "Family Name";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }

            // toggle sort order forwards & backwards
            int _sortDesc = sortDesc ?? 0;
            _sortDesc = (_sortDesc + 1) % 2;
            ViewBag.sortDesc = _sortDesc;

            ViewBag.CurrentFilter = searchString;

            var vc = db.VolunteerCommitments.Include(v => v.VolunteerEvent).Include(v => v.VolunteerRole);

            if (!String.IsNullOrEmpty(searchString))
            {
                vc = vc.Where(v => v.VolunteerEvent.EventName.ToUpper().Contains(searchString.ToUpper())
                                       || v.VolunteerRole.Role.ToUpper().Contains(searchString.ToUpper())
                                       || v.Email.ToUpper().Contains(searchString.ToUpper())
                                       || v.FamilyName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (_sortDesc)
            {
                case 1:
                    switch (sortOrder)
                    {
                        case "Role Name":
                            vc = vc.OrderBy(s => s.VolunteerRole.Role);
                            break;
                        case "Registers Email":
                            vc = vc.OrderBy(s => s.Email);
                            break;
                        case "Family Name":
                            vc = vc.OrderBy(s => s.FamilyName);
                            break;
                        case "Event Name":
                        default:
                            vc = vc.OrderBy(s => s.VolunteerEvent.EventName);
                            break;
                     }
                    break;
                default:
                    switch (sortOrder)
                    {
                        case "Role Name":
                            vc = vc.OrderByDescending(s => s.VolunteerRole.Role);
                            break;
                        case "Registers Email":
                            vc = vc.OrderByDescending(s => s.Email);
                            break;
                        case "Family Name":
                            vc = vc.OrderByDescending(s => s.FamilyName);
                            break;
                        case "Event Name":
                        default:
                            vc = vc.OrderByDescending(s => s.VolunteerEvent.EventName);
                            break;
                     }
                    break;
            }
                
                return View(vc.ToList());
        }

        //
        // GET: /VolunteerCommitments/Details/5
        public ActionResult Details(Int32 id)
        {
            VolunteerCommitments volunteercommitments = db.VolunteerCommitments.Find(id);
            if (volunteercommitments == null)
            {
                return HttpNotFound();
            }
            return View(volunteercommitments);
        }

        //
        // GET: /VolunteerCommitments/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.VolunteerEvents, "EventID", "EventName");
            PopulateEventDropDownList();
            ViewBag.RoleID = new SelectList(db.VolunteerRoles, "RoleID", "Role");
            return View();
        }

        //
        // POST: /VolunteerCommitments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VolunteerCommitments volunteercommitments)
        {
            if (ModelState.IsValid)
            {
                db.VolunteerCommitments.Add(volunteercommitments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.VolunteerEvents, "EventID", "EventName", volunteercommitments.EventID);
            PopulateEventDropDownList(volunteercommitments.EventID);
            ViewBag.RoleID = new SelectList(db.VolunteerRoles, "RoleID", "Role", volunteercommitments.RoleID);
            return View(volunteercommitments);
        }

        //
        // GET: /VolunteerCommitments/Edit/5
        public ActionResult Edit(Int32 id)
        {
            VolunteerCommitments volunteercommitments = db.VolunteerCommitments.Find(id);
            if (volunteercommitments == null)
            {
                return HttpNotFound();
            }
            //ViewBag.EventID = new SelectList(db.VolunteerEvents, "EventID", "EventName", volunteercommitments.EventID);
            PopulateEventDropDownList(volunteercommitments.EventID);
            ViewBag.RoleID = new SelectList(db.VolunteerRoles, "RoleID", "Role", volunteercommitments.RoleID);
            return View(volunteercommitments);
        }

        //
        // POST: /VolunteerCommitments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VolunteerCommitments volunteercommitments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteercommitments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.EventID = new SelectList(db.VolunteerEvents, "EventID", "EventName", volunteercommitments.EventID);
            PopulateEventDropDownList(volunteercommitments.EventID);
            ViewBag.RoleID = new SelectList(db.VolunteerRoles, "RoleID", "Role", volunteercommitments.RoleID);
            return View(volunteercommitments);
        }
        
        // custom
        private void PopulateEventDropDownList(object selectedEvent = null)
        {
            var volunteerEventsQuery = from v in db.VolunteerEvents
                                       orderby v.Date
                                       where v.Active == true
                                       select v;

            ViewBag.EventID = new SelectList(volunteerEventsQuery, "EventID", "EventName", selectedEvent);
        }

        // custom
        private void PopulateRoleDropDownList(object selectedEvent = null)
        {
            var volunteerEventsQuery = from v in db.VolunteerEvents
                                       orderby v.Date
                                       where v.Active == true
                                       select v;

            ViewBag.EventID = new SelectList(volunteerEventsQuery, "EventID", "EventName", selectedEvent);
        }

        //
        // GET: /VolunteerCommitments/Delete/5
        public ActionResult Delete(Int32 id)
        {
            VolunteerCommitments volunteercommitments = db.VolunteerCommitments.Find(id);
            if (volunteercommitments == null)
            {
                return HttpNotFound();
            }
            return View(volunteercommitments);
        }

        //
        // POST: /VolunteerCommitments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            VolunteerCommitments volunteercommitments = db.VolunteerCommitments.Find(id);
            db.VolunteerCommitments.Remove(volunteercommitments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // custom
        [HttpPost]
        public ActionResult GetUserID()
        {
            return Json(User.Identity.Name);
        }

        // custom
        [HttpPost]
        public ActionResult GetRoles(string eventId)
        {
            int eventIdInt = 0;
            if (int.TryParse(eventId, out eventIdInt))
            {
                var _event = db.VolunteerEvents
                        .Where(e => e.Active == true)
                        .Where(e => e.EventID == eventIdInt)
                        .Single();

                var _roles = db.VolunteerRoles
                    .Where(r => r.Active == true)
                    .Where(r => ((r.AdminRole == _event.AdminRole) & (r.AdminRole == true)) ||
                                ((r.RaceRole == _event.RaceRole) & (r.RaceRole == true)) ||
                                ((r.OtherRole == _event.OtherRole) & (r.OtherRole == true)))
                    .Select(r => new
                    {
                        Key = r.Role,
                        Value = r.RoleID
                    }
                    );

                return Json(_roles);
            }

            return Json(false);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
