using Grpc.Core;
using Prime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Prime.PrimeNumberService;

namespace server
{
    public class PrimeNumberServiceImpl : PrimeNumberServiceBase
    {
        public override async Task PrimeNumberDecomposition(PrimeNumberDecompositionRequest request, IServerStreamWriter<PrimeNumberDecompositionResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine("The server received the request : ");
            Console.WriteLine(request.ToString());

            int number = request.Number;
            int divisor = 2;

            while (number > 1)
            {
                if (number % divisor == 0)
                {
                    number /= divisor;
                    await responseStream.WriteAsync(new PrimeNumberDecompositionResponse() { PrimeFactor = divisor });
                }
                else
                {
                    divisor++;
                }
            }


            //return base.PrimeNumberDecomposition(request, responseStream, context);
        }
    }
}

//- Example:
//	- The client will send one number (120) and the server will respond with a stream of (2,2,2,3,5), because 120 = 2 * 2 * 2 * 3 * 5
//  - Algorithm (pseudo code):
//	  k = 2
//    N = 210
//    while N > 1:
//      if N % k == 0:	// if k evenly divides into N
//        print k      	// this is a factor
//        N = N / k    	// divide N by k so that we have the rest of the number left.
//      else:
//	      k = k + 1