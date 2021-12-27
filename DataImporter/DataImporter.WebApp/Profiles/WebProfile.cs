using AutoMapper;
using DataImporter.Logic.BusinessObjects;
using DataImporter.WebApp.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Profiles
{
    public class WebProfile : Profile 
    {
        public WebProfile()
        {
            CreateMap<CreateGroupModel, Groups>().ReverseMap();
            CreateMap<EditGroupModel, Groups>().ReverseMap();
            CreateMap<UploadFileModel, TemporaryData>().ReverseMap();
        }
    }
}
