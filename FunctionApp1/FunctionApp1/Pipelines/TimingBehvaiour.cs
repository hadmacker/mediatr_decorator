using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionApp1.Pipelines
{
    internal class TimingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TimingBehaviour<TRequest, TResponse>> _logger;

        public TimingBehaviour(ILogger<TimingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var sw = new Stopwatch();
            sw.Start();
            var response = await next();
            _logger.LogInformation($"Elapsed: {sw.Elapsed}");
            _logger.LogInformation($"ElapsedMillis: {sw.ElapsedMilliseconds}");

            return response;
        }
    }
}
