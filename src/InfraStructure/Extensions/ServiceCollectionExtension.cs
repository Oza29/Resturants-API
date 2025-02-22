using Core.RepositoryContracts;
using Domain.Entites;
using Domain.ServiceContract;
using InfraStructure.AppDbContext;
using InfraStructure.Authorization;
using InfraStructure.Authorization.Services;
using InfraStructure.Repository;
using InfraStructure.Seeders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfraStructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultKey")).
            EnableSensitiveDataLogging());
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()                              //adds Role information to claims
                .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>() //adds the custom claims inested of default one
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorizationBuilder().AddPolicy(PolicyNames.HasNationality, builder =>
            builder.RequireClaim(AppClaimTypes.DateOfBirth,"german"));

            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        }
    }
}
