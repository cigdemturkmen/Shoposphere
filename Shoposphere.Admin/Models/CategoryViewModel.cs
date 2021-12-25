using Microsoft.AspNetCore.Http;
using Shoposphere.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Description")]
        public string CategoryDescription { get; set; }

        public bool IsActive { get; set; }

        public List<Product> Products { get; set; }

        [DisplayName("Category Picture")]
        public string PictureStr { get; set; }
        public IFormFile Picture { get; set; }
    }
}
