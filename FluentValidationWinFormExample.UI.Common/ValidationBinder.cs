using FluentValidation;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Dynamically wires up FluentValidation to WinForms controls by reflecting over
    /// the view interface and discovering properties tagged with
    /// <see cref="ValidatesPropertyAttribute"/>.
    ///
    /// Subscribes to each control's change event:
    ///   • <see cref="TextBox"/>        → TextChanged
    ///   • <see cref="DateTimePicker"/> → ValueChanged
    ///   • <see cref="DataGridView"/>   → RowsAdded, RowsRemoved, CellValueChanged
    ///
    /// On every change, runs full model validation and refreshes errors only for
    /// fields the user has already touched — so untouched fields stay clean and
    /// cross-field rules (e.g. graduation &gt; birth) are re-evaluated automatically.
    /// The session's <see cref="IValidationSession.IsValid"/> always reflects the
    /// whole model, and <see cref="IValidationSession.StateChanged"/> fires on every
    /// change so presenters can gate a Submit button on "dirty &amp;&amp; valid".
    ///
    /// Input filtering (blocking invalid keystrokes) is handled separately by
    /// <see cref="InputFilterBinder"/>; this class has no knowledge of it.
    /// </summary>
    public class ValidationBinder : IValidationBinder
    {
        public IValidationSession Bind<TModel, TView>(
            TView view,
            IValidator<TModel> validator,
            Func<TModel> modelFactory)
            where TView : class, IView
        {
            var bindings = ViewBindingCollector.Collect(typeof(TView));
            var session = new ValidationSession<TModel>(view, bindings, validator, modelFactory);

            foreach (var binding in bindings)
            {
                var name = binding.ViewPropertyName;
                var control = view.GetControl(name);
                if (control == null)
                {
                    continue;
                }

                if (control is DateTimePicker dtp)
                {
                    dtp.ValueChanged += (_, __) => session.OnFieldChanged(name);
                }
                else if (control is DataGridView dgv)
                {
                    dgv.RowsAdded        += (_, __) => session.OnFieldChanged(name);
                    dgv.RowsRemoved      += (_, __) => session.OnFieldChanged(name);
                    dgv.CellValueChanged += (_, __) => session.OnFieldChanged(name);
                }
                else
                {
                    control.TextChanged += (_, __) => session.OnFieldChanged(name);
                }
            }

            // Establish the initial IsValid state (no errors are displayed because
            // nothing is touched yet) so callers can gate the Submit button
            // immediately after binding.
            session.Refresh();

            return session;
        }

        // ------------------------------------------------------------------ session

        private sealed class ValidationSession<TModel> : IValidationSession
        {
            private readonly IView              _view;
            private readonly List<BindingInfo>  _bindings;
            private readonly IValidator<TModel> _validator;
            private readonly Func<TModel>       _modelFactory;
            private readonly HashSet<string>    _touched
                = new HashSet<string>(StringComparer.Ordinal);

            public ValidationSession(
                IView              view,
                List<BindingInfo>  bindings,
                IValidator<TModel> validator,
                Func<TModel>       modelFactory)
            {
                _view         = view;
                _bindings     = bindings;
                _validator    = validator;
                _modelFactory = modelFactory;
            }

            public bool IsValid { get; private set; }

            public bool IsDirty => _touched.Count > 0;

            public event EventHandler StateChanged;

            /// <summary>Called by the binder whenever a bound control's value changes.</summary>
            public void OnFieldChanged(string viewPropertyName)
            {
                _touched.Add(viewPropertyName);
                Refresh();
            }

            /// <summary>
            /// Validates the full model, updates error icons for touched fields,
            /// and notifies listeners that the session state may have changed.
            /// </summary>
            public void Refresh()
            {
                var model  = _modelFactory();
                var result = _validator.Validate(model);

                IsValid = result.IsValid;

                foreach (var b in _bindings)
                {
                    if (!_touched.Contains(b.ViewPropertyName))
                    {
                        continue;
                    }

                    var error = result.Errors
                        .FirstOrDefault(e => e.PropertyName == b.ModelPropertyName);

                    if (error != null)
                    {
                        _view.ShowValidationError(b.ViewPropertyName, error.ErrorMessage);
                    }
                    else
                    {
                        _view.ClearValidationError(b.ViewPropertyName);
                    }
                }

                StateChanged?.Invoke(this, EventArgs.Empty);
            }

            public bool ValidateAll()
            {
                foreach (var b in _bindings)
                {
                    _touched.Add(b.ViewPropertyName);
                }

                Refresh();
                return IsValid;
            }
        }
    }
}
