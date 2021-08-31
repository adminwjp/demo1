using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TakeOutFoot.Application;

namespace TakeOutFoot.Api
{
    public class TakeOutFootStartup
    {
        public TakeOutFootStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //������ ��� ���� һ�� �� ������ ������������
           // TestTakeOutFoot.Register(services);
            //return TestTakeOutFoot.serviceProvider;
            return null;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //TestTakeOutFoot.ApplicationBuilder = app;
           // TestTakeOutFoot.Start();
           
        }
    }
}
