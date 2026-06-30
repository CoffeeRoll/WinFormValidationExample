namespace FluentValidationWinFormExample.Models
{
    /// <summary>
    /// Supplies and persists the runtime-configurable validation rules for
    /// <see cref="SampleModel.Options"/>.  Implementations can back this with
    /// in-memory state, a JSON file, a database, etc. without changing the
    /// validator or the UI layer.
    /// </summary>
    public interface IOptionsRulesProvider
    {
        OptionsValidationRules GetRules();
        void SaveRules(OptionsValidationRules rules);
    }
}
