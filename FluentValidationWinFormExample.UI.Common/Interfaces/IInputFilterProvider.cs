using System.Text.RegularExpressions;

namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    /// <summary>
    /// Provides compiled regular expressions used by <see cref="IValidationBinder"/>
    /// to restrict keystroke input on text controls.
    ///
    /// The regex is matched against the full <em>projected</em> text — i.e. what the
    /// field would contain after the keystroke is applied — so a single pattern can
    /// enforce all positional rules (leading minus, single decimal separator, digit cap)
    /// without per-character branching in the binder.
    /// </summary>
    public interface IInputFilterProvider
    {
        /// <summary>
        /// Returns the regex for the given filter, or <c>null</c> when no restriction
        /// applies (<see cref="InputFilter.None"/>).
        /// </summary>
        /// <param name="filter">The filter mode declared on the view property.</param>
        /// <param name="maxDecimalPlaces">
        /// For <see cref="InputFilter.DecimalOnly"/>, the maximum number of digits
        /// permitted after the decimal separator. 0 means no cap.
        /// Ignored for all other filter modes.
        /// </param>
        Regex GetRegex(InputFilter filter, int maxDecimalPlaces);
    }
}
