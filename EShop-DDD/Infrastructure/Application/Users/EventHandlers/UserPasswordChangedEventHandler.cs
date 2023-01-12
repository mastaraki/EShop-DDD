using Domain.Aggregates.Users.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.EventHandlers
{
    public sealed class UserPasswordChangedEventHandler : object, MediatR.INotificationHandler<UserPasswordChangedEvent>
    {
        public Task Handle(UserPasswordChangedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
