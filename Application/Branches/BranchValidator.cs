using Domain;
using FluentValidation;

namespace Application.Branches
{
        public class BranchValidator : AbstractValidator<Branch>
        {
            public BranchValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(15);
                RuleFor(x => x.Location).NotEmpty().WithMessage("Location is Empty");
            }
        }
}