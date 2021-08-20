using Application.Branches;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extension
{
    public static class ControllerServicesExtensions
    {
        public static IServiceCollection AddControllerServices(this IServiceCollection services,
        IConfiguration config)
        {
            services.AddControllers(opt => 
            {
                opt.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddFluentValidation( _config => 
                {
                    _config.RegisterValidatorsFromAssemblyContaining<CreateBranch>();
                }
            );
            // Disable automatic response message for fluent validation 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
}