using Application.Common.Contracts.Repositories;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.Features.Items.Commands.UpdateItem
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        //TODO: localization, generic validator?!
        public UpdateItemCommandValidator(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(o => o.Request.Id)
                .NotEmpty().WithMessage("Id field is required")
                .MustAsync(EntityExists);

            RuleFor(o => o.Request.Name)
                .NotEmpty().WithMessage("Name field is required");
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
