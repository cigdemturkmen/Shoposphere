using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Shoposphere.UI.Models;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Shoposphere.UI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Shipper> _shipperRepository;
        public OrderController(IRepository<Order> orderRepository, IRepository<Shipper> shipperRepositor)
        {
            _orderRepository = orderRepository;
            _shipperRepository = shipperRepositor;
        }

        [Authorize(Roles = "1")]
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

        //    var cartItemList = new List<CartItem>();

        //    var sessionCart = HttpContext.Session.GetString("SessionShopCart");

        //    if (sessionCart!= null)
        //    {
        //        cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("SessionShopCart"));
        //    }

        //    var orderDetails = new List<OrderDetail>();

        //    orderDetails = cartItemList.Select(x => new OrderDetail()
        //    {
        //        ProductID = x.Product.Id,
        //        Quantity = x.Quantity,
        //        UnitPrice = x.Product.UnitPrice,
        //         Discount = 0,
        //         Product = x.Product,
        //    }).ToList();

        //    var currentUserId = GetCurrentUserId();
        //    var order = new Order()
        //    {
        //        CreatedById = currentUserId,
        //        CreatedDate = DateTime.Now, // (OrderDate)
        //        IsActive = true, 
        //        UserId = currentUserId,
        //        OrderDetails = orderDetails,
        //    };

        //    var result = _orderRepository.Add(order);

        //    if (result)
        //    {
        //        HttpContext.Session.SetString("SessionShopCart", "");
        //        TempData["Message"] = "Payment successful";
        //        return RedirectToAction("Index", "Home");
        //    }

        //    return RedirectToAction("List", "Cart");
        //}

        [Authorize(Roles = "2")]
        public ActionResult Checkout()
        {
            ViewBag.Shippers = _shipperRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.ShipperName,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.Cart = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("SessionShopCart"));

            if (ViewBag.Cart == null)
            {
                TempData["Message"] = "Your cart is empty";
                return RedirectToAction("List", "Cart");
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(OrderViewModel model)
        {
            var cartItemList = new List<CartItem>();

            var sessionCart = HttpContext.Session.GetString("SessionShopCart");

            if (sessionCart != null)
            {
                cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("SessionShopCart"));
            }

            var orderDetails = new List<OrderDetail>();

            orderDetails = cartItemList.Select(x => new OrderDetail()
            {
                ProductID = x.Product.Id,
                Quantity = x.Quantity,
                UnitPrice = x.Product.UnitPrice,
                Discount = 0,
                Product = x.Product,
            }).ToList();

            var currentUserId = GetCurrentUserId();
            var order = new Order()
            {
                CreatedById = currentUserId,
                CreatedDate = DateTime.Now, // (OrderDate)
                IsActive = true,
                UserId = currentUserId,
                OrderDetails = orderDetails,
                ShipAddress = model.ShipAddress,  
                ShipperId = 3,
                
            };

            var result = _orderRepository.Add(order);

            if (result)
            {
                HttpContext.Session.Clear(); // DONT - HttpContext.Session.SetString("SessionShopCart", "");
                TempData["Message"] = "Payment successful";
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("List", "Cart");
        }
    }
}
