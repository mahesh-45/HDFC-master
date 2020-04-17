using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class CostCodeMappingProfile : Profile
    {
        public CostCodeMappingProfile()
        {
            CreateMap<CostCode, CostCodeDto>().ReverseMap();
        }
    }
}
