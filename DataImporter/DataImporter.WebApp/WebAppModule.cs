using Autofac;
using DataImporter.WebApp.Models.Application;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.WebApp
{
    public class WebAppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateGroupModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<GroupListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UserModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UploadFileModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CancelFileUploadModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DeleteGroupModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<UploadConfirmationModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ContactstListModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExportGroupModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ImportHistoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<EditGroupModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryModel>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DashboardModel>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
