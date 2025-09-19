using Application.PipelineBehaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    //this class groups packages, libraries and dependencies - it's an extension method that will help to cleanly pass the dependency from one layer to another
    public static class ServiceCollectionExtensions
    {
        public static void AddAppilactionServices(this IServiceCollection services)
        {
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehaviour<,>));
        }
    }
}
