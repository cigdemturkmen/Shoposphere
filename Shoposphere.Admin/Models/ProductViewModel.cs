using Microsoft.AspNetCore.Mvc.Rendering;
using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public bool Isactive { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string CategoryName { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public string SupplierName { get; set; }

    }
}
