using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoManager.Dal;
using TodoManager.Domain;
using TodoManager.Domain.Commands;
using TodoManager.Framework.Command;
using TodoManager.Framework.Events;

namespace TodoManager.Framework.Handlers
{
    public class TodoItemCommandHandler : ICommandHandler<CreateTodoItemCommand>
    {
        private readonly TodoItemContext todoItemContext;
        private readonly IEventBus eventBus;

        public TodoItemCommandHandler(
             TodoItemContext todoItemContext,
            IEventBus eventBus)
        {
            this.todoItemContext = todoItemContext;
            this.eventBus = eventBus;
        }

        public async Task<Unit> Handle(CreateTodoItemCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var todoItem = new TodoItem(command.Id, command.Name, command.State, command.Deadline);

            await todoItemContext.TodoItem.AddAsync(todoItem);
            await SaveAndPublish(todoItem, cancellationToken);

            return Unit.Value;
        }

        private async Task SaveAndPublish(TodoItem todoItem, CancellationToken cancellationToken)
        {
            await todoItemContext.SaveChangesAsync(cancellationToken);

            await eventBus.Publish(todoItem.PendingEvents.ToArray());
        }
    }
}
