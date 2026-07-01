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
    /// Wiring (input filters, change-driven validation, Submit enablement while
    /// dirty &amp;&amp; valid) comes from
    /// <see cref="ValidatingPresenterBase{TModel,TView}"/>; this class only
    /// supplies the view-to-model mapping in <see cref="BuildModel"/>.
    /// </summary>
    public class SampleModelPresenter
        : ValidatingPresenterBase<SampleModel, ISampleModelView>, ISampleModelPresenter
    {
        public SampleModelPresenter(
            ISampleModelView        view,
            IValidator<SampleModel> validator,
            IValidationBinder       binder,
            IInputFilterBinder      inputFilterBinder)
            : base(view, validator, binder, inputFilterBinder)
        {
        }

        /// <summary>
        /// Copies the view's current values onto a new <see cref="SampleModel"/>.
        /// Runs on every change event, so it only reads the view — no side effects.
        /// </summary>
        protected override SampleModel BuildModel() => new SampleModel
        {
            Name             = View.Name,
            Email            = View.Email,
            // Null when the field is empty — triggers "Age is required" in the validator.
            Age              = int.TryParse(View.Age, out int age) ? (int?)age : null,
            Income           = double.TryParse(View.Income, out double income) ? income : 0d,
            // The view exposes date-only values (see SampleModelForm), so no
            // truncation is needed here.
            DateOfBirth      = View.DateOfBirth,
            DateOfGraduation = View.DateOfGraduation,
            Options          = View.Options?.ToList(),
            // Nested model: SubModelValidator reports errors as "SubModel.MaidenName"
            // etc., matching the dotted bindings on ISampleModelView.
            SubModel         = new SubModel
            {
                MaidenName = View.MaidenName,
                Address    = View.Address
            }
        };
    }
}
