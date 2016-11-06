using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeCollectionMVC.Models
{
    public class ShoeCategories
    {
        public int ID { get; set; }

        [Display(Name = "Category Name")]
        [MaxLength(64, ErrorMessage = "Maximum length of 64 characters")]
        [Required(ErrorMessage = "Please enter a type name.")]
        public string CategoryName { get; set; }
        [Display(Name = "Last Modified")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        public virtual ICollection<ShoeItem> Products
        { get; set; }
    }
}