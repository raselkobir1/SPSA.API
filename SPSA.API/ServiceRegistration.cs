using Microsoft.EntityFrameworkCore;
using SPSA.API.DataAccess.DataContext;

namespace SPSA.API
{
    public static class ServiceRegistration
    {
        private static readonly string connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config) 
        {
            //var connection = config["ConnectionStrings:DefaultConnection"]; // 2nd way
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connection));

        }
    }
}
