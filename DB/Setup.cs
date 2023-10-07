using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CashFlowAPI.DB;

public static class DBSetup
{
    public static IServiceCollection AddDBServices(this IServiceCollection services)
    {
        IConfiguration configuration;
        using (var serviceProvider = services.BuildServiceProvider())
        {
            configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        services.Configure<MongoConfig>(configuration.GetSection("MongoConfig"));
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var options = serviceProvider.GetService<IOptions<MongoConfig>>().Value;
            if (options is null)
                // Log MongoDB not configured
                return new MongoClient();
            return new MongoClient(options.ConnectionString);
        });

        services.AddTransient<IMongoDatabase>(serviceProvider => 
        {
            var options = serviceProvider.GetService<IOptions<MongoConfig>>().Value;
            var client = serviceProvider.GetService<IMongoClient>();
            return client.GetDatabase(options.DatabaseName);
        });
        return services;
    }
}