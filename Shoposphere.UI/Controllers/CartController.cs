using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shoposphere.UI.Helpers;
using Shoposphere.UI.Models;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoposphere.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public CartController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        const string SessionShopCart = "";

        public IActionResult List()
        {
            var cartItemList = new List<CartItem>();
            var sessionCart = HttpContext.Session.GetString("SessionShopCart");

            if (sessionCart != null)
            {
                cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
            }

            return View(cartItemList);
        }

        #region Resources (Sessions in net core)
        // docs.microsoft.com/tr-tr/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0

        // learningprogramming.net/net/asp-net-core-mvc-5/build-shopping-cart-with-session-in-asp-net-core-mvc-5/

        // c-sharpcorner.com/article/how-to-use-session-in-asp-net-core/

        // medium.com/@fadilzeybek/net-core-session-kullan%C4%B1m%C4%B1-b6f187c34eb0

        #endregion
        public IActionResult Add(int id)
        {
            var product = _productRepository.Get(x => x.IsActive && x.Id == id);
            var sessionCart = HttpContext.Session.GetString("SessionShopCart");

            if (product != null)
            {
                if (sessionCart == null) // if session is null, add 1 product
                {
                    var cartItemList = new List<CartItem>();
                    var cartItem = new CartItem()
                    {
                        Product = product,
                        Quantity = 1,
                        PictureStr = Convert.ToBase64String(product.Picture),
                    };

                    cartItemList.Add(cartItem); // adds new CartItem to the cartItemList

                    HttpContext.Session.SetString("SessionShopCart", JsonConvert.SerializeObject(cartItemList)); // set session

                    // cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(sessionStr);
                }
                else
                {
                    var cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("SessionShopCart"));


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

                        cartItemList.Add(cartItem);
                    }
                    HttpContext.Session.SetString("SessionShopCart", JsonConvert.SerializeObject(cartItemList));
                }
            }
            
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var cartItemList = new List<CartItem>();

            var sessionCart = HttpContext.Session.GetString("SessionShopCart");

            if (sessionCart != null)
            {
                cartItemList = JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
            }

            var currentProduct = cartItemList.FirstOrDefault(x => x.Product.Id == id);
            if (currentProduct != null)
            {
                currentProduct.Quantity -= 1;
                if (currentProduct.Quantity == 0)
                {
                    cartItemList.Remove(currentProduct);
                }
                HttpContext.Session.SetString("SessionShopCart", JsonConvert.SerializeObject(cartItemList));
            }
            return RedirectToAction("List");
        }

        //public PartialViewResult _CartCountPartialView()
        //{
        //    TempData["Cart"] = JsonConvert.DeserializeObject<List<CartItem>>(HttpContext.Session.GetString("SessionShopCart"));
        //    return PartialView();
        //}

    }
}
