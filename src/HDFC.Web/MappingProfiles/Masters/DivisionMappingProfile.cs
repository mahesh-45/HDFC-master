using AutoMapper;
using HDFC.Core.Dtos.Masters.Division;
using HDFC.Core.Entities.Masters.Division;
using HDFC.Web.ViewModels.Masters.Division;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class DivisionMappingProfile:Profile
    {
        public DivisionMappingProfile()
        {
            CreateMap<Division, DivisionDto>()
               .ForMember(dest => dest.SubDivisions, opt => opt.MapFrom(src => src.SubDivisions)).ReverseMap();
            CreateMap<SubDivision, SubDivisionDto>().ReverseMap();

            CreateMap<Division, DivisionCreateViewModel>()
               .ForMember(dest => dest.SubDivisions, opt => opt.MapFrom(src => src.SubDivisions)).ReverseMap();

            CreateMap<Division, DivisionEditViewModel>()
                .ForMember(dest => dest.SubDivisions, opt => opt.MapFrom(src => src.SubDivisions)).ReverseMap();

            CreateMap<SubDivision, SubDivisionViewModel>().ReverseMap();
            CreateMap<SubDivision, SubDivisionDto>().ReverseMap();
        }
    }
}
