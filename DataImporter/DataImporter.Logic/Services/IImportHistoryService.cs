using DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public interface IImportHistoryService
    {
        void Create(ImportHistory history);
        void Update(ImportHistory history);
        ImportHistory GetHistory(string fileName);
        int TotalImported(Guid userId);
        (IList<ImportHistory> records, int total, int totalDisplay) GetImortHistories(Guid userId, int pageIndex, int pageSize);
    }
}
