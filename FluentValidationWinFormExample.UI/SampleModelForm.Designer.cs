namespace FluentValidationWinFormExample.UI
{
    partial class SampleModelForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpPersonal         = new System.Windows.Forms.GroupBox();
            this.lblName             = new System.Windows.Forms.Label();
            this.txtName             = new System.Windows.Forms.TextBox();
            this.lblEmail            = new System.Windows.Forms.Label();
            this.txtEmail            = new System.Windows.Forms.TextBox();
            this.lblAge              = new System.Windows.Forms.Label();
            this.txtAge              = new System.Windows.Forms.TextBox();
            this.lblIncome           = new System.Windows.Forms.Label();
            this.txtIncome           = new System.Windows.Forms.TextBox();
            this.lblDateOfBirth      = new System.Windows.Forms.Label();
            this.dtpDateOfBirth      = new System.Windows.Forms.DateTimePicker();
            this.lblDateOfGrad       = new System.Windows.Forms.Label();
            this.dtpDateOfGraduation = new System.Windows.Forms.DateTimePicker();
            this.grpOptions          = new System.Windows.Forms.GroupBox();
            this.dgvOptions          = new System.Windows.Forms.DataGridView();
            this.colTitle            = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddOption        = new System.Windows.Forms.Button();
            this.btnRemoveOption     = new System.Windows.Forms.Button();
            this.btnSubmit           = new System.Windows.Forms.Button();

            this.grpPersonal.SuspendLayout();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptions)).BeginInit();
            this.SuspendLayout();

            // ---- grpPersonal ----
            this.grpPersonal.Location = new System.Drawing.Point(12, 12);
            this.grpPersonal.Name     = "grpPersonal";
            this.grpPersonal.Size     = new System.Drawing.Size(460, 210);
            this.grpPersonal.TabIndex = 0;
            this.grpPersonal.Text     = "Personal Details";
            this.grpPersonal.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblName, this.txtName,
                this.lblEmail, this.txtEmail,
                this.lblAge, this.txtAge,
                this.lblIncome, this.txtIncome,
                this.lblDateOfBirth, this.dtpDateOfBirth,
                this.lblDateOfGrad, this.dtpDateOfGraduation
            });

            int lx = 12, tx = 130, tw = 300, rh = 28;

            // Name
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(lx, 26);
            this.lblName.Text     = "Name:";
            this.txtName.Location = new System.Drawing.Point(tx, 23);
            this.txtName.Size     = new System.Drawing.Size(tw, 20);
            this.txtName.Name     = "txtName";

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(lx, 26 + rh);
            this.lblEmail.Text     = "Email:";
            this.txtEmail.Location = new System.Drawing.Point(tx, 23 + rh);
            this.txtEmail.Size     = new System.Drawing.Size(tw, 20);
            this.txtEmail.Name     = "txtEmail";

            // Age
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(lx, 26 + rh * 2);
            this.lblAge.Text     = "Age:";
            this.txtAge.Location = new System.Drawing.Point(tx, 23 + rh * 2);
            this.txtAge.Size     = new System.Drawing.Size(80, 20);
            this.txtAge.Name     = "txtAge";

            // Income
            this.lblIncome.AutoSize = true;
            this.lblIncome.Location = new System.Drawing.Point(lx, 26 + rh * 3);
            this.lblIncome.Text     = "Income:";
            this.txtIncome.Location = new System.Drawing.Point(tx, 23 + rh * 3);
            this.txtIncome.Size     = new System.Drawing.Size(120, 20);
            this.txtIncome.Name     = "txtIncome";

            // Date of Birth
            this.lblDateOfBirth.AutoSize = true;
            this.lblDateOfBirth.Location = new System.Drawing.Point(lx, 26 + rh * 4);
            this.lblDateOfBirth.Text     = "Date of Birth:";
            this.dtpDateOfBirth.Location = new System.Drawing.Point(tx, 23 + rh * 4);
            this.dtpDateOfBirth.Size     = new System.Drawing.Size(180, 20);
            this.dtpDateOfBirth.Name     = "dtpDateOfBirth";
            this.dtpDateOfBirth.Format   = System.Windows.Forms.DateTimePickerFormat.Short;

            // Date of Graduation
            this.lblDateOfGrad.AutoSize = true;
            this.lblDateOfGrad.Location = new System.Drawing.Point(lx, 26 + rh * 5);
            this.lblDateOfGrad.Text     = "Date of Graduation:";
            this.dtpDateOfGraduation.Location = new System.Drawing.Point(tx, 23 + rh * 5);
            this.dtpDateOfGraduation.Size     = new System.Drawing.Size(180, 20);
            this.dtpDateOfGraduation.Name     = "dtpDateOfGraduation";
            this.dtpDateOfGraduation.Format   = System.Windows.Forms.DateTimePickerFormat.Short;

            // ---- grpOptions ----
            this.grpOptions.Location = new System.Drawing.Point(12, 232);
            this.grpOptions.Name     = "grpOptions";
            this.grpOptions.Size     = new System.Drawing.Size(460, 160);
            this.grpOptions.TabIndex = 1;
            this.grpOptions.Text     = "Options (enter manually)";
            this.grpOptions.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvOptions, this.btnAddOption, this.btnRemoveOption
            });

            // DataGridView
            this.dgvOptions.Location            = new System.Drawing.Point(12, 20);
            this.dgvOptions.Name                = "dgvOptions";
            this.dgvOptions.Size                = new System.Drawing.Size(360, 128);
            this.dgvOptions.TabIndex            = 0;
            this.dgvOptions.AllowUserToAddRows  = false;  // Managed via Add button
            this.dgvOptions.RowHeadersVisible   = false;
            this.dgvOptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOptions.SelectionMode       = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colTitle, this.colDescription
            });

            this.colTitle.Name       = "colTitle";
            this.colTitle.HeaderText = "Title";
            this.colTitle.FillWeight = 40;

            this.colDescription.Name       = "colDescription";
            this.colDescription.HeaderText = "Description";
            this.colDescription.FillWeight = 60;

            // Add button
            this.btnAddOption.Location = new System.Drawing.Point(382, 20);
            this.btnAddOption.Name     = "btnAddOption";
            this.btnAddOption.Size     = new System.Drawing.Size(68, 26);
            this.btnAddOption.Text     = "Add";

            // Remove button
            this.btnRemoveOption.Location = new System.Drawing.Point(382, 52);
            this.btnRemoveOption.Name     = "btnRemoveOption";
            this.btnRemoveOption.Size     = new System.Drawing.Size(68, 26);
            this.btnRemoveOption.Text     = "Remove";

            // ---- btnSubmit ----
            // grpOptions bottom = 232 + 160 = 392; submit sits 12px below.
            this.btnSubmit.Location = new System.Drawing.Point(397, 404);
            this.btnSubmit.Name     = "btnSubmit";
            this.btnSubmit.Size     = new System.Drawing.Size(75, 26);
            this.btnSubmit.Text     = "Submit";

            // ---- SampleModelForm ----
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(484, 442);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox         = false;
            this.Name                = "SampleModelForm";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "Sample Model";
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.grpPersonal,
                this.grpOptions,
                this.btnSubmit
            });

            this.grpPersonal.ResumeLayout(false);
            this.grpPersonal.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOptions)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox   grpPersonal;
        private System.Windows.Forms.Label      lblName;
        private System.Windows.Forms.TextBox    txtName;
        private System.Windows.Forms.Label      lblEmail;
        private System.Windows.Forms.TextBox    txtEmail;
        private System.Windows.Forms.Label      lblAge;
        private System.Windows.Forms.TextBox    txtAge;
        private System.Windows.Forms.Label      lblIncome;
        private System.Windows.Forms.TextBox    txtIncome;
        private System.Windows.Forms.Label      lblDateOfBirth;
        private System.Windows.Forms.DateTimePicker dtpDateOfBirth;
        private System.Windows.Forms.Label      lblDateOfGrad;
        private System.Windows.Forms.DateTimePicker dtpDateOfGraduation;

        private System.Windows.Forms.GroupBox   grpOptions;
        private System.Windows.Forms.DataGridView dgvOptions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.Button     btnAddOption;
        private System.Windows.Forms.Button     btnRemoveOption;

        private System.Windows.Forms.Button     btnSubmit;
    }
}
