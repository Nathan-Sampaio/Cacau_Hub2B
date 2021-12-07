using AutoMapper;
using Dominio.Entidade;
using Dominio.Entidade.AutoMapper;
using Dominio.Entidade.Configuracoes;
using Dominio.Entidade.Pedido;
using Dominio.Interface.Repositorios;
using Dominio.Interface.Servico;
using Dominio.Interface.Servico.Nf_e;
using Dominio.Interface.Servico.Pedido;
using Dominio.Interface.Servico.Tracking;
using Infra.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repositorio;
using Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();

                cfg.AddProfile(typeof(PedidoProfile));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            //Settings
            services.Configure<HubConfig>(options => Configuration.GetSection("HubConfig").Bind(options));
            services.Configure<RedisConfig>(options => Configuration.GetSection("RedisConfig").Bind(options));
            services.Configure<OmsConfig>(options => Configuration.GetSection("OMSConfig").Bind(options));

            //DI
            services.AddScoped<IRedisRepositorio, RedisRepositorio>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPedidoServico, PedidoService>();
            services.AddScoped<IIntegracaoService, IntegracaoService>();
            services.AddScoped<INotaFiscalService, NotaFiscalService>();
            services.AddScoped<ITrackingService, TrackingService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

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
