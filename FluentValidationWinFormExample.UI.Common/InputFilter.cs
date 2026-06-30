namespace FluentValidationWinFormExample.UI.Common
{
    /// <summary>
    /// Specifies what character input a bound control will accept.
    /// </summary>
    public enum InputFilter
    {
        /// <summary>No filtering — all characters are accepted.</summary>
        None,

        /// <summary>Digits only, with an optional leading minus sign.</summary>
        IntegerOnly,

        /// <summary>Digits, one decimal point, and an optional leading minus sign.</summary>
        DecimalOnly,

        /// <summary>Alphanumeric characters and the @ symbol only.</summary>
        AlphanumericWithAt
    }
}
