using System;
namespace TodoManager.Models.Enum
{
    public enum TodoItemEventTypeEnum
    {
        None = 0,
        GetById = 1,
        GetList = 2,
        Created = 3,
        Updated = 4,
        Deleted = 5,
        StateChanged = 6
    }
}
