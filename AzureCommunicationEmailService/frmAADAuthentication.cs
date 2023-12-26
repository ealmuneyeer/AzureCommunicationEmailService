using AzureCommunicationEmailService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCommunicationEmailService
{
    public partial class frmAADAuthentication : Form
    {
        private Helpers.CredentialsSource _valueSource;
        private AADCredentials _credentials;

        public frmAADAuthentication(Helpers.CredentialsSource source, AADCredentials credentials)
        {
            InitializeComponent();

            _valueSource = source;
            _credentials = credentials;
        }

        private void frmAADAuthentication_Load(object sender, EventArgs e)
        {
            txtClientID.Text = _credentials.ClientId;
            txtTenantID.Text = _credentials.TenantId;
            txtClientSecret.Text = _credentials.ClientSecret;

            if (_valueSource == Helpers.CredentialsSource.EnvironmentVariables)
            {
                txtClientID.ReadOnly = txtClientSecret.ReadOnly = txtTenantID.ReadOnly = true;
                btnSave.Enabled = false;
            }
        }

        private void btnShowSecrets_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "Show")
            {
                button.Text = "Hide";
                txtClientSecret.UseSystemPasswordChar = txtClientID.UseSystemPasswordChar = false;
            }
            else
            {
                button.Text = "Show";
                txtClientSecret.UseSystemPasswordChar = txtClientID.UseSystemPasswordChar = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_valueSource == Helpers.CredentialsSource.AppSettings)
            {
                Helpers.UpdateClientCredentials(txtTenantID.Text, txtClientID.Text, txtClientSecret.Text);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
