using Grpc.Core;
using ProductEntities;
using ProductServices.Interface;

namespace ProductAPI.Service
{
    public class ProductService : ProductProto.ProductProtoBase, IProduct
    {
        private ProductServiceDbContext context {  get; set; }

        public ProductService(ProductServiceDbContext context)
        {
            this.context = context;
        }


        public override Task<Product> gGetProduct(ProductId request, ServerCallContext context)
        {
            var productId = request.Id;

            var product = GetProduct(productId);

            if (product == null) 
            {
                product = new();
            }

            return Task.FromResult( new Product { Id = product.Id, Amount = product.Amount, Name = product.Name, Price = (float)product.Price});
        }

        public ProductEntities.Product? GetProduct(int id)
        {
            return context.Product.FirstOrDefault(x => x.Id == id);
        }

        public override Task<EmptyResponse> gAddProduct(Product request, ServerCallContext context)
        {
            ProductEntities.Product product = new ProductEntities.Product()
            {
                Name = request.Name,
                Amount = request.Amount,
            };

            AddProduct(product);

            return Task.FromResult(new EmptyResponse { });
        }

        public void AddProduct(ProductEntities.Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
        }

        public override Task<EmptyResponse> gUpdateAmount(ProductIdAmount request, ServerCallContext context)
        {
            var productId = request.Id;
            var amount = request.Amount;

            UpdateProduct(productId, amount);

            return Task.FromResult(new EmptyResponse { });
        }

        public void UpdateProduct(int productId, int amount)
        {
            var product = GetProduct(productId);
            if (product != null)
            {
                product.Amount += amount;
                if (product.Amount < 0) throw new InvalidOperationException("Product.Amount < 0");
            }
            else
                throw new Exception("Product with Id=" + productId + " is undefined");
            context.SaveChanges();
        }
    }
}
