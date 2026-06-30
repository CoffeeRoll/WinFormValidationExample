using System;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Applies character-level input restrictions to the control bound by
    /// <see cref="ValidatesPropertyAttribute"/> on the same property.
    ///
    /// The binder enforces the filter via the control's KeyPress event, blocking
    /// characters that do not satisfy the chosen <see cref="InputFilter"/> mode.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class InputFilterAttribute : Attribute
    {
        /// <summary>The character-level filter to apply.</summary>
        public InputFilter Filter { get; }

        /// <summary>
        /// For <see cref="InputFilter.DecimalOnly"/> controls, caps the number of digits
        /// that may appear after the decimal separator. 0 (default) means no cap.
        /// </summary>
        public int MaxDecimalPlaces { get; set; } = 0;

        public InputFilterAttribute(InputFilter filter)
        {
            Filter = filter;
        }
    }
}
