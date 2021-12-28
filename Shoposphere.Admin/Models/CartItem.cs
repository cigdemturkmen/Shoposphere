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
        public short Quantity { get; set; }
        public decimal Subtotal
        {
            get
            {
                return this.Product.UnitPrice * this.Quantity;
            }
        }
        public string PictureStr { get; set; }

    }
}
