using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using DataImporter.WebApp.Models.Application;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Tests
{
    public class CreateGroupModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private CreateGroupModel _model;
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
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<CreateGroupModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _mapperMock?.Reset();
        }
        [Test]
        public void CreateGroup_Mapping_Model_And_BusinessObject_Tests()
        {
            var group = new Groups();
            _model.Name = "Test-10";
            _model.ApplicationUserId = new Guid("ae12f203-71fc-4013-3b3c-08d98105b9d6");

             _mapperMock.Setup(m => m.Map<Groups>(It.IsAny<CreateGroupModel>())).Returns((CreateGroupModel createGroupModel) => new Groups() 
             { Name = createGroupModel.Name, ApplicationUserId = createGroupModel.ApplicationUserId }).Verifiable();
            
            //Act
            _model.CreateGroup();
            //Assert
            _mapperMock.VerifyAll();
        }
    }
}
