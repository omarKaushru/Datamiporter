using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Exceptions;
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

namespace DataImporter.Logic.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupServiceTests
    {
        private AutoMock _mock;
        private Mock<IDataImporterUnitOfWrok> _dataImporterUnitOfWrokMock;
        private Mock<IGroupsRepository> _groupsRepositoryMock;
        private IGroupsService _groupsServiceMock;

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
            _groupsRepositoryMock = _mock.Mock<IGroupsRepository>();
            _groupsServiceMock = _mock.Create<GroupsService>();
        }
       
        [Test]
        public void CreateGroup_Group_Is_Null_Throw_Exceptions()
        {
            var group = new Groups();

            _dataImporterUnitOfWrokMock.Setup(x => x.Groups).Returns(_groupsRepositoryMock.Object);

            Should.Throw<InvalidOperationException>(
                   () => _groupsServiceMock.CreateGroup(group)
               );
        }
        [Test]
        public void UpdateGroup_group_has_Same_Id_and_UserId()
        {
            var userid = new Guid("366a12c7-669c-4c88-54ba-08d981e356ce");
            var id = new Guid("356a12c7-669c-4c88-54ba-08d981e356ce");
            var group = new Groups { Id = id, Name = "Test 1", ApplicationUserId = userid };
            var groupEntity = new EO.Groups{ Id = id, Name = "Test 11", ApplicationUserId = userid };

            _dataImporterUnitOfWrokMock.Setup(x => x.Groups).Returns(_groupsRepositoryMock.Object);
            _groupsRepositoryMock.Setup(x => x.GetById(id)).Returns(groupEntity);

            _groupsServiceMock.UpdateGroup(group);

            this.ShouldSatisfyAllConditions(
               () => _dataImporterUnitOfWrokMock.VerifyAll(),
               () => _groupsRepositoryMock.VerifyAll(),
               () => groupEntity.ApplicationUserId.ShouldBe(group.ApplicationUserId),
               () => groupEntity.Id.ShouldBe(group.Id)
           );
        }
    }
}
