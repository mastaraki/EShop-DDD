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
    public class ValidDateFrom:Date
    {

        #region Static Member

        public static Result<ValidDateFrom> Create(DateTime? value)
        {
			var result = new Result<ValidDateFrom>();

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Value.Date < Utility.Today)
            {

                string errorMessage = string.Format
                                    (Resources.Messages.Validations.GreaterThanOrEqualTo_FieldValue,
                                    Resources.DataDictionary.ValidDateFrom, Resources.DataDictionary.CurrentDate);

                result.WithError(errorMessage);

                return result;
            }


            var returnValue = new ValidDateFrom(value);

            result.WithValue(returnValue);

            return result;
        }

        #endregion


        #region Constractor

        private ValidDateFrom(DateTime? value):base(value)
        {

        }

        private ValidDateFrom():base()
        {

        }
        #endregion
    }
}
