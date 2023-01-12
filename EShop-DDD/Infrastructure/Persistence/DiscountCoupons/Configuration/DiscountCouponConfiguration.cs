using Domain.Aggregates.DiscountCoupons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Persistence.DiscountCoupons.Configuration
{
	class DiscountCouponConfiguration : object,
		Microsoft.EntityFrameworkCore
		.IEntityTypeConfiguration<DiscountCoupon>
	{
		public DiscountCouponConfiguration() : base()
		{
		}

		public void Configure
			(Microsoft.EntityFrameworkCore.Metadata.Builders
			.EntityTypeBuilder<Domain.Aggregates.DiscountCoupons.DiscountCoupon> builder)
		{
			// **************************************************
			//builder
			//	// using Microsoft.EntityFrameworkCore;
			//	.ToTable(name: "DiscountCoupons")
			//	;
			// **************************************************

			// **************************************************
			builder
				.Property(p => p.DiscountPercent)
				.IsRequired(required: true)
				.HasConversion(p => p.Value,
					p => Domain.Aggregates.DiscountCoupons.ValueObjects.DiscountPercent.Create(p).Value)
				;
			// **************************************************

			// **************************************************
			builder
				.Property(p => p.ValidDateFrom)
				.IsRequired(required: true)
				.HasConversion(p => p.Value,
					p => Domain.Aggregates.DiscountCoupons.ValueObjects.ValidDateFrom.Create(p).Value)
				;
			// **************************************************

			// **************************************************
			builder
				.Property(p => p.ValidDateTo)
				.IsRequired(required: true)
				.HasConversion(p => p.Value,
					p => Domain.Aggregates.DiscountCoupons.ValueObjects.ValidDateTo.Create(p).Value)
				;
			// **************************************************
		}
	}
}
