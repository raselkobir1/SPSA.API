﻿using Microsoft.EntityFrameworkCore;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Manager.Implementaion;
using SPSA.API.Manager.Intrerface;

namespace SPSA.API
{
    public static class ServiceRegistration
    {
        private static readonly string connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();   

            services.AddScoped<IAuthManager, AuthManager>();   
            services.AddScoped<IUserManager, UserManager>();   

            //var connection = config["ConnectionStrings:DefaultConnection"]; // 2nd way
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connection));

        }
    }
}
