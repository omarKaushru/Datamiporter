using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{ 
    public class ImportHistoryModel
    {
        private ILifetimeScope _scope;
        private IImportHistoryService  _importHistoryService;
        public ImportHistoryModel()
        { }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _importHistoryService = _scope.Resolve<IImportHistoryService>();
        }
        public ImportHistoryModel(IImportHistoryService  importHistoryService)
        {
            _importHistoryService = importHistoryService;
        }

        internal object GetAll(Guid userId, DataTablesAjaxRequestModel dataTablesModel)
        {
            var data = _importHistoryService.GetImortHistories(userId,
                dataTablesModel.PageIndex,
                dataTablesModel.PageSize);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                           record.GroupName,
                           record.DateCreated.ToString(),
                           record.TotalData,
                           record.Status
                        }
                    ).ToArray()
            };
        }
    }
}
