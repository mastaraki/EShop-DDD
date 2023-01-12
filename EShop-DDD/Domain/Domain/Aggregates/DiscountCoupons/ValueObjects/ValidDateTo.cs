using Domain.SeedWork;
using Domain.SharedKernel;
using FluentResults;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
    public class ValidDateTo : Date
    {

        #region Static Member

        public static Result<ValidDateTo> Create(DateTime? value)
        {
            var result = new Result<ValidDateTo>();

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Value.Date < Utility.Today)
            {

                string errorMessage = string.Format
                                (Resources.Messages.Validations.GreaterThanOrEqualTo_FieldValue,
                                Resources.DataDictionary.ValidDateTo, Resources.DataDictionary.CurrentDate);

                result.WithError(errorMessage);

                return result;
            }


            var returnValue = new ValidDateTo(value);

            result.WithValue(returnValue);

            return result;
        }

        #endregion


        #region Constractor

        private ValidDateTo(DateTime? value) : base(value)
        {

        }

        private ValidDateTo() : base()
        {

        }
        #endregion
    }

}
