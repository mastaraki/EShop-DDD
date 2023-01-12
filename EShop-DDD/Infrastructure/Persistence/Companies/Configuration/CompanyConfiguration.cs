using Domain.Aggregates.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Companies.Configuration
{
    public class CompanyConfiguration : object, IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.NationalIdentity)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
