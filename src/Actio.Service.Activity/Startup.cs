using Actio.Common.Commands;
using Actio.Common.RabbitMq;
using Actio.Service.Activity.Domain.Repositories;
using Actio.Service.Activity.Handlers;
using Actio.Service.Activity.MongoDb;
using Actio.Service.Activity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Actio.Service.Activity
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
            services.AddControllers();
            services.AddRabbitMq(Configuration);
            services.AddMongo(Configuration);
            services.AddTransient<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddScoped<IActivityService,ActivityService>();
            services.AddScoped<IActivityRepository,ActivityRepository>();
            services.AddScoped<ICategoryRepository,CategoryRepository>();
            


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
