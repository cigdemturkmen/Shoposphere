using System;
using System.Collections.Generic;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class Role : BaseEntity
    {
        public UserRole UserRole { get; set; }
        

        #region Relations

        public List<User> Users { get; set; } 

        #endregion
    }
}
