using Shoposphere.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shoposphere.Data.Entities
{
    public class BaseEntity : IBaseEntity // TODO - make it abstract class and the properties virtual properties, LATER?
    {

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedById { get; set; }

        public bool IsActive { get; set; }
    }
}
