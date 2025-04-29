using ProductEntities;
using ProductService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Application.Interface
{
    public interface IProduct
    {
        public Task<Product?> GetProduct(int id);
        public Task AddProduct(Product product);
        public Task<Product?> UpdateProduct(int productId, int amount);
    }
}
