using Autofac.Extras.Moq;
using DataImporter.Logic.BusinessObjects;
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
    public class CancelFileUploadModelTests
    {
        private AutoMock _mock;
        private Mock<ITemporaryDataService> _temporaryDataserviceMock;
        private CancelFileUploadModel _model;
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
            _temporaryDataserviceMock = _mock.Mock<ITemporaryDataService>();
            _model = _mock.Create<CancelFileUploadModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _temporaryDataserviceMock?.Reset();
        }
        [Test]
        public void Cancel_GetTemporary_Data()
        {
            //Arrange
            var userid = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            var temporaryData = new TemporaryData()
            {
                Id = new Guid("ae12f203-71fc-4013-3b3c-08d98105b9d6"),
                GroupId = new Guid("406a12c7-669c-4c88-54ba-08d981e356ce"),
                FileName = "Testa.xls",
                UserId = userid,
            };
            _temporaryDataserviceMock.Setup(x => x.Get(userid)).Returns(temporaryData).Verifiable();
            //Act
            _model.Cancel(userid, "");
            //Assert
            _temporaryDataserviceMock.VerifyAll();
        }
    }
}
