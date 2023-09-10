namespace AzureCommunicationEmailService
{
    partial class frmAADAuthentication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtTenantID = new TextBox();
            txtClientID = new TextBox();
            txtClientSecret = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            btnShowSecrets = new Button();
            lblEnvVariablesNote = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 12);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Tenant ID: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 39);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 1;
            label2.Text = "Client ID: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 64);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 2;
            label3.Text = "Client Secret: ";
            // 
            // txtTenantID
            // 
            txtTenantID.Location = new Point(99, 9);
            txtTenantID.Margin = new Padding(3, 2, 3, 2);
            txtTenantID.Name = "txtTenantID";
            txtTenantID.Size = new Size(345, 23);
            txtTenantID.TabIndex = 3;
            // 
            // txtClientID
            // 
            txtClientID.Location = new Point(99, 34);
            txtClientID.Margin = new Padding(3, 2, 3, 2);
            txtClientID.Name = "txtClientID";
            txtClientID.Size = new Size(345, 23);
            txtClientID.TabIndex = 4;
            txtClientID.UseSystemPasswordChar = true;
            // 
            // txtClientSecret
            // 
            txtClientSecret.Location = new Point(99, 59);
            txtClientSecret.Margin = new Padding(3, 2, 3, 2);
            txtClientSecret.Name = "txtClientSecret";
            txtClientSecret.Size = new Size(345, 23);
            txtClientSecret.TabIndex = 5;
            txtClientSecret.UseSystemPasswordChar = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(361, 87);
            btnSave.Margin = new Padding(3, 2, 3, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(82, 22);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(449, 87);
            btnCancel.Margin = new Padding(3, 2, 3, 2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(82, 22);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnShowSecrets
            // 
            btnShowSecrets.Location = new Point(449, 32);
            btnShowSecrets.Margin = new Padding(3, 2, 3, 2);
            btnShowSecrets.Name = "btnShowSecrets";
            btnShowSecrets.Size = new Size(82, 47);
            btnShowSecrets.TabIndex = 6;
            btnShowSecrets.Text = "Show";
            btnShowSecrets.UseVisualStyleBackColor = true;
            btnShowSecrets.Click += btnShowSecrets_Click;
            // 
            // lblEnvVariablesNote
            // 
            lblEnvVariablesNote.AutoSize = true;
            lblEnvVariablesNote.Location = new Point(7, -3);
            lblEnvVariablesNote.Name = "lblEnvVariablesNote";
            lblEnvVariablesNote.Size = new Size(390, 15);
            lblEnvVariablesNote.TabIndex = 11;
            lblEnvVariablesNote.Text = "Environment variables can be modified in advanced system settings only";
            lblEnvVariablesNote.Visible = false;
            // 
            // frmAADAuthentication
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 118);
            ControlBox = false;
            Controls.Add(lblEnvVariablesNote);
            Controls.Add(btnShowSecrets);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtClientSecret);
            Controls.Add(txtClientID);
            Controls.Add(txtTenantID);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmAADAuthentication";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AAD Authentication Configuration";
            Load += frmAADAuthentication_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtTenantID;
        private TextBox txtClientID;
        private TextBox txtClientSecret;
        private Button btnSave;
        private Button btnCancel;
        private Button btnShowSecrets;
        private Label lblEnvVariablesNote;
    }
}