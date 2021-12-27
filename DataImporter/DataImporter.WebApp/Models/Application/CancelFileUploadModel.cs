using Autofac;
using DataImporter.Logic.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class CancelFileUploadModel
    {
        private ITemporaryDataService _temporaryDataService;
        private ILifetimeScope _scope;
        public CancelFileUploadModel()
        { }
        public CancelFileUploadModel(ITemporaryDataService temporaryDataService)
        {
            _temporaryDataService = temporaryDataService;
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _temporaryDataService = _scope.Resolve<ITemporaryDataService>();
        }

        public void Cancel(Guid userId, string rootFolder)
        {
            var data = _temporaryDataService.Get(userId);
            FileInfo file = new FileInfo(Path.Combine(rootFolder, data.FileName));
            file.Delete();
            _temporaryDataService.Delete(data.Id);
        }
    }
}
