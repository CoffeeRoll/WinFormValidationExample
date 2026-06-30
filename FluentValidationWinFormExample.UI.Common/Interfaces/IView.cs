using System.Windows.Forms;

namespace FluentValidationWinFormExample.UI.Common.Interfaces
{
    public interface IView
    {
        Control GetControl(string viewPropertyName);
        void ShowValidationError(string viewPropertyName, string message);
        void ClearValidationError(string viewPropertyName);
        bool IsDisposed { get; }
    }
}
