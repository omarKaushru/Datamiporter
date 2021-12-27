using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataExporter.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DataExporter _dataExporter;
        public Worker(ILogger<Worker> logger, DataExporter dataExporter)
        {
            _logger = logger;
            _dataExporter = dataExporter;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Run(() => _dataExporter.HasDataToExport());
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
