using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoeCollectionMVC.Models
{
    [NotMapped]
    public class ProductViewModel : ShoeItem
    {
        public IEnumerable<SelectListItem> AllCategories { get; set; }

        private IList<int> selectedCategoryIds;
        public IList<int> SelectedCategoryIds
        {
            get
            {
                if (selectedCategoryIds == null)
                {
                    selectedCategoryIds = this.Categories.Select(c => c.ID).ToList();
                }
                return selectedCategoryIds;
            }
            set { selectedCategoryIds = value; }
        }

    }
}