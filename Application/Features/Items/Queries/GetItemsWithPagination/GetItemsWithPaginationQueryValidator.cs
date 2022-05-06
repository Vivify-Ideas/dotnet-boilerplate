
using FluentValidation;

namespace Application.Features.Items.Queries.GetItemsWithPagination
{
    public class GetItemsWithPaginationQueryValidator : AbstractValidator<GetItemsWithPaginationQuery>
    {
        public GetItemsWithPaginationQueryValidator()
        {

            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
   
}
