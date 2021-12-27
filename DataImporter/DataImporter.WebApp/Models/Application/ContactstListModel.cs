using Autofac;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class ContactstListModel
    {
        private IExcelRecordService _excelRecordService;
        private ILifetimeScope _scope;
        public Guid GroupId { get; set; }
        public IList<string> FieldName = new List<string>();
        public IList<string> FieldValue = new List<string>();
        public int Index = 0;
        public ContactstListModel() { }
        public ContactstListModel(IExcelRecordService excelRecordService)
        {
            _excelRecordService = excelRecordService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _excelRecordService = _scope.Resolve<IExcelRecordService>();
        }
        public void Get()
        {
            var excelRecord = _excelRecordService.GetExcleRecordData(GroupId);
            foreach (var item in excelRecord.ExcelDatas)
            {
                if (!FieldName.Contains(item.FieldName))
                    FieldName.Add(item.FieldName);
                FieldValue.Add(item.FieldValue);
            }
        }
    }
}
