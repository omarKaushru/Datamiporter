using DataImporter.Data;
using DataImporter.Logic.Contexts;
using DataImporter.Logic.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataImporter.Logic.Repositories
{
    public class GroupsRepository : Repository <Groups, Guid>, IGroupsRepository
    {
        public GroupsRepository(IDataImporterContext context)
            : base((DbContext)context) { }
    }
}
