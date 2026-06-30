using FluentValidation;
using System;

namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    /// <summary>
    /// Returned by <see cref="IValidationBinder.Bind{TModel,TView}"/> so callers
    /// can trigger a full-form validation pass (e.g. on Submit).
    /// </summary>
    public interface IValidationSession
    {
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
