namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    public interface IPresenter
    {
        void Initialize();

        /// <summary>
        /// The view driven by this presenter.
        /// Exposed so callers (e.g. Program.cs) can retrieve the form without
        /// resolving a second dependency from the container.
        /// </summary>
        IView View { get; }
    }
}
