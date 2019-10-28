using System;
using System.ComponentModel.DataAnnotations;
using TodoManager.Domain;
using TodoManager.Framework.Command;

namespace TodoManager.Domain.Commands
{
    public class ChangeStateOfTodoItemCommand : ICommand
    {
        public Guid Id { get; set; }

        public int State { get; set; }

        public ChangeStateOfTodoItemCommand(Guid id, int state)
        {
            Id = id;
            State = state;
        }
    }
}
