using AutoMapper;
using EO = DataImporter.Logic.Entities;
using BO = DataImporter.Logic.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Logic.Profiles
{
    public class LogicProfile : Profile
    {
        public LogicProfile()
        {
            CreateMap<EO.ExcelFile, BO.ExcelFile>().ReverseMap();
            CreateMap<EO.Groups, BO.Groups>().ReverseMap();
            CreateMap<EO.ExcelData, BO.ExcelData>().ReverseMap();
            CreateMap<EO.ExcelRecord, BO.ExcelRecord>().ReverseMap();
            CreateMap<EO.TemporaryData, BO.TemporaryData>().ReverseMap();
            CreateMap<EO.ImportHistory, BO.ImportHistory>().ReverseMap();
            CreateMap<EO.ExportHistory, BO.ExportHistory>().ReverseMap();
        }
    }
}
