using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.UI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Surname")]
        public string LastName { get; set; }

        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string Password { get; set; }

        #region Relations
        public List<Comment> Comments { get; set; }

        public List<Order> Orders { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        [DisplayName("User Role")]
        public UserRole UserRole { get; set; }

        #endregion


    }
}
