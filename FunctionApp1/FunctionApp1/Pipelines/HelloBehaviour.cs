using FunctionApp1.Requests;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionApp1.Pipelines
{
    internal class HelloBehaviour : IPipelineBehavior<Hello, string>
    {
        private readonly ILogger<HelloBehaviour> _logger;

        public HelloBehaviour(ILogger<HelloBehaviour> logger)
        {
            _logger = logger;
        }

        public async Task<string> Handle(Hello request, CancellationToken cancellationToken, RequestHandlerDelegate<string> next)
        {
            _logger.LogInformation("UPPERCASING HELLO TARGET");
            request.Target = request.Target.ToUpper();
            var response = await next();
            return response;
        }
    }
}
