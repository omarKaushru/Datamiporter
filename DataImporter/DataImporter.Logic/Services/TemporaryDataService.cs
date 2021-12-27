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
    public class TemporaryDataService : ITemporaryDataService
    {
        private readonly IDataImporterUnitOfWrok _dataImporterUnitOfWrok;
        private readonly IMapper _mapper;
        public TemporaryDataService(IDataImporterUnitOfWrok dataImporterUnitOfWrok, IMapper mapper)
        {
            _dataImporterUnitOfWrok = dataImporterUnitOfWrok;
            _mapper = mapper;
        }
        public void Create(TemporaryData temporaryData)
        {
            if(temporaryData!=null)
            {
                _dataImporterUnitOfWrok.TemporaryData.Add(
                    _mapper.Map(temporaryData, new Entities.TemporaryData())
                    );
                _dataImporterUnitOfWrok.Save();
            }
        }

        public void Delete(Guid id)
        {
            _dataImporterUnitOfWrok.TemporaryData.Remove(id);
            _dataImporterUnitOfWrok.Save();
        }

        public TemporaryData Get(Guid UserId)
        {
            var groupEntitie = _dataImporterUnitOfWrok.TemporaryData.Get(x => x.UserId == UserId, string.Empty).FirstOrDefault();
            var tempData = _mapper.Map<TemporaryData>(groupEntitie);
            return tempData;
        }
    }
}
