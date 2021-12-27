using Autofac;
using DataImporter.Worker.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker
{
    public class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InsertExcelRecordModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataImporter>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ImportHistoryModel>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
