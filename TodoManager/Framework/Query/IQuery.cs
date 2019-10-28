using System;
using System.Threading.Tasks;
using MediatR;

namespace TodoManager.Framework.Query
{
    public interface IQuery<TResponse> : IRequest<TResponse>
    {
    }
}
