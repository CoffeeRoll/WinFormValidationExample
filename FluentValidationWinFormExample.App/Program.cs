using FluentValidationWinFormExample.UI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace FluentValidationWinFormExample.App
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var provider = Bootstrapper.BuildServiceProvider())
            using (var scope = provider.CreateScope())
            {
                // Resolving the presenter is sufficient — DI injects ISampleModelView
                // (which maps to the scoped SampleModelForm) automatically.
                // The view is then retrieved from the presenter, avoiding a second
                // container resolution.
                var presenter = scope.ServiceProvider.GetRequiredService<ISampleModelPresenter>();
                presenter.Initialize();

                Application.Run(presenter.View as Form);
            }
        }
    }
}
