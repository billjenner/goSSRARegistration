using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using goSSRA.Registration.Models;
using goSSRA.Registration.ViewModel;
using goSSRA.Registration.Areas;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Reflection;
using System.Net.Mail;
using MvcMembership;
using System.Text;


// To Due - future PDF generation
// The AllowPartiallyTrustedCallersAttribute requires the assembly to be signed with a strong name key. 
// This attribute is necessary since the control is called by either an intranet or Internet 
// Web page that should be running under restricted permissions.
//[assembly: AllowPartiallyTrustedCallers]
// To Due - future PDF generation



namespace goSSRA.Registration.Controllers
{
    [Authorize]
    public class EnrollmentController : Controller
    {

        private RegistrationDB db = new RegistrationDB();
        private readonly ISmtpClient _smtpClient;


        public EnrollmentController()
            : this(new SmtpClientProxy())
        {
        }

        public EnrollmentController(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        //
        // GET: /Order/
        public ActionResult Index(string sortOrder, int? sortDesc, string currentFilter, string searchString, bool excelFreindly = false)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.EnrollersNameSortParm = "Enrollers Name";
            ViewBag.FullNameSortParm = "Full Name";
            ViewBag.TitleSortParm = "Title";
            ViewBag.PaymentEmailSortParm = "Payment Email";
            ViewBag.PaidSortParm = "Paid";
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

            var enrollment = db.Enrollment
                .Where(e => e.EnrollmentID > 0)
                .Include(e => e.Athlete)
                .Include(e => e.Program);

            // only active program list for non-admins
            if (!User.IsInRole("Admin"))
            {
                enrollment = db.Enrollment
                    .Where(e => e.EnrollersName == User.Identity.Name)
                    .Include(e => e.Athlete)
                    .Include(e => e.Program);

                ViewBag.adminRole = false;
            }
            else
            {
                ViewBag.adminRole = true;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                enrollment = enrollment.Where(a => a.EnrollersName.ToUpper().Contains(searchString.ToUpper())
                                       || a.Athlete.FirstName.ToUpper().Contains(searchString.ToUpper())
                                       || a.Athlete.LastName.ToUpper().Contains(searchString.ToUpper())
                                       || a.Program.Title.ToUpper().Contains(searchString.ToUpper())
                                       || a.PaymentEmail.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (_sortDesc)
            {
                case 1:
                    switch (sortOrder)
                    {
                        case "Enrollers Name":
                            enrollment = enrollment.OrderBy(a => a.EnrollersName);
                            break;
                        case "Full Name":
                            enrollment = enrollment.OrderBy(a => a.Athlete.FirstName).OrderBy(a => a.Athlete.LastName);
                            break;
                        case "Title":
                            enrollment = enrollment.OrderBy(a => a.Program.Title);
                            break;
                        case "Payment Email":
                            enrollment = enrollment.OrderBy(a => a.PaymentEmail);
                            break;
                        case "Paid":
                            enrollment = enrollment.OrderBy(a => a.Paid);
                            break;
                        default:
                            enrollment = enrollment
                                .OrderBy(a => a.Athlete.FirstName).OrderBy(a => a.Athlete.LastName);
                            break;
                    }
                    break;
                default:
                    switch (sortOrder)
                    {
                        case "Enrollers Name":
                            enrollment = enrollment.OrderByDescending(a => a.EnrollersName);
                            break;
                        case "Full Name":
                            enrollment = enrollment.OrderByDescending(a => a.Athlete.FirstName).OrderByDescending(a => a.Athlete.LastName);
                            break;
                        case "Title":
                            enrollment = enrollment.OrderByDescending(a => a.Program.Title);
                            break;
                        case "Payment Email":
                            enrollment = enrollment.OrderByDescending(a => a.PaymentEmail);
                            break;
                        case "Paid":
                            enrollment = enrollment.OrderByDescending(a => a.Paid);
                            break;
                        default:
                            enrollment = enrollment
                                .OrderByDescending(a => a.Athlete.FirstName).OrderByDescending(a => a.Athlete.LastName);
                            break;
                    }
                    break;
            }
            if (excelFreindly)
            {
                return View("excelIndex", enrollment.ToList());
            }           
    
            return View(enrollment.ToList());
        }

        //
        // GET: /Order/Details/5
        public ActionResult Details(Int32 id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        //
        // GET: /Order/Create
        public ActionResult Create()
        {
            // only active program list for non-admins
            if (User.IsInRole("Admin"))
            {
                var _athletes = db.Athletes
                    .OrderBy(a => a.LastName)
                    .OrderBy(a => a.FirstName);

                ViewBag.AthleteID = new SelectList(_athletes, "AthleteID", "FullName");
            }
            else
            {
                var _athletes = db.Athletes
                    .Where(a => a.Email == User.Identity.Name);

                ViewBag.AthleteID = new SelectList(_athletes, "AthleteID", "FullName");
            }

            ViewBag.ProgramID = new SelectList(db.Programs.Where(p => p.Active == true).OrderBy(p => p.Title), "ProgramID", "Title");
            return View();
        }

        //
        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollment.Add(enrollment);
                db.SaveChanges();

                return RedirectToAction("Confirmation", enrollment);

                // old way to call receipt
                //return RedirectToAction("Receipt", enrollment);
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "FullName", enrollment.AthleteID);
            ViewBag.ProgramID = new SelectList(db.Programs.Where(p => p.Active == true).OrderBy(p => p.Title), "ProgramID", "Title", enrollment.ProgramID);
            return View(enrollment);
        }

        //
        // GET: /Order/Edit/5
        public ActionResult Edit(Int32 id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            // only active program list for non-admins
            if (User.IsInRole("Admin"))
            {
                var _athletes = db.Athletes
                    .OrderBy(a => a.LastName)
                    .OrderBy(a => a.FirstName);

                ViewBag.AthleteID = new SelectList(_athletes, "AthleteID", "FullName", enrollment.AthleteID);
            }
            else
            {
                var _athletes = db.Athletes
                    .Where(a => a.Email == User.Identity.Name);

                ViewBag.AthleteID = new SelectList(_athletes, "AthleteID", "FullName", enrollment.AthleteID);
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "FullName", enrollment.AthleteID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Title", enrollment.ProgramID);
            return View(enrollment);
        }

        //
        // POST: /Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // only active program list for non-admins
            if (User.IsInRole("Admin"))
            {
                var _athletes = db.Athletes
                    .OrderBy(a => a.LastName)
                    .OrderBy(a => a.FirstName);

                ViewBag.AthleteID = new SelectList(_athletes, "AthleteID", "FullName", enrollment.AthleteID);
            }
            else
            {
                var _athletes = db.Athletes
                    .Where(a => a.Email == User.Identity.Name);

                ViewBag.AthleteID = new SelectList(_athletes, "AthleteID", "FullName", enrollment.AthleteID);
            }

            ViewBag.AthleteID = new SelectList(db.Athletes, "AthleteID", "FullName", enrollment.AthleteID);
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Title", enrollment.ProgramID);
            return View(enrollment);
        }

        //
        // GET: /Order/Delete/5
        public ActionResult Delete(Int32 id)
        {
            Enrollment order = db.Enrollment.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        //
        // POST: /Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            Enrollment order = db.Enrollment.Find(id);
            db.Enrollment.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // custom
        // Displayed after submitting Enrollemnt for to let user know successful tran
        public ActionResult Confirmation(Enrollment _enrollment)
        {
            // Verify saved record
            Enrollment enrollment = db.Enrollment.Find(_enrollment.EnrollmentID);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            return View("Confirmation", enrollment);
        }


        // custom
        // first pass controller, find by ID, 
        // then pass on Enrollment object to next controller
        public ActionResult IdReceipt(Int32 id)
        {
            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            return RedirectToAction("Receipt", enrollment);
        }


        // custom
        public ActionResult Receipt(Enrollment enrollment)
        {
            var _enrollment = db.Enrollment
                                .Include(e => e.Athlete)
                                .Include(e => e.Program)
                                .Where(e => e.AthleteID == enrollment.AthleteID)
                                .Where(e => e.ProgramID == enrollment.ProgramID)
                                .Where(e => e.EnrollmentID == enrollment.EnrollmentID)
                                .Single();

            var _receiptData = db.ReceiptDatas.First();

            var _receipt = new Receipt()
            {
                ReceiptData = _receiptData,
                Enrollment = _enrollment,
                ImageUrl = FillImageUrl("receipt.png")
            };

            return PartialView(_receipt);
        }

        public ActionResult EmailReceipt(Int32 id)
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("EmailError");
            }

            Enrollment enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }

