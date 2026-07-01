using FluentValidationWinFormExample.Models;
using FluentValidationWinFormExample.UI.Common;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace FluentValidationWinFormExample.UI.Interfaces
{
    /// <summary>
    /// View contract for the SampleModel form.
    ///
    /// Each property tagged with <see cref="ValidatesPropertyAttribute"/> binds to
    /// the <see cref="SampleModel"/> property of the same name (or the dotted path
    /// given to the attribute).  The binder uses these bindings to wire change
    /// events and route validation errors to the registered control; the presenter
    /// maps the values onto the model in its BuildModel method.  Submit plumbing
    /// (SubmitRequested, SetSubmitEnabled, ShowSubmitSuccess) is inherited from
    /// <see cref="ISubmitView"/>.
    /// </summary>
    public interface ISampleModelView : ISubmitView
    {
        // ---- Text fields ----

        [ValidatesProperty]
        string Name { get; }

        [ValidatesProperty]
        [InputFilter(InputFilter.AlphanumericWithAt)]
        string Email { get; }

        /// <summary>Raw text; mapped to the model's nullable int.</summary>
        [ValidatesProperty]
        [InputFilter(InputFilter.IntegerOnly)]
        string Age { get; }

        /// <summary>
        /// Raw text; mapped to the model's double.
        /// Capped to 2 decimal places at the keystroke level by the binder.
        /// </summary>
        [ValidatesProperty]
        [InputFilter(InputFilter.DecimalOnly, MaxDecimalPlaces = 2)]
        string Income { get; }

        // ---- Nested SubModel fields ----
        // Dotted paths bind to nested model properties; errors from the child
        // SubModelValidator carry the same dotted names, so they map straight
        // back to these controls.

        [ValidatesProperty("SubModel.MaidenName")]
        string MaidenName { get; }

        [ValidatesProperty("SubModel.Address")]
        string Address { get; }

        // ---- Date fields ----

        [ValidatesProperty]
        DateTime DateOfBirth { get; }

        [ValidatesProperty]
        DateTime DateOfGraduation { get; }

        // ---- Options collection ----

        /// <summary>
        /// Returns the current set of options entered by the user.
        /// Backed by a DataGridView; the binder subscribes to its row/cell events.
        /// </summary>
        [ValidatesProperty]
        IEnumerable<OptionModel> Options { get; }
    }
}
