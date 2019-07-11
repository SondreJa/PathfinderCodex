using Api.Middleware;
using Domain.Handlers;
using Domain.Models.Spells;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using SimpleInjector;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api
{
    public class Startup
    {
        private Container container;

        public Startup(IConfiguration configuration)
        {
            container = new Container();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PathfinderCodex API", Version = "version 1" });
                c.AddSecurityDefinition("Basic",
                new ApiKeyScheme
                {
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Basic", Enumerable.Empty<string>() },
            });
            });

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore()
                .AddControllerActivation()
                .AddTagHelperActivation();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseMiddleware<AuthenticationMiddleware>();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PathfinderCodex API V1");
            });

            app.UseSimpleInjector(container);
            InitializeContainer();
            container.Verify();

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void InitializeContainer()
        {
            container.RegisterCosmosRepository<Spell>(Configuration);
            container.RegisterSingleton<ISpellHandler, SpellHandler>();
        }
    }

    static class ContainerExtensions
    {
        private const string DatabaseName = "PathfinderCodex";

        public static void RegisterCosmosRepository<T>(this Container container, IConfiguration configuration) where T : class, new()
        {
            var cosmosUriString = configuration.GetSection("Cosmos").GetSection("Uri").Value;
            var cosmosUri = new Uri(cosmosUriString);
            var authKey = configuration["Cosmos-AuthKey"];

            var repo = new CosmosRepository<T>(cosmosUri, authKey, DatabaseName, typeof(T).Name);
            container.RegisterSingleton<ICosmosRepository<T>>(() => repo);
        } 
    }
}
