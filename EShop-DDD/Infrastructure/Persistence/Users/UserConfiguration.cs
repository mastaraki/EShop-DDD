using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;
using Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Users
{
    public class UserConfiguration : object, IEntityTypeConfiguration<User>
    {
        public UserConfiguration() : base()
        {

        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(maxLength: UserName.MaxLength)
                .HasConversion(x => x.Value,
                 x => UserName.Create(x).Value);


            builder.Property(x => x.Password)
              .IsRequired()
              .HasMaxLength(maxLength: Password.MaxLength)
              .HasConversion(x => x.Value,
               x => Password.Create(x).Value);

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(foreignKeyPropertyNames: "RoleId")
                .IsRequired(required: true);

            builder.Property<int>("RoleId")
                .HasColumnName("RoleId");


            builder.OwnsOne(x => x.FullName, x =>
               {
                   x.HasOne(xx => xx.Gender)
                   .WithMany()
                   .HasForeignKey(foreignKeyPropertyNames: "GenderId")
                   .IsRequired(required: true);

                   x.Property<int>("GenderId")
                        .HasColumnName("GenderId");

                   x.Property(x=>x.FirstName)
                   .HasColumnName(nameof(FirstName))
                   .HasMaxLength(FirstName.MaxLength)
                   .IsRequired()
                   .HasConversion(x=>x.Value,x=>FirstName.Create(x).Value);

                   x.Property(x => x.LastName)
                  .HasColumnName(nameof(LastName))
                  .HasMaxLength(LastName.MaxLength)
                  .IsRequired()
                  .HasConversion(x => x.Value, x => LastName.Create(x).Value);
               });


            builder
                .OwnsOne(p => p.EmailAddress, p =>
                {
                    // **************************************************
                    p.Property(pp => pp.Value)
                        .HasColumnName(nameof(Domain.SharedKernel.EmailAddress))
                        .HasMaxLength(maxLength: Domain.SharedKernel.EmailAddress.MaxLength)
                        .IsRequired(required: true) // فعلا باگ دارد و کار نمی‌کند
                        ;
                    // **************************************************

                    // **************************************************
                    p.Property(pp => pp.IsVerified)
                        .HasColumnName(nameof(Resources.DataDictionary.IsEmailAddressVerified))
                        .IsRequired(required: true) // فعلا باگ دارد و کار نمی‌کند
                        ;
                    // **************************************************

                    // **************************************************
                    p.Property(pp => pp.VerificationKey)
                        .HasColumnName(nameof(Resources.DataDictionary.EmailAddressVerificationKey))
                        .HasMaxLength(maxLength: Domain.SharedKernel.EmailAddress.VerificationKeyMaxLength)
                        .IsRequired(required: true) // فعلا باگ دارد و کار نمی‌کند
                        ;
                    // **************************************************
                });








        }
    }
}
