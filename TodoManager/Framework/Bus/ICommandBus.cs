using System;
using System.Threading.Tasks;
using TodoManager.Framework.Command;

namespace TodoManager.Bus
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
