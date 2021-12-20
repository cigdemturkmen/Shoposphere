using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class SupplierViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string SupplierName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public bool IsActive { get; set; }
        public List<Product> Products { get; set; } // --
         
    }
}
