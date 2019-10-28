using System;
using MediatR;

namespace TodoManager.Framework.Command
{
    public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}
