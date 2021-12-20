using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        

        #region Relations
        public List<Comment> Comments { get; set; }

        public List<Order> Orders { get; set; }

        public UserRole UserRole { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        #endregion

    }
}
