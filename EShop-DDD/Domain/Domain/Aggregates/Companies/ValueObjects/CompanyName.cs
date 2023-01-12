using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Companies.ValueObjects
{
    public class CompanyName:SeedWork.ValueObject
    {

        #region Constant(s)
        public const int MaxLength = 10;
        #endregion /Constant(s)


        #region Static Member(s)

        public static FluentResults.Result<CompanyName> Create(string value)
        {
            value = value.Fix();

            var result =
            new FluentResults.Result<CompanyName>();

            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, Resources.DataDictionary.CompanyName);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.MaxLength, Resources.DataDictionary.CompanyName, MaxLength);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            var returnValue =  new CompanyName(value);

            result.WithValue(value: returnValue);

            return result;
        }

       

        #endregion


        #region Constractors
        private CompanyName() : base()
        {
        }

        protected CompanyName(string value) : this()
        {
            Value = value;
        }

        public string Value { get; private set; }
        #endregion

        protected override
        System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
