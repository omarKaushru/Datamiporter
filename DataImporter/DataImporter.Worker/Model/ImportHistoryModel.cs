using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.Model
{
    public class  ImportHistoryModel
    {
        private readonly IImportHistoryService  _importHistoryService;
        public ImportHistoryModel(IImportHistoryService importHistoryService)
        {
            _importHistoryService = importHistoryService;
        }
        public ImportHistory GetHistory(string fileName)
        {
            var history = _importHistoryService.GetHistory(fileName);
            return history;
        }
        public void Update(ImportHistory history)
        {
            _importHistoryService.Update(history);
        }
    }
}
