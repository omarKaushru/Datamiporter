using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.Model
{
    public class InsertExcelRecordModel
    {
        private readonly IExcelRecordService _excelRecordService;
        public InsertExcelRecordModel(IExcelRecordService excelRecordService)
        { 
            _excelRecordService = excelRecordService;
        }
        public void Create(ExcelRecord excelRecord)
        {
            _excelRecordService.CreateExcelRecord(excelRecord);
        }
    }
}
