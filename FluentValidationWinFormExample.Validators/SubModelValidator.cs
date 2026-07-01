using FluentValidation;
using FluentValidationWinFormExample.Models;

namespace FluentValidationWinFormExample.Validators
{
    /// <summary>
    /// Validates <see cref="SubModel"/>.  Wired into <see cref="SampleModelValidator"/>
    /// via <c>SetValidator</c>, so its errors surface with property names prefixed
    /// by the parent property ("SubModel.MaidenName") — which is exactly how the
    /// view interface binds them.
    /// </summary>
    public class SubModelValidator : AbstractValidator<SubModel>
    {
        public SubModelValidator()
        {
            RuleFor(x => x.MaidenName)
                .NotEmpty().WithMessage("Maiden name is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.");
        }
    }
}
