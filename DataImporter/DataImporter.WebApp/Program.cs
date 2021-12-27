using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.WebApp
{
    public class Program
    {
        private const string _connectionStringName = "DefaultConnection";
        private const string _schemaName = "dbo";
        private const string _tableName = "LogEvents";
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var columnOptionsSection = configuration.GetSection("Serilog:ColumnOptions");
            var sinkOptionsSection = configuration.GetSection("Serilog:SinkOptions");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.MSSqlServer(
                    connectionString: _connectionStringName,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = _tableName,
                        SchemaName = _schemaName,
                        AutoCreateSqlTable = true
                    },
                    sinkOptionsSection: sinkOptionsSection,
                    appConfiguration: configuration,
                    columnOptionsSection: columnOptionsSection)
                    .WriteTo.Email(new EmailConnectionInfo
                    {
                        FromEmail = "aspnetdeveloper2021@gmail.com",
                        ToEmail = "one.zero.truefalse@gmail.com",
                        EmailSubject = "Log Message of DataImporter",
                        EnableSsl = true,
                        IsBodyHtml = true,
                        MailServer = "smtp.gmail.com",
                        Port = 465,
                        NetworkCredentials = new NetworkCredential
                        {
                            UserName = "aspnetdeveloper2021@gmail.com",
                            Password = "******"
                        }
                    },
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {SourceContext} {Message}{NewLine}{Exception}",
                batchPostingLimit: 1,
                period: TimeSpan.FromSeconds(10),
                restrictedToMinimumLevel: LogEventLevel.Verbose
                )
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Debug("Strurting up");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"),
                Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Error(e, "Failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:80");
                });
    }
}
