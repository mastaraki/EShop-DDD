using Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.SharedKernel
{
    internal class GenderConfiguration : object, IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable(name: "Genders");

            builder.HasKey(p => p.Value);

            builder.Property(p => p.Value).IsRequired(true).ValueGeneratedNever();

            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(Gender.MaxLength);


            #region Data Seeding

            builder.HasData(Gender.Male);

            builder.HasData(Gender.Famale);

            #endregion
        }
    }
}
