using FunctionApp1;
using FunctionApp1.Pipelines;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp1
{
    //Nuget install: 
    //Microsoft.Azure.Functions.Extensions
    //Microsoft.NET.Sdk.Functions package version 1.0.28 or later
    //Microsoft.Extensions.DependencyInjection
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //https://github.com/jbogard/MediatR/wiki#aspnet-core-or-net-core-in-general
            builder.Services.AddMediatR(GetType());

            //[Registering Open Generics](https://www.vaughanreid.com/2020/04/asp-net-core-dependency-injection-registering-open-generics/#:~:text=A%20really%20interesting%20trick%20you,having%20to%20register%20it%20specifically.)
            //[GH: Pipeline Behaviours](https://github.com/jbogard/MediatR/wiki/Behaviors)

            // Pipeline Behaviours, OPTION 1: Manually declare all behaviours. Pipeline Behaviours execute in order of declaration below:
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            //builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TimingBehaviour<,>));

            // Pipeline Behaviours, OPTION 2: Scrutor to scan all assemblies. Unclear about controlling pipeline order.
            //[Scrutor Assembly Scanning DI](https://github.com/khellang/Scrutor)
            builder.Services.Scan(scan => scan.FromAssemblyOf<Startup>()
                .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            //[Serilog.net](https://serilog.net/)
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            _ = builder.Services.AddLogging(lb => lb.AddSerilog(logger));
        }
    }
}
