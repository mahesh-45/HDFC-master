using HDFC.Core.Entities.Masters.Division;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDFC.Infrastructure.Persistence.EntityConfigurations
{
    public class DivisionConfigurations : IEntityTypeConfiguration<Division>
    {
        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.HasMany(child => child.SubDivisions).WithOne(parent => parent.Division).HasForeignKey(foreignKey => foreignKey.DivisionId);

        }
    }
}
