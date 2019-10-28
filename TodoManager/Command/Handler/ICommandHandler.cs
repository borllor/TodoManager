using System;
using MediatR;

namespace TodoManager.Command.Handler
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}
