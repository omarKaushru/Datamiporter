using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public class ExcelRecordService : IExcelRecordService
    {
        private readonly IDataImporterUnitOfWrok _dataImporterUnitOfWrok;
        private readonly IMapper _mapper;
        public ExcelRecordService(IDataImporterUnitOfWrok dataImporterUnitOfWrok, IMapper mapper)
        {
            _dataImporterUnitOfWrok = dataImporterUnitOfWrok;
            _mapper = mapper;
        }
        public void CreateExcelRecord(ExcelRecord excelRecord)
        {
            _dataImporterUnitOfWrok.ExcelRecords.Add(
                
                _mapper.Map(excelRecord, new Entities.ExcelRecord())
                );
            _dataImporterUnitOfWrok.Save();
        }

        public void DeleteRecrod(Guid groupId)
        {
            var record = GetGroupRecord(groupId);
            if (record!=null)
            {
                _dataImporterUnitOfWrok.ExcelRecords.Remove(record.Id);
            }
        }

        public ExcelRecord GetExcleRecordData(Guid groupId)
        {
            var record = _dataImporterUnitOfWrok.ExcelRecords.Get(x => x.GroupsId == groupId, string.Empty).FirstOrDefault();
            if (record == null)
                throw new InvalidOperationException("No Records found for this group.");
            var excelDatas = _dataImporterUnitOfWrok.ExcelDatas.Get(x => x.ExcelRecordId == record.Id, string.Empty);
            record.ExcelDatas = excelDatas;
            return _mapper.Map<ExcelRecord>(record);
        }

        public ExcelRecord GetGroupExcleRecord(Guid groupId)
        {
            var record = GetGroupRecord(groupId);
            if (record == null)
                throw new InvalidOperationException("No Records found for this group.");
            return _mapper.Map<ExcelRecord>(record);
        }

        public IList<ExcelRecord> HasExcelRecord(Guid userId)
        {
            var execlRecordEnitities = _dataImporterUnitOfWrok.ExcelRecords.Get(x => x.ApplicationUserId == userId, string.Empty);
            var excelRecords = new List<ExcelRecord>();

            foreach (var entity in execlRecordEnitities)
            {
                excelRecords.Add(_mapper.Map<ExcelRecord>(entity));
            }
            return excelRecords;
        }

        private ExcelRecord GetGroupRecord(Guid groupId)
        {
            var record = _dataImporterUnitOfWrok.ExcelRecords.Get(x => x.GroupsId == groupId, string.Empty).FirstOrDefault();
            return _mapper.Map<ExcelRecord>(record);
        }
    }
}
