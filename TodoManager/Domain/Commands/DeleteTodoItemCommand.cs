using System;
using System.ComponentModel.DataAnnotations;
using TodoManager.Domain;
using TodoManager.Framework.Command;

namespace TodoManager.Domain.Commands
{
    public class DeleteTodoItemCommand : ICommand
    {
        public Guid Id { get; set; }

        public DeleteTodoItemCommand(Guid id)
        {
            Id = id;
        }
    }
}
