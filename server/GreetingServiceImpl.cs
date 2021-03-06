using Greet;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Greet.GreetingService;

namespace server
{
    public class GreetingServiceImpl : GreetingServiceBase
    {
        public override Task<GreetingResponse> Greet(GreetingRequest request, ServerCallContext context)
        {
            string result = $"hello {request.Greeting.FirstName} {request.Greeting.LastName}";

            return Task.FromResult(new GreetingResponse() { Result = result });

            //return base.Greet(request, context);
        }

        public override async Task GreetManyTimes(GreetManyTimesRequest request, IServerStreamWriter<GreetManyTimesResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("The server received the request : ");
            Console.WriteLine(request.ToString());

            string result = $"hello {request.Greeting.FirstName} {request.Greeting.LastName}";

            foreach (int i in Enumerable.Range(1, 10))
            {
                await responseStream.WriteAsync(new GreetManyTimesResponse() { Result = $"[{i}] {result}" });
                //return Task.FromResult(new GreetingResponse() { Result = result });
            }

            //return base.GreetManyTimes(request, responseStream, context);
        }

        public override async Task<LongGreetResponse> LongGreet(IAsyncStreamReader<LongGreetRequest> requestStream, ServerCallContext context)
        {
            string result = "";

            while (await requestStream.MoveNext())
            {
                Greeting greeting = requestStream.Current.Greeting;
                result += $"Hello {greeting.FirstName} {greeting.LastName} {Environment.NewLine}";
            }

            return new LongGreetResponse() { Result = result };

            //return base.LongGreet(requestStream, context);
        }

        public override async Task GreetEveryone(IAsyncStreamReader<GreetEveryoneRequest> requestStream, IServerStreamWriter<GreetEveryoneResponse> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext())
            {
                Greeting greeting = requestStream.Current.Greeting;

                // Sending
                var result = $"Hello {greeting.FirstName} {greeting.LastName}";
                Console.WriteLine($"Sending : {result}");

                // Received
                //await responseStream.WriteAsync(new GreetEveryoneResponse() { Result = $"Received : {result}" });
                await responseStream.WriteAsync(new GreetEveryoneResponse() { Result = result });
            }

            //return base.GreetEveryone(requestStream, responseStream, context);
        }

        public override async Task<GreetDeadlinesResponse> GreetDeadlines(GreetDeadlinesRequest request, ServerCallContext context)
        {
            await Task.Delay(300);

            return new GreetDeadlinesResponse() { Result = $"Hello {request.Name}" };

            // return base.GreetDeadlines(request, context);
        }
    }
}
