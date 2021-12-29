using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shoposphere.UI.Models;
using Shoposphere.Data.Entities;
using Shoposphere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoposphere.UI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Supplier> _supplierRepository;

        public ProductController(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IRepository<Supplier> supplierRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _supplierRepository = supplierRepository;

        }

        public IActionResult List(int? id)
        {
            var products = _productRepository.GetAll(x => x.IsActive, include: x => x.Include(y => y.Category).Include(y => y.Supplier));


            if (id != null)
            {
                products = _productRepository.GetAll(x => x.CategoryId == id);
            }

            var vm = products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ReorderLevel = x.ReorderLevel,
                Discontinued = x.Discontinued,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                SupplierId = x.SupplierId,
                SupplierName = x.Supplier.SupplierName, 
                PictureStr = Convert.ToBase64String(x.Picture),
            }).ToList();

            return View(vm);
        }

        public IActionResult Detail(int id)
        {
           // var product = _productRepository.Get(x => x.IsActive && x.Id == id );

            var product = _productRepository.Get(x => x.Id == id && x.IsActive, include: x => x.Include(y => y.Category).Include(y => y.Supplier));

            if (product != null)
            {
                var vm = new ProductViewModel()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    UnitPrice = product.UnitPrice,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.CategoryName,
                    PictureStr = Convert.ToBase64String(product.Picture),
                    Supplier = product.Supplier,
                };
                return View(vm);
            }

            return RedirectToAction("Shop");
        }

        public IActionResult Shop(int? id)
        {
            var products = _productRepository.GetAll(x => x.IsActive, include: x => x.Include(y => y.Category).Include(y => y.Supplier));
     

            var categories = _categoryRepository.GetAll(x => x.IsActive).Select(x =>
            new CategoryViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,    
                PictureStr = Convert.ToBase64String(x.Picture),
            }).ToList();

            if (id != null)
            {
                products = _productRepository.GetAll(x => x.IsActive && x.CategoryId == id);
            }

            var vm = products.Select(x => new ProductViewModel()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                ReorderLevel = x.ReorderLevel,
                Discontinued = x.Discontinued,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName,
                SupplierId = x.SupplierId,
                SupplierName = x.Supplier.SupplierName,
                PictureStr = Convert.ToBase64String(x.Picture),
               
            }).ToList();


            ViewBag.Categories = categories;
            return View(vm);
        }

        
        public IActionResult Add()
        {
            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();

            ViewBag.Suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList();

                ViewBag.Suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.SupplierName,
                    Value = x.Id.ToString()

                }).ToList();

                return View(model);
            }

            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();

            ViewBag.Suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.Id.ToString()

            }).ToList();

            Product entity = new Product()
            {
                ProductName = model.ProductName,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                Discontinued = model.Discontinued,
                ReorderLevel = model.ReorderLevel,
                CreatedDate = DateTime.Now,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId,
                IsActive = true,  
            };

            if (model.Picture != null && model.Picture.Length > 0 )
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.Picture.CopyTo(memoryStream);
                    var fileByteArray = memoryStream.ToArray();

                    entity.Picture = fileByteArray;
                }
            }
            else
            {
                TempData["Message"] = "Please upload product picture";
                return View(model);
            }

            int currentUserId = GetCurrentUserId();
            entity.CreatedById = currentUserId;

            var result = _productRepository.Add(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            return View("Add", model);
        }

        public IActionResult Edit(int id)
        {
            var product = _productRepository.Get(x => x.Id == id && x.IsActive, include: x => x.Include(y => y.Category).Include(y => y.Supplier));

            if (product != null)
            {
                var vm = new ProductViewModel()
                {
                    Id = product.Id,
                    Supplier = product.Supplier,
                    Category = product.Category,
                    SupplierName = product.Supplier.SupplierName,
                    CategoryName = product.Category.CategoryName,
                    Isactive = product.IsActive,
                    UnitsInStock = product.UnitsInStock,
                    UnitPrice = product.UnitPrice,
                    ReorderLevel = product.ReorderLevel,
                    Discontinued = product.Discontinued,
                    PictureStr = Convert.ToBase64String(product.Picture),
                };

                ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList();

                ViewBag.Suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.SupplierName,
                    Value = x.Id.ToString()

                }).ToList();

                vm.ProductName = product.ProductName;
                vm.SupplierName = product.Supplier.SupplierName;
                return View(vm);
            }

            TempData["Message"] = "Product cannot be found!";
            return View("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString(),
                }).ToList();

                ViewBag.Suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
                {
                    Text = x.SupplierName,
                    Value = x.Id.ToString()
                }).ToList();

                return View(model);
            }

            ViewBag.Categories = _categoryRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            }).ToList();

            ViewBag.Suppliers = _supplierRepository.GetAll(x => x.IsActive).Select(x => new SelectListItem()
            {
                Text = x.SupplierName,
                Value = x.Id.ToString()
            }).ToList();

            int currentUserId = GetCurrentUserId();
            var entity = new Product()
            {
                Id = model.Id,
                Discontinued = model.Discontinued,
                ReorderLevel = model.ReorderLevel,
                CategoryId = model.CategoryId,
                SupplierId = model.SupplierId,
                ProductName = model.ProductName,
                UnitsInStock = model.UnitsInStock,
                UnitPrice = model.UnitPrice,
                UpdatedById = currentUserId,
                UpdatedDate = DateTime.Now,
                IsActive = model.Isactive,
            };

            if (model.Picture == null)
            {
                byte[] picture = Encoding.ASCII.GetBytes(model.PictureStr);
                entity.Picture = picture;
            }
            else
            {
                if (model.Picture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        model.Picture.CopyTo(memoryStream);
                        var fileByteArray = memoryStream.ToArray();

                        entity.Picture = fileByteArray;
                    }
                }
                else
                {
                    TempData["Message"] = "You cannot upload empty file";
                }
            }
            

            var result = _productRepository.Edit(entity);

            if (result)
            {
                return RedirectToAction("List");
            }

            TempData["Message"] = "Uh oh! Something went wrong...";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _productRepository.Delete(id);

            TempData["Message"] = result ? "Deleted" : "Delete failed";
            return RedirectToAction("List");
        }
    }
}
