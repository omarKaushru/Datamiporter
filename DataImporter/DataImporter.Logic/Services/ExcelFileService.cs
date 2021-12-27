using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.Logic.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public class ExcelFileService : IExcelFileService
    {
        private readonly IDataImporterUnitOfWrok _dataImporterUnitOfWrok;
        private readonly IMapper _mapper;
        public ExcelFileService(IDataImporterUnitOfWrok dataImporterUnitOfWrok, IMapper mapper)
        {
            _dataImporterUnitOfWrok = dataImporterUnitOfWrok;
            _mapper = mapper;
        }
        public void Create(ExcelFile excelFile)
        {
            _dataImporterUnitOfWrok.ExcelFiles.Add(
                _mapper.Map(excelFile, new Entities.ExcelFile())
                );
            _dataImporterUnitOfWrok.Save();
        }

        public void DeleteExcelFile(Guid id)
        {
            _dataImporterUnitOfWrok.ExcelFiles.Remove(id);
            _dataImporterUnitOfWrok.Save();
        }

        public ExcelFile GetExcelFile()
        {
            var excelFile = _dataImporterUnitOfWrok.ExcelFiles.GetAll().FirstOrDefault();
            if (excelFile == null)
                return null;
            return _mapper.Map<ExcelFile>(excelFile);
        }
    }
}
