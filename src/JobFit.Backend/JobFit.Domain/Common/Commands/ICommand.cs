using MediatR;

namespace JobFit.Domain.Common.Commands;

public interface ICommand<out TResult> : ICommand, IRequest<TResult>
{
    
}

public interface ICommand
{
    
}