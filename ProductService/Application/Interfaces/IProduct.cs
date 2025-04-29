using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces
{
    public interface IProduct
    {
        public Task<Product> GetProduct(int id);
        public Task AddProduct(Product product);
        public Task UpdateProduct(int productId, int amount);
        public Task ReduceProductAmount(int productId, int amount);
    }
}
