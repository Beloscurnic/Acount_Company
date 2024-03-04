using Application.Common.FluidValidation;
using Application.Service;
using Application.Service.ISAuthService;
using Application.Service.Token;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjection).Assembly);
            //services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            //services.AddTransient(typeof(IPipelineBehavior<,>),
            //    typeof(ValidationBehavior<,>));
            services.AddSingleton<IAuthURLs>();
            services.AddSingleton<GlobalQuery>();
            services.AddTransient<ITokenService, TokenService>();
            return services;
        }
    }
}
