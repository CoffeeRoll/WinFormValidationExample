using System;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Marks a view interface property as bound to a model property for validation.
    /// The binder discovers this attribute via reflection and wires up change events
    /// and error display for the associated control.
    ///
    /// When the model property has the same name as the view property (the common
    /// case) use the parameterless constructor; only pass a name when they differ.
    /// Nested model properties are addressed with a dotted path
    /// (e.g. <c>[ValidatesProperty("SubModel.MaidenName")]</c>) and match the
    /// property names FluentValidation reports for child-validator errors.
    ///
    /// To restrict what characters the user can type, add
    /// <see cref="InputFilterAttribute"/> to the same property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatesPropertyAttribute : Attribute
    {
        /// <summary>
        /// Name of the property on the model that this view property maps to,
        /// or null to use the view property's own name.
        /// </summary>
        public string ModelPropertyName { get; }

        /// <summary>Binds to the model property with the same name as the view property.</summary>
        public ValidatesPropertyAttribute()
        {
        }

        public ValidatesPropertyAttribute(string modelPropertyName)
        {
            if (string.IsNullOrWhiteSpace(modelPropertyName))
            {
                throw new ArgumentNullException(nameof(modelPropertyName));
            }

            ModelPropertyName = modelPropertyName;
        }
    }
}
