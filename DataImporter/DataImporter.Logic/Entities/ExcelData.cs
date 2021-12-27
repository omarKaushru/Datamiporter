using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Entities
{
    public class ExcelData : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public Guid ExcelRecordId { get; set; }
        public ExcelRecord ExcelRecord { get; set; }
    }
}
