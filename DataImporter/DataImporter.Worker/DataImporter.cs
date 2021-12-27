using DataImporter.Common.Utilities;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using DataImporter.Worker.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class DataImporter
    {
        private readonly IDateTimeUtility _dateTimeUtility;
        private readonly ImportHistoryModel _importHistoryModel;
        private readonly InsertExcelRecordModel  _insertExcelRecordModel;
        private IExcelFileService _excelFileService;
        private readonly ILogger<DataImporter> _logger;
        public DataImporter(ILogger<DataImporter> logger, InsertExcelRecordModel  insertExcelRecordModel, IExcelFileService excelFileService, 
             IDateTimeUtility dateTimeUtility, ImportHistoryModel importHistoryModel)
        {
            _insertExcelRecordModel = insertExcelRecordModel;
            _logger = logger;
            _excelFileService = excelFileService;
            _dateTimeUtility = dateTimeUtility;
            _importHistoryModel = importHistoryModel;

        }
        public void HasFileToImport()
        {
            var data = _excelFileService.GetExcelFile();
            if(data!=null)
            {
                int totalData = 0;
                var history = _importHistoryModel.GetHistory(data.FileName);
                history.Status = "Processing";
                _importHistoryModel.Update(history);

                IList<ExcelData> excelDatas = new List<ExcelData>();
                FileInfo file = new FileInfo(data.FileName);
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    foreach (var worksheet in package.Workbook.Worksheets)
                    {
                        if (worksheet == null)
                        {
                            _logger.LogInformation("Empty File");
                        }
                        else
                        {
                            var rowCount = worksheet.Dimension.Rows;
                            var colCount = worksheet.Dimension.Columns;
                            totalData = totalData + rowCount - 1;
                            for (int i = 1; i < rowCount; i++)
                            {
                                for (int j = 1; j <= colCount; j++)
                                {
                                    var excelData = new ExcelData();
                                    excelData.FieldName = (worksheet.Cells[1, j].Value ?? string.Empty).ToString().Trim();
                                    excelData.FieldValue = (worksheet.Cells[i+1, j].Value ?? string.Empty).ToString().Trim();
                                    excelDatas.Add(excelData);
                                }
                            }
                        }
                    }
                }
                var excelrecord = new ExcelRecord
                {
                    DateCreated = _dateTimeUtility.Now,
                    GroupsId = data.GroupId,
                    ApplicationUserId = data.UserId,
                    ExcelDatas = excelDatas
                };

                _insertExcelRecordModel.Create(excelrecord);
                history.TotalData = totalData.ToString();
                history.Status = "Completed";
                _importHistoryModel.Update(history);
                _excelFileService.DeleteExcelFile(data.Id);
                file.Delete();
                _logger.LogInformation("Excle File Record Inserted");
            }
        }
    }
}
