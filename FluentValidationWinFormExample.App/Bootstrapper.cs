using FluentValidation;
using FluentValidationWinFormExample.InputFilters;
using FluentValidationWinFormExample.Models;
using FluentValidationWinFormExample.UI;
using FluentValidationWinFormExample.UI.Common;
using FluentValidationWinFormExample.UI.Common.Interfaces;
using FluentValidationWinFormExample.UI.Interfaces;
using FluentValidationWinFormExample.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace FluentValidationWinFormExample.App
{
    public static class Bootstrapper
    {
        /// <summary>
        /// Builds and returns a configured <see cref="ServiceProvider"/>.
        ///
        /// Registration notes:
        ///   • <see cref="SampleModelForm"/> is Scoped so that both
        ///     <see cref="ISampleModelView"/> and the concrete form resolve to the
        ///     same instance within a single scope (needed by Application.Run).
        ///   • <see cref="ValidationBinder"/> is Singleton — it is stateless and safe
        ///     to share across all presenters.
        ///   • <see cref="SampleModelPresenter"/> is Scoped to match the view scope.
        /// </summary>
        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // --- Input filters ---
            services.AddSingleton<IInputFilterProvider, InputFilterProvider>();
            services.AddSingleton<IInputFilterBinder, InputFilterBinder>();

            // --- Validators ---
            services.AddTransient<IValidator<SampleModel>, SampleModelValidator>();

            // --- UI Common ---
            services.AddSingleton<IValidationBinder, ValidationBinder>();

            // --- UI (scoped so view and presenter share the same form instance) ---
            services.AddScoped<SampleModelForm>();
            services.AddScoped<ISampleModelView>(p => p.GetRequiredService<SampleModelForm>());
            services.AddScoped<ISampleModelPresenter, SampleModelPresenter>();

            return services.BuildServiceProvider();
        }
    }
}
