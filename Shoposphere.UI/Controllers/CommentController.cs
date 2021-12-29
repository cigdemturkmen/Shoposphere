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
    public class CommentController : BaseController
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
            var vm = _commentRepository.GetAll(include: x => x.Include(y => y.Product).Include(z => z.User)).Where(x => x.Product.Id == id).Select(x => new CommentViewModel()
            {
                Content = x.Content,
                IsPublished = x.IsPublished,
                UserId = x.UserId,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                ProductId = x.ProductId,
                OrderId = x.OrderId
            }).ToList();

            return View(vm);
        }

        [Authorize(Roles = "0")]
        public IActionResult Add() // bunu sadece customer kullanabilecek.
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Comment()
            {
                CreatedById = currentUserId,
                CreatedDate = DateTime.Now,
                Content = model.Content,
                ProductId = model.ProductId,
                OrderId = model.OrderId,
                UserId = currentUserId,
            };

            var result = _commentRepository.Add(entity);

            if (!result)
            {
                TempData["Message"] = "Uh oh! Something went wrong...";
                return View("Add", model);
            }

            return View(); // TODO - burada yorum yaptıktan sonra nereye yönlendireceğiz?
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Edit(int id)
        {
            var comment = _commentRepository.Get(x => x.Id == id && x.IsActive && x.IsPublished);

            if (comment != null)
            {
                var vm = new CommentViewModel()
                {
                    Content = comment.Content,
                    Id = comment.Id,
                    IsActive = comment.IsActive,
                    IsPublished = comment.IsPublished,
                    // buradaki Id, ispublished ve isactive edit'i yaptığımız Form'a hidden olarak eklenecek. formun yeri henüz belli değil
                };

                return View(vm);
            }

            TempData["Message"] = "Comment cannot be found!";
            return RedirectToAction("List"); // TODO - burda listeye değil de başka bir yere yönlensin. onu sonra düşün.
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Comment()
            {
                Id = model.Id,
                IsPublished = model.IsPublished,
                IsActive = model.IsActive,
                Content = model.Content,
                UpdatedById = currentUserId,
                UpdatedDate = DateTime.Now,
            };


            var result = _commentRepository.Edit(entity);

            if (!result)
            {
                TempData["Message"] = "Uh oh! Something went wrong...";
                return View("Edit", model);
            }

            return RedirectToAction("List"); // TODO - yorumu düzenledikten sonra nereye yönlensin?
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Delete(int id)
        {
            var result = _commentRepository.Delete(id);

            TempData["Message"] = result ? "Yorum silindi" : "Silme yapılamadı";

            return RedirectToAction("List"); // TODO - yorumu sildikten sonra nereye yönlensin?
        }
    }
}
