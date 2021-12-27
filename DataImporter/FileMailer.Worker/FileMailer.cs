using DataImporter.Common.Utilities;
using FileMailer.Worker.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMailer.Worker
{
    public class FileMailer
    {
        private readonly ExportHistoryModel _exportHistoryModel;
        private readonly IEmailService _emailService;
        private readonly ILogger<FileMailer> _logger;
        public FileMailer(ExportHistoryModel exportHistoryModel, IEmailService emailService, ILogger<FileMailer> logger)
        {
            _exportHistoryModel = exportHistoryModel;
            _emailService = emailService;
            _logger = logger;
        }
        public void HasFileToMail()
        {
            var exportHistory = _exportHistoryModel.GetExportHistory("Completed");
            if(exportHistory!=null)
            {
                string path =   exportHistory.FileName;
                _emailService.SendEmail(exportHistory.MailingAddress, "Exported File", "", path);
                exportHistory.Status = "Mailed";
                _exportHistoryModel.Update(exportHistory);
                FileInfo file = new FileInfo(path);
                file.Delete();
                _logger.LogInformation("Exported File Mailed");
            }
        }
    }
}
