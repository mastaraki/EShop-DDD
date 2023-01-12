using Domain.Aggregates.DiscountCoupons.ValueObjects;
using Domain.SeedWork;
using FluentResults;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.DiscountCoupons
{
    public class DiscountCoupon : AggregateRoot
    {

        #region Static Members
        public static Result<DiscountCoupon> Create
            (int? discountPercent, DateTime? validDateFrom, DateTime? validDateTo)
        {
            var result =
                new Result<DiscountCoupon>();

            // **************************************************
            var discountPercentResult =
                DiscountPercent.Create(value: discountPercent);

            result.WithErrors(errors: discountPercentResult.Errors);
            // **************************************************

            // **************************************************
            var validDateFromResult =
                ValidDateFrom.Create(value: validDateFrom);

            result.WithErrors(errors: validDateFromResult.Errors);
            // **************************************************

            // **************************************************
            var validDateToResult =
                ValidDateTo.Create(value: validDateTo);

            result.WithErrors(errors: validDateToResult.Errors);
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            if (validDateToResult.Value < validDateFromResult.Value)
            {
                string errorMessage =
                    string.Format(Validations.GreaterThanOrEqualTo_TwoFields,
                    DataDictionary.ValidDateTo, DataDictionary.ValidDateFrom);

                result.WithError(errorMessage);
            }

            if (result.IsFailed)
            {
                return result;
            }

            var returnValue =
                new DiscountCoupon
                (discountPercentResult.Value,
                validDateFromResult.Value, validDateToResult.Value);

            result.WithValue(value: returnValue);

            return result;
        }


        public Result Update
           (int? discountPercent, DateTime? validDateFrom, DateTime? validDateTo)
        {
            var result = Create(discountPercent: discountPercent,
                                validDateFrom: validDateFrom,
                                validDateTo: validDateTo);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            ValidDateTo = result.Value.ValidDateTo;
            ValidDateFrom = result.Value.ValidDateFrom;
            DiscountPercent = result.Value.DiscountPercent;

            return result.ToResult();
        }

        #endregion


        #region Constractors

        public DiscountPercent DiscountPercent { get; private set; }
        public ValidDateFrom ValidDateFrom { get; private set; }
        public ValidDateTo ValidDateTo { get; private set; }
        private DiscountCoupon(DiscountPercent discountPercent, ValidDateFrom validDateFrom, ValidDateTo validDateTo) : this()
        {
            DiscountPercent = discountPercent;
            ValidDateFrom = validDateFrom;
            ValidDateTo = validDateTo;
        }

        private DiscountCoupon() : base()
        {

        }



        #endregion
    }
}
