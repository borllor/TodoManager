using System;
namespace TodoManager.Domain
{
    public interface IAggregate<TType>
    {
        TType Id { get; set; }
    }
}
