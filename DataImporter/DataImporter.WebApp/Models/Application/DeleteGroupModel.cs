using Autofac;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class DeleteGroupModel
    {
        private IGroupsService _groupsService;
        private ILifetimeScope _scope;
        public DeleteGroupModel()
        {
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupsService = _scope.Resolve<IGroupsService>();
        }
        public DeleteGroupModel(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }
        public void Delete(Guid id)
        {
            _groupsService.DeleteGroup(id);
        }
    }
}
