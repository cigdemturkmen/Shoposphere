using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class ShipperViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ShipperName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public List<Order> Orders { get; set; }

        public bool IsAvtice { get; set; }

    }
}
