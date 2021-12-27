using System;

namespace DataImporter.Logic.BusinessObjects
{
    public class ExcelData 
    {
        public Guid Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public Guid ExcelRecordId { get; set; }
        public ExcelRecord ExcelRecord { get; set; }
    }
}
