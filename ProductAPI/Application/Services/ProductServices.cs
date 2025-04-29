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

        public void AddProduct(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public Product? GetProduct(int id)
        {
            return context.Product.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateProduct(int productId, int amount)
        {
            var product = GetProduct(productId);
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

            context.SaveChanges();
        }
    }
}
