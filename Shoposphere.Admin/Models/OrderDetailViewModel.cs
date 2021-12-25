using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("Product Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        public short Quantity { get; set; }

        public float Discount { get; set; } 
    }
}
