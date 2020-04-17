using AutoMapper;
using HDFC.Core.Dtos.Procurements;
using HDFC.Core.Dtos.Procurements.Budgeting;
using HDFC.Core.Entities.Budgeting;
using HDFC.Web.ViewModels.Budgets;

namespace HDFC.Web.MappingProfiles.Procurements
{
    public class BudgetMappingProfile : Profile
    {
        public BudgetMappingProfile()
        {
            CreateMap<Budget, BudgetDto>()
                .ForMember(dest => dest.SubBudgets, opt => opt.MapFrom(src => src.SubBudgets))
                .ForMember(dest => dest.BudgetSpendLimits, opt => opt.MapFrom(src => src.BudgetSpendLimits))
                .ForMember(dest => dest.BudgetCostCodes, opt => opt.MapFrom(src => src.BudgetCostCodes)).ReverseMap();

            CreateMap<Budget, BudgetCreateViewModel>()
                .ForMember(dest => dest.BudgetCostCodes, opt => opt.MapFrom(src => src.BudgetCostCodes))
                .ForMember(dest => dest.BudgetSpendLimits, opt => opt.MapFrom(src => src.BudgetSpendLimits)).ReverseMap();

            CreateMap<Budget, BudgetEditViewModel>()
                .ForMember(dest => dest.BudgetCostCodes, opt => opt.MapFrom(src => src.BudgetCostCodes))
                .ForMember(dest => dest.BudgetSpendLimits, opt => opt.MapFrom(src => src.BudgetSpendLimits)).ReverseMap();

            CreateMap<BudgetCostCode, BudgetCostCodeViewModel>().ReverseMap();
            CreateMap<SubBudget, SubBudgetViewModel>().ReverseMap();
            CreateMap<BudgetSpendLimit, BudgetSpendLimitViewModel>().ReverseMap();

            CreateMap<BudgetCostCode, BudgetCostCodeDto>().ReverseMap();
            CreateMap<SubBudget, SubBudgetDto>().ReverseMap();
            CreateMap<BudgetSpendLimit, BudgetSpendLimitDto>().ReverseMap();
        }
    }
}
