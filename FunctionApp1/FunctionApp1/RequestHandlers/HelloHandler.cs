using FunctionApp1.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionApp1.RequestHandlers
{
    internal class HelloHandler : IRequestHandler<Hello, string>
    {
        public Task<string> Handle(Hello request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"Hello {request.Target}");
        }
    }
}
