using System;
using System.Collections.Generic;

namespace TodoManager.Framework.Events
{
    public interface IEventSource<TType> : IAggregate<TType>
    {
        Queue<IEvent> PendingEvents { get; }
    }
}
