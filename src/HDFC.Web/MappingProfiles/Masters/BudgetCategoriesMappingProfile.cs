using AutoMapper;
using HDFC.Core.Dtos.Masters;
using HDFC.Core.Entities.Masters;
namespace HDFC.Web.MappingProfiles.Masters
{
    public class BudgetCategoriesMappingProfile : Profile
    {
        public BudgetCategoriesMappingProfile()
        {
            CreateMap<BudgetCategory, BudgetCategoryDto>().ReverseMap();
        }
    }
}
