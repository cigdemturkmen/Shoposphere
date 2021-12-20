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
    public class ShipperController : BaseController
    {
        private readonly IRepository<Shipper> _shipperRepository;
        public ShipperController(IRepository<Shipper> shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public IActionResult List()
        {
            var shippers = _shipperRepository.GetAll(x => x.IsActive).Select(x =>
             new ShipperViewModel()
             {
                 Id = x.Id,
                 ShipperName = x.ShipperName,
                 Phone = x.Phone,

             }).ToList();

            return View(shippers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ShipperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Shipper()
            {
               ShipperName = model.ShipperName,
               Phone = model.Phone,
               CreatedById = currentUserId,
               CreatedDate = DateTime.Now,      
            };

            bool result;

            result = _shipperRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View("Add", model);
        }

        public IActionResult Edit(int id)
        {
            var shipper = _shipperRepository.Get(x => x.Id == id && x.IsActive == true);

            if (shipper != null)
            {
                var vm = new ShipperViewModel()
                {
                    Id = shipper.Id,
                    ShipperName = shipper.ShipperName,
                    Phone = shipper.Phone,
                    IsAvtice = shipper.IsActive,
                    
                };

                return View(vm);
            }

            TempData["Message"] = "Shipper cannot be found!";
            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ShipperViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Shipper()
            {
                Id = model.Id,
                ShipperName = model.ShipperName,
                Phone = model.Phone,
               UpdatedById = currentUserId,
               UpdatedDate = DateTime.Now,
               IsActive = model.IsAvtice,
               
                
            };

            bool result;

            result = _shipperRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View("Edit", model);
        }

        public IActionResult Delete(int id)
        {
            var result = _shipperRepository.Delete(id);

            TempData["Message"] = result ? "Kayıt silindi." : "Silme yapılamadı";

            return RedirectToAction("List");
        }
    }
}
