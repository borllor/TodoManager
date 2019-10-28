using System;
using TodoManager.Framework.Events;
using TodoManager.Models.Enum;

namespace TodoManager.Domain.Events
{
    public class TodoItemEvent : IEvent
    {
        public TodoItemEventTypeEnum TodoItemEventType { get; private set; }
        public TodoItem TodoItem { get; private set; }

        public TodoItemEvent(TodoItemEventTypeEnum todoItemEventType, TodoItem todoItem)
        {
            TodoItemEventType = todoItemEventType;
            TodoItem = todoItem;
        }
    }
}
