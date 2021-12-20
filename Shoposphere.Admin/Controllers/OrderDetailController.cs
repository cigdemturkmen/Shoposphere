using Microsoft.AspNetCore.Mvc;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        public OrderDetailController(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        //public IActionResult List()
        //{
        //    return View();
        //}

    }
}
