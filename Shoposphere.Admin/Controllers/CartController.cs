using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shoposphere.Admin.Helpers;
using Shoposphere.Admin.Models;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.Admin.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public CartController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        const string SessionShopCart = "_sessionShopCart";
        List<CartItem> cartItemList = new List<CartItem>();

        #region Resources (Sessions in net core)
        // c-sharpcorner.com/article/how-to-use-session-in-asp-net-core/

        // medium.com/@fadilzeybek/net-core-session-kullan%C4%B1m%C4%B1-b6f187c34eb0
        #endregion
        public IActionResult Add(int id)
        {
            
            HttpContext.Session.SetString("SessionShopCart", JsonConvert.SerializeObject(cartItemList)); // Session["ShopCart] = cartItemList
            var sessionStr = HttpContext.Session.GetString("SessionShopCart"); // converts session into string


            var product = _productRepository.Get(x => x.IsActive && x.Id == id);

            if (product != null)
            {
                if (sessionStr != null) // if session is not null
                {
                    cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(sessionStr);
                }

                if (cartItemList.Any(x => x.Product.Id == id))
                {
                    var currentProduct = cartItemList.FirstOrDefault(x => x.Product.Id == id);
                    currentProduct.Quantity += 1;
                }
                else
                {
                    var cartItem = new CartItem()
                    {
                        Product = product,
                        Quantity = 1
                    };

                    cartItemList.Add(cartItem); // adds new CartItem to the list
                }

                
                sessionStr = cartItemList.ToString(); // Session["ShopCard"] = cardItemList;
                TempData["Session"] = sessionStr;
            }
            
            return RedirectToAction("List", TempData["Session"]);
        }

        public IActionResult List()
        {
            // HttpContext.Session.SetString("SessionShopCart", JsonConvert.SerializeObject(cartItemList));// Session["ShopCart] = cartItemList

            //var sessionStr = TempData["Session"].ToString(); 
            var sessionStr = HttpContext.Session.GetString("SessionShopCart");
            //var sessionStr = cartItemList.ToString();
           

            if (sessionStr != null)
            {
                cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(sessionStr);
            }

            return View(cartItemList);
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
