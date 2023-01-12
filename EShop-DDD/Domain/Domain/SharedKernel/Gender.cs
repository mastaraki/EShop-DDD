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
    public class Gender : Enumeration
    {
        #region Constant
        public const int MaxLength = 10;
        #endregion

        #region Static Members

        public static readonly Gender Male = new(0, DataDictionary.Male);
        public static readonly Gender Famale = new(1, DataDictionary.Female);


        public static Result<Gender> GetByValue(int? value)
        {
            var result = new Result<Gender>();

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.Gender);

                return result.WithError(errorMessage: errorMessage);
            }

            var gender = FromValue<Gender>(value: value.Value);

            if (gender is null)
            {
                string errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

                return result.WithError(errorMessage: errorMessage);

            }
            result.WithValue(gender);

            return result;
        }


        #endregion

        #region Constractors

        private Gender() : base()
        {

        }
        private Gender(int value, string name) : base(value, name)
        {

        }

        #endregion

    }
}
