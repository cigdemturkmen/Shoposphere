using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoposphere.UI.Models;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.UI.Controllers
{
    // [Authorize]
    public class HomeController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;
        public HomeController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll(x => x.IsActive, include: x => x.Include(y => y.Products)).Select(x =>
            new CategoryViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                PictureStr = Convert.ToBase64String(x.Picture),
                Products = x.Products,
            }).ToList();

            return View(categories);
        }
    }
}
