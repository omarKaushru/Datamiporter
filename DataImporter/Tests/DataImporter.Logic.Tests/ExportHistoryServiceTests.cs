using Autofac.Extras.Moq;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Repositories;
using DataImporter.Logic.Services;
using DataImporter.Logic.UnitOfWorks;
using EO = DataImporter.Logic.Entities;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DataImporter.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class ExportHistoryServiceTests
    {
        private AutoMock _mock;
        private Mock<IDataImporterUnitOfWrok> _dataImporterUnitOfWrokMock;
        private Mock<IExportHistoryRepository> _exportHistoryRepositoryMock;
        private IExportHistoryService _exportHistoryServiceMock;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _dataImporterUnitOfWrokMock = _mock.Mock<IDataImporterUnitOfWrok>();
            _exportHistoryRepositoryMock = _mock.Mock<IExportHistoryRepository>();
            _exportHistoryServiceMock = _mock.Create<ExportHistoryService>();
        }
        [Test]
        public void Create_ExporttHistory_Is_Null_Throw_Exceptions()
        {
            var exportHistory = new ExportHistory();
            
            _dataImporterUnitOfWrokMock.Setup(x => x.ExportHistory).Returns(_exportHistoryRepositoryMock.Object);

            Should.Throw<InvalidOperationException>(
                   () => _exportHistoryServiceMock.Create(exportHistory)
               );
        }
        [Test]
        public void GetExportHistory()
        {
            var fileName = "Test11.xls";
            var userid = new Guid("366a12c7-669c-4c88-54ba-08d981e356ce");
            var id = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            var exportHistoryEnity = new EO.ExportHistory
            {
                FileName = fileName,
                Status = "Pending",
                GroupName = "Test11",
                Id = id,
                UserId = userid
            };

            var exporttHistoryEntities = new List<EO.ExportHistory>
            {
                new EO.ExportHistory{ Id = id,  FileName = "Test11.xls", Status = "Pending",  GroupName = "Test11", UserId = userid},
                new EO.ExportHistory{ Id = new Guid("336a12c7-669c-4c88-54ba-08d981e356ce"),  FileName = "Test12.xls", Status = "Completed",
                     GroupName = "Test1", UserId = new Guid("466a12c7-669c-4c88-54ba-08d981e356ce")},
                new EO.ExportHistory{ Id = new Guid("456a12c7-669c-4c88-54ba-08d981e356ae"),  FileName = "Test13.xls", Status = "Completed",
                     GroupName = "Test2",UserId = new Guid("576a12c7-669c-4c88-54ba-08d981e356ce")}

            };

            _dataImporterUnitOfWrokMock.Setup(x => x.ExportHistory).Returns(_exportHistoryRepositoryMock.Object);
            
            _exportHistoryRepositoryMock.Setup(x => x.Get(It.Is<Expression<Func<EO.ExportHistory, bool>>>(y => y.Compile()(exportHistoryEnity)), string.Empty))
                 .Returns(exporttHistoryEntities).Verifiable();

            _exportHistoryServiceMock.GetExportHistory("Pending");

            this.ShouldSatisfyAllConditions(
               () => _dataImporterUnitOfWrokMock.VerifyAll(),
               () => _exportHistoryRepositoryMock.VerifyAll()
           );
        }
    }
}
