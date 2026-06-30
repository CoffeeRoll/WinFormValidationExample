using FluentValidation;
using FluentValidationWinFormExample.Models;
using FluentValidationWinFormExample.UI.Common;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using FluentValidationWinFormExample.UI.Interfaces;
using System.Linq;

namespace FluentValidationWinFormExample.UI
{
    /// <summary>
    /// Presenter for the SampleModel form.
    ///
    /// Responsibilities:
    ///   1. On Initialize: wire up keystroke filtering via <see cref="IInputFilterBinder"/>,
    ///      then wire up control-level validation via <see cref="IValidationBinder"/>.
    ///   2. On SubmitRequested: run a full validation pass across all fields; if valid,
    ///      tell the view to show success — otherwise errors are already displayed.
    /// </summary>
    public class SampleModelPresenter : PresenterBase<ISampleModelView>, ISampleModelPresenter
    {
        private readonly IValidator<SampleModel> _validator;
        private readonly IValidationBinder       _binder;
        private readonly IInputFilterBinder      _inputFilterBinder;
        private IValidationSession               _session;

        public SampleModelPresenter(
            ISampleModelView        view,
            IValidator<SampleModel> validator,
            IValidationBinder       binder,
            IInputFilterBinder      inputFilterBinder)
            : base(view)
        {
            _validator         = validator;
            _binder            = binder;
            _inputFilterBinder = inputFilterBinder;
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            // Wire up keystroke filters first so they are in place before the
            // user can interact with any control.
            _inputFilterBinder.Bind<ISampleModelView>(View);

            View.SubmitRequested += OnSubmitRequested;

            // Wire up validation change events and store the session so we can
            // call ValidateAll() on Submit.
            _session = _binder.Bind<SampleModel, ISampleModelView>(View, _validator, BuildModel);
        }

        // ---- event handlers ----

        private void OnSubmitRequested(object sender, System.EventArgs e)
        {
            // ValidateAll() marks every field as touched, shows all errors, and
            // returns true only when the model is fully valid.
            if (_session.ValidateAll())
                View.ShowSubmitSuccess();
        }

        // ---- model factory ----

        private SampleModel BuildModel() => new SampleModel
        {
            Name             = View.Name,
            Email            = View.Email,
            // Null when the field is empty — triggers "Age is required" in the validator.
            Age              = int.TryParse(View.Age, out int age) ? (int?)age : null,
            Income           = double.TryParse(View.Income, out double income) ? income : 0d,
            // Strip the time component so two pickers on the same calendar date
            // are always treated as equal, regardless of DateTime.Now initialization order.
            DateOfBirth      = View.DateOfBirth.Date,
            DateOfGraduation = View.DateOfGraduation.Date,
            Options          = View.Options?.ToList()
        };
    }
}
