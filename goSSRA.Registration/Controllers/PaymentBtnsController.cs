using System;
using System.Collections.Generic;
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
    public class PaymentBtnsController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /ReceiptData/
        public ActionResult Index()
        {
            return View(db.PaymentBtns.ToList());
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
        public ActionResult Create(PaymentBtns paymentBtns)
        {
            if (ModelState.IsValid)
            {
                db.PaymentBtns.Add(paymentBtns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentBtns);
        }

        //
        // GET: /ReceiptData/Edit/5
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Edit(Int32 id)
        {
            PaymentBtns paymentBtns = db.PaymentBtns.Find(id);
            if (paymentBtns == null)
            {
                return HttpNotFound();
            }
            return View(paymentBtns);
        }

        //
        // POST: /ReceiptData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Edit(PaymentBtns paymentBtns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentBtns).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentBtns);
        }

        //
        // GET: /Program/Delete/5
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult Delete(Int32 id)
        {
            PaymentBtns paymentBtns = db.PaymentBtns.Find(id);
            if (paymentBtns == null)
            {
                return HttpNotFound();
            }
            return View(paymentBtns);
        }

        //
        // POST: /Program/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeUnlessOnlyUser(Roles = "Admin")]
        public ActionResult DeleteConfirmed(Int32 id)
        {
            PaymentBtns paymentBtns = db.PaymentBtns.Find(id);
            db.PaymentBtns.Remove(paymentBtns);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // Custom
        public ActionResult PaymentOptions()
        {
            return View(db.PaymentBtns.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
