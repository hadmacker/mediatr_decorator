using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MediatR;
using FunctionApp1.Requests;
using System.Linq;

namespace FunctionApp1
{
    public class HelloFunction
    {
        private readonly IMediator _mediator;

        public HelloFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("Hello")]
        public async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Hello")] HttpRequest req)
        {
            var target = req.Query.TryGetValue("target", out var targetValue) 
                && targetValue.Any() 
                    ? targetValue.ToString() 
                    : "world";
            return await _mediator.Send(new Hello
            {
                Target = target
            });
        }
    }
}
