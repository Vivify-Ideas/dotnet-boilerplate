using Application.Common.Contracts.Repositories;
using Common.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Common.DTOs.Responses;

namespace Application.Features.Items.Queries.GetItemById
{
    public class GetItemByIdQuery : IRequest<Result<GetItemByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Result<GetItemByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetItemByIdQueryHandler (IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetItemByIdResponse>> Handle (GetItemByIdQuery request, CancellationToken cancellation)
        {
            var model = await _unitOfWork.Repository<Item>().GetByIdAsync(request.Id);

            var response = _mapper.Map<GetItemByIdResponse>(model);
            return Result<GetItemByIdResponse>.Success(response);
        }
    }
}
