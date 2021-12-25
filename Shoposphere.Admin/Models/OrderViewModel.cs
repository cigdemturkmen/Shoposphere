using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        [DisplayName("Expected Shipment")]
        public DateTime? RequiredDate { get; set; }
        [DisplayName("Shipped On")]
        public DateTime ShippedDate { get; set; }

        public decimal Freight { get; set; }

        [Required]
        [StringLength(60)]
        [DisplayName("Address")]
        public string ShipAddress { get; set; }

        public int CustomerId { get; set; }
        public User Customer { get; set; }
        [DisplayName("Name")]
        public string CustomerName { get; set; }
        [DisplayName("Surname")]
        public string CustomerSurname { get; set; }

        public int ShipperId { get; set; }
        public Shipper Shipper { get; set; }
        [DisplayName("Shipper")]
        public string ShipperName { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
