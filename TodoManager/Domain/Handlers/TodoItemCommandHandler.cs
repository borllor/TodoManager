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
    public class TodoItemCommandHandler :
        ICommandHandler<CreateTodoItemCommand>,
        ICommandHandler<UpdateTodoItemCommand>,
        ICommandHandler<ChangeStateOfTodoItemCommand>,
        ICommandHandler<DeleteTodoItemCommand>

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

        public async Task<Unit> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem(request.Id, request.Name, request.State, request.Deadline);

            await todoItemContext.TodoItem.AddAsync(todoItem);

            await SaveAndPublish(todoItem, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await todoItemContext.TodoItem.FindAsync(request.Id);
            todoItem.Update(request.Name, request.State, request.Deadline);

            todoItemContext.TodoItem.Update(todoItem);

            await SaveAndPublish(todoItem, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(ChangeStateOfTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await todoItemContext.TodoItem.FindAsync(request.Id);
            todoItem.ChangeState( request.State);

            todoItemContext.TodoItem.Update(todoItem);

            await SaveAndPublish(todoItem, cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await todoItemContext.TodoItem.FindAsync(request.Id);

            todoItemContext.TodoItem.Remove(todoItem);

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
