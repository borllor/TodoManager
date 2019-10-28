using System;
using System.Threading;
using System.Threading.Tasks;
using TodoManager.Domain.Events;
using TodoManager.Framework.Events;

namespace TodoManager.Domain.Handlers
{
    public class TodoItemEventHandler : IEventHandler<TodoItemEvent>
    {
        public Task Handle(TodoItemEvent notification, CancellationToken cancellationToken)
        {
            //TODO store events
            return Task.Run(() => { });
        }
    }
}
