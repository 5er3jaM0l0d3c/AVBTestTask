using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.DbContexts;

namespace   ProductService.Application.Services
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
        }

        public async Task<Product> GetProduct(int id)
        {
            return await context.Product.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException("Product with Id = " + id + " not found");
        }

        public async Task UpdateProduct(int productId, int amount)
        {
            var product = await GetProduct(productId);
            product.Amount = amount;

            await context.SaveChangesAsync();
        }

        public async Task ReduceProductAmount(int productId, int amount)
        {
            var product = await GetProduct(productId);
            if (product.Amount < amount)
            {
                throw new InvalidOperationException("Product.Amount < 0");
            }

            product.Amount -= amount;

            await context.SaveChangesAsync();
        }
    }
}
