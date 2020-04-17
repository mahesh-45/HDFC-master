using HDFC.Core.Repositories;
using HDFC.Core.Repositories.Masters;
using HDFC.Core.Repositories.Procurements;
using HDFC.Infrastructure.Persistence;
using HDFC.Infrastructure.Repositories.Masters;
using HDFC.Infrastructure.Repositories.Procurements;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public ICountryRepository Countries { get; private set; }
        public IBudgetHeadRepository BudgetHeads { get; private set; }
        public ICostCodeRepository CostCodes { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IVendorRepository Vendors { get; private set; }

        public IBudgetRepository Budgets { get; private set; }
        public ICurrencyRepository Currencies { get; private set; }
        public IDivisionRepository Divisions { get; private set; }
        public INumberingSequenceRepository NumberingSequences { get; private set; }
       public IBudgetCategoriesRepository BudgetCategories { get; private set; }
        public IDepartmentRepository Departments { get; private set; }
        public UnitOfWork(AppDbContext context)
        {
            _dbContext = context;
            Countries = new CountryRepository(context);
            BudgetHeads = new BudgetHeadRepository(context);
            CostCodes = new CostCodeRepository(context);
            Currencies = new CurrencyRepository(context);
            Divisions = new DivisionRepository(context);
            Budgets = new BudgetRepository(context);
            Employees = new EmployeeRepository(context);
            Vendors = new VendorRepository(context);
            NumberingSequences = new NumberingSequenceRepository(context);
            BudgetCategories = new BudgetCategoriesRepository(context);
            Departments = new DepartmentRepository(context);
        }

        public async Task CompleteAsync(long userId)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
