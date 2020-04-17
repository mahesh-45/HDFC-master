using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;

namespace HDFC.Web.MappingProfiles.Masters
{
    public class NumberingSequencesMappingProfile : Profile
    {
        public NumberingSequencesMappingProfile()
        {
            CreateMap<NumberingSequence, NumberingSequenceDto>().ReverseMap();
        }
    }
}
