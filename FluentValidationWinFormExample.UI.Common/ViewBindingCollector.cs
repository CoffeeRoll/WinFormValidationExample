using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Shared helper that reflects over a view interface and returns one
    /// <see cref="BindingInfo"/> per property tagged with
    /// <see cref="ValidatesPropertyAttribute"/>.
    ///
    /// Both <see cref="InputFilterBinder"/> and <see cref="ValidationBinder"/>
    /// call this so the reflection work is defined in one place.
    /// </summary>
    internal static class ViewBindingCollector
    {
        internal static List<BindingInfo> Collect(Type viewType)
        {
            var interfaceTypes = viewType.IsInterface
                ? new[] { viewType }.Concat(viewType.GetInterfaces())
                : viewType.GetInterfaces();

            var seen = new HashSet<string>(StringComparer.Ordinal);
            var result = new List<BindingInfo>();

            foreach (var iface in interfaceTypes)
            {
                foreach (var prop in iface.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (!seen.Add(prop.Name))
                    {
                        continue;
                    }

                    var validatesAttr = prop.GetCustomAttribute<ValidatesPropertyAttribute>();
                    if (validatesAttr == null)
                    {
                        continue;
                    }

                    var filterAttr = prop.GetCustomAttribute<InputFilterAttribute>();

                    result.Add(new BindingInfo
                    {
                        ViewPropertyName = prop.Name,
                        // Null means "same name as the view property".
                        ModelPropertyName = validatesAttr.ModelPropertyName ?? prop.Name,
                        InputFilter = filterAttr?.Filter ?? InputFilter.None,
                        MaxDecimalPlaces = filterAttr?.MaxDecimalPlaces ?? 0
                    });
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Holds the reflected metadata for a single bound view property.
    /// Internal to UI.Common; shared between <see cref="InputFilterBinder"/>
    /// and <see cref="ValidationBinder"/>.
    /// </summary>
    internal sealed class BindingInfo
    {
        public string ViewPropertyName { get; set; }
        public string ModelPropertyName { get; set; }
        public InputFilter InputFilter { get; set; }
        public int MaxDecimalPlaces { get; set; }
    }
}
