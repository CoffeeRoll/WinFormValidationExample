using FluentValidationWinFormExample.Models;
using FluentValidationWinFormExample.UI.Common;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace FluentValidationWinFormExample.UI.Interfaces
{
    public interface ISampleModelView : IView
    {
        // ---- Text fields ----

        [ValidatesProperty(nameof(SampleModel.Name))]
        string Name { get; }

        [ValidatesProperty(nameof(SampleModel.Email))]
        [InputFilter(InputFilter.AlphanumericWithAt)]
        string Email { get; }

        /// <summary>Raw text; the presenter parses this to int.</summary>
        [ValidatesProperty(nameof(SampleModel.Age))]
        [InputFilter(InputFilter.IntegerOnly)]
        string Age { get; }

        /// <summary>
        /// Raw text; the presenter parses this to double.
        /// Capped to 2 decimal places at the keystroke level by the binder.
        /// </summary>
        [ValidatesProperty(nameof(SampleModel.Income))]
        [InputFilter(InputFilter.DecimalOnly, MaxDecimalPlaces = 2)]
        string Income { get; }

        // ---- Date fields ----

        [ValidatesProperty(nameof(SampleModel.DateOfBirth))]
        DateTime DateOfBirth { get; }

        [ValidatesProperty(nameof(SampleModel.DateOfGraduation))]
        DateTime DateOfGraduation { get; }

        // ---- Options collection ----

        /// <summary>
        /// Returns the current set of options entered by the user.
        /// Backed by a DataGridView; the binder subscribes to its row/cell events.
        /// </summary>
        [ValidatesProperty(nameof(SampleModel.Options))]
        IEnumerable<OptionModel> Options { get; }

        // ---- Submit ----

        /// <summary>Raised when the user clicks Submit.</summary>
        event EventHandler SubmitRequested;

        /// <summary>Called by the presenter when the model passes full validation.</summary>
        void ShowSubmitSuccess();
    }
}
