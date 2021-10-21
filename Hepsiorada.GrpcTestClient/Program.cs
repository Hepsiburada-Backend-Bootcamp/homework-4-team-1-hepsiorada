using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hepsiorada.GrpcTestClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                // The port number(5001) must match the port of the gRPC server.
                Console.ReadKey();
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new OrderRpc.OrderRpcClient(channel);
                var reply = await client.GetOrdersAsync(
                                  new OrderFilter { UserId = "1" });
                Console.WriteLine("Order: " + reply.Order);
                Console.WriteLine("Press any key to repeat...");
                Console.ReadKey();
            }
        }
    }
}
