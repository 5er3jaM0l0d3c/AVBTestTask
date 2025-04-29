using Grpc.Core;
using ProductService.Application.Interface;
using ProductService.Application.Service;
using ProductService.Grpc.Protos;

namespace ProductService.Grpc.Service
{
    public class ProductServerService : ProductProto.ProductProtoBase   
    {
        private IProduct service { get; set; }

        public ProductServerService(IProduct service)
        {
            this.service = service;
        }


        public async override Task<Product> gGetProduct(ProductId request, ServerCallContext context)
        {
            var productId = request.Id;

            var product = await service.GetProduct(productId);

            if (product == null)
            {
                product = new();
            }

            return await Task.FromResult(new Product { Id = product.Id, Amount = product.Amount, Name = product.Name, Price = (float)product.Price });
        }

        public override Task<EmptyResponse> gAddProduct(Product request, ServerCallContext context)
        {
            ProductService.Domain.Entities.Product product = new ()
            {
                Name = request.Name,
                Amount = request.Amount,
            };

            service.AddProduct(product);

            return Task.FromResult(new EmptyResponse { });
        }

        public override Task<EmptyResponse> gUpdateAmount(ProductIdAmount request, ServerCallContext context)
        {
            var productId = request.Id;
            var amount = request.Amount;

            service.UpdateProduct(productId, amount);

            return Task.FromResult(new EmptyResponse { });
        }

    }
}
