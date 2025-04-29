using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interface;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace ProductService.Application.Service
{
    public class ProductServices : IProduct
    {

        private ProductServiceDbContext context { get; set; }
        public ProductServices(ProductServiceDbContext context)
        {
            this.context = context;
        }

        public async Task AddProduct(Product product)
        {
            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();
            await context.Product.FirstOrDefaultAsync(x => x == product);
        }

        public async Task<Product?> GetProduct(int id)
        {
            return await context.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> UpdateProduct(int productId, int amount)
        {
            var product = await GetProduct(productId);
            if (product != null)
            {
                product.Amount += amount;
                if (product.Amount < 0)
                {
                    throw new InvalidOperationException("Product.Amount < 0");
                }
            }
            else
            {
                throw new Exception("Product with Id=" + productId + " is undefined");
            }

            await context.SaveChangesAsync();
            return await context.Product.FirstOrDefaultAsync(x => x.Id == productId);
        }
    }
}
