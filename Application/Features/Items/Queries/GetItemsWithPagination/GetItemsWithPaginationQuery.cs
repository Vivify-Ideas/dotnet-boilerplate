using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.DTOs.Responses;
using MediatR;

namespace Application.Features.Items.Queries
{
    public class GetItemsWithPaginationQuery : IRequest<PaginatedList<GetItemsResponse>>
    {
        //provjeriti sto ne radi validator
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetItemsWithPaginationQueryHandler : IRequestHandler<GetItemsWithPaginationQuery, PaginatedList<GetItemsResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<GetItemsResponse>> Handle(GetItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items
                .ProjectTo<GetItemsResponse>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
