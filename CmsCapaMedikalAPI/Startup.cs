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
            services.AddDbContext<CapamedikalApiContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("CapaMedikalDB")));
            services.AddControllers();
            services.AddCors();

            var appSettingSection = Configuration.GetSection("TokenOptions");
            services.Configure<TokenOptions>(appSettingSection);
            var appSettings = appSettingSection.Get<TokenOptions>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecurityKey);

            //[Authorize] belirtilmeyen şemalarda da varsayılan olarak AuthenticationScheme kullanılır.
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => {
                x.RequireHttpsMetadata = false; // Https istekleri için gerekli olan adres yapılandırmasını istemiyorum
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key), //Secret ile oluşturduğumuz anahtarı güvenlik anaktarı olarak atıyorum.
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
