using DataImporter.Data;
using DataImporter.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.UnitOfWorks
{
    public interface IDataImporterUnitOfWrok : IUnitOfWork
    {
        public IGroupsRepository Groups { get; }
        public IExcelRecordRepository ExcelRecords { get; }
        public IExcelDataRepository ExcelDatas { get; }
        public IExcelFileRepository ExcelFiles { get; }
        public ITemporaryDataRepository TemporaryData { get; }
        public IImportHistoryRepository  ImportHistory { get; }
        IExportHistoryRepository ExportHistory { get; }
    }
}
