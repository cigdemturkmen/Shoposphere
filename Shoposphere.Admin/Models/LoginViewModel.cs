using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(12, MinimumLength = 4)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
