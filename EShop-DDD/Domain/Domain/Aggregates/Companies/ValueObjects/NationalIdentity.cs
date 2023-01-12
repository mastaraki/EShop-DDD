using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Aggregates.Companies.ValueObjects
{
    public class NationalIdentity : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int FixLength = 10;

        public const string RegularExpression = "^[0-9]{10}$";

        #endregion /Constant(s)

        #region Static Member(s)

        public static FluentResults.Result<NationalIdentity> Create(string value)
        {
            value = value.Fix();

            var result = new FluentResults.Result<NationalIdentity>();


            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, Resources.DataDictionary.NationalIdentity);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.FixLengthNumeric,
                    Resources.DataDictionary.NationalIdentity, FixLength);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (Regex.IsMatch(input:value,pattern:RegularExpression) == false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.RegularExpression, Resources.DataDictionary.NationalIdentity);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            var returnValue = new NationalIdentity(value:value);

            result.WithValue(returnValue);

            return result;
        }

        #endregion


        #region Constractors

        public NationalIdentity():base()
        {

        }

        public NationalIdentity(string value)
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
