using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly DataImporter  _dataImporter;
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger, DataImporter  dataImporter)
        {
            _logger = logger;
            _dataImporter = dataImporter;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Run(() => _dataImporter.HasFileToImport());
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
