using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public interface IGroupsService
    {
        void CreateGroup(Groups groups);
        void UpdateGroup(Groups groups);
        void DeleteGroup(Guid id);
        Groups GetGroup(Guid id);
        IList<Groups> GetGroups(Guid UserId);
        (IList<Groups> records, int total, int totalDisplay) GetGroups(Guid userId, int pageIndex, int pageSize);
        int TotalGroup(Guid userId);
    }
}
