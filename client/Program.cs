using Average;
using Calculator;
using Dummy;
using Greet;
using Grpc.Core;
using Prime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        const string target = "127.0.0.1:50051";
        //const string target = "127.0.0.1:50052";

        //static void Main(string[] args)
        static async Task Main(string[] args)
        {
            Channel channel = new Channel(target, ChannelCredentials.Insecure);

            // channel.ConnectAsync().ContinueWith((task) =>
            await channel.ConnectAsync().ContinueWith((task) =>
            {
                if (task.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected successfully");
                }
            });

            //var client = new DummyService.DummyServiceClient(channel);

            //var client = new GreetingService.GreetingServiceClient(channel);

            //var greeting = new Greeting()
            //{
            //    FirstName = "Clement",
            //    LastName = "Jean"
            //};

            //******************************************************************
            // Unary API
            //******************************************************************
            //var request = new GreetingRequest() { Greeting = greeting };
            //var response = client.Greet(request);
            //Console.WriteLine(response.Result);

            //******************************************************************
            // Calculator API
            //******************************************************************
            //var client = new CalculatorService.CalculatorServiceClient(channel);
            //var request = new SumRequest()
            //{
            //    A = 3,
            //    B = 10
            //};
            //var response = client.Sum(request);
            //Console.WriteLine(response.Result);

            //******************************************************************
            // Streaming Server API
            //******************************************************************
            //var client = new GreetingService.GreetingServiceClient(channel);
            //var greeting = new Greeting()
            //{
            //    FirstName = "Clement",
            //    LastName = "Jean"
            //};
            //var request = new GreetManyTimesRequest() { Greeting = greeting };
            //var response = client.GreetManyTimes(request);
            //while (await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.Result);
            //    await Task.Delay(200);
            //}
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // PrimeNumberDecomposition API
            //******************************************************************
            //var client = new PrimeNumberService.PrimeNumberServiceClient(channel);
            //var request = new PrimeNumberDecompositionRequest() { Number = 120 };
            //var response = client.PrimeNumberDecomposition(request);
            //while (await response.ResponseStream.MoveNext())
            //{
            //    Console.WriteLine(response.ResponseStream.Current.PrimeFactor);
            //    await Task.Delay(200);
            //}
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // Streaming Client API
            //******************************************************************
            //var client = new GreetingService.GreetingServiceClient(channel);
            //var greeting = new Greeting()
            //{
            //    FirstName = "Clement",
            //    LastName = "Jean"
            //};
            //var request = new LongGreetRequest() { Greeting = greeting };
            //var stream = client.LongGreet();
            //foreach (int i in Enumerable.Range(1, 10))
            //{
            //    await stream.RequestStream.WriteAsync(request);
            //}
            //await stream.RequestStream.CompleteAsync();
            //var response = await stream.ResponseAsync;
            //Console.WriteLine(response.Result);
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // Average API
            //******************************************************************
            var client = new AverageService.AverageServiceClient(channel);
            var stream = client.ComputeAverage();
            foreach (int number in Enumerable.Range(1, 4))
            {
                var request = new AverageRequest() { Number = number };
                await stream.RequestStream.WriteAsync(request);
            }
            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;
            Console.WriteLine(response.Result);
            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
