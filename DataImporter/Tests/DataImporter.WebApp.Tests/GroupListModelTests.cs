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
    public class GroupListModelTests
    {
        private AutoMock _mock;
        private Mock<IGroupsService> _groupserviceMock;
        private GroupListModel _model;
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
            _model = _mock.Create<GroupListModel>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _groupserviceMock?.Reset();
        }
        [Test]
        public void GetAll_Tests()
        {
            var userid = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            var groups = new List<Groups>
            {
                new Groups{Id = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce"), Name = "Test 1", ApplicationUserId = userid},
                new Groups{Id = new Guid("376a12c7-669c-4c88-54ba-08d981e356de"), Name = "Test 2", ApplicationUserId = userid},
                new Groups{Id = new Guid("386a12c7-669c-4c88-54ba-08d981e356ae"), Name = "Test 3", ApplicationUserId = userid},
                new Groups{Id = new Guid("456a12c7-669c-4c88-54ba-08d981e356be"), Name = "Test 4", ApplicationUserId = userid},
                new Groups{Id = new Guid("446a12c7-669c-4c88-54ba-08d981e356cb"), Name = "Test 5", ApplicationUserId = userid}
            };
            _groupserviceMock.Setup(x => x.GetGroups(userid)).Returns(groups);

            //Act
            _model.GetAll(userid);

            //Assert
            _groupserviceMock.VerifyAll();
        }
    }
}
