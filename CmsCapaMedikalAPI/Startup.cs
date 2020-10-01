using System;
using System.Text;
using CmsCapaMedikalAPI.Helper;
using CmsCapaMedikalAPI.Models;
using CmsCapaMedikalAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace CmsCapaMedikalAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var env = Environment.GetEnvironmentVariable("CapaMedikalDB");
            services.AddDbContext<ProductsContext>(opt => opt.UseSqlServer(env));
            services.AddControllers();
            services.AddCors();

            var appSettingSection = Configuration.GetSection("TokenOptions");
            services.Configure<TokenOptions>(appSettingSection);
            var appSettings = appSettingSection.Get<TokenOptions>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecurityKey);

            //[Authorize] belirtilmeyen þemalarda da varsayýlan olarak AuthenticationScheme kullanýlýr.
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false; // Https istekleri için gerekli olan adres yapýlandýrmasýný istemiyorum
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), //Secret ile oluþturduðumuz anahtarý güvenlik anaktarý olarak atýyorum.
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUserService, UserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
                
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
