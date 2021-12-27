using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using DataExporter.Worker.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExporter.Worker
{
    public class DataExporter
    {
        private readonly ExportHistoryModel _exportHistoryModel;
        private readonly DataExporterModel _dataExporterModel;
        private readonly ILogger<DataExporter> _logger;
        private IExcelRecordService _excelRecordService;
        public DataExporter(ILogger<DataExporter> logger, ExportHistoryModel exportHistoryModel, IExcelRecordService excelRecordService,
            DataExporterModel dataExporterModel)
        {
            _exportHistoryModel = exportHistoryModel;
            _excelRecordService = excelRecordService;
            _dataExporterModel = dataExporterModel;
            _logger = logger;
        }
        public void HasDataToExport()
        {
            IList<string> FieldName = new List<string>();
            IList<string> FieldValue = new List<string>();
            var exporthistory =_exportHistoryModel.GetExportHistory("Pending");
            if(exporthistory!=null)
            {
                var excelRecord = _excelRecordService.GetExcleRecordData(exporthistory.GroupId);
                exporthistory.Status = "Processing";
                _exportHistoryModel.Update(exporthistory);
                foreach (var item in excelRecord.ExcelDatas)
                {
                    if (!FieldName.Contains(item.FieldName))
                        FieldName.Add(item.FieldName);
                    FieldValue.Add(item.FieldValue);

                }
                _dataExporterModel.Export(FieldName, FieldValue, exporthistory);
                _logger.LogInformation("Group Record Exported");
            }
        
        }
    }
}
