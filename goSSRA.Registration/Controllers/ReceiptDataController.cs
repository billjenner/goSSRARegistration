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
    public class ReceiptDataController : Controller
    {
        private RegistrationDB db = new RegistrationDB();

        //
        // GET: /ReceiptData/
        public ActionResult Index()
        {
            return View(db.ReceiptDatas.ToList());
        }

        //
        // GET: /ReceiptData/Edit/5
        public ActionResult Edit(Int32 id)
        {
            ReceiptData receiptdata = db.ReceiptDatas.Find(id);
            if (receiptdata == null)
            {
                return HttpNotFound();
            }
            return View(receiptdata);
        }

        //
        // POST: /ReceiptData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReceiptData receiptdata)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receiptdata).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(receiptdata);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
