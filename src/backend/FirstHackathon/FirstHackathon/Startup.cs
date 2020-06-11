using FirstHackathon.Context;
using FirstHackathon.Context.Repository;
using FirstHackathon.Extensions;
using FirstHackathon.Middlewares;
using FirstHackathon.Models.Authentication;
using FirstHackathon.Models.Repository;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace FirstHackathon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region MVC

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssemblyContaining<Startup>();
                configuration.LocalizationEnabled = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            })
            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddHttpContextAccessor();

            services.AddSwagger();

            services.AddControllers();

            #endregion

            #region AuthenticationAndAuthorization

            services.AddAuthentication().AddJwtBearer("person", options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Auth:PersonJwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Auth:PersonJwt:Audience"],
                    ValidateLifetime = false,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:PersonJwt:SecretKey"])),
                    ValidateIssuerSigningKey = true,
                };
            }).AddJwtBearer("admin", options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["Auth:AdminJwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["Auth:AdminJwt:Audience"],
                    ValidateLifetime = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:AdminJwt:SecretKey"])),
                    ValidateIssuerSigningKey = true,
                };
            });

            services.AddAuthorization(o =>
            {
                var minerPolicy = new AuthorizationPolicyBuilder("person").RequireAuthenticatedUser();
                o.AddPolicy("person", minerPolicy.Build());

                var adminPolicy = new AuthorizationPolicyBuilder("admin").RequireAuthenticatedUser();
                o.AddPolicy("admin", adminPolicy.Build());
            });

            services.AddScoped<IJwtAccessTokenFactory, JwtAccessTokenFactory>();
            services.Configure<JwtAuthPersonOptions>(Configuration.GetSection("Auth:PersonJwt"));
            services.Configure<JwtAuthHouseOptions>(Configuration.GetSection("Auth:AdminJwt"));

            #endregion

            #region DbContext

            services.AddDbContext<FirstHackathonDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FirstHackathon")));

            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(options => { options.RouteTemplate = "{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../v1/swagger.json", "FirstHackathon MyHome Api");
            });

            app.UseRequestResponseLogging(Microsoft.Extensions.Logging.LogLevel.Information, Microsoft.Extensions.Logging.LogLevel.Information);

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect(@"^$", "swagger", (int)HttpStatusCode.Redirect));
        }
    }
}
