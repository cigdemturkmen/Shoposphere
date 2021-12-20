using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public bool IsPublished { get; set; }

        #region Relations

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        
        public int OrderId { get; set; }
        public Order Order { get; set; }
    
        #endregion

    }
}
