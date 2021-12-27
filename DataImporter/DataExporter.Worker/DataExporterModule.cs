using Autofac;
using DataExporter.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExporter.Worker
{
    public class DataExporterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataExporter>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DataExporterModel>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
