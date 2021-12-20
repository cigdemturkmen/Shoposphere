using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Supplier : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string SupplierName { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        #region Relations
        public List<Product> Products { get; set; }
        public List<ProductSupplier> ProductSuppliers { get; set; }
        #endregion
    }
}
