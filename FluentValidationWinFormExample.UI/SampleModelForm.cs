using FluentValidationWinFormExample.Models;
using FluentValidationWinFormExample.UI.Common;
using FluentValidationWinFormExample.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FluentValidationWinFormExample.UI
{
    /// <summary>
    /// WinForms view for <see cref="SampleModel"/>.
    ///
    /// Derives from <see cref="ViewBase"/> which supplies the <c>ErrorProvider</c>
    /// and the <c>RegisterControl</c> / <c>GetControl</c> plumbing.  Every bound
    /// view property is registered explicitly below, so the property-to-control
    /// mapping is visible in one place.
    ///
    /// All <see cref="ISampleModelView"/> members that clash with inherited
    /// Form/Control names (e.g. <c>Name</c>) are implemented explicitly.
    /// </summary>
    public partial class SampleModelForm : ViewBase, ISampleModelView
    {
        public SampleModelForm()
        {
            InitializeComponent();

            // Register every validated control under its view-interface property name.
            // The binder uses these mappings to hook up change events and the
            // ErrorProvider uses them to display error icons next to the right control.
            RegisterControl(nameof(ISampleModelView.Name),             txtName);
            RegisterControl(nameof(ISampleModelView.Email),            txtEmail);
            RegisterControl(nameof(ISampleModelView.Age),              txtAge);
            RegisterControl(nameof(ISampleModelView.Income),           txtIncome);
            RegisterControl(nameof(ISampleModelView.MaidenName),       txtMaidenName);
            RegisterControl(nameof(ISampleModelView.Address),          txtAddress);
            RegisterControl(nameof(ISampleModelView.DateOfBirth),      dtpDateOfBirth);
            RegisterControl(nameof(ISampleModelView.DateOfGraduation), dtpDateOfGraduation);
            RegisterControl(nameof(ISampleModelView.Options),          dgvOptions);

            btnAddOption.Click    += BtnAddOption_Click;
            btnRemoveOption.Click += BtnRemoveOption_Click;
            btnSubmit.Click       += BtnSubmit_Click;
        }

        // ---- ISampleModelView: text fields ----

        string ISampleModelView.Name   => txtName.Text;
        string ISampleModelView.Email  => txtEmail.Text;
        string ISampleModelView.Age    => txtAge.Text;
        string ISampleModelView.Income => txtIncome.Text;

        // ---- ISampleModelView: nested SubModel fields ----

        string ISampleModelView.MaidenName => txtMaidenName.Text;
        string ISampleModelView.Address    => txtAddress.Text;

        // ---- ISampleModelView: date fields ----

        // Strip the time component so two pickers on the same calendar date
        // are always treated as equal, regardless of DateTime.Now initialization order.
        DateTime ISampleModelView.DateOfBirth      => dtpDateOfBirth.Value.Date;
        DateTime ISampleModelView.DateOfGraduation => dtpDateOfGraduation.Value.Date;

        // ---- ISampleModelView: options collection ----

        IEnumerable<OptionModel> ISampleModelView.Options => ReadOptionsFromGrid();

        private IEnumerable<OptionModel> ReadOptionsFromGrid()
        {
            var result = new List<OptionModel>();
            foreach (DataGridViewRow row in dgvOptions.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                result.Add(new OptionModel
                {
                    Title       = row.Cells[colTitle.Name].Value?.ToString()       ?? string.Empty,
                    Description = row.Cells[colDescription.Name].Value?.ToString() ?? string.Empty
                });
            }
            return result;
        }

        // ---- ISubmitView ----

        public event EventHandler SubmitRequested;

        public void SetSubmitEnabled(bool enabled)
        {
            btnSubmit.Enabled = enabled;
        }

        public void ShowSubmitSuccess()
        {
            MessageBox.Show(
                "The form was submitted successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ---- button handlers ----

        private void BtnAddOption_Click(object sender, EventArgs e)
        {
            int newIndex = dgvOptions.Rows.Add(string.Empty, string.Empty);
            dgvOptions.CurrentCell = dgvOptions.Rows[newIndex].Cells[colTitle.Name];
            dgvOptions.BeginEdit(true);
        }

        private void BtnRemoveOption_Click(object sender, EventArgs e)
        {
            if (dgvOptions.CurrentRow != null && !dgvOptions.CurrentRow.IsNewRow)
            {
                dgvOptions.Rows.Remove(dgvOptions.CurrentRow);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            SubmitRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
