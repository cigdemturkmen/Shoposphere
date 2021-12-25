using Microsoft.AspNetCore.Mvc;
using Shoposphere.Admin.Models;
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

        public IActionResult Index(int id)
        {
            ////var order = _db.Orders.FirstOrDefault(x => x.OrderId == id);
            //var orderDetail = _db.OrderDetails.FirstOrDefault(x => x.OrderID == id);
            //return View(orderDetail);

            var order = _orderDetailRepository.Get(x => x.OrderID == id );

            if (order != null)
            {
                var vm = new OrderDetailViewModel()
                {
                    OrderID = order.OrderID,
                    ProductID = order.ProductID,
                    ProductName = order.Product.ProductName,
                    Quantity = order.Quantity,
                    Discount = order.Discount,
                };

                return View(vm);
            }

            return RedirectToAction("List", "Order");
            
        }

    }
}
