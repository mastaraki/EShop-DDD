using Domain.SeedWork;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class UserName : ValueObject
    {
        #region Constants

        public const int MinLength = 8;

        public const int MaxLength = 20;

        public const string RegularExpression = "^[a-zA-Z0-9_-]{8,20}$";

        #endregion

        #region Static Members

        public static FluentResults.Result<UserName> Create(string value)
        {
            var result = new FluentResults.Result<UserName>();

            value = Dtat.String.Fix(text: value);

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.Username);

                return result.WithError(errorMessage);
            }

            if (value.Length < MinLength)
            {
                string errorMessage = string.Format(Validations.MinLength, DataDictionary.Username, MinLength);

                return result.WithError(errorMessage);
            }

            if (value.Length > MaxLength)
            {
                string errorMessage = string.Format(Validations.MaxLength, DataDictionary.Username, MaxLength);

                return result.WithError(errorMessage);
            }

            if (Regex.IsMatch(input: value, pattern: RegularExpression) == false) 
            {
                string errorMessage = string.Format(Validations.RegularExpression, DataDictionary.Username);

                return result.WithError(errorMessage);
            }

            var returnValue = new UserName(value);

            return result.WithValue(returnValue);
        }

        #endregion

        #region Constractors

        public string Value { get; }

        private UserName(string Value) : this()
        {
            this.Value = Value;
        }

        private UserName():base()
        {

        }

      

        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
