using FluentValidation;
using System;

namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    /// <summary>
    /// Returned by <see cref="IValidationBinder.Bind{TModel,TView}"/> so callers
    /// can trigger a full-form validation pass (e.g. on Submit) and observe the
    /// form's live validity/dirty state (e.g. to enable a Submit button).
    /// </summary>
    public interface IValidationSession
    {
        /// <summary>
        /// True when the model built from the current view state passes all
        /// validation rules — including rules for fields the user has not
        /// touched yet (those show no error icon but still count).
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// True once the user has changed at least one bound field since the
        /// session was created.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>Raised whenever <see cref="IsValid"/> or <see cref="IsDirty"/> may have changed.</summary>
        event EventHandler StateChanged;

        /// <summary>
        /// Marks every bound field as touched, runs the full model validation,
        /// shows / clears each error, and returns <c>true</c> when the model is valid.
        /// </summary>
        bool ValidateAll();
    }

    public interface IValidationBinder
    {
        IValidationSession Bind<TModel, TView>(TView view, IValidator<TModel> validator, Func<TModel> modelFactory)
            where TView : class, IView;
    }
}
