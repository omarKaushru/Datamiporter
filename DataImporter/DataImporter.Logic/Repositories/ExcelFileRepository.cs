using DataImporter.Data;
using DataImporter.Logic.Contexts;
using DataImporter.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Repositories
{
    public class ExcelFileRepository : Repository<ExcelFile, Guid>, IExcelFileRepository
    {
        public ExcelFileRepository(IDataImporterContext context)
            : base((DbContext)context) { }
    }
}
