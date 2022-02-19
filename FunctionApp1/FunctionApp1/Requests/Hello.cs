using MediatR;

namespace FunctionApp1.Requests
{
    internal class Hello : IRequest<string> 
    { 
        public string Target { get; set; }
    }
}
