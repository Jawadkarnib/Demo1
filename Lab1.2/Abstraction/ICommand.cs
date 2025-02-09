using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lab1._2.Abstraction;

public interface ICommand :IRequest 
{
    
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
    
}