using FluentValidationWinFormExample.Models;

namespace FluentValidationWinFormExample.Validators
{
    /// <summary>
    /// Holds <see cref="OptionsValidationRules"/> in memory for the lifetime of the
    /// application. Swap this for a JSON- or DB-backed implementation without touching
    /// any other class.
    /// </summary>
    public class InMemoryOptionsRulesProvider : IOptionsRulesProvider
    {
        private OptionsValidationRules _rules = new OptionsValidationRules();

        public OptionsValidationRules GetRules() => _rules;

        public void SaveRules(OptionsValidationRules rules)
        {
            _rules = rules ?? new OptionsValidationRules();
        }
    }
}
