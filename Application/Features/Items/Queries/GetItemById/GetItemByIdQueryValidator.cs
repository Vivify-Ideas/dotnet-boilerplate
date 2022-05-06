using Application.Common.Contracts.Repositories;
using Application.Exceptions;
using Application.Features.Items.Queries.GetItemById;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Items.Commands.DeleteItem
{
    public class GetItemByIdQueryValidator : AbstractValidator<GetItemByIdQuery>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetItemByIdQueryValidator(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(o => o.Id)
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
