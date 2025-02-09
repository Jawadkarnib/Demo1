using Lab1._2.Query;
using MediatR;

namespace Lab1._2.Abstraction;

public interface IQueryHandler<TQuery,TResponse>:IRequestHandler<TQuery,TResponse> where TQuery:IQuery<TResponse>
{
    
}