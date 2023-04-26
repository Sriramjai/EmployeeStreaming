using Confluent.Kafka;
using EmployeeInfoConsumer.Data;
using EmployeeInfoConsumer.Services;
using EmployeeInfoConsumer.Services.ConsumerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeInfoConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create serice collection
            var services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            using var serviceProvider = services.BuildServiceProvider();


            // entry to run app
            serviceProvider.GetService<EmployeeInfoConsumerService>().ConsumeMessage();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            //configure logging
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();

            });

            // build config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            IConfiguration config = builder.Build();

            services.Configure<EmployeeInfoConsumerConfiguration>(config.GetSection("consumerConfiguration"));

            services.AddDbContext<EmployeeDBContext>(options => options.UseSqlServer(config.GetConnectionString("EmployeeDB")));

            // add app
            services.AddSingleton<EmployeeInfoConsumerService>();
            services.AddSingleton<ConsumerDBService>();
        }
    }
}