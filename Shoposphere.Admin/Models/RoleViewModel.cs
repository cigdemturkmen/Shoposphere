using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public UserRole UserRole { get; set; }
        public bool IsActive { get; set; }
        public List<User> Users { get; set; }

       
    }
}
