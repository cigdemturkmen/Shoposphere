using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.UI.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        [DisplayName("Published")]
        public bool IsPublished { get; set; }

        #region Relations

        public int UserId { get; set; }
        public User User { get; set; }

        [DisplayName("User Name")]
        public string FirstName { get; set; }
        [DisplayName("User Surnaame")]
        public string LastName { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        #endregion
    }
}
