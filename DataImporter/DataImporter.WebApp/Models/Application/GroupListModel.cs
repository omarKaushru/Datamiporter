using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class GroupListModel
    {
        private IGroupsService _groupsService;
        private IExcelRecordService _excelRecordService;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        public GroupListModel()
        {
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupsService = _scope.Resolve<IGroupsService>();
            _mapper = _scope.Resolve<IMapper>();
            _excelRecordService = _scope.Resolve<IExcelRecordService>();
        }

        public GroupListModel(IGroupsService groupsService, IMapper mapper, IExcelRecordService excelRecordService)
        {
            _groupsService = groupsService;
            _mapper = mapper;
            _excelRecordService = excelRecordService;
        }

        public IList<Groups> GetAll(Guid userId)
        {
            var groupsData = _groupsService.GetGroups(userId);
            return groupsData;
        }

        public IList<Groups> GetAllThatHasData(Guid userId)
        {
            var excleRecord = _excelRecordService.HasExcelRecord(userId);
            var groupsData = _groupsService.GetGroups(userId);
            var grourplist = new List<Groups>();
            for (int i = 0; i < groupsData.Count; i++)
            {
                for (int j = 0; j < excleRecord.Count; j++)
                {
                    if (groupsData[i].Id == excleRecord[j].GroupsId)
                    {
                        grourplist.Add(groupsData[i]);
                    }
                }
            }
            return grourplist;
        }
        public object GetAll(Guid userId, DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = _groupsService.GetGroups(userId, dataTablesModel.PageIndex, dataTablesModel.PageSize);
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                           record.Name,
                           record.DateCreated.ToString(),
                           record.Id.ToString(),
                        }
                    ).ToArray()
            };
        }

        public IList<Groups> GetAllWithoutExcelRecordRecord(Guid userId)
        {
            var excleRecord = _excelRecordService.HasExcelRecord(userId);
            var groupsData = _groupsService.GetGroups(userId);
            if (excleRecord == null)
                return groupsData;
            for(int i =0;i< excleRecord.Count; i++)
            {
                for(int j=0;j<groupsData.Count; j++ )
                {
                    if (groupsData[j].Id == excleRecord[i].GroupsId)
                        groupsData.Remove(groupsData[j]);
                }
            }
            return groupsData;
        }
    }
}
