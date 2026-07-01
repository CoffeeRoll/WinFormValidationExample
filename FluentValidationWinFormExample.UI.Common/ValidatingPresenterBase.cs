using FluentValidation;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using System;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Generic presenter for a validated submit form.  On Initialize it:
    ///   1. Wires keystroke filtering via <see cref="IInputFilterBinder"/>.
    ///   2. Wires control-level validation via <see cref="IValidationBinder"/>,
    ///      using the model built by the derived presenter's <see cref="BuildModel"/>.
    ///   3. Keeps the view's Submit button enabled only while the form is both
    ///      dirty (the user changed something) and valid.
    ///
    /// A concrete presenter implements <see cref="BuildModel"/> — plain code that
    /// copies view values onto a new model — and can override
    /// <see cref="OnSubmitAccepted"/> to do more than show the success message.
    /// </summary>
    public abstract class ValidatingPresenterBase<TModel, TView> : PresenterBase<TView>
        where TView : class, ISubmitView
    {
        private readonly IValidator<TModel> _validator;
        private readonly IValidationBinder  _binder;
        private readonly IInputFilterBinder _inputFilterBinder;
        private IValidationSession          _session;

        protected ValidatingPresenterBase(
            TView              view,
            IValidator<TModel> validator,
            IValidationBinder  binder,
            IInputFilterBinder inputFilterBinder)
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
            _inputFilterBinder.Bind(View);

            View.SubmitRequested += OnSubmitRequested;

            _session = _binder.Bind<TModel, TView>(View, _validator, BuildModel);
            _session.StateChanged += (_, __) => UpdateSubmitState();

            // A freshly bound form is never dirty, so Submit starts disabled.
            UpdateSubmitState();
        }

        /// <summary>
        /// Builds the model from the view's current values.  Implemented by each
        /// presenter as plain, debuggable mapping code — this runs on every change
        /// event, so it should only read the view and construct the model.
        /// </summary>
        protected abstract TModel BuildModel();

        /// <summary>Called when Submit is clicked and full validation passes.</summary>
        protected virtual void OnSubmitAccepted() => View.ShowSubmitSuccess();

        private void UpdateSubmitState() => View.SetSubmitEnabled(_session.IsDirty && _session.IsValid);

        private void OnSubmitRequested(object sender, EventArgs e)
        {
            // ValidateAll() marks every field as touched, shows all errors, and
            // returns true only when the model is fully valid.  The button is
            // normally disabled while invalid, but this guards submits triggered
            // any other way.
            if (_session.ValidateAll())
            {
                OnSubmitAccepted();
            }
        }
    }
}
