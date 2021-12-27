using DataImporter.Data;
using DataImporter.Logic.Contexts;
using DataImporter.Logic.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.UnitOfWorks
{
    public class DataImporterUnitOfWrok : UnitOfWork, IDataImporterUnitOfWrok
    {
        public IGroupsRepository Groups { get; set; }
        public IExcelRecordRepository ExcelRecords { get; set; }
        public IExcelDataRepository ExcelDatas { get; set; }
        public IExcelFileRepository ExcelFiles { get; set; }
        public ITemporaryDataRepository TemporaryData { get; set; }
        public IImportHistoryRepository  ImportHistory { get; set; }
        public IExportHistoryRepository ExportHistory { get; set; }
        public DataImporterUnitOfWrok(IDataImporterContext context, IGroupsRepository groups, IExcelRecordRepository excelRecord, 
            IExcelDataRepository excelData, IExcelFileRepository excelFile, ITemporaryDataRepository temporaryData, IImportHistoryRepository  importHistory,
            IExportHistoryRepository exportHistory)
            : base((DbContext)context)
        {
            Groups = groups;
            ExcelRecords = excelRecord;
            ExcelDatas = excelData;
            ExcelFiles = excelFile;
            TemporaryData = temporaryData;
            ImportHistory = importHistory;
            ExportHistory = exportHistory;
        }
    }
}
