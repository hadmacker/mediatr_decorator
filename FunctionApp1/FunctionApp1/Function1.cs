using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using MediatR;
using FunctionApp1.Requests;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly IMediator _mediator;

        public Function1(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("Function1")]
        public async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            return await _mediator.Send(new Ping());
        }
    }
}
