using Core.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var Assemby = typeof(ServiceCollectionExtensions).Assembly;
            //services.AddLogging();
            services.AddAutoMapper(Assemby);
            services.AddValidatorsFromAssembly(Assemby).
                AddFluentValidationAutoValidation();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assemby));
            services.AddScoped<IUserContext, UserContext>();

        }
    }
}
