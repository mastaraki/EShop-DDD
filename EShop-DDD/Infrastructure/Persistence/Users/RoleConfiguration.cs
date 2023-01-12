using Domain.Aggregates.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Users
{
    internal class RoleConfiguration : object, IEntityTypeConfiguration<Role>
    {
        public RoleConfiguration():base()
        {

        }
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(name: "Roles");

            builder.HasKey(x => x.Value);

            builder.Property(x => x.Value)
                .ValueGeneratedNever()
                .IsRequired(required:true);

            builder.Property(x => x.Name)
               .IsRequired(required: true)
               .HasMaxLength(maxLength:Role.MaxLength);

            // Data Seeding

            builder.HasData(Domain.Aggregates.Users.ValueObjects.Role.Programmer);
            builder.HasData(Domain.Aggregates.Users.ValueObjects.Role.Administrator);
            builder.HasData(Domain.Aggregates.Users.ValueObjects.Role.Supervisor);
            builder.HasData(Domain.Aggregates.Users.ValueObjects.Role.Agent);
            builder.HasData(Domain.Aggregates.Users.ValueObjects.Role.Customer);
        }
    }
}
