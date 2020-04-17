using HDFC.Core.Entities.Masters.Vendor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HDFC.Infrastructure.Persistence.EntityConfigurations
{
    public class VendorConfigurations : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasMany(i => i.VendorAttachments).WithOne(s => s.Vendor).HasForeignKey(i => i.VendorId);
            builder.HasMany(i => i.VendorUsers).WithOne(s => s.Vendor).HasForeignKey(i => i.VendorId);
            builder.HasMany(i => i.VendorLocations).WithOne(s => s.Vendor).HasForeignKey(i => i.VendorId);

            builder.HasMany(i => i.VendorBankDetails).WithOne(s => s.Vendor).HasForeignKey(i => i.VendorId);
            builder.HasMany(i => i.VendorMSMEDetails).WithOne(s => s.Vendor).HasForeignKey(i => i.VendorId); 
        }
    }
}
