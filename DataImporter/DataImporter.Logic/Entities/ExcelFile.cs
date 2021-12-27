using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Entities
{
    public class ExcelFile : IEntity<Guid>
    {
        public Guid Id { get ; set ; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string FileName { get; set; }
    }
}
