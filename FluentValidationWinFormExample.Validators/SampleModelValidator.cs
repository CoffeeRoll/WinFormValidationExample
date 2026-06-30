using FluentValidation;
using FluentValidationWinFormExample.Models;
using System.Linq;

namespace FluentValidationWinFormExample.Validators
{
    public class SampleModelValidator : AbstractValidator<SampleModel>
    {
        // Reuse a single instance — FluentValidation validators are thread-safe.
        private static readonly OptionModelValidator _optionValidator = new OptionModelValidator();

        public SampleModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            // Graduation must be strictly after birth (date only — time component is ignored).
            RuleFor(x => x.DateOfGraduation)
                .GreaterThan(x => x.DateOfBirth)
                .WithMessage("Date of graduation must be after the date of birth.");

            // Null means the field was left empty; the range rule only runs when a value exists.
            RuleFor(x => x.Age)
                .NotNull().WithMessage("Age is required.");
            RuleFor(x => x.Age)
                .InclusiveBetween(0, 150).WithMessage("Age must be between 0 and 150.")
                .When(x => x.Age.HasValue);

            RuleFor(x => x.Income)
                .GreaterThanOrEqualTo(0).WithMessage("Income must be 0 or greater.");

            // ---- Options rules ----------------------------------------------------------
            // All rules target the "Options" property so the binder can map errors to the
            // DataGridView control. Individual-item validation is delegated to
            // OptionModelValidator but the result is aggregated here so the error property
            // name stays "Options" rather than "Options[n].Title".

            RuleFor(x => x.Options)
                .Must(opts => (opts?.Count() ?? 0) >= 1)
                .WithMessage("At least 1 option is required.");

            RuleFor(x => x.Options)
                .Must(opts => (opts?.Count() ?? 0) <= 5)
                .WithMessage("No more than 5 options are allowed.");

            RuleFor(x => x.Options)
                .Must(opts => opts == null || opts.All(o => _optionValidator.Validate(o).IsValid))
                .WithMessage("All options must have a title.");

            RuleFor(x => x.Options)
                .Must(opts =>
                {
                    if (opts == null) return true;
                    var titles = opts.Select(o => o.Title ?? string.Empty).ToList();
                    return titles.Count == titles.Distinct().Count();
                })
                .WithMessage("Option titles must be unique.");
        }
    }
}
