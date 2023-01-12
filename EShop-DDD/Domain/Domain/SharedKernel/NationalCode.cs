﻿using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedKernel
{
    public class NationalCode : SeedWork.ValueObject
    {
        #region Constant(s)
        public const int FixLength = 10;

        public const string RegularExpression = "^[0-9]{10}$";
        #endregion /Constant(s)

        #region Constractors

        private NationalCode() : base()
        {
        }

        protected NationalCode(string value) : this()
        {
            Value = value;
        }

        public string Value { get; }
        #endregion

        #region Static Member(s)
        public static FluentResults.Result<NationalCode> Create(string value)
        {
            var result =
                new FluentResults.Result<NationalCode>();

            value = value.Fix();

            if (value is null)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.Required, Resources.DataDictionary.NationalCode);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.FixLengthNumeric,
                    Resources.DataDictionary.NationalCode, FixLength);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch
                (input: value, pattern: RegularExpression) == false)
            {
                string errorMessage = string.Format
                    (Resources.Messages.Validations.RegularExpression, Resources.DataDictionary.NationalCode);

                result.WithError(errorMessage: errorMessage);

                return result;
            }

            var returnValue =
                new NationalCode(value: value);


            result.WithValue(value: returnValue);

            return result;

        }

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
