using Core.Extensions;
using Domain.Entites;
using InfraStructure.Extensions;
using InfraStructure.Seeders;
using Microsoft.OpenApi.Models;
using Resturants.API.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
// enables API exploration and documentation for Minimal APIs in ASP.NET Core.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {{
        new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference {Type=ReferenceType.SecurityScheme,Id="bearerAuth" }
        },
        []
        }
    });
});
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
builder.Host.UseSerilog((context, configuration) =>


configuration.ReadFrom.Configuration(context.Configuration)
    //configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning).
    //MinimumLevel.Override("Microsoft.EntityFrameWorkCore", LogEventLevel.Information).
    //WriteTo.Console(outputTemplate: "[{Timestamp: dd-MM:HH:mm:ss} {Level:u3}] | {SourceContext} |{NewLine} {Message:lj}{NewLine}{Exception}")
    //.WriteTo.File("Logs/ResturantAPI-Lgos",rollingInterval:RollingInterval.Day,rollOnFileSizeLimit:true,fileSizeLimitBytes: 1000000)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();
app.UseSerilogRequestLogging();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();
if (app.Environment.IsDevelopment()) // Only in development mode
{
    app.UseSwagger();
    app.UseSwaggerUI();
   
}

app.UseHttpsRedirection();

//	Maps Identity API endpoints to HTTP routes

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
