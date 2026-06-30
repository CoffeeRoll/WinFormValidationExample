using FluentValidationWinFormExample.UI.Common.Interfaces;
using System;
using System.Windows.Forms;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Subscribes <c>KeyPress</c> handlers to <see cref="TextBox"/> controls whose
    /// view properties are tagged with <see cref="InputFilterAttribute"/>.
    ///
    /// For each keystroke the handler projects the text that <em>would</em> result
    /// after the character is inserted and tests it against the regex supplied by
    /// <see cref="IInputFilterProvider"/>.  If the projected text does not match,
    /// the keystroke is cancelled — so invalid characters are never shown at all.
    ///
    /// This class has no knowledge of FluentValidation or model binding; it is
    /// purely concerned with what the user is allowed to type.
    /// </summary>
    public class InputFilterBinder : IInputFilterBinder
    {
        private readonly IInputFilterProvider _filterProvider;

        public InputFilterBinder(IInputFilterProvider filterProvider)
        {
            _filterProvider = filterProvider
                ?? throw new ArgumentNullException(nameof(filterProvider));
        }

        public void Bind<TView>(TView view) where TView : class, IView
        {
            var bindings = ViewBindingCollector.Collect(typeof(TView));

            foreach (var binding in bindings)
            {
                if (binding.InputFilter == InputFilter.None) continue;

                var control = view.GetControl(binding.ViewPropertyName);
                if (!(control is TextBox filterBox)) continue;

                var regex = _filterProvider.GetRegex(binding.InputFilter, binding.MaxDecimalPlaces);
                if (regex == null) continue;

                // Capture filterBox in a local so the lambda always refers to the
                // correct instance even if the loop variable is reused.
                var capturedBox = filterBox;

                filterBox.KeyPress += (_, e) =>
                {
                    if (char.IsControl(e.KeyChar)) return;

                    string projected =
                        capturedBox.Text.Substring(0, capturedBox.SelectionStart)
                        + e.KeyChar
                        + capturedBox.Text.Substring(capturedBox.SelectionStart + capturedBox.SelectionLength);

                    if (!regex.IsMatch(projected))
                        e.Handled = true;
                };
            }
        }
    }
}
