using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoeCollectionMVC.Models;

namespace ShoeCollectionMVC.Controllers
{
    public class ShoeCategoriesController : Controller
    {
        private ShoeDb db = new ShoeDb();

        // GET: ShoeCategories
        public ActionResult Index()
        {
            return View(db.ShoeCategories.ToList());
        }

        // GET: ShoeCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeCategories shoeCategories = db.ShoeCategories.Find(id);
            if (shoeCategories == null)
            {
                return HttpNotFound();
            }
            return View(shoeCategories);
        }

        // GET: ShoeCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoeCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryName,LastModified,LastModifiedBy")] ShoeCategories shoeCategories)
        {
            if (ModelState.IsValid)
            {
                db.ShoeCategories.Add(shoeCategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoeCategories);
        }

        // GET: ShoeCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeCategories shoeCategories = db.ShoeCategories.Find(id);
            if (shoeCategories == null)
            {
                return HttpNotFound();
            }
            return View(shoeCategories);
        }

        // POST: ShoeCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryName,LastModified,LastModifiedBy")] ShoeCategories shoeCategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoeCategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoeCategories);
        }

        // GET: ShoeCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeCategories shoeCategories = db.ShoeCategories.Find(id);
            if (shoeCategories == null)
            {
                return HttpNotFound();
            }
            return View(shoeCategories);
        }

        // POST: ShoeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoeCategories shoeCategories = db.ShoeCategories.Find(id);
            db.ShoeCategories.Remove(shoeCategories);
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
