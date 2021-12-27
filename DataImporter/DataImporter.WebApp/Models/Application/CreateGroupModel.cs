using Autofac;
using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class CreateGroupModel
    {
        public Guid Id { get; set; }
        [StringLength(20, ErrorMessage = "Group Name is too large")]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ApplicationUserId { get; set; }
        private IGroupsService _groupsService;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        public CreateGroupModel()
        {
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupsService = _scope.Resolve<IGroupsService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        public CreateGroupModel(IGroupsService groupsService, IMapper mapper)
        {
            _groupsService = groupsService;
            _mapper = mapper;
        }
        public void CreateGroup()
        {
            var group = _mapper.Map<Groups>(this);
            _groupsService.CreateGroup(group);
        }
    }
}
