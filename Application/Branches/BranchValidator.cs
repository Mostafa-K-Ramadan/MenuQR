using Domain;
using FluentValidation;

namespace Application.Branches
{
        public class BranchValidator : AbstractValidator<Branch>
        {
            public BranchValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Empty").MaximumLength(15).WithMessage("Length of name should be less than 15");
                RuleFor(x => x.Location).NotEmpty().WithMessage("Location is Empty");
            }
        }
}