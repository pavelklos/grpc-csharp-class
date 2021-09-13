using Average;
using Calculator;
using Greet;
using Grpc.Core;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;
using Max;
using Prime;
using Sqrt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        const int Port = 50051;
        //const int Port = 50052;

        //public static object CalculatorServiceImplervice { get; private set; }

        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                // SSL Security
                var caCrt = File.ReadAllText("ssl/ca.crt");
                var serverCrt = File.ReadAllText("ssl/server.crt");
                var serverKey = File.ReadAllText("ssl/server.key");
                var keypair = new KeyCertificatePair(serverCrt, serverKey);
                var credentials = new SslServerCredentials(new List<KeyCertificatePair>() { keypair }, caCrt, true);

                // gRPC C# Server Reflection
                // https://github.com/grpc/grpc/blob/master/doc/csharp/server_reflection.md
                // the reflection service will be aware of "Greeter" and "ServerReflection" services.
                var reflectionServiceImpl = new ReflectionServiceImpl(
                    GreetingService.Descriptor, ServerReflection.Descriptor);


                server = new Server()
                {
                    //Services = { GreetingService.BindService(new GreetingServiceImpl()) },
                    Services = { GreetingService.BindService(
                        new GreetingServiceImpl()),
                        ServerReflection.BindService(reflectionServiceImpl)},
                    //Services = { CalculatorService.BindService(new CalculatorServiceImpl()) },
                    //Services = { PrimeNumberService.BindService(new PrimeNumberServiceImpl()) },
                    //Services = { AverageService.BindService(new AverageServiceImpl()) },
                    //Services = { FindMaxService.BindService(new FindMaxServiceImpl()) },
                    //Services = { SqrtService.BindService(new SqrtServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                    //Ports = { new ServerPort("localhost", Port, credentials) }
                };

                server.Start();
                Console.Write($"The server is listening on the port : {Port}");
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine($"The server failed to start : {e.Message}");
                throw;
            }
            finally
            {
                if (server != null)
                {
                    server.ShutdownAsync().Wait();
                }
            }

        }
    }
}
