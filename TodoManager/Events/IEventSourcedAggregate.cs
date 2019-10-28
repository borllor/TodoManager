using System;
using System.Collections.Generic;
using TodoManager.Domain;

namespace TodoManager.Events
{
    public interface IEventSourcedAggregate: IAggregate<Guid>
    {
        Queue<IEvent> PendingEvents { get; }
    }
}
