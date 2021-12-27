using Autofac.Extras.Moq;
using DataImporter.Logic.Services;
using DataImporter.WebApp.Models.Application;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Tests
{
    [ExcludeFromCodeCoverage]
    public class DashboardModelTests
    {
        private AutoMock _mock;
        private Mock<IGroupsService> _groupServiceMock;
        private Mock<IExportHistoryService> _exportHistorySeriveMock;
        private Mock<IImportHistoryService> _importHistoryMock;
        private DashboardModel _model;
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
            _groupServiceMock = _mock.Mock<IGroupsService>();
            _exportHistorySeriveMock = _mock.Mock<IExportHistoryService>();
            _importHistoryMock = _mock.Mock<IImportHistoryService>();
            _model = _mock.Create<DashboardModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupServiceMock?.Reset();
            _exportHistorySeriveMock?.Reset();
            _importHistoryMock?.Reset();
        }
        [Test]
        public void Get_totlaGroup_totalExport_totalImport()
        {
            var userid = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            int TotalGroups = 5;
            int TotalImported = 3;
            int TotalExported = 4;
            _groupServiceMock.Setup(x => x.TotalGroup(userid)).Returns(TotalGroups).Verifiable();
            _exportHistorySeriveMock.Setup(x => x.TotalExported(userid)).Returns(TotalExported).Verifiable();
            _importHistoryMock.Setup(x => x.TotalImported(userid)).Returns(TotalImported).Verifiable();

            //Act
            _model.Get(userid);
            //Assert
            _groupServiceMock.VerifyAll();
            _exportHistorySeriveMock.VerifyAll();
            _importHistoryMock.VerifyAll();
        }
    }
}
