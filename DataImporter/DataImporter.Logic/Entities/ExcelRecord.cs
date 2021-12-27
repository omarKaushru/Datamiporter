using DataImporter.Data;
using DataImporter.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Entities
{
    public class ExcelRecord : IEntity<Guid>
    {
        public Guid Id { get; set ; }
        public DateTime DateCreated { get; set; }
        public Guid GroupsId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public IList<ExcelData> ExcelDatas { get; set; }
    }
}
