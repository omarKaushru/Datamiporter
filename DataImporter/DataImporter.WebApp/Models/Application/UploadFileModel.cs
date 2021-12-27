using Autofac;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class UploadFileModel
    {
        private ILifetimeScope _scope;
        private ITemporaryDataService _temporaryDataService;
        public Guid GroupId { get; set; }
        public IFormFile iFile { get; set; }

        public UploadFileModel()
        {

        }
        public UploadFileModel(ITemporaryDataService temporaryDataService)
        {
            _temporaryDataService = temporaryDataService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _temporaryDataService = _scope.Resolve<ITemporaryDataService>();
        }
        internal void Read(Guid userId, string rootFolder)
        {
            string fileName = Guid.NewGuid().ToString() + iFile.FileName;
            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            
            using (var stream = new MemoryStream())
            {
                iFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    package.SaveAs(file);
                }
            }
            var temporaryData = new TemporaryData
            {
                GroupId = GroupId,
                UserId = userId,
                FileName = fileName
            };
            _temporaryDataService.Create(temporaryData);
        }
    }
}
