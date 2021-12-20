using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        #region Relations

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List <ProductSupplier> ProductSuppliers { get; set; }

        public List<Comment> Comments { get; set; } 
        #endregion

    }
}
