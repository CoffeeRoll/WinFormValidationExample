using FluentValidation;
using FluentValidationWinFormExample.Models;

namespace FluentValidationWinFormExample.Validators
{
    /// <summary>
    /// Validates a single <see cref="OptionModel"/> entry.
    ///
    /// Used by <see cref="SampleModelValidator"/> via a <c>Must</c> predicate that runs
    /// each item through this validator and aggregates the result onto the parent
    /// <c>Options</c> property. This keeps error messages bound to the DataGridView
    /// control rather than the per-row property paths (<c>Options[n].Title</c>) that
    /// <c>RuleForEach</c> would produce and that the validation binder cannot map.
    /// </summary>
    public class OptionModelValidator : AbstractValidator<OptionModel>
    {
        public OptionModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Option title is required.");
        }
    }
}
