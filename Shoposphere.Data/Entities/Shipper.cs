using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Shipper: BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string ShipperName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        #region Relations
        public List<Order> Orders { get; set; }
        #endregion
    }
}
