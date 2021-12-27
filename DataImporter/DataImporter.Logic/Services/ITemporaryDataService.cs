using DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Services
{
    public interface ITemporaryDataService
    {
        void Create(TemporaryData temporaryData);
        TemporaryData Get(Guid UserId);
        void Delete(Guid id);
    }
}
