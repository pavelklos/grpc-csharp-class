using Average;
using Blog;
using Calculator;
using Dummy;
using Greet;
using Grpc.Core;
using Max;
using Prime;
using Sqrt;
using System;
using System.Collections.Generic;
using System.IO;
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
            // SSL Security
            var caCrt = File.ReadAllText("ssl/ca.crt");
            var clientCrt = File.ReadAllText("ssl/client.crt");
            var clientKey = File.ReadAllText("ssl/client.key");
            var channelCredential = new SslCredentials(caCrt,
                new KeyCertificatePair(clientCrt, clientKey));

            //Channel channel = new Channel(target, ChannelCredentials.Insecure);
            //Channel channel = new Channel("localhost", 50051, channelCredential);
            Channel channel = new Channel("localhost", 50051, ChannelCredentials.Insecure);

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
            //var client = new AverageService.AverageServiceClient(channel);
            //var stream = client.ComputeAverage();
            //foreach (int number in Enumerable.Range(1, 4))
            //{
            //    var request = new AverageRequest() { Number = number };
            //    await stream.RequestStream.WriteAsync(request);
            //}
            //await stream.RequestStream.CompleteAsync();
            //var response = await stream.ResponseAsync;
            //Console.WriteLine(response.Result);
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();


            //******************************************************************
            // Bi Directional 'BiDi' Streaming API
            //******************************************************************
            //TODO:
            //var client = new GreetingService.GreetingServiceClient(channel);
            //DoSimpleGreet(client);
            //await DoManyGreetings(client);
            //await DoLongGreet(client);
            //await DoGreetEveryone(client);
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // Max API
            //******************************************************************
            //var client = new FindMaxService.FindMaxServiceClient(channel);
            //var stream = client.FindMaximum();
            //var responseReaderTask = Task.Run(async () =>
            //{
            //    while (await stream.ResponseStream.MoveNext())
            //    {
            //        Console.WriteLine($"Max : {stream.ResponseStream.Current.Max}");
            //    }
            //});
            //int[] numbers = { 1, 5, 3, 6, 2, 20 };
            //foreach (int number in numbers)
            //{
            //    await stream.RequestStream.WriteAsync(new FindMaxRequest() { Number = number });
            //}
            //await stream.RequestStream.CompleteAsync();
            //await responseReaderTask;
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // Sqrt API
            //******************************************************************
            //var client = new SqrtService.SqrtServiceClient(channel);
            //int number = -1; // number = 16 [OK], -1 [ERROR]
            //try
            //{
            //    var request = new SqrtRequest() { Number = number };
            //    var response = client.sqrt(request);
            //    Console.WriteLine($"Sqrt : {response.SquareRoot}");
            //}
            //catch (RpcException e)
            //{
            //    Console.WriteLine($"Error : {e.Status.Detail}");
            //    //throw;
            //}
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // Deadlines
            //******************************************************************
            //var client = new GreetingService.GreetingServiceClient(channel);
            //try
            //{
            //    var request = new GreetDeadlinesRequest() { Name = "John" };
            //    var response = client.GreetDeadlines(request,
            //        deadline: DateTime.UtcNow.AddMilliseconds(100)); // 500 [OK], 100 [DEADLINE]
            //    Console.WriteLine(response.Result);
            //}
            //catch (RpcException e) when (e.StatusCode == StatusCode.DeadlineExceeded)
            //{
            //    Console.WriteLine($"Error : {e.Status.Detail}");

            //    //throw;
            //}
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // SSL Security for Unary API = 'DoSimpleGreet(client)'
            //******************************************************************
            //var client = new GreetingService.GreetingServiceClient(channel);
            //DoSimpleGreet(client);
            //channel.ShutdownAsync().Wait();
            //Console.ReadKey();

            //******************************************************************
            // BLOG by MongoDB
            //******************************************************************
            var client = new BlogService.BlogServiceClient(channel);
            var request = new CreateBlogRequest()
            {
                Blog = new Blog.Blog()
                {
                    AuthorId = "Clement",
                    Title = "New blog!",
                    Content = "Hello world, this is a new blog"
                }
            };
            var response = client.CreateBlog(request);
            Console.WriteLine($"The blog with id '{response.Blog.Id}' was created!");
            channel.ShutdownAsync().Wait();
            Console.ReadKey();

        }

        // 3x TODO: ONLY 2x (DoSimpleGreet() READY)
        public static void DoSimpleGreet(GreetingService.GreetingServiceClient client)
        {
            var greeting = new Greeting()
            {
                FirstName = "Clement",
                LastName = "Jean"
            };

            var request = new GreetingRequest() { Greeting = greeting };
            var response = client.Greet(request);

            Console.WriteLine(response.Result);
        }
        public static async Task DoManyGreetings(GreetingService.GreetingServiceClient client)
        {
            //TODO:
        }
        public static async Task DoLongGreet(GreetingService.GreetingServiceClient client)
        {
            //TODO:
        }

        public static async Task DoGreetEveryone(GreetingService.GreetingServiceClient client)
        {
            var stream = client.GreetEveryone();

            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine($"Received : {stream.ResponseStream.Current.Result}");
                }
            });

            Greeting[] greetings =
            {
                new Greeting() { FirstName = "John", LastName = "Doe" },
                new Greeting() { FirstName = "Clement", LastName = "Jean" },
                new Greeting() { FirstName = "Patricia", LastName = "Hertz" },
            };

            foreach (var greeting in greetings)
            {
                Console.WriteLine($"Sending : {greeting.ToString()}");
                await stream.RequestStream.WriteAsync(new GreetEveryoneRequest()
                {
                    Greeting = greeting
                });
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;
        }
    }
}
