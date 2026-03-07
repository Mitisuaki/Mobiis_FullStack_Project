using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Interfaces.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        IReadOnlyCollection<string> Mensagens { get; }
        bool Invalido { get; }
    }
}
