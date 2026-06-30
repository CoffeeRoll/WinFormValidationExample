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
            var touched  = new HashSet<string>(StringComparer.Ordinal);

            // Refreshes errors for every field the user has already interacted with.
            void RefreshErrors()
            {
                var model  = modelFactory();
                var result = validator.Validate(model);

                foreach (var b in bindings)
                {
                    if (!touched.Contains(b.ViewPropertyName)) continue;

                    var error = result.Errors
                        .FirstOrDefault(e => e.PropertyName == b.ModelPropertyName);

                    if (error != null)
                        view.ShowValidationError(b.ViewPropertyName, error.ErrorMessage);
                    else
                        view.ClearValidationError(b.ViewPropertyName);
                }
            }

            foreach (var binding in bindings)
            {
                var localBinding = binding;
                var control = view.GetControl(localBinding.ViewPropertyName);
                if (control == null) continue;

                Action markAndRefresh = () =>
                {
                    touched.Add(localBinding.ViewPropertyName);
                    RefreshErrors();
                };

                if (control is DateTimePicker dtp)
                {
                    dtp.ValueChanged += (_, __) => markAndRefresh();
                }
                else if (control is DataGridView dgv)
                {
                    dgv.RowsAdded        += (_, __) => markAndRefresh();
                    dgv.RowsRemoved      += (_, __) => markAndRefresh();
                    dgv.CellValueChanged += (_, __) => markAndRefresh();
                }
                else
                {
                    control.TextChanged += (_, __) => markAndRefresh();
                }
            }

            return new ValidationSession<TModel>(view, bindings, validator, modelFactory, touched);
        }

        // ------------------------------------------------------------------ session

        private sealed class ValidationSession<TModel> : IValidationSession
        {
            private readonly IView              _view;
            private readonly List<BindingInfo>  _bindings;
            private readonly IValidator<TModel> _validator;
            private readonly Func<TModel>       _modelFactory;
            private readonly HashSet<string>    _touched;

            public ValidationSession(
                IView              view,
                List<BindingInfo>  bindings,
                IValidator<TModel> validator,
                Func<TModel>       modelFactory,
                HashSet<string>    touched)
            {
                _view         = view;
                _bindings     = bindings;
                _validator    = validator;
                _modelFactory = modelFactory;
                _touched      = touched;
            }

            public bool ValidateAll()
            {
                foreach (var b in _bindings)
                    _touched.Add(b.ViewPropertyName);

                var model  = _modelFactory();
                var result = _validator.Validate(model);

                foreach (var b in _bindings)
                {
                    var error = result.Errors
                        .FirstOrDefault(e => e.PropertyName == b.ModelPropertyName);

                    if (error != null)
                        _view.ShowValidationError(b.ViewPropertyName, error.ErrorMessage);
                    else
                        _view.ClearValidationError(b.ViewPropertyName);
                }

                return result.IsValid;
            }
        }
    }
}
