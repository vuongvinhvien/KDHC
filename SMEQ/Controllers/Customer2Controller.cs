using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Store.Data.DataDbContext;

namespace SMEQ.Controllers
{
    public class Customer2Controller : Controller
    {
        private DataChatBox db = new DataChatBox();

        // GET: Customer2
        public ActionResult Index()
        {
            var customer2 = db.Customer2.Include(c => c.District);
            return View(customer2.ToList());
        }

        // GET: Customer2/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer2 customer2 = db.Customer2.Find(id);
            if (customer2 == null)
            {
                return HttpNotFound();
            }
            return View(customer2);
        }

        // GET: Customer2/Create
        public ActionResult Create()
        {
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name");
            return View();
        }

        // POST: Customer2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationName,TaxCode,Name,Position,PhoneNumber,Address,DistrictId,Fax,CreatedDate")] Customer2 customer2)
        {
            if (ModelState.IsValid)
            {
                db.Customer2.Add(customer2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", customer2.DistrictId);
            return View(customer2);
        }

        // GET: Customer2/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer2 customer2 = db.Customer2.Find(id);
            if (customer2 == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", customer2.DistrictId);
            return View(customer2);
        }

        // POST: Customer2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationName,TaxCode,Name,Position,PhoneNumber,Address,DistrictId,Fax,CreatedDate")] Customer2 customer2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictId = new SelectList(db.Districts, "Id", "Name", customer2.DistrictId);
            return View(customer2);
        }

        // GET: Customer2/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer2 customer2 = db.Customer2.Find(id);
            if (customer2 == null)
            {
                return HttpNotFound();
            }
            return View(customer2);
        }

        // POST: Customer2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Customer2 customer2 = db.Customer2.Find(id);
            db.Customer2.Remove(customer2);
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
