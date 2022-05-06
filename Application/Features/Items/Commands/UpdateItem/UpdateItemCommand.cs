using Application.Common.Contracts.Repositories;
using Application.Common.Models;
using Application.Common.Security;
using AutoMapper;
using Common.DTOs;
using Common.DTOs.Requests;
using Common.DTOs.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Items.Commands.UpdateItem
{
    [Authorize]
    public class UpdateItemCommand : BaseCommand<UpdateItemRequest, Result<UpdateItemResponse>>
    {
        public UpdateItemCommand(UpdateItemRequest request) : base(request) { }
    }

    public class UpdateCommandHandler : BaseUpdateCommandHandler<UpdateItemCommand, UpdateItemRequest, Item, UpdateItemResponse>
    {
        public UpdateCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork) : base(mapper, unitOfWork)  { }
    }
}
