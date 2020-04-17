using HDFC.Core.Entities.Budgeting;
using HDFC.Core.Entities.Masters;
using HDFC.Core.Entities.Masters.Division;
using HDFC.Core.Entities.Masters.Employee;
using HDFC.Core.Entities.Masters.Vendor;
using HDFC.Core.Entities.Misc;
using HDFC.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace HDFC.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<BudgetHead> BudgetHeads { get; set; }
        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<CostCode> CostCodes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<SubBudget> SubBudgets { get; set; }
        public DbSet<BudgetSpendLimit> BudgetSpendLimits { get; set; }
        public DbSet<BudgetCostCode> BudgetCostCodes { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorAttachment> VendorAttachments { get; set; }
        public DbSet<VendorUser> VendorUsers { get; set; }
        public DbSet<VendorLocation> VendorLocations { get; set; }
        public DbSet<VendorBankDetail> VendorBankDetails { get; set; }
        public DbSet<VendorMSMEDetail> VendorMSMEDetails { get; set; }

        public DbSet<NumberingSequence> NumberingSequences { get; set; }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<SubDivision> SubDivisions { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<long>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<long>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<long>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<long>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<long>>().ToTable("UserTokens");


            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


            StringifyEnums(builder);
            SetUpLogicalDelete(builder);

            //Configure Defualt Decimal
            foreach (var property in builder.Model.GetEntityTypes().SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetColumnType("decimal(19, 4)");
            }
        }

        private void StringifyEnums(ModelBuilder builder)
        {
            var modelTypes = typeof(AppDbContext).GetProperties()
                                         .Where(x => x.PropertyType.IsGenericType && x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                                         .Select(x => x.PropertyType.GetGenericArguments().First())
                                         .ToList();

            foreach (Type modelType in modelTypes)
            {
                var properties = modelType.GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsEnum)
                    {
                        builder.Entity(modelType).Property(property.Name).HasColumnType("char(50)").HasConversion<string>();
                        continue;
                    }
                }
            }
        }

        private void SetUpLogicalDelete(ModelBuilder builder)
        {
            //Logical Delete
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.AddProperty("IsDeleted", typeof(bool));
                var parameter = Expression.Parameter(entityType.ClrType);
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));
                var lambda = Expression.Lambda(compareExpression, parameter);
                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        new public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        foreach (var navigationEntry in entry.Navigations.Where(n => !n.Metadata.IsDependentToPrincipal()))
                        {
                            if (navigationEntry is CollectionEntry collectionEntry)
                            {
                                foreach (var dependentEntry in collectionEntry.CurrentValue)
                                {
                                    HandleDependent(Entry(dependentEntry));
                                }
                            }
                            else
                            {
                                var dependentEntry = navigationEntry.CurrentValue;
                                if (dependentEntry != null)
                                {
                                    HandleDependent(Entry(dependentEntry));
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void HandleDependent(EntityEntry entry)
        {
            entry.CurrentValues["IsDeleted"] = true;
        }

    }
}
