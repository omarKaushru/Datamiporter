using DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public interface IExcelFileService
    {
        void Create(ExcelFile excelFile);
        void DeleteExcelFile(Guid id);
        ExcelFile GetExcelFile();
    }
}
