using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OMSX.ProductsService.Database;
using OMSX.ProductsService.Implementation;
using OMSX.ProductsService.Interfaces;
using OMSX.Shared.Entities.Common;
using System;

namespace OMSX.ProductsService.DI
{
    public static class RegisterWithDependencyInjection
    {

        public static void AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProductDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void RegisterAutomapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);

        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            var assembly = typeof(EntityBase).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(EntityBase).IsAssignableFrom(type) && type.Name != "EntityBase")
                {
                    var method = typeof(RegisterWithDependencyInjection).GetMethod("RegisterWithDependencyInjection");
                    if (method != null)
                    {
                        var genericMethod = method.MakeGenericMethod(type);
                        genericMethod.Invoke(null, new object?[] { services });
                    }
                }
            }
        }
    }
}
