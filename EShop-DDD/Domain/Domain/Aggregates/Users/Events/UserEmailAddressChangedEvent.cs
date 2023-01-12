using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.Users.Events
{
    public sealed class UserEmailAddressChangedEvent:SeedWork.IDomainEvent

    {
        public UserEmailAddressChangedEvent(SharedKernel.FullName fullName, SharedKernel.EmailAddress emailAddress):base()
        {
            FullName = fullName;
            EmailAddress = emailAddress;
        }

        public FullName FullName { get; }
        public EmailAddress EmailAddress { get; }
    }
}
