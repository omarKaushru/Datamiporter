using Autofac;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class DashboardModel
    {
        public int TotalGroups = 0;
        public int TotalImported = 0;
        public int TotalExported = 0;
        private IExportHistoryService _exportHistoryService;
        private IImportHistoryService _importHistoryService;
        private IGroupsService _groupsService;
        private ILifetimeScope _scope;
        public DashboardModel() { }
        public DashboardModel(IExportHistoryService exportHistoryService, IImportHistoryService importHistoryService, IGroupsService groupsService)
        {
            _exportHistoryService = exportHistoryService;
            _groupsService = groupsService;
            _importHistoryService = importHistoryService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupsService = _scope.Resolve<IGroupsService>();
            _importHistoryService = _scope.Resolve<IImportHistoryService>();
            _exportHistoryService = _scope.Resolve<IExportHistoryService>();
        }
        public void Get(Guid userId)
        {
            TotalGroups = _groupsService.TotalGroup(userId);
            TotalExported = _exportHistoryService.TotalExported(userId);
            TotalImported = _importHistoryService.TotalImported(userId);
        }
    }
}
