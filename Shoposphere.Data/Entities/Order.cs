using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Order : BaseEntity
    {
        public DateTime? RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public decimal Freight { get; set; }

        [Required]
        [StringLength(60)]
        public string ShipAddress { get; set; }

        #region Relations

        public int UserId { get; set; }
        public User User { get; set; }

        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List<Comment> Comments { get; set; }

        #endregion
    }
}
