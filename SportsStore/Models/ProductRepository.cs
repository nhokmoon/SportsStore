using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public IQueryable<Product> Products => context.Products;

        public Product DeleteProduct(int productId)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public void SaveProduct(Product product)
        {

            //nếu thêm 1 sản phẩm mới
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else//nếu cập nhật sản phảm
            {
                Product p = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (p != null)
                {
                    p.Name = product.Name;
                    p.Description = product.Description;
                    p.Price = product.Price;
                    p.Category = product.Category;
                    p.ImageUrl = product.ImageUrl;

                }
            }
            context.SaveChanges();
        }
    }
}
