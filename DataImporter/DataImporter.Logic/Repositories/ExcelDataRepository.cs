using DataImporter.Data;
using DataImporter.Logic.Contexts;
using DataImporter.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataImporter.Logic.Repositories
{
    public class ExcelDataRepository : Repository<ExcelData, Guid>, IExcelDataRepository
    {
        public ExcelDataRepository(IDataImporterContext context)
            : base((DbContext)context) { }
    }
}
