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
    public class UserController : BaseController
    {
        private readonly IRepository<Data.Entities.User> _userRepository;
        public UserController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult List()
        {
            var users = _userRepository.GetAll(x => x.IsActive).Select(x =>
             new UserViewModel()
             {
                 Id = x.Id,
                 FirstName = x.FirstName,
                 LastName = x.LastName,
                 BirthDate = x.BirthDate,
                 Email = x.Email,
                 Password = x.Password,
                 RoleId = x.RoleId, 
                 UserRole = x.UserRole,
             }).ToList();

            return View(users);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            User entity = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                RoleId = 1,
                UserRole= UserRole.Admin,
                BirthDate = model.BirthDate,
                CreatedDate = DateTime.Now,
                Email = model.Email,
                CreatedById = currentUserId,
                Password = model.Password,
            };

            bool result;


            result = _userRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View("Add", model);
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepository.Get(x => x.Id == id && x.IsActive == true);

            if (user != null)
            {
                var vm = new UserViewModel()
                {
                    Id = user.Id,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    Password = user.Password,
                    RoleId = user.RoleId,
                };

                return View(vm);
            }

            TempData["Message"] = "Category cannot be found!";
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var currentUserId = GetCurrentUserId();

            User entity = new User()
            {
                Id = model.Id,
                LastName = model.LastName,
                RoleId=model.RoleId,
                BirthDate = model.BirthDate,
                CreatedDate = DateTime.Now,
                FirstName = model.FirstName,
                Email = model.Email,
                CreatedById = currentUserId,
                IsActive = model.IsActive,
                Password=model.Password
                
                
            };

            bool result;

            result = _userRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View("Edit", model);
        }

        public IActionResult Delete(int id)
        {
            var result = _userRepository.Delete(id);

            TempData["Message"] = result ? "Kayıt silindi" : "Silme yapılamadı";

            return RedirectToAction("List");


        }
    }
}
