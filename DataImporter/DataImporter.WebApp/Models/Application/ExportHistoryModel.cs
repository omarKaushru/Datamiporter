using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class ExportHistoryModel
    {
        private IExportHistoryService _exportHistoryService;
        ILifetimeScope _scope;
        public ExportHistoryModel() { }
        public ExportHistoryModel(IExportHistoryService exportHistoryService)
        {
            _exportHistoryService = exportHistoryService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _exportHistoryService = _scope.Resolve<IExportHistoryService>();
        }
        internal object GetAll(Guid userId, DataTablesAjaxRequestModel tableModel)
        {
            var data = _exportHistoryService.GetExportHistories(userId,
                tableModel.PageIndex,
                tableModel.PageSize);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                           record.GroupName,
                           record.CreateDate.ToString(),
                           record.MailingAddress,
                           record.Status
                        }
                    ).ToArray()
            };
        }
    }
}
