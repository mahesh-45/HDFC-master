using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<Country, CountryDto>();
        }
    }
}
