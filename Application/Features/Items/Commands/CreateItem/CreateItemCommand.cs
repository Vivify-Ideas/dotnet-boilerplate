using Application.Common.Contracts.Repositories;
using Application.Common.Models;
using Application.Common.Security;
using AutoMapper;
using Common.DTOs;
using Common.DTOs.Requests;
using Common.DTOs.Responses;
using Domain.Common.Entities;
using Domain.Entities;
using MediatR;

namespace Application.Features.Items.Commands.CreateItem
{
    //[Authorize(Policy = "CanCreateItemOnly")]
    public class CreateItemCommand : BaseCommand<CreateItemRequest, Result<CreateItemResponse>>
    {
        public CreateItemCommand(CreateItemRequest request) : base(request) { }
    }

    public class CreateItemCommandHandler : BaseCreateCommandHandler<CreateItemCommand, CreateItemRequest, Item, CreateItemResponse>
    {
        public CreateItemCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork) : base(mapper, unitOfWork) { }
    }

    
}
