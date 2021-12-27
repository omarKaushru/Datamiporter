using Autofac;
using Autofac.Extensions.DependencyInjection;
using DataImporter.Common;
using DataImporter.Logic;
using DataImporter.Logic.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileMailer.Worker
{
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        private static IConfiguration _configuration;
        public static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();

            _connectionString = _configuration.GetConnectionString("DefaultConnection");

            _migrationAssemblyName = typeof(Worker).Assembly.FullName;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();
            try
            {
                Log.Information("Wroker Srvice Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message, "Worker ServiceApplication start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder => {
                    builder.RegisterModule(new FileMailerModule());
                    builder.RegisterModule(new LogicModule(_connectionString,
                        _migrationAssemblyName));
                    builder.RegisterModule(new CommonModule());

                })

                .ConfigureServices((hostContext, services) =>
                {
                    _migrationAssemblyName = typeof(Worker).Assembly.FullName;
                    _connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
                    services.AddHostedService<Worker>();
                    services.AddDbContext<DataImporterContext>(options =>
                    options.UseSqlServer(_connectionString, b =>
                    b.MigrationsAssembly(_migrationAssemblyName)));
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                });
    }
}
