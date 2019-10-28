using System;
using System.Collections.Generic;

namespace TodoManager.Framework.Events
{
    public abstract class EventSource<TType> : IEventSource<TType>
    {
        public TType Id { get; set; }

        public Queue<IEvent> PendingEvents { get; private set; }

        protected EventSource()
        {
            PendingEvents = new Queue<IEvent>();
        }

        protected void Append(IEvent @event)
        {
            PendingEvents.Enqueue(@event);
        }
    }
}
