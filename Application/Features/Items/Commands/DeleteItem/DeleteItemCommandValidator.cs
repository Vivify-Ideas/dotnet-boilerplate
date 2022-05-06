using Application.Common.Contracts.Repositories;
using Application.Common.Interfaces;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Items.Commands.DeleteItem
{
    public class DeleteItemCommandValidator : AbstractValidator<DeleteItemCommand>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteItemCommandValidator(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(o => o.Request)
                .NotEmpty().WithMessage("Id is required")
                .MustAsync(EntityExists); 
        }

        public async Task<bool> EntityExists(int id, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<Item>().GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), id);
            }

            return true;
        }
    }
}
