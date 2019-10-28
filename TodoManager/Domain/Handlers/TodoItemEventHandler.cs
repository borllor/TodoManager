using System;
using System.Threading;
using System.Threading.Tasks;
using TodoManager.Bus;
using TodoManager.Domain.Commands;
using TodoManager.Domain.Events;
using TodoManager.Framework.Events;
using TodoManager.Models.Enum;

namespace TodoManager.Domain.Handlers
{
    public class TodoItemEventHandler : IEventHandler<TodoItemEvent>
    {
        private readonly ICommandBus commandBus;

        public TodoItemEventHandler(ICommandBus commandBus)
        {
            this.commandBus = commandBus;
        }

        public Task Handle(TodoItemEvent notification, CancellationToken cancellationToken)
        {
            if (notification.TodoItemEventType == TodoItemEventTypeEnum.NeedToCreateNew)
            {
                return commandBus.Send(new CreateTodoItemCommand()
                {
                    Id = notification.TodoItem.Id,
                    Name = notification.TodoItem.Name,
                    State = notification.TodoItem.State,
                    Deadline = notification.TodoItem.Deadline
                });
            }
            return Task.Run(() => { });
        }
    }
}
