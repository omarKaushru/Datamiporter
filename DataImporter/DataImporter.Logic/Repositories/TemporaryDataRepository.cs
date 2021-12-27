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
    public class TemporaryDataRepository : Repository<TemporaryData, Guid>, ITemporaryDataRepository
    {
        public TemporaryDataRepository(IDataImporterContext context)
           : base((DbContext)context) { }
    }
}
