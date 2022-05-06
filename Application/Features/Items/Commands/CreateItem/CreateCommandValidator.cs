using FluentValidation;

namespace Application.Features.Items.Commands.CreateItem
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        //TODO: localization, generic validator?!
        public CreateItemCommandValidator()
        {
            RuleFor(o => o.Request.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            
        }
    }
}
