using EquipmentPlacementService.Data;
using EquipmentPlacementService.Features.Contracts;
using EquipmentPlacementService.Features.EquipmentTypes;
using EquipmentPlacementService.Features.Premisses;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace EquipmentPlacementService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetDefaultConnectionString()));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddTransient<IContractService, ContractService>()
                .AddTransient<IPremisesService, PremisesService>()
                .AddTransient<IEquipmentTypeService, EquipmentTypeService>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EquipmentPlacementServiceApi", Version = "v1" });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "API key needed to access the endpoints",
                    Scheme = "ApiKeyScheme"
                });
                
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}
