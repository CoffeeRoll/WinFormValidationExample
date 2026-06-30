namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    /// <summary>
    /// Wires character-level input filters to view controls.
    ///
    /// Call <see cref="Bind{TView}"/> once per view in presenter initialisation,
    /// before <see cref="IValidationBinder.Bind{TModel,TView}"/>.  For each
    /// property on the view tagged with <see cref="InputFilterAttribute"/> the
    /// binder subscribes a <c>KeyPress</c> handler that blocks keystrokes which
    /// would leave the field's text in an invalid state according to the
    /// filter's regex.
    /// </summary>
    public interface IInputFilterBinder
    {
        void Bind<TView>(TView view) where TView : class, IView;
    }
}
