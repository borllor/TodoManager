using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoManager.Command.Handler;

namespace TodoManager.Command
{
    public class TodoItemCommandHandler : ICommandHandler<CreateTodoItemCommand>
    {


        public Task<Unit> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
