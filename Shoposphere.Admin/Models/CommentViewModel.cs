using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class CommentViewModel
    {
        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public bool IsPublished { get; set; }

        #region Relations

        public int UserId { get; set; }
        public User User { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        #endregion
    }
}
