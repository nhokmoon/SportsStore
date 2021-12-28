using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        public ProductController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        public ViewResult List(string category)
        {
            return View(new ProductListViewModel()
               {
                   Products = productRepository.Products
                   .Where(p => category == null || p.Category == category)
                   .OrderBy(p => p.ProductId)
               });

        }

        public IActionResult Detail(int productId)
        {
            return View(productRepository.Products.FirstOrDefault(p => p.ProductId == productId));
        }
    }
}
