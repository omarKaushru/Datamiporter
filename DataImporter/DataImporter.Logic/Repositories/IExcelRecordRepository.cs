using DataImporter.Data;
using DataImporter.Logic.Entities;
using System;

namespace DataImporter.Logic.Repositories
{
    public interface IExcelRecordRepository : IRepository<ExcelRecord, Guid>
    {
    }
}
