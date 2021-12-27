using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExporter.Worker.Model
{
    public class ExportHistoryModel
    {
        private readonly IExportHistoryService _exportHistoryService;
        public ExportHistoryModel(IExportHistoryService exportHistoryService)
        {
            _exportHistoryService = exportHistoryService;
        }

        public ExportHistory GetExportHistory(string status)
        {
            var exportHitory = _exportHistoryService.GetExportHistory(status);
            return exportHitory;
        }
        public void Update(ExportHistory exportHistory)
        {
            _exportHistoryService.Update(exportHistory);
        }
    }
}
