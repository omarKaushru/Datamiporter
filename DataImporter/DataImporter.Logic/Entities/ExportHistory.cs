using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Entities
{
    public class ExportHistory : IEntity<Guid>
    {
        public Guid Id { get; set ; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string Status { get; set; }
        public string FileName { get; set; }
        public String GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public string MailingAddress { get; set; }
    }
}
