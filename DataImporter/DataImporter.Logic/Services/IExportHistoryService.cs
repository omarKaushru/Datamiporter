using DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public interface IExportHistoryService 
    {
        void Create(ExportHistory exportHistory);
        void Update(ExportHistory exportHistory);
        ExportHistory GetExportHistory(string status);
        (IList<ExportHistory> records, int total, int totalDisplay) GetExportHistories(Guid userId, int pageIndex, int pageSize);
        int TotalExported(Guid userId);
    }
}
