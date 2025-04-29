using Grpc.Net.Client;
using OrderService.Grpc.Protos;

namespace OrderService.Grpc.Service
{
    public static class ProductClientService
    {
        public async static Task<bool> IsProductExist(int id)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5057");

            var client = new ProductProto.ProductProtoClient(channel);

            ProductId productId = new ProductId() { Id = id };

            Product product = await client.gGetProductAsync(productId);

            Product response = new Product()
            {
                Id = productId.Id,
            };

            return response.Id != 0;
        }

        public async static Task UpdateAmount(int id, int amount)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5057");

            var client = new ProductProto.ProductProtoClient(channel);

            ProductIdAmount productId = new ProductIdAmount() { Id = id, Amount = amount };

            await client.gReduceProductAmountAsync(productId);
        }
    }
}
