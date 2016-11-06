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
    public class AssignCategoryController : Controller
    {
        private ShoeDb db = new ShoeDb();

        // GET: AssignCategory
        public ActionResult Index(int ShoeId)
        {
            AssignCategoryViewModel model = CreateAssignCategoryViewModel(ShoeId);

            if (model.ShoeId == 0)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(int ShoeId, int CategoryId)
        {
            if (ShoeId == 0)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid && CategoryId > 0)
            {
                ShoeItem product = db.ShoeItems.SingleOrDefault(p => p.ID == ShoeId);
                if (product == null)
                {
                    return HttpNotFound();
                }
                //Save
                ShoeCategories category = db.ShoeCategories.Where(pc => pc.ID == CategoryId).SingleOrDefault();
                if (category == null)
                {
                    return HttpNotFound("Category not found");
                }
                product.Categories.Add(category);
                db.SaveChanges();
            }

            AssignCategoryViewModel model = CreateAssignCategoryViewModel(ShoeId);

            return View(model);
        }

        // GET: AssignCategory/Delete/5
        public ActionResult Delete(int CategoryId, int ShoeId)
        {
            if (CategoryId > 0)
            {
                ShoeCategories category = db.ShoeCategories.Where(pc => pc.ID == CategoryId).SingleOrDefault();
                if (category == null)
                {
                    return HttpNotFound();
                }
                if (ShoeId > 0)
                {
                    ShoeItem product = db.ShoeItems.Where(p => p.ID == ShoeId).SingleOrDefault();
                    if (product == null)
                    {
                        return HttpNotFound();
                    }
                    product.Categories.Remove(category);
                    db.SaveChanges();
                }
            }
            return View("Index", CreateAssignCategoryViewModel(ShoeId));
        }

        private AssignCategoryViewModel CreateAssignCategoryViewModel(int ShoeId)
        {
            AssignCategoryViewModel model = new AssignCategoryViewModel();

            ShoeItem product =
                db.ShoeItems
                .Where(p => p.ID == ShoeId)
                .FirstOrDefault();

            if (product == null)
            {
                model.ShoeId = 0;
            }
            else
            {
                model.ShoeId = product.ID;
                model.ShoeName = product.ShoeName;

                //Get the list of assigned category ids for this product

                IList<int> assignedCategoryIds = product.Categories.Select(c => c.ID).ToList();

                //Get a list of assigned category objects
                IList<ShoeCategories> assignedCategories = db.ShoeCategories.Where(c => assignedCategoryIds.Contains(c.ID)).ToList();

                model.AssociatedCategories = assignedCategories;

                //Get a list of available category objects that could be assigned
                IList<ShoeCategories> availableCategories = db.ShoeCategories.Where(c => !assignedCategoryIds.Contains(c.ID)).ToList();

                availableCategories.Insert(0, new ShoeCategories() { ID = 0, CategoryName = "--- Select a Category ---" });

                //Convert to SelectList object for use with (bind to) drop down list.
                model.Categories = new SelectList(availableCategories, "ID", "CategoryName", 0);
            }

            return model;
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
