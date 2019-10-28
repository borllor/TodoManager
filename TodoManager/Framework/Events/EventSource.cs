using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoManager.Framework.Events
{
    public abstract class EventSource<TType> : IEventSource<TType>
    {
        public TType Id { get; set; }

        [NotMapped]
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
