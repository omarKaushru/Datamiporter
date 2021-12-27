using Autofac;
using FileMailer.Worker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMailer.Worker
{
    public class FileMailerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ExportHistoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<FileMailer>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
