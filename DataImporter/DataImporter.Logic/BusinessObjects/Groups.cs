using System;
using System.Collections.Generic;

namespace DataImporter.Logic.BusinessObjects
{
    public class Groups 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ICollection<ExcelRecord> ExcelRecords { get; set; }
    }
}
