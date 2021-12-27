using Autofac;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class UploadConfirmationModel
    {
        public IList<string> FieldName = new List<string>();
        public IList<string> FieldValue = new List<string>();
        private ITemporaryDataService _temporaryDataService;
        private IExcelFileService _excelFileService;
        private ILifetimeScope _scope;
        IGroupsService _groupsService;
        private IImportHistoryService _importHistoryService;
        public FileInfo FileInfo { get; set; }
        public Guid GroupId = new Guid();
        public Guid TempDataId = new Guid();
        public UploadConfirmationModel()
        {

        }
        public UploadConfirmationModel(ITemporaryDataService temporaryDataService, IExcelFileService excelFileService, IGroupsService groupsService, 
            IImportHistoryService importHistoryService)
        {
            _temporaryDataService = temporaryDataService;
            _excelFileService = excelFileService;
            _groupsService = groupsService;
            _importHistoryService = importHistoryService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _temporaryDataService = _scope.Resolve<ITemporaryDataService>();
            _excelFileService = _scope.Resolve<IExcelFileService>();
            _groupsService = _scope.Resolve<IGroupsService>();
            _importHistoryService = _scope.Resolve<IImportHistoryService>();
        }

        internal void Read(Guid userId, string rootFolder)
        {
            var data = _temporaryDataService.Get(userId);
            GroupId = data.GroupId;
            TempDataId = data.Id;
            FileInfo file = new FileInfo(Path.Combine(rootFolder, data.FileName));
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {

                }
                else
                {
                    var rowCount = worksheet.Dimension.Rows;
                    var colCount = worksheet.Dimension.Columns;
                    if (rowCount > 6)
                        rowCount = 6;

                    for (int row = 1; row <= 1; row++)
                    {
                        for (int col = 1; col <= colCount; col++)
                        {
                            FieldName.Add((worksheet.Cells[row, col].Value ?? string.Empty).ToString().Trim());
                        }
                    }
                    for (int i = 1; i < rowCount; i++)
                    {
                        for (int j = 1; j <= colCount; j++)
                        {
                            FieldValue.Add((worksheet.Cells[i + 1, j].Value ?? string.Empty).ToString().Trim());
                        }
                    }
                }
            }
        }

        internal void Save(Guid userId, string rootFolder)
        {
            var data = _temporaryDataService.Get(userId);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, data.FileName));
            var excleFile = new ExcelFile
            {
                GroupId = data.GroupId,
                FileName = file.ToString(),
                UserId = userId
            };
            var groupData = _groupsService.GetGroup(data.GroupId);
            var history = new ImportHistory
            {
                UserId = userId,
                FileName = file.ToString(),
                GroupName = groupData.Name,
                Status = "Pending",
                TotalData = "Unknown",
            };
            _importHistoryService.Create(history);
            _excelFileService.Create(excleFile);
            _temporaryDataService.Delete(data.Id);
        }
    }
}
