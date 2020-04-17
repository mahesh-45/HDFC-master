using HDFC.Core.Repositories.Masters;
using HDFC.Core.Repositories.Procurements;
using System.Threading.Tasks;

namespace HDFC.Core.Repositories
{
    public interface IUnitOfWork
    {
        ICountryRepository Countries { get; }
        IBudgetHeadRepository BudgetHeads { get; }
        ICostCodeRepository CostCodes { get; }
        IEmployeeRepository Employees { get; }
        IVendorRepository Vendors { get; }
        IBudgetRepository Budgets { get; }
        INumberingSequenceRepository NumberingSequences { get; }
        ICurrencyRepository Currencies { get; }
        IDivisionRepository Divisions { get; }
        IBudgetCategoriesRepository BudgetCategories { get; }
        IDepartmentRepository Departments { get; }
        Task CompleteAsync(long userId);
    }
}
