using ProductEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServices.Interface
{
    public interface IProduct
    {
        public Product? GetProduct(int id);
        public void AddProduct(Product product);
        public void UpdateProduct(int productId, int amount);
    }
}
