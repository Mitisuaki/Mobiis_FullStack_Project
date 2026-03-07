using Fretefy.Test.Domain.Interfaces.Entities;
using Fretefy.Test.Domain.Notifications;
using System;

namespace Fretefy.Test.Domain.Entities
{
    public abstract class Entity : Notification, IEntity
    {
        public Guid Id { get; set; }
    }
}
