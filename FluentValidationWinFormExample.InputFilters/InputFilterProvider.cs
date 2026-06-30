using FluentValidationWinFormExample.UI.Common;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FluentValidationWinFormExample.InputFilters
{
    /// <summary>
    /// Provides compiled regular expressions for each <see cref="InputFilter"/> mode.
    ///
    /// Each regex is matched against the full <em>projected</em> text (what the field
    /// will contain after the keystroke), so a single pattern encapsulates all
    /// positional constraints:
    ///
    /// <list type="table">
    ///   <item>
    ///     <term><see cref="InputFilter.IntegerOnly"/></term>
    ///     <description><c>^-?\d*$</c> — optional leading minus, then digits only.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="InputFilter.DecimalOnly"/></term>
    ///     <description>
    ///       <c>^-?\d*\.?\d*$</c> (uncapped) or <c>^-?\d*\.?\d{0,N}$</c> when
    ///       <c>maxDecimalPlaces</c> is set — enforces a single separator and the digit cap.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="InputFilter.AlphanumericWithAt"/></term>
    ///     <description><c>^[A-Za-z0-9@.]*$</c> — letters, digits, @ and . only.</description>
    ///   </item>
    /// </list>
    /// </summary>
    public class InputFilterProvider : IInputFilterProvider
    {
        private static readonly Regex IntegerRegex =
            new Regex(@"^-?\d*$", RegexOptions.Compiled);

        private static readonly Regex DecimalRegex =
            new Regex(@"^-?\d*\.?\d*$", RegexOptions.Compiled);

        private static readonly Regex AlphanumericWithAtRegex =
            new Regex(@"^[A-Za-z0-9@.]*$", RegexOptions.Compiled);

        // Decimal regexes with a specific places cap are built on first use and cached.
        private readonly Dictionary<int, Regex> _cappedDecimalCache = new Dictionary<int, Regex>();

        /// <inheritdoc/>
        public Regex GetRegex(InputFilter filter, int maxDecimalPlaces)
        {
            switch (filter)
            {
                case InputFilter.IntegerOnly:
                    return IntegerRegex;

                case InputFilter.DecimalOnly:
                    return maxDecimalPlaces > 0
                        ? GetCappedDecimalRegex(maxDecimalPlaces)
                        : DecimalRegex;

                case InputFilter.AlphanumericWithAt:
                    return AlphanumericWithAtRegex;

                default:
                    return null;
            }
        }

        private Regex GetCappedDecimalRegex(int maxDecimalPlaces)
        {
            if (!_cappedDecimalCache.TryGetValue(maxDecimalPlaces, out Regex regex))
            {
                regex = new Regex(
                    $@"^-?\d*\.?\d{{0,{maxDecimalPlaces}}}$",
                    RegexOptions.Compiled);

                _cappedDecimalCache[maxDecimalPlaces] = regex;
            }
            return regex;
        }
    }
}
