using Domain.Aggregates.Users.ValueObjects;
using Domain.SeedWork;
using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users
{
    public class User : SeedWork.AggregateRoot
    {
        #region Static Member(s)
        public static FluentResults.Result<User> Create
            (string username, string password, string emailAddress,
            int? role, int? gender, string firstName, string lastName)
        {
            var result =
                new FluentResults.Result<User>();

            // **************************************************
            var usernameResult =
                UserName.Create(value: username);

            result.WithErrors(errors: usernameResult.Errors);
            // **************************************************

            // **************************************************
            var passwordResult =
                Password.Create(value: password);

            result.WithErrors(errors: passwordResult.Errors);
            // **************************************************

            // **************************************************
            var emailAddressResult =
                EmailAddress.Create(value: emailAddress);

            result.WithErrors(errors: emailAddressResult.Errors);
            // **************************************************

            // **************************************************
            var roleResult =
                Role.GetByValue(value: role);

            result.WithErrors(errors: roleResult.Errors);
            // **************************************************

            // **************************************************
            var fullNameResult =
                FullName.Create
                (gender: gender, firstName: firstName, lastName: lastName);

            result.WithErrors(errors: fullNameResult.Errors);
            // **************************************************

            if (result.IsFailed)
            {
                return result;
            }

            var returnValue =
                new User(username: usernameResult.Value, password: passwordResult.Value,
                emailAddress: emailAddressResult.Value, role: roleResult.Value, fullName: fullNameResult.Value);

            result.WithValue(value: returnValue);

            return result;
        }
        #endregion /Static Member(s)

        private User() : base()
        {
        }

        private User
            (ValueObjects.UserName username,
            ValueObjects.Password password,
            SharedKernel.EmailAddress emailAddress,
            ValueObjects.Role role,
            SharedKernel.FullName fullName) : this()
        {
            Role = role;
            FullName = fullName;
            Username = username;
            Password = password;
            EmailAddress = emailAddress;
        }

        public ValueObjects.Role Role { get; private set; }

        public ValueObjects.UserName Username { get; private set; }

        //[System.Text.Json.Serialization.JsonIgnore]
        public ValueObjects.Password Password { get; private set; }

        public SharedKernel.FullName FullName { get; private set; }

        public SharedKernel.EmailAddress EmailAddress { get; private set; }


        public FluentResults.Result ChangePassword(string newPassword)
        {
            var result = new FluentResults.Result();

            var newPasswordResult = ValueObjects.Password.Create(value: newPassword);

            if (newPasswordResult.IsFailed)
            {
               return result.WithErrors(errors: newPasswordResult.Errors);
            }

            Password = newPasswordResult.Value;

            RaiseDomainEvent(new Events.UserPasswordChangedEvent(fullName: FullName, emailAddress: EmailAddress));

            return result;
        }
    }
}
