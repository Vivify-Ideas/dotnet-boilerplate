using Application.Common.Contracts.Repositories;
using Application.Common.Models;
using Application.Common.Security;
using Application.Exceptions;
using AutoMapper;
using Common.DTOs;
using Common.DTOs.Responses;
using Domain.Entities;
using MediatR;

namespace Application.Features.Items.Commands.DeleteItem
{
    [Authorize]
    public class DeleteItemCommand : BaseCommand<int, Result<DeleteItemResponse>>
    {
        public DeleteItemCommand(int request) : base(request) { }
    }

    public class DeleteItemCommandHandler : BaseDeleteCommandHandler<DeleteItemCommand, int, Item, DeleteItemResponse>
    {
        public DeleteItemCommandHandler(IMapper mapper, IUnitOfWork<int> unitOfWork) : base(mapper, unitOfWork) { }
    }

}
