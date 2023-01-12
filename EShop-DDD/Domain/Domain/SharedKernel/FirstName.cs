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
    public class FirstName : ValueObject
    {
        #region Constant
        public const int MaxLength = 50;
        #endregion

        #region Static Members

        public static Result<FirstName> Create(string value)
        {
            var result = new Result<FirstName>();

            value = Dtat.String.Fix(text: value);

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

                result.WithError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                string errorMessage = string.Format(Validations.MaxLength, DataDictionary.FirstName, MaxLength);

                result.WithError(errorMessage);

                return result;
            }

            var returnValue = new FirstName(value);

            result.WithValue(returnValue);

            return result;
        }

        #endregion


        #region Constractors
        public string Value { get; }

        private FirstName(string value) : this()
        {
            Value = value;
        }

        public FirstName() : base()
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
