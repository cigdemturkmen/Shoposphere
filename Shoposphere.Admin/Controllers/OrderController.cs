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
    public class OrderController : BaseController
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult List()
        {
            var orders = _orderRepository.GetAll(x => x.IsActive).Select(x =>
           new OrderViewModel()
           {
              Id = x.Id,
              CustomerName = x.User.FirstName,
              CustomerSurname = x.User.LastName,
              CustomerId = x.UserId,
              ShipAddress = x.ShipAddress,
              ShipperId = x.ShipperId,
              ShipperName = x.Shipper.ShipperName,
              RequiredDate = x.RequiredDate,
              ShippedDate = x.ShippedDate,
              Freight = x.Freight, 
              
            }).ToList();

            return View(orders);
        }

        //public ActionResult Add()
        //{
        //    // sepetteki ürünleri order detail olarak düzenleyerek order nesnesine ekleyerek dbye kayıt edeceğiz.

        //    var cardItemList = new List<CardItem>();

        //    if (Session["ShopCard"] != null)
        //    {
        //        cardItemList = (List<CardItem>)Session["ShopCard"];
        //    }

        //    var orderDetails = new List<OrderDetail>();

        //    orderDetails = cardItemList.Select(x => new OrderDetail()
        //    {
        //        ProductId = x.Product.Id,
        //        Quantity = x.Quantity,
        //        UnitPrice = x.Product.UnitPrice,
        //        Discount = 0
        //    }).ToList();

        //    var order = new Order()
        //    {
        //        CreatedById = 1,
        //        CreatedDate = DateTime.Now,
        //        IsActive = true,
        //        OrderDate = DateTime.Now,
        //        UserId = 1,
        //        OrderDetails = orderDetails
        //    };

        //    _db.Orders.Add(order);

        //    var sonuc = _db.SaveChanges();

        //    return View();
        //}

    }
}
