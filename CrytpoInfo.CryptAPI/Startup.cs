using CrytpoInfo.Buisness.Repositories;
using CrytpoInfo.Core.Repositories;
using CrytpoInfo.CryptAPI.Services;
using CrytpoInfo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace CrytpoInfo.CryptAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrytpoInfo.CryptAPI", Version = "v1" });
            });

            services.AddSingleton(Configuration);

            services.AddHttpClient<ITwitterRepository, TwitterRepository>(cl =>
                cl.BaseAddress = new Uri("https://api.twitter.com/2/"));

            services.AddHttpClient<IHistoricalDataRepository<HistoricalDataResults>, CoinMarketHistoricalDataRepository>(cl =>
                cl.BaseAddress = new Uri(Configuration["HistoricalData:Repositories:CoinMarket:Url"]));

            services.AddSingleton<HistoricalDataService>();
            services.AddSingleton<TwitterService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrytpoInfo.CryptAPI v1"));
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
