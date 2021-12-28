using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace SportsStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository productRepository;
        private IHostingEnvironment hostingEnvironment;

        public AdminController(IProductRepository _productRepository, IHostingEnvironment _hostingEnvironment)
        {
            productRepository = _productRepository;
            hostingEnvironment = _hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(new ProductListViewModel()
            {
                Products = productRepository.Products
                   .OrderBy(p => p.ProductId)
            });
        }

        public IActionResult Edit(int productId)
        {
            return View(productRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)//thông tin nhập vào hợp lệ
            {
                productRepository.SaveProduct(product);
                return RedirectToAction("Index");
            }
            else// ko hợp lệ hiện view edit
            {
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;

                if (model.Photo != null)
                {
                    //photo sẽ được lưu trên thư mực wwwroot/images
                    //lấy đường dẫn lưu hình
                    string path = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    fileName = model.Photo.FileName;
                    string filePath = Path.Combine(path, fileName);
                    //lưu hình vào thư mục images
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Product p = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Category = model.Category,
                    ImageUrl = fileName,
                };
                productRepository.SaveProduct(p);

                return RedirectToAction("Index", model);
            }
            else return View(model);
        }

        public IActionResult Delete(int productId)
        {
            productRepository.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}
