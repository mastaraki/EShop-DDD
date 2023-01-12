using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Province
{
    internal class ProvinceConfiguration : object, IEntityTypeConfiguration<Domain.Aggregates.Provinces.Province>
    {

        public void Configure(EntityTypeBuilder<Domain.Aggregates.Provinces.Province> builder)
        {
            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(maxLength: Domain.SharedKernel.Name.MaxLength)
               .HasConversion(x => x.Value,
               x => Domain.SharedKernel.Name.Create(x).Value);



            builder
                .HasIndex(p => p.Name)
                .IsUnique(unique: true);


            builder
                .HasMany(x => x.Cities)
                .WithOne(x => x.Province)
                .IsRequired()
                 //.HasForeignKey("ProvinceId")
                .HasForeignKey(nameof(Domain.Aggregates.Provinces.Province) + nameof(Domain.SeedWork.Entity.Id))
                .OnDelete(DeleteBehavior.NoAction);



            // Seed
            // **************************************************
            var province =
                Domain.Aggregates.Provinces.Province.Create("Tehran");

            builder.HasData(province.Value);

            province =
                Domain.Aggregates.Provinces.Province.Create("Alborz");

            builder.HasData(province.Value);
        }
    }

}
