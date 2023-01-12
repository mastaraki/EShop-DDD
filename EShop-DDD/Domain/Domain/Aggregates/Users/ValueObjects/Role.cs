using Domain.SeedWork;
using FluentResults;
using Resources;
using Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users.ValueObjects
{
    public class Role : Enumeration
    {
        #region Constant
        public const int MaxLength = 50;
        #endregion

        #region Static Members

        public static readonly Role Customer = new(0, DataDictionary.Customer);
        public static readonly Role Agent = new(1, DataDictionary.Agent);
        public static readonly Role Supervisor = new(2, DataDictionary.Supervisor);
        public static readonly Role Administrator = new(3, DataDictionary.Administrator);
        public static readonly Role Programmer = new(4, DataDictionary.Programmer);


        public static Result<Role> GetByValue(int? value)
        {
            var result = new Result<Role>();

            if (value is null)
            {
                string errorMessage = string.Format(Validations.Required, DataDictionary.UserRole);

                return result.WithError(errorMessage: errorMessage);
            }

            var role = FromValue<Role>(value: value.Value);

            if (role is null)
            {
                string errorMessage = string.Format(Validations.InvalidCode, DataDictionary.UserRole);

                return result.WithError(errorMessage: errorMessage);

            }
            result.WithValue(role);

            return result;
        }


        #endregion

        #region Constractors

        private Role() : base()
        {

        }
        private Role(int value, string name) : base(value, name)
        {

        }

        #endregion

    }
}
