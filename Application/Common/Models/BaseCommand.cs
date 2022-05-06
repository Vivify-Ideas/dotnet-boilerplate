using MediatR;

namespace Application.Common.Models
{
    public class BaseCommand<TRequest, TResponse> : IRequest<TResponse>
    {
        public TRequest Request { get; }

        public BaseCommand(TRequest request)
        {
            Request = request;
        }
    }
}
