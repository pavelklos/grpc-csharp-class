﻿using Calculator;
using Greet;
using Grpc.Core;
using Prime;
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

        public static object CalculatorServiceImplervice { get; private set; }

        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    //Services = { GreetingService.BindService(new GreetingServiceImpl()) },
                    //Services = { CalculatorService.BindService(new CalculatorServiceImpl()) },
                    Services = { PrimeNumberService.BindService(new PrimeNumberServiceImpl()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
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
