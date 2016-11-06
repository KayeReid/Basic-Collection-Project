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
    public class ShoeItemsController : Controller
    {
        private ShoeDb db = new ShoeDb();

        // GET: ShoeItems
        public ActionResult Index()
        {
            var shoeItems = db.ShoeItems.Include(s => s.ShoeTypes);
            return View(shoeItems.ToList());
        }

        // GET: ShoeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeItem shoeItem = db.ShoeItems.Find(id);
            if (shoeItem == null)
            {
                return HttpNotFound();
            }
            return View(shoeItem);
        }

        // GET: ShoeItems/Create
        public ActionResult Create()
        {
            var model = new ProductViewModel();
            ViewBag.ShoeTypeId = new SelectList(db.ShoeTypes, "ID", "TypeName");
            model.AllCategories = db.ShoeCategories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.ID.ToString() }).ToList();
            return View();
        }

        // POST: ShoeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ShoeName,Description,Price,Enabled,StockCount,LastOrderDate,LastModified,LastModifiedBy,Comments,ShoeTypeId")] ProductViewModel shoeProdVM)
        {
             if (ModelState.IsValid)
            {
                ShoeItem p = new ShoeItem()
                {
                    ID = shoeProdVM.ID,
                    ShoeName = shoeProdVM.ShoeName,
                    Description = shoeProdVM.Description,
                    Price = shoeProdVM.Price,
                    Enabled = shoeProdVM.Enabled,
                    StockCount = shoeProdVM.StockCount,
                    LastOrderDate = shoeProdVM.LastOrderDate,
                    LastModified = shoeProdVM.LastModified,
                    LastModifiedBy = shoeProdVM.LastModifiedBy,
                    Comments = shoeProdVM.Comments,
                    ShoeTypeId = shoeProdVM.ShoeTypeId,
                    ShoeTypes = shoeProdVM.ShoeTypes,
                    Categories = db.ShoeCategories.Where(c => shoeProdVM.SelectedCategoryIds.Contains(c.ID)).ToList()
                };

                db.ShoeItems.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ShoeTypeId = new SelectList(db.ShoeTypes, "ID", "TypeName", shoeProdVM.ShoeTypeId);
            return View(shoeProdVM);
        }

        // GET: ShoeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeItem shoeItem = db.ShoeItems.Find(id);
            if (shoeItem == null)
            {
                return HttpNotFound();
            }
            var model = new ProductViewModel()
            {
                ID = shoeItem.ID,
                ShoeName = shoeItem.ShoeName,
                Description = shoeItem.Description,
                Price = shoeItem.Price,
                Enabled = shoeItem.Enabled,
                StockCount = shoeItem.StockCount,
                LastOrderDate = shoeItem.LastOrderDate,
                LastModified = shoeItem.LastModified,
                LastModifiedBy = shoeItem.LastModifiedBy,
                Comments = shoeItem.Comments,
                ShoeTypeId = shoeItem.ShoeTypeId,
                ShoeTypes = shoeItem.ShoeTypes,
                Categories = shoeItem.Categories,
                SelectedCategoryIds = shoeItem.Categories.Select(c => c.ID).ToList()
            };
            ViewBag.ShoeTypeId = new SelectList(db.ShoeTypes, "ID", "TypeName", shoeItem.ShoeTypeId);
            model.AllCategories = db.ShoeCategories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.ID.ToString() }).ToList(); 
            return View(shoeItem);
        }

        // POST: ShoeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ShoeName,Description,Price,Enabled,StockCount,LastOrderDate,LastModified,LastModifiedBy,Comments,ShoeTypeId")] ProductViewModel shoeProdVM)
        {
            if (ModelState.IsValid)
            {
                ShoeItem existingProduct = db.ShoeItems.Find(shoeProdVM.ID);
                //edit the product
                existingProduct.ShoeName = shoeProdVM.ShoeName;
                existingProduct.Description = shoeProdVM.Description;
                existingProduct.Price = shoeProdVM.Price;
                existingProduct.Enabled = shoeProdVM.Enabled;
                existingProduct.StockCount = shoeProdVM.StockCount;
                existingProduct.LastOrderDate = shoeProdVM.LastOrderDate;
                existingProduct.LastModified = shoeProdVM.LastModified;
                existingProduct.LastModifiedBy = shoeProdVM.LastModifiedBy;
                existingProduct.Comments= shoeProdVM.Comments;
                existingProduct.ShoeTypeId = shoeProdVM.ShoeTypeId;
                existingProduct.ShoeTypes = shoeProdVM.ShoeTypes;
                existingProduct.Categories = shoeProdVM.Categories;

                //get a list of existing category ids from database
                var existingProductCategoryIds = existingProduct.Categories.Select(c => c.ID).ToList();

                //Find ids of the deleted categories from the product's Categories collection by the product's existing categoriy ids minus current category ids from the view
                var deletedCategories = existingProductCategoryIds.Except(shoeProdVM.SelectedCategoryIds).ToList();
                //Find ids of the added categories to the current product's Category id list from the view minus the product's existing category ids (from the db)
                var addedCategories = shoeProdVM.SelectedCategoryIds.Except(existingProductCategoryIds).ToList();
                
                //remove deleted categories from the products' categories collection
                foreach(int id in deletedCategories)
                {
                    var category = db.ShoeCategories.Where(c => c.ID == id).FirstOrDefault();
                    existingProduct.Categories.Remove(category);
                }

                //add the new categories to the products' categories collection
                foreach(int id in addedCategories)
                {
                    var category = db.ShoeCategories.Where(c => c.ID == id).FirstOrDefault();
                  //  var category = db.Categories.FirstOrDefault(c => c.ID == id);   this is another way to write the line above
                    existingProduct.Categories.Add(category);
                }
                                      
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShoeTypeId = new SelectList(db.ShoeTypes, "ID", "TypeName", shoeProdVM.ShoeTypeId);
            return View(shoeProdVM);
        }

        // GET: ShoeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoeItem shoeItem = db.ShoeItems.Find(id);
            if (shoeItem == null)
            {
                return HttpNotFound();
            }
            return View(shoeItem);
        }

        // POST: ShoeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoeItem shoeItem = db.ShoeItems.Find(id);
            db.ShoeItems.Remove(shoeItem);
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
