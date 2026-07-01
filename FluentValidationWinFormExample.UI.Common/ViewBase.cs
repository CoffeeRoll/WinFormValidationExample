using FluentValidationWinFormExample.UI.Common.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Base WinForms <see cref="Form"/> that implements <see cref="IView"/>.
    ///
    /// Derived forms call <see cref="RegisterControl"/> during initialisation to map
    /// view-interface property names to their corresponding controls.  The validation
    /// binder uses these mappings to hook up change events, and validation errors are
    /// surfaced via a shared <see cref="ErrorProvider"/> next to the mapped control.
    /// </summary>
    public abstract class ViewBase : Form, IView
    {
        private readonly Dictionary<string, Control> _controlMap
            = new Dictionary<string, Control>();

        private readonly ErrorProvider _errorProvider;

        protected ViewBase()
        {
            _errorProvider = new ErrorProvider
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink,
                ContainerControl = this
            };
        }

        /// <summary>
        /// Associates a view-interface property name with the control that represents it.
        /// Call this from the form's constructor after <c>InitializeComponent</c>,
        /// once for every property tagged with <see cref="ValidatesPropertyAttribute"/>.
        /// </summary>
        protected void RegisterControl(string viewPropertyName, Control control)
        {
            _controlMap[viewPropertyName] = control;
        }

        /// <inheritdoc/>
        public Control GetControl(string viewPropertyName)
        {
            _controlMap.TryGetValue(viewPropertyName, out var control);
            return control;
        }

        /// <inheritdoc/>
        public void ShowValidationError(string viewPropertyName, string message)
        {
            if (_controlMap.TryGetValue(viewPropertyName, out var control))
            {
                _errorProvider.SetError(control, message);
            }
        }

        /// <inheritdoc/>
        public void ClearValidationError(string viewPropertyName)
        {
            if (_controlMap.TryGetValue(viewPropertyName, out var control))
            {
                _errorProvider.SetError(control, string.Empty);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _errorProvider?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
