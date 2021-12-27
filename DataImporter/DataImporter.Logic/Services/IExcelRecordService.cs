using DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public interface IExcelRecordService
    {
        void CreateExcelRecord(ExcelRecord excelRecord);
        void DeleteRecrod(Guid groupId);
        ExcelRecord GetGroupExcleRecord(Guid groupId);
        ExcelRecord GetExcleRecordData(Guid groupId);
        IList<ExcelRecord> HasExcelRecord(Guid userId)  ;
    }
}
