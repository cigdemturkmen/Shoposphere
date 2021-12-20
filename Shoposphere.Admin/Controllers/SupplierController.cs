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
    public class SupplierController : BaseController
    {
        private readonly IRepository<Supplier> _supplierRepository;
        public SupplierController(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IActionResult List()
        {
            var suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x =>
            new SupplierViewModel()
            {
                Id = x.Id,
                SupplierName = x.SupplierName,
                Address = x.Address,
                Phone = x.Phone,
                //Products = x.Products,
            }).ToList();

            return View(suppliers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUserId = GetCurrentUserId();

            var entity = new Supplier()
            {
                SupplierName = model.SupplierName,
                Phone = model.Phone,
                Address = model.Address,
                CreatedDate = DateTime.Now,
                CreatedById = currentUserId,

            };
            bool result;
            result = _supplierRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View(model);
        }

        public IActionResult Edit(int id)
        {

           var supplier = _supplierRepository.Get(x => x.Id == id && x.IsActive);

            if (supplier != null)
            {
                var vm = new SupplierViewModel()
                {
                    Id = supplier.Id,
                    SupplierName = supplier.SupplierName,
                    Address = supplier.Address,
                    Phone = supplier.Phone,
                    IsActive = supplier.IsActive,
                };

                return View(vm);
            }

            TempData["Message"] = "Supplier cannot be found.";
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Edit(SupplierViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var currentUserId = GetCurrentUserId();
            var entity = new Supplier()
            {
                Id = model.Id,
                SupplierName = model.SupplierName,
                Address = model.Address,
                Phone = model.Phone,
                IsActive = model.IsActive,
                UpdatedById = currentUserId,
                UpdatedDate = DateTime.Now,
            };

           

            var result = _supplierRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _supplierRepository.Delete(id);

            TempData["Message"] = result ? "" : "Silme yapılamadı";

            return RedirectToAction("List");
           
        }
    }
}
