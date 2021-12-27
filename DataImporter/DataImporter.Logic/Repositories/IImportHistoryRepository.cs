using DataImporter.Data;
using DataImporter.Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Repositories
{
    public interface IImportHistoryRepository : IRepository<ImportHistory, Guid>
    {
    }
}
