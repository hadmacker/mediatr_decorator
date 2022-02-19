using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MediatR;
using FunctionApp1.Requests;

namespace FunctionApp1
{
    public class PingFunction
    {
        private readonly IMediator _mediator;

        public PingFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("Ping")]
        public async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Ping")] HttpRequest req)
        {
            return await _mediator.Send(new Ping());
        }
    }
}
