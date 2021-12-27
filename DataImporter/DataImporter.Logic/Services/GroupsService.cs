using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Exceptions;
using DataImporter.Logic.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly IDataImporterUnitOfWrok  _dataImporterUnitOfWrok;
        private readonly IMapper _mapper;
        private readonly IExcelRecordService _excelRecordService;
        private readonly IDateTimeUtility _dateTimeUtility;
        public GroupsService(IDataImporterUnitOfWrok dataImporterUnitOfWrok, IMapper mapper, 
            IExcelRecordService excelRecordService, IDateTimeUtility  dateTimeUtility)
        {
            _dataImporterUnitOfWrok = dataImporterUnitOfWrok;
            _mapper = mapper;
            _excelRecordService = excelRecordService;
            _dateTimeUtility = dateTimeUtility;
        }
        public void CreateGroup(Groups groups)
        {
            if (groups.Name == null)
                throw new InvalidOperationException("Groups not provided");
            if (IsAlreadyExist(groups.Name, groups.ApplicationUserId))
                throw new DuplicateValueException("Group Already Created");
            groups.DateCreated = _dateTimeUtility.Now;
            _dataImporterUnitOfWrok.Groups.Add(
                _mapper.Map(groups, new Entities.Groups())
                );
            _dataImporterUnitOfWrok.Save();
        }

        public void DeleteGroup(Guid id)
        {
            _dataImporterUnitOfWrok.Groups.Remove(id);
            _excelRecordService.DeleteRecrod(id);
            _dataImporterUnitOfWrok.Save();
        }

        public Groups GetGroup(Guid groupId)
        {
            var groupData = _dataImporterUnitOfWrok.Groups.GetById(groupId);
            return _mapper.Map<Groups>(groupData);
        }

        public IList<Groups> GetGroups(Guid userId)
        {
            var groupEntities = _dataImporterUnitOfWrok.Groups.Get(x => x.ApplicationUserId == userId, string.Empty);
          
            var groups = new List<Groups>();

            foreach (var entity in groupEntities)
            {
                var group = _mapper.Map<Groups>(entity);
                groups.Add(group);
            }

            return groups;
        }

        public (IList<Groups> records, int total, int totalDisplay) GetGroups(Guid userId, int pageIndex, int pageSize)
        {
            var groupdata = _dataImporterUnitOfWrok.Groups.Get(
              x => x.ApplicationUserId == userId, s=>s.OrderBy(g=>g.Name).ThenByDescending(p=>p.DateCreated), string.Empty, pageIndex, pageSize);
            var resultData = (from groups in groupdata.data
                              select _mapper.Map<Groups>(groups)).ToList();

            return (resultData, resultData.Count, groupdata.totalDisplay);
        }

        public int TotalGroup(Guid userId)
        {
            return _dataImporterUnitOfWrok.Groups.Get(x => x.ApplicationUserId == userId, string.Empty).Count();
        }

        public void UpdateGroup(Groups groups)
        {
            if(groups.Name==null)
                throw new InvalidOperationException("Groups not provided");
            if (IsAlreadyExist(groups.Name, groups.ApplicationUserId))
                throw new DuplicateValueException("Group Already Created");

            var groupEntity = _dataImporterUnitOfWrok.Groups.GetById(groups.Id);
            if (groupEntity != null)
            {
                groups.DateCreated = groupEntity.DateCreated;
                _mapper.Map(groups, groupEntity);
                _dataImporterUnitOfWrok.Save();
            }
        }
        private bool IsAlreadyExist(string Name, Guid userId) =>
            _dataImporterUnitOfWrok.Groups.GetCount(x => x.Name == Name && x.ApplicationUserId == userId) > 0;
    }
}
