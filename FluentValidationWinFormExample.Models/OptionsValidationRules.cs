namespace FluentValidationWinFormExample.Models
{
    /// <summary>
    /// User-configurable rules that govern the <see cref="SampleModel.Options"/> collection.
    /// Stored and mutated at runtime via <see cref="IOptionsRulesProvider"/>.
    /// The maximum-count cap (5) is a fixed rule in the validator and is not exposed here.
    /// </summary>
    public class OptionsValidationRules
    {
        /// <summary>When true, every option must have a distinct Title. Default true.</summary>
        public bool RequireUniqueTitle { get; set; } = true;
    }
}
