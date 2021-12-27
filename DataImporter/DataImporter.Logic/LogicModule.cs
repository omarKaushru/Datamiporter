using Autofac;
using DataImporter.Logic.Contexts;
using DataImporter.Logic.Repositories;
using DataImporter.Logic.Services;
using DataImporter.Logic.UnitOfWorks;
using System;

namespace DataImporter.Logic
{
    public class LogicModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public LogicModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataImporterContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<DataImporterContext>().As<IDataImporterContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupsRepository>().As<IGroupsRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExcelRecordRepository>().As<IExcelRecordRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataRepository>().As<IExcelDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExcelFileRepository>().As<IExcelFileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TemporaryDataRepository>().As<ITemporaryDataRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ImportHistoryRepository>().As<IImportHistoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryRepository>().As<IExportHistoryRepository>().InstancePerLifetimeScope();

            builder.RegisterType<DataImporterUnitOfWrok>().As<IDataImporterUnitOfWrok>().InstancePerLifetimeScope();

            builder.RegisterType<GroupsService>().As<IGroupsService>().InstancePerLifetimeScope();
            builder.RegisterType<ExcelRecordService>().As<IExcelRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<ExcelFileService>().As<IExcelFileService>().InstancePerLifetimeScope();
            builder.RegisterType<TemporaryDataService>().As<ITemporaryDataService>().InstancePerLifetimeScope();
            builder.RegisterType<ImportHistoryService>().As<IImportHistoryService>().InstancePerLifetimeScope();
            builder.RegisterType<ExportHistoryService>().As<IExportHistoryService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
