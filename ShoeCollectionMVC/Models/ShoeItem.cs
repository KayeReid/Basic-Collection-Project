using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeCollectionMVC.Models
{
    public class ShoeItem
    {
        public int ID { get; set; }
        [Display(Name = "Shoe Name")]
        [Required]
        public string ShoeName { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(typeof(Decimal), "1", "1001")]
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        [Display(Name = "Stock Count")]
        [Required]
        [Range(1, 1000)]
        public int StockCount { get; set; }
        [Display(Name = "Last Order Date")]
        public DateTime LastOrderDate { get; set; }
        [Display(Name = "Last Modified")]
        public DateTime LastModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        public string Comments { get; set; }
        public int? ShoeTypeId { get; set; }
        public IList<int> ShoeCategoryIds { get; set; }
        public virtual ShoeType ShoeTypes { get; set; }
        public virtual ICollection<ShoeCategories> Categories { get; set; }
    }
}