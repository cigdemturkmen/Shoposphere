using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal
        {
            get
            {
                return this.Product.UnitPrice * this.Quantity;
            }
        }

    }
}
