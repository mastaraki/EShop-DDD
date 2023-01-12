using Domain.Aggregates.Cities;
using Domain.Aggregates.Companies;
using Domain.Aggregates.DiscountCoupons;
using Domain.Aggregates.Provinces;
using Domain.Aggregates.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DatabaseContext
{
    public class DatabaseContext:DbContext
    {

        //public DatabaseContext():base()
        //{
        //    Database.EnsureCreated();

        //}

        public Mediator Mediator { get; }
        public DatabaseContext(MediatR.Mediator mediator) : base()
        {
            Mediator = mediator;
            Database.EnsureCreated();
        }


        public DbSet<DiscountCoupon> DiscountCoupons { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Domain.Aggregates.Provinces.Province> Provinces { get; set; }

        public DbSet<Domain.Aggregates.Cities.City> Cities { get; set; }

        public DbSet<Company> Companies { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{


        //    string connectionString = GetConnectionString();

        //    optionsBuilder.UseSqlServer(connectionString);

        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override void OnModelCreating
              (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(DiscountCoupons.Configuration.DiscountCouponConfiguration).Assembly);
        }

        public override async Task<int> SaveChangesAsync
            (CancellationToken cancellationToken = default)
        {
            int affectedRows =
                await base.SaveChangesAsync(cancellationToken: cancellationToken);

            if (affectedRows > 0)
            {
                var aggregateRoots =
                    ChangeTracker.Entries()
                    .Where(current => current.Entity is Domain.SeedWork.IAggregateRoot)
                    .Select(current => current.Entity as Domain.SeedWork.IAggregateRoot)
                    .ToList()
                    ;

                foreach (var aggregateRoot in aggregateRoots)
                {
                    // Dispatch Events!
                    foreach (var domainEvent in aggregateRoot.DomainEvents)
                    {
                        await Mediator.Publish(domainEvent, cancellationToken);
                    }

                    // Clear Events!
                    aggregateRoot.ClearDomainEvents();
                }
            }

            return affectedRows;
        }


    }
}
