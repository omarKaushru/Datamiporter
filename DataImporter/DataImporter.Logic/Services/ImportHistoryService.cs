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
    public class ImportHistoryService : IImportHistoryService
    {
        private readonly IDataImporterUnitOfWrok _dataImporterUnitOfWrok;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTimeUtility;
        public ImportHistoryService(IDataImporterUnitOfWrok dataImporterUnitOfWrok, IMapper mapper, IDateTimeUtility dateTimeUtility)
        {
            _dataImporterUnitOfWrok = dataImporterUnitOfWrok;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
        }
        public void Create(ImportHistory history)
        {
            if (history.FileName != null && history.GroupName != null)
            {
                history.DateCreated = _dateTimeUtility.Now;
                _dataImporterUnitOfWrok.ImportHistory.Add(
                    _mapper.Map(history, new Entities.ImportHistory())
                    );
                _dataImporterUnitOfWrok.Save();
            }
            else throw new InvalidOperationException("ImportHistory is not provided.");
        }

        public ImportHistory GetHistory(string fileName)
        {
            var hitory = _dataImporterUnitOfWrok.ImportHistory.Get(x => x.FileName == fileName, string.Empty).FirstOrDefault();
            return _mapper.Map<ImportHistory>(hitory);
        }

        public (IList<ImportHistory> records, int total, int totalDisplay) GetImortHistories(Guid userId, int pageIndex, int pageSize)
        {
            var exportHistoryData = _dataImporterUnitOfWrok.ImportHistory.Get(x => x.UserId == userId, s => s.OrderBy(g => g.GroupName).ThenByDescending(p => p.DateCreated),
               string.Empty, pageIndex, pageSize);
            var resultData = (from history in exportHistoryData.data
                              select _mapper.Map<ImportHistory>(history)).ToList();

            return (resultData, resultData.Count, exportHistoryData.totalDisplay);
        }

        public int TotalImported(Guid userId)
        {
            return _dataImporterUnitOfWrok.ImportHistory.Get(x => x.UserId == userId, string.Empty).Count();
        }

        public void Update(ImportHistory history)
        {
            var historyEntity = _dataImporterUnitOfWrok.ImportHistory.GetById(history.Id);
            if (historyEntity != null)
            {
                _mapper.Map(history, historyEntity);
                _dataImporterUnitOfWrok.Save();
            }
        }
    }
}
