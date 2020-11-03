using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Actio.Service.Activity.MongoDb
{
    public static class Extensions
    { 
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            MongoOptions options = new MongoOptions();
            configuration.GetSection("mongo").Bind(options);

            services.AddSingleton<IMongoClient>(c => new MongoClient(options.ConnectionString));
            services.AddScoped<IMongoDatabase>(c =>
            {
                var client = c.GetService<IMongoClient>();
                return client.GetDatabase(options.DatabaseName);
            });
            return services;
        }
    }
}