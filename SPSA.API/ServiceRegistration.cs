using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SPSA.API.DataAccess.DataContext;
using SPSA.API.DataAccess.UnitOfWorks;
using SPSA.API.Domain;
using SPSA.API.Helper.Configurations;
using SPSA.API.Manager.Implementaion;
using SPSA.API.Manager.Intrerface;
using System.Text;

namespace SPSA.API
{
    public static class ServiceRegistration
    {
        private static readonly string connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            //var connection = configuration["ConnectionStrings:DefaultConnection"]; // 2nd way
            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connection));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            #region Options Config
            services.AddOptions<JwtConfiguration>().BindConfiguration(nameof(JwtConfiguration)).ValidateDataAnnotations();
            #endregion
            #region Json Web Token config
            // Configure Authentication
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                //options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JwtConfiguration:Issuer"],
                    ValidAudience = configuration["JwtConfiguration:Audience"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfiguration:SigningKey"]))
                };

            });
            #endregion
            #region Swagger config
            //services.AddAuthorization();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SPSA API",
                });
            });
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on text-box below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                setup.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        jwtSecurityScheme, Array.Empty<string>()
                    }
                });
            });
            #endregion
            #region Fluent Validation config
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddFluentValidationRulesToSwagger();
            #endregion



            //Application service

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IUserManager, UserManager>();

        }
    }
}
