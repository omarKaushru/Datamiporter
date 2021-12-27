using System;
using System.Collections.Generic;

namespace DataImporter.Logic.BusinessObjects
{
    public class ExcelRecord 
    {
        public Guid Id { get; set ; }
        public DateTime DateCreated { get; set; }
        public Guid GroupsId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public IList<ExcelData> ExcelDatas { get; set; }
    }
}
