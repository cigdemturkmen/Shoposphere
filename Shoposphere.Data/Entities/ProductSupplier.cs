using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class ProductSupplier
    {
        //[Key]
        //[Column(Order = 0)]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        //[Key]
        //[Column(Order = 1)]
        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
}
