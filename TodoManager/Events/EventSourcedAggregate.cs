using System;
using System.Collections.Generic;

namespace TodoManager.Events
{
    public abstract class EventSourcedAggregate : IEventSourcedAggregate
    {
        public Guid Id { get; set; }

        public Queue<IEvent> PendingEvents { get; private set; }

        protected EventSourcedAggregate()
        {
            PendingEvents = new Queue<IEvent>();
        }

        protected void Append(IEvent @event)
        {
            PendingEvents.Enqueue(@event);
        }
    }
}
