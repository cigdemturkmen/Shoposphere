using Microsoft.AspNetCore.Mvc;
using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.UI.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }

        
        [DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 3)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        
    }
}
