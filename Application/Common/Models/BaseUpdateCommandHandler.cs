
using Application.Common.Contracts.Repositories;
using AutoMapper;
using Common.DTOs;
using Domain.Common.Entities;
using MediatR;

namespace Application.Common.Models
{
    public class BaseUpdateCommandHandler<TCommand, TRequest, TModel, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : BaseCommand<TRequest, Result<TResponse>> where TModel : BaseEntity<int>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;

        public BaseUpdateCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TModel>(command.Request);

            await _unitOfWork.Repository<TModel>().UpdateAsync(entity);
            await _unitOfWork.Commit(cancellationToken);

            var response = _mapper.Map<TResponse>(entity);

            return Result<TResponse>.Success(response);
        }
    }
}
