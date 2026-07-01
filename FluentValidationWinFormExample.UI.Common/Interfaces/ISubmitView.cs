using System;

namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    /// <summary>
    /// A view with a Submit button, driven by
    /// <see cref="ValidatingPresenterBase{TModel,TView}"/>.  View interfaces for
    /// concrete forms extend this and add their bound field properties.
    /// </summary>
    public interface ISubmitView : IView
    {
        /// <summary>Raised when the user clicks Submit.</summary>
        event EventHandler SubmitRequested;

        /// <summary>
        /// Called by the presenter as the form's dirty/valid state changes;
        /// the view enables or disables its Submit button accordingly.
        /// </summary>
        void SetSubmitEnabled(bool enabled);

        /// <summary>Called by the presenter when the model passes full validation.</summary>
        void ShowSubmitSuccess();
    }
}
