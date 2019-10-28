using System;
using TodoManager.Domain.Events;
using TodoManager.Framework.Events;
using TodoManager.Models.Enum;

namespace TodoManager.Domain
{
    public class TodoItem : EventSource<Guid>
    {
        public string Name { get; set; }
        public int State { get; set; }
        public DateTime Deadline { get; set; }

        public TodoItem()
        {
        }

        public TodoItem(Guid id, string name, int state, DateTime deadline)
        {
            Id = id;
            Name = name;
            State = state;

            Append(new TodoItemEvent(TodoItemEventTypeEnum.Created, this));
        }

        public void Update(string name, int state, DateTime deadline)
        {
            Name = name;
            State = state;
            Deadline = deadline;

            Append(new TodoItemEvent(TodoItemEventTypeEnum.Updated, this));
        }

        public void ChangeState(int state)
        {
            State = state;

            Append(new TodoItemEvent(TodoItemEventTypeEnum.StateChanged, this));
        }
    }
}
