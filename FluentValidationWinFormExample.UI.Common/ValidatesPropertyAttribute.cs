using System;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Marks a view interface property as bound to a model property for validation.
    /// The binder discovers this attribute via reflection and wires up change events
    /// and error display for the associated control.
    ///
    /// To restrict what characters the user can type, add
    /// <see cref="InputFilterAttribute"/> to the same property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatesPropertyAttribute : Attribute
    {
        /// <summary>Name of the property on the model that this view property maps to.</summary>
        public string ModelPropertyName { get; }

        public ValidatesPropertyAttribute(string modelPropertyName)
        {
            if (string.IsNullOrWhiteSpace(modelPropertyName))
                throw new ArgumentNullException(nameof(modelPropertyName));

            ModelPropertyName = modelPropertyName;
        }
    }
}
