using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FileMailer.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly FileMailer _fileMailer;
        public Worker(ILogger<Worker> logger, FileMailer fileMailer)
        {
            _logger = logger;
            _fileMailer = fileMailer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Run(() => _fileMailer.HasFileToMail());
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
