using Microsoft.EntityFrameworkCore;

namespace OMSX.ProductsService.DI
{
    public static class RegisterWithDependencyInjection
    {

        public static void AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Database.ProductDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
