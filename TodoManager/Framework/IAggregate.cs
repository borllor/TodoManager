using System;
namespace TodoManager.Framework
{
    public interface IAggregate<TType>
    {
        TType Id { get; set; }
    }
}
