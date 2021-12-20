using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shoposphere.Admin.Models;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Controllers
{
    public class CommentController : Controller
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;
        public CommentController(IRepository<Comment> commentRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository, IRepository<User> userRepository)
        {
            _commentRepository = commentRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public IActionResult List(int id)
        {
            var vm = _commentRepository.GetAll(include: x => x.Include(y => y.Product).Include(z=>z.User)).Where(x=> x.Product.Id == id).Select(x => new CommentViewModel()
            {
                Content=x.Content,
                IsPublished=x.IsPublished,
                User=x.User,
                UserId=x.UserId,
                FirstName=x.User.FirstName,
                LastName=x.User.LastName,
                ProductId=x.ProductId,
                Product=x.Product,
                ProductName=x.Product.ProductName,
                Order=x.Order,
                OrderId=x.OrderId
                
               
            }).ToList();

            return View(vm);

        }

        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        //public IActionResult Edit()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Edit()
        //{
        //    return View();
        //}

        //public IActionResult Delete()
        //{
        //    return View();
        //}
    }
}
