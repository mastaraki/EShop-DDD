using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users.Events
{
    public sealed class UserPasswordChangedEvent:object,SeedWork.IDomainEvent
    {
        public UserPasswordChangedEvent(SharedKernel.FullName fullName, SharedKernel.EmailAddress emailAddress)
        {
            FullName = fullName;
            EmailAddress = emailAddress;
        }

        public FullName FullName { get; }
        public EmailAddress EmailAddress { get; }
    }
}