            var _enrollment = db.Enrollment
                                .Include(e => e.Athlete)
                                .Include(e => e.Program)
                                .Where(e => e.AthleteID == enrollment.AthleteID)
                                .Where(e => e.ProgramID == enrollment.ProgramID)
                                .Where(e => e.EnrollmentID == enrollment.EnrollmentID)
                                .Single();

            var _receiptData = db.ReceiptDatas.First();

            StringBuilder sb = new StringBuilder();
            sb.Append("</body>");
            sb.Append("<h3>");
            sb.Append("    <p width='80%' align='center' cellpadding='100' cellspacing='100'>");
            sb.AppendFormat("        {0}", _receiptData.HeaderInfo.ToString());
            sb.Append("    </p>");
            sb.Append("</h3>");
            sb.Append("<br />");

            sb.Append("<table width='80%' cellpadding='3' cellspacing='3' align='center'>");
            sb.Append("    <tr border='1' bgcolor='#777777' color='#ffffff'>");
            sb.Append("        <td width='40%' align='center'></td>");
            sb.Append("        <td width='60%' align='center'></td>");
            sb.Append("    </tr>");
            sb.Append("");
            sb.Append("    <tr border='1' bgcolor='#DDEEEE'>");
            sb.Append("        <td>");
            sb.Append("            Enrollment Date");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", _enrollment.EnrollmentDate.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#EEFFFF'>");
            sb.Append("        <td>");
            sb.Append("            Enrollers Name");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", _enrollment.EnrollersName.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#DDEEEE'>");
            sb.Append("        <td>");
            sb.Append("            Athletes Name");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", _enrollment.Athlete.FullName.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#EEFFFF'>");
            sb.Append("        <td>");
            sb.Append("            Program");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", _enrollment.Program.Title.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#DDEEEE'>");
            sb.Append("        <td>");
            sb.Append("            Price");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", _enrollment.Price.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#EEFFFF'>");
            sb.Append("        <td>");
            sb.Append("            Description");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", _enrollment.Program.Description.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#DDEEEE'>");
            sb.Append("        <td>");
            sb.Append("            Read and Agree with terms and conditions of SSRA Code of Conduct Form");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", (_enrollment.SSRACodeofConductForm) ? "Yes" : "No");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#EEFFFF'>");
            sb.Append("        <td>");
            sb.Append("            Read and Agree with terms and conditions of Parent Code of Conduct - SSRA Form");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", (_enrollment.ParentCodeofConductSSRAForm) ? "Yes" : "No");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#DDEEEE'>");
            sb.Append("        <td>");
            sb.Append("            Read and Agree with terms and conditions of Volunteer Form");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", (_enrollment.VolunteerForm) ? "Yes" : "No");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#EEFFFF'>");
            sb.Append("        <td>");
            sb.Append("            Read and Agree with terms and conditions of Assumption and Acceptance of rs Form");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", (_enrollment.AssumptionAndAcceptanceOfRsForm) ? "Yes" : "No");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#EEFFFF'>");
            sb.Append("        <td>");
            sb.Append("            Read and Agree with terms and conditions of Medical Release Form");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", (_enrollment.MedicalReleaseForm) ? "Yes" : "No");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#DDEEEE'>");
            sb.Append("        <td>");
            sb.Append("            Read and Agree with terms and conditions of Concussion Information Form");
            sb.Append("        </td>");
            sb.Append("        <td>");
            sb.AppendFormat("            {0}", (_enrollment.ConcussionInfoForm) ? "Yes" : "No");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr border='1' bgcolor='#777777' color='#ffffff'>");
            sb.Append("        <td></td>");
            sb.Append("        <td></td>");
            sb.Append("    </tr>");
            sb.Append("</table>");
            sb.Append("<br />");

            sb.Append("<table width='80%' cellpadding='3' cellspacing='3' align='center'>");
            sb.Append("    <tr>");
            sb.Append("        <td>");
            sb.AppendFormat("    {0}", _receiptData.Footer1Info.ToString());
            sb.Append("            <br />");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr>");
            sb.Append("        <td>");
            sb.AppendFormat("    {0}", _receiptData.Footer2Info.ToString());
            sb.Append("            <br />");
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("    <tr>");
            sb.Append("        <td>");
            sb.AppendFormat("    {0}", _receiptData.Footer3Info.ToString());
            sb.Append("        </td>");
            sb.Append("    </tr>");
            sb.Append("</table>");

            sb.Append("<br />");
            sb.Append("        <hr />");
            sb.Append("        <footer>");
            sb.Append("            <p>&copy; 2013 - SSRA</p>");
            sb.Append("        </footer>");
            sb.Append("    </div>");
            sb.Append("</body>");

            MailMessage mm = new MailMessage()
            {
                Subject = "SSRA Receipt - " + _receiptData.HeaderInfo.ToString(),
                IsBodyHtml = true,
                Body = sb.ToString()
            };
            // Add CC Recipients
            mm.To.Add(User.Identity.Name);
            if (!String.IsNullOrEmpty(User.Identity.Name))
                mm.CC.Add(_receiptData.EmailRecipient1.ToString());
            if (!String.IsNullOrEmpty(User.Identity.Name))
                mm.CC.Add(_receiptData.EmailRecipient2.ToString());

            _smtpClient.Send(mm);

            return RedirectToAction("IdReceipt", new { id });
        }

        public ActionResult EmailForms()
        {
            if (String.IsNullOrEmpty(User.Identity.Name))
            {
                return RedirectToAction("EmailError");
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("</body>");
            sb.Append("    <div>");
            sb.Append("    <br />");
            sb.Append("    <br />");
            sb.Append("    <br />");
            sb.Append("    <br />");
            sb.Append("    <h2>");
            sb.Append("    <p align='center' cellpadding='100' cellspacing='100'>");
            sb.Append("        SSRA Forms are attached.");
            sb.Append("    </p>");
            sb.Append("    </h2>");
            sb.Append("    <h3>");
            sb.Append("    <p align='left' cellpadding='100' cellspacing='100'>");
            sb.Append("            To complete registration: read, print, sign and date each attachement.");
            sb.Append("            Email forms to SSRA Register.");
            sb.Append("            Please check website for most recent mailing address and contact information - http://www.gossra-registration.org/Home/Contact");
            sb.Append("    </p>");
            sb.Append("    </h3>");
            sb.Append("    <br />");
            sb.Append("    <br />");
            sb.Append("    <br />");
            sb.Append("    <br />");
            sb.Append("    <div>");
            sb.Append("        <footer>");
            sb.Append("            <p>&copy; 2013 - SSRA</p>");
            sb.Append("        </footer>");
            sb.Append("    </div>");
            sb.Append("</body>");

            MailMessage mm = new MailMessage()
            {
                Subject = "SSRA forms to complete registration.",
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                Body = sb.ToString()
            };
            mm.To.Add(User.Identity.Name);
            // Add necassary attachments
            mm.Attachments.Add(new Attachment(Server.MapPath("ssra_team_code_of_conduct_athlete.pdf")));
            mm.Attachments.Add(new Attachment(Server.MapPath("volunteer_form.pdf")));
            mm.Attachments.Add(new Attachment(Server.MapPath("assumption_and_acceptance_of_risk_release_ and_indemnity.pdf")));
            mm.Attachments.Add(new Attachment(Server.MapPath("medical_release.pdf")));
            mm.Attachments.Add(new Attachment(Server.MapPath("concussion_form.pdf")));

            _smtpClient.Send(mm);

            return RedirectToAction("EmailConfirmed");
        }

        public ActionResult EmailConfirmed()
        {
            return View();
        }

        public ActionResult EmailError()
        {
            return View();
        }

        private string FillImageUrl(string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            return url + "img/" + imageName;
        }

        // custom
        [HttpPost]
        public ActionResult GetUserID()
        {
            return Json(User.Identity.Name);

        }

        // custom
        [HttpPost]
        public ActionResult GetPrice(string programId)
        {
            int programIdInt = 0;
            if (int.TryParse(programId, out programIdInt))
            {
                var _program = db.Programs
                        .Where(p => p.Active == true)
                        .Where(p => p.ProgramID == programIdInt)
                    .Select(p => new
                    {
                        Price = p.Price
                    }
                    )
                    .Single();

                return Json(_program);
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
