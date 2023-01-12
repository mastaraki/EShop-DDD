using Domain.SeedWork;
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
    public class DiscountPercent : ValueObject
    {

        #region Constant
        public const int Minimum = 0;
        public const int Maximum = 100;


        #endregion


        #region Static Member 

        public static Result<DiscountPercent> Create(int? value)
        {
            var result = new Result<DiscountPercent>();

            if (value == null)
            {
                string errorMessage = string.Format
                     (Validations.Required, DataDictionary.DiscountPercent);

                result.WithError(errorMessage);

                return result;
            }

            if ((value < Minimum) || (value > Maximum) )
            {
                string errorMessage = string.Format(Validations.Range, DataDictionary.DiscountPercent,Minimum,Maximum);

                result.WithError(errorMessage);

                return result;
            }



            var returnValue = new DiscountPercent(value);


            result.WithValue(value: returnValue);

            return result;


        }

        #endregion

        #region Constractor

        public int? Value { get; }
        private DiscountPercent(int? value) : this()
        {
            Value = value;
        }
        private DiscountPercent() : base()
        {

        }

      
        #endregion



        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


        public override string ToString()
        {
            if (Value is null)
            {
                return "-------";
            }
            else
            {
                return Value.ToString();
            }
        }
    }
}
