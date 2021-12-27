using Autofac.Extras.Moq;
using AutoMapper;
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
    public class EditGroupModelTests
    {
        private AutoMock _mock;
        private Mock<IMapper> _mapperMock;
        private Mock<IGroupsService> _groupserviceMock;
        private EditGroupModel _model;
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
            _groupserviceMock = _mock.Mock<IGroupsService>();
            _mapperMock = _mock.Mock<IMapper>();
            _model = _mock.Create<EditGroupModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupserviceMock?.Reset();
            _mapperMock?.Reset();
        }
        [Test]
        public void LoadModelData_GroupExists_LoadProperties()
        {
            //Arrange
            Guid id = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            var group = new Groups
            {
                Id= id,
                ApplicationUserId = new Guid("ae12f203-71fc-4013-3b3c-08d98105b9d6"),
                Name = "Test2",
                DateCreated = DateTime.Now,
            };
            _groupserviceMock.Setup(x => x.GetGroup(id)).Returns(group).Verifiable();

            _mapperMock.Setup(x => x.Map(group, It.IsAny<EditGroupModel>())).Verifiable();

            _model.LoadModelData(id);
            _groupserviceMock.VerifyAll();
            _mapperMock.VerifyAll();
        }
        [Test]
        public void Update_Group_Mapping_Model_And_BusinessObject_Tests()
        {
            Guid id = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            
            _model.Id = id;
            _model.Name = "Test 10";
            _model.ApplicationUserId = new Guid("ae12f203-71fc-4013-3b3c-08d98105b9d6");

            _mapperMock.Setup(m => m.Map<Groups>(It.IsAny<EditGroupModel>())).Returns((EditGroupModel editGroupModel) => new Groups()
            { Name = editGroupModel.Name, ApplicationUserId = editGroupModel.ApplicationUserId }).Verifiable();

            _model.Update(_model.ApplicationUserId);
            _mapperMock.VerifyAll();
        }
    }
}
