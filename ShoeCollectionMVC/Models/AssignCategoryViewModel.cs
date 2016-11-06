using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeCollectionMVC.Models
{
    public class AssignCategoryViewModel
    {
        public int ShoeId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public string ShoeName { get; set; }

        public IList<ShoeCategories> AssociatedCategories { get; set; }

        public SelectList Categories { get; set; }

    }
}