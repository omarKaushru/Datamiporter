using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public class ExportHistoryService : IExportHistoryService
    {
        private readonly IDataImporterUnitOfWrok _dataImporterUnitOfWrok;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;
        public ExportHistoryService(IDataImporterUnitOfWrok dataImporterUnitOfWrok, IMapper mapper, 
            IDateTimeUtility dateTimeUtility)
        {
            _dataImporterUnitOfWrok = dataImporterUnitOfWrok;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
        }
        public void Create(ExportHistory exportHistory)
        {
            if (exportHistory.GroupName != null || exportHistory.FileName != null)
            {
                exportHistory.CreateDate = _dateTimeUtility.Now;
                _dataImporterUnitOfWrok.ExportHistory.Add(
                    _mapper.Map(exportHistory, new Entities.ExportHistory())
                    );
                _dataImporterUnitOfWrok.Save();
            }
            else
                throw new InvalidOperationException("No Exporthistory Provided!");
        }
        public (IList<ExportHistory> records, int total, int totalDisplay) GetExportHistories(Guid userId, int pageIndex, int pageSize)
        {
            var historyData = _dataImporterUnitOfWrok.ExportHistory.Get(x => x.UserId == userId, s => s.OrderBy(g => g.GroupName).ThenByDescending(p => p.CreateDate), 
                string.Empty, pageIndex, pageSize);
            var resultData = (from history in historyData.data
                              select _mapper.Map<ExportHistory>(history)).ToList();

            return (resultData, resultData.Count, historyData.totalDisplay);
        }
        public ExportHistory GetExportHistory(string status)
        {
            var exportHistory = _dataImporterUnitOfWrok.ExportHistory.Get(x => x.Status == status, string.Empty).FirstOrDefault();
            return _mapper.Map<ExportHistory>(exportHistory);
        }
        public int TotalExported(Guid userId)
        {
            return _dataImporterUnitOfWrok.ExportHistory.Get(x => x.UserId == userId, string.Empty).Count();
        }
        public void Update(ExportHistory exportHistory)
        {
            var exportHistoryEnity = _dataImporterUnitOfWrok.ExportHistory.GetById(exportHistory.Id);
            if(exportHistory!=null)
            {
                _mapper.Map(exportHistory, exportHistoryEnity);
                _dataImporterUnitOfWrok.Save();
            }
        }
    }
}
