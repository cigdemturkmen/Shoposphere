using Microsoft.AspNetCore.Mvc;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //public IActionResult List()
        //{
        //    return View();
        //}

    }
}
