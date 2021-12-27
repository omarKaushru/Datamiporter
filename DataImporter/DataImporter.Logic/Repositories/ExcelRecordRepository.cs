using DataImporter.Data;
using DataImporter.Logic.Contexts;
using DataImporter.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataImporter.Logic.Repositories
{
    public class ExcelRecordRepository : Repository<ExcelRecord, Guid>, IExcelRecordRepository
    {
        public ExcelRecordRepository(IDataImporterContext context)
            : base((DbContext)context) { }
    }
}
