using Domain.SeedWork;
using FluentResults;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedKernel
{
    public class LastName : ValueObject
    {
        #region Constant
        public const int MaxLength = 50;
        #endregion

        #region Static Members

        public static Result<LastName> Create(string value)
        {
            var result = new Result<LastName>();

            value = Dtat.String.Fix(text: value);

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.LastName);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                string errorMessage = string.Format(Validations.MaxLength, DataDictionary.LastName, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new LastName(value);

            result.WithValue(returnValue);

            return result;
        }

        #endregion


        #region Constractors
        public string Value { get; }

        private LastName(string value) : this()
        {
            Value = value;
        }

        public LastName() : base()
        {
        }

        #endregion
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }


        public override string ToString()
        {
            if (string.IsNullOrEmpty(Value))
            {
                return "-------";
            }
            else
            {
                return Value;
            }
        }
    }

}
