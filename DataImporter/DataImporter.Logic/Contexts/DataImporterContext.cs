using DataImporter.Logic.Entities;
using DataImporter.Users.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Contexts
{
    public class DataImporterContext : DbContext, IDataImporterContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public DataImporterContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers", t => t.ExcludeFromMigrations())
                .HasMany<Groups>()
                .WithOne(t => t.ApplicationUser);

            // one to many relationship
            modelBuilder.Entity<ExcelRecord>()
                .HasMany(c => c.ExcelDatas)
                .WithOne(t => t.ExcelRecord);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<ExcelRecord> ExcelRecords { get; set; }
        public DbSet<ExcelData> ExcelDatas { get; set; }
        public DbSet<ExcelFile> ExcelFiles { get; set; }
        public DbSet<TemporaryData> TemporaryDatas { get; set; }
        public DbSet<ImportHistory>  ImportHistories { get; set; }
        public DbSet<ExportHistory>  ExportHistories { get; set; }
    }
}

