using FluentValidationWinFormExample.UI.Common.Interfaces;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Generic base presenter that holds a typed view reference and
    /// implements <see cref="IPresenter"/>.
    /// </summary>
    public abstract class PresenterBase<TView> : IPresenter
        where TView : IView
    {
        /// <summary>The strongly-typed view this presenter drives.</summary>
        protected TView View { get; }

        /// <summary>
        /// Explicit implementation of <see cref="IPresenter.View"/>.
        /// Lets callers that only hold an <see cref="IPresenter"/> reference retrieve
        /// the view (e.g. to cast to <see cref="System.Windows.Forms.Form"/> for
        /// <c>Application.Run</c>) without needing a second container resolution.
        /// </summary>
        IView IPresenter.View => View;

        protected PresenterBase(TView view)
        {
            View = view;
        }

        /// <inheritdoc/>
        public abstract void Initialize();
    }
}
