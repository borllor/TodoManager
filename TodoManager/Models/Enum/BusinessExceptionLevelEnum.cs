using System;

namespace TodoManager.Models.Enum
{
    public enum BusinessExceptionLevelEnum
    {
        Unknown = 0,
        Dal = 1,
        Service = 2,
        Domain = 3,
        Controller = 4
    }
}
