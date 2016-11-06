using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeCollectionMVC.Models
{
    public class ShoeType
    {
        public int ID { get; set; }

        [Display(Name = "Type Name")]
        [Required]
        public string TypeName { get; set; }
        [Display(Name = "Last Modified")]
        public DateTime? LastModified { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
    }
}