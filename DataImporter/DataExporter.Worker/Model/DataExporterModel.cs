using DataImporter.Common.Utilities;
using DataImporter.Logic.BusinessObjects;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExporter.Worker.Model
{
    public class DataExporterModel 
    {
        private readonly ExportHistoryModel _exportHistoryModel;
        private readonly IDateTimeUtility _dateTimeUtility;
        public DataExporterModel(ExportHistoryModel exportHistoryModel, IDateTimeUtility dateTimeUtility)
        {
            _exportHistoryModel = exportHistoryModel;
            _dateTimeUtility = dateTimeUtility;
        }
        public void Export(IList<string>FieldName, IList<string> FieldValue, ExportHistory exporthistory)
        {
            string folder = exporthistory.FileName;
            string excelName = $"{exporthistory.GroupName}{"_"}{exporthistory.GroupId.ToString()}{_dateTimeUtility.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(folder, excelName));
            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                for (int i = 0; i <FieldName.Count; i++)
                {
                    workSheet.Cells[1, i+1].Value = FieldName[i];
                }
                int t = 0;
                for (int i = 1; i < FieldValue.Count / FieldName.Count; i++)
                {
                    
                    for (int j = 1; j <= FieldName.Count; j ++)
                    {
                        workSheet.Cells[i + 1, j].Value = FieldValue[t];
                        t++;
                    }
                }
                
                package.Save();
            }
            exporthistory.FileName = file.ToString();
            exporthistory.Status = "Completed";
            _exportHistoryModel.Update(exporthistory);
        }
    }
}
