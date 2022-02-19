using FunctionApp1.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunctionApp1.RequestHandlers
{
    internal class PingHandler : IRequestHandler<Ping, string>
    {
        public Task<string> Handle(Ping request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Pong");
        }
    }
}
