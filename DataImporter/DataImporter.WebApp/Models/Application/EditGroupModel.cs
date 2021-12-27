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
    public class EditGroupModel
    {
        public Guid Id { get; set; }
        [StringLength(20, ErrorMessage ="Group Name is too large")]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ApplicationUserId { get; set; }
        private IGroupsService _groupsService;
        private IMapper _mapper;
        private ILifetimeScope _scope;
        public EditGroupModel()
        {
        }
        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _groupsService = _scope.Resolve<IGroupsService>();
            _mapper = _scope.Resolve<IMapper>();
        }
        public EditGroupModel(IGroupsService groupsService, IMapper mapper)
        {
            _groupsService = groupsService;
            _mapper = mapper;
        }
        public void LoadModelData(Guid id)
        {
           var group = _groupsService.GetGroup(id);
            _mapper.Map(group, this);
        }

        public void Update(Guid userId)
        {
            
            var group = _mapper.Map<Groups>(this);
            group.ApplicationUserId = userId;
            _groupsService.UpdateGroup(group);
        }
    }
}
