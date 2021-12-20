using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Category: BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        public string CategoryDescription { get; set; }

        #region Relations
        public List<Product> Products { get; set; } 
        #endregion



    }
}
