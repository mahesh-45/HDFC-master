using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
namespace HDFC.Web.MappingProfiles.Masters
{
    public class BudgetHeadMappingProfile : Profile
    {
        public BudgetHeadMappingProfile()
        {
            CreateMap<BudgetHead, BudgetHeadDto>().ReverseMap();
        }
    }
}
