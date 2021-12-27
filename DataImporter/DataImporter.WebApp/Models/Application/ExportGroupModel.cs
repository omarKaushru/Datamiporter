using Autofac;
using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class ExportGroupModel
    {
        private ILifetimeScope _scope;
        private IExportHistoryService _exportHistoryService;
        private IGroupsService _groupsService;
        private IExcelRecordService _excelRecordService;
        public ExportGroupModel() { }
        public ExportGroupModel(IExportHistoryService exportHistoryService, IGroupsService groupsService, IExcelRecordService excelRecordService)
        {
            _exportHistoryService = exportHistoryService;
            _groupsService = groupsService;
            _excelRecordService = excelRecordService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _exportHistoryService = _scope.Resolve<IExportHistoryService>();
            _groupsService = _scope.Resolve<IGroupsService>();
            _excelRecordService = _scope.Resolve<IExcelRecordService>();
        }
        public void Export(Guid groupId, Guid userId,string mailingAddress, string fileInfo)
        {
            var excelRecod = _excelRecordService.GetGroupExcleRecord(groupId);
            if(excelRecod!=null)
            {
                var groupName = _groupsService.GetGroup(groupId).Name;
                var exportGroup = new ExportHistory
                {
                    GroupId = groupId,
                    UserId = userId,
                    GroupName = groupName,
                    MailingAddress = mailingAddress,
                    Status = "Pending",
                    FileName = fileInfo
                };
                _exportHistoryService.Create(exportGroup);
            }
        }
    }
}
