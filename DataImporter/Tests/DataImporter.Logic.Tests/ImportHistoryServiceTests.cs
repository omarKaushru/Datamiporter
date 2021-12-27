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
    public class ImportHistoryServiceTests
    {
        private AutoMock _mock;
        private Mock<IDataImporterUnitOfWrok> _dataImporterUnitOfWrokMock;
        private Mock<IImportHistoryRepository> _importHistoryRepositoryMock;
        private IImportHistoryService _importHistoryServiceMock;

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
            _importHistoryRepositoryMock = _mock.Mock<IImportHistoryRepository>();
            _importHistoryServiceMock = _mock.Create<ImportHistoryService>();
        }
        [Test]
        public void Create_ImportHistory_Is_Null_Throw_Exceptions()
        {
            var importHistory = new ImportHistory();

            _dataImporterUnitOfWrokMock.Setup(x => x.ImportHistory).Returns(_importHistoryRepositoryMock.Object);

            Should.Throw<InvalidOperationException>(
                   () => _importHistoryServiceMock.Create(importHistory)
               );
        }
        [Test]
        public void GetImortHistory()
        {
            var fileName = "Test11.xls";
            var userid = new Guid("366a12c7-669c-4c88-54ba-08d981e356ce");
            var id = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            var importHistoryEnity = new EO.ImportHistory
            {
                FileName = fileName,
                Status = "Pending",
                TotalData = "Unknown",
                GroupName = "Test11",
                Id = id,
                UserId = userid
            };

            var importHistoryEntities = new List<EO.ImportHistory>
            {
                new EO.ImportHistory{ Id = id,  FileName = "Test11.xls", Status = "Pending", TotalData = "Unknown", GroupName = "Test11", UserId = userid},
                new EO.ImportHistory{ Id = new Guid("336a12c7-669c-4c88-54ba-08d981e356ce"),  FileName = "Test12.xls", Status = "Completed", 
                    TotalData = "Unknown", GroupName = "Test1", UserId = new Guid("466a12c7-669c-4c88-54ba-08d981e356ce")},
                new EO.ImportHistory{ Id = new Guid("456a12c7-669c-4c88-54ba-08d981e356ae"),  FileName = "Test13.xls", Status = "Completed", 
                    TotalData = "Unknown", GroupName = "Test2",UserId = new Guid("576a12c7-669c-4c88-54ba-08d981e356ce")}

            };

            _dataImporterUnitOfWrokMock.Setup(x => x.ImportHistory)
                .Returns(_importHistoryRepositoryMock.Object);
            _importHistoryRepositoryMock.Setup(x => x.Get(
                 It.Is<Expression<Func<EO.ImportHistory, bool>>>(y => y.Compile()(importHistoryEnity)), string.Empty))
                 .Returns(importHistoryEntities).Verifiable();
           
            _importHistoryServiceMock.GetHistory(fileName);

            this.ShouldSatisfyAllConditions(
               () => _dataImporterUnitOfWrokMock.VerifyAll(),
               () => _importHistoryRepositoryMock.VerifyAll()
           );
        }
    }
}
