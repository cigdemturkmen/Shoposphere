using Microsoft.AspNetCore.Http;
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
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [DisplayName("Price")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Stock")]
        public short UnitsInStock { get; set; }

        [DisplayName("Reorder Level")]
        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public bool Isactive { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }

        [DisplayName("Product Picture")]
        public string PictureStr { get; set; }
        public IFormFile Picture { get; set; }

    }
}
