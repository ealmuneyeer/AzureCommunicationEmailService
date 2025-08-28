namespace AzureCommunicationEmailService
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpAttachments = new GroupBox();
            lblAttachmentsSize = new Label();
            label4 = new Label();
            label3 = new Label();
            lblAttachmentsCount = new Label();
            dgAttachments = new DataGridView();
            attachFilePath = new DataGridViewTextBoxColumn();
            attachMimeType = new DataGridViewTextBoxColumn();
            attachSize = new DataGridViewTextBoxColumn();
            attachCID = new DataGridViewTextBoxColumn();
            attachInline = new DataGridViewCheckBoxColumn();
            btnAddAttachment = new Button();
            chkIsHtmlBody = new CheckBox();
            txtFrom = new TextBox();
            label5 = new Label();
            dgReceipeints = new DataGridView();
            recEmailAddress = new DataGridViewTextBoxColumn();
            recDisplayName = new DataGridViewTextBoxColumn();
            recType = new DataGridViewComboBoxColumn();
            txtBody = new TextBox();
            txtSubject = new TextBox();
            label2 = new Label();
            grpTrace = new GroupBox();
            txtTrace = new TextBox();
            grpBody = new GroupBox();
            btnGetBase64String = new Button();
            grpReceipents = new GroupBox();
            label6 = new Label();
            cmbImportance = new ComboBox();
            grpCustomHeders = new GroupBox();
            dgCustomHeaders = new DataGridView();
            colHeaderName = new DataGridViewTextBoxColumn();
            colHeaderValue = new DataGridViewTextBoxColumn();
            grpMessageDeliveryStatus = new GroupBox();
            btnGetMsgDeliveryStatus = new Button();
            txtMessageID = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label11 = new Label();
            cmbAuthType = new ComboBox();
            cmbClientType = new ComboBox();
            label9 = new Label();
            cmbSendWaitUntil = new ComboBox();
            label10 = new Label();
            numEmailsToSend = new NumericUpDown();
            panel1 = new Panel();
            btnSendEmail = new Button();
            panel2 = new Panel();
            btnStopSendingEmail = new Button();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabConfiguration = new TabPage();
            grpEventHandling = new GroupBox();
            btnServiceBusStopMonitoring = new Button();
            btnServiceBusStartMonitoring = new Button();
            pnlServiceBusConfig = new Panel();
            btnShowServiceBusConnString = new Button();
            txtServiceBusQueueName = new TextBox();
            txtServiceBusConnectionString = new TextBox();
            label27 = new Label();
            label26 = new Label();
            grpInitializeEmailClients = new GroupBox();
            pnlInitializeEmailClients = new Panel();
            dgEmailClientConfig = new DataGridView();
            ColName = new DataGridViewTextBoxColumn();
            ColValue = new DataGridViewTextBoxColumn();
            ColNotes = new DataGridViewTextBoxColumn();
            btnInitializeEmailClients = new Button();
            btnResetEmailClients = new Button();
            tabEmailOperations = new TabPage();
            grpSendEmail = new GroupBox();
            grpAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgAttachments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgReceipeints).BeginInit();
            grpTrace.SuspendLayout();
            grpBody.SuspendLayout();
            grpReceipents.SuspendLayout();
            grpCustomHeders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgCustomHeaders).BeginInit();
            grpMessageDeliveryStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEmailsToSend).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabConfiguration.SuspendLayout();
            grpEventHandling.SuspendLayout();
            pnlServiceBusConfig.SuspendLayout();
            grpInitializeEmailClients.SuspendLayout();
            pnlInitializeEmailClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgEmailClientConfig).BeginInit();
            tabEmailOperations.SuspendLayout();
            grpSendEmail.SuspendLayout();
            SuspendLayout();
            // 
            // grpAttachments
            // 
            grpAttachments.Controls.Add(lblAttachmentsSize);
            grpAttachments.Controls.Add(label4);
            grpAttachments.Controls.Add(label3);
            grpAttachments.Controls.Add(lblAttachmentsCount);
            grpAttachments.Controls.Add(dgAttachments);
            grpAttachments.Controls.Add(btnAddAttachment);
            grpAttachments.Location = new Point(6, 498);
            grpAttachments.Margin = new Padding(3, 2, 3, 2);
            grpAttachments.Name = "grpAttachments";
            grpAttachments.Padding = new Padding(3, 2, 3, 2);
            grpAttachments.Size = new Size(724, 151);
            grpAttachments.TabIndex = 5;
            grpAttachments.TabStop = false;
            grpAttachments.Text = "Attachments";
            // 
            // lblAttachmentsSize
            // 
            lblAttachmentsSize.AutoSize = true;
            lblAttachmentsSize.Location = new Point(402, 23);
            lblAttachmentsSize.Name = "lblAttachmentsSize";
            lblAttachmentsSize.Size = new Size(13, 15);
            lblAttachmentsSize.TabIndex = 15;
            lblAttachmentsSize.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(304, 22);
            label4.Name = "label4";
            label4.Size = new Size(91, 15);
            label4.TabIndex = 14;
            label4.Text = "Total size (Byte):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(125, 22);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 13;
            label3.Text = "Total count:";
            // 
            // lblAttachmentsCount
            // 
            lblAttachmentsCount.AutoSize = true;
            lblAttachmentsCount.Location = new Point(197, 22);
            lblAttachmentsCount.Name = "lblAttachmentsCount";
            lblAttachmentsCount.Size = new Size(13, 15);
            lblAttachmentsCount.TabIndex = 12;
            lblAttachmentsCount.Text = "0";
            // 
            // dgAttachments
            // 
            dgAttachments.AllowUserToAddRows = false;
            dgAttachments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgAttachments.Columns.AddRange(new DataGridViewColumn[] { attachFilePath, attachMimeType, attachSize, attachCID, attachInline });
            dgAttachments.Location = new Point(5, 46);
            dgAttachments.Margin = new Padding(3, 2, 3, 2);
            dgAttachments.Name = "dgAttachments";
            dgAttachments.RowHeadersWidth = 51;
            dgAttachments.RowTemplate.Height = 29;
            dgAttachments.Size = new Size(713, 100);
            dgAttachments.TabIndex = 2;
            dgAttachments.RowsAdded += dgAttachments_RowsAdded;
            dgAttachments.RowsRemoved += dgAttachments_RowsRemoved;
            // 
            // attachFilePath
            // 
            attachFilePath.HeaderText = "File path";
            attachFilePath.MinimumWidth = 6;
            attachFilePath.Name = "attachFilePath";
            attachFilePath.ReadOnly = true;
            attachFilePath.Width = 286;
            // 
            // attachMimeType
            // 
            attachMimeType.HeaderText = "MIME type";
            attachMimeType.MinimumWidth = 6;
            attachMimeType.Name = "attachMimeType";
            attachMimeType.Width = 117;
            // 
            // attachSize
            // 
            attachSize.HeaderText = "Size (Byte)";
            attachSize.MinimumWidth = 6;
            attachSize.Name = "attachSize";
            attachSize.Width = 90;
            // 
            // attachCID
            // 
            attachCID.HeaderText = "Content Id";
            attachCID.Name = "attachCID";
            // 
            // attachInline
            // 
            attachInline.HeaderText = "Inline";
            attachInline.Name = "attachInline";
            attachInline.Width = 50;
            // 
            // btnAddAttachment
            // 
            btnAddAttachment.Location = new Point(5, 20);
            btnAddAttachment.Margin = new Padding(3, 2, 3, 2);
            btnAddAttachment.Name = "btnAddAttachment";
            btnAddAttachment.Size = new Size(115, 22);
            btnAddAttachment.TabIndex = 1;
            btnAddAttachment.Text = "Add attachment";
            btnAddAttachment.UseVisualStyleBackColor = true;
            btnAddAttachment.Click += btnAddAttachment_Click;
            // 
            // chkIsHtmlBody
            // 
            chkIsHtmlBody.AutoSize = true;
            chkIsHtmlBody.Location = new Point(649, 20);
            chkIsHtmlBody.Margin = new Padding(3, 2, 3, 2);
            chkIsHtmlBody.Name = "chkIsHtmlBody";
            chkIsHtmlBody.Size = new Size(69, 19);
            chkIsHtmlBody.TabIndex = 2;
            chkIsHtmlBody.Text = "Is HTML";
            chkIsHtmlBody.UseVisualStyleBackColor = true;
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(86, 4);
            txtFrom.Margin = new Padding(3, 2, 3, 2);
            txtFrom.Name = "txtFrom";
            txtFrom.Size = new Size(537, 23);
            txtFrom.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(36, 7);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 6;
            label5.Text = "From:";
            // 
            // dgReceipeints
            // 
            dgReceipeints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgReceipeints.Columns.AddRange(new DataGridViewColumn[] { recEmailAddress, recDisplayName, recType });
            dgReceipeints.Location = new Point(5, 20);
            dgReceipeints.Margin = new Padding(3, 2, 3, 2);
            dgReceipeints.Name = "dgReceipeints";
            dgReceipeints.RowHeadersWidth = 51;
            dgReceipeints.RowTemplate.Height = 29;
            dgReceipeints.Size = new Size(572, 100);
            dgReceipeints.TabIndex = 5;
            // 
            // recEmailAddress
            // 
            recEmailAddress.HeaderText = "Email Address";
            recEmailAddress.MinimumWidth = 6;
            recEmailAddress.Name = "recEmailAddress";
            recEmailAddress.Width = 267;
            // 
            // recDisplayName
            // 
            recDisplayName.HeaderText = "Display Name";
            recDisplayName.MinimumWidth = 6;
            recDisplayName.Name = "recDisplayName";
            recDisplayName.Width = 160;
            // 
            // recType
            // 
            recType.HeaderText = "Type";
            recType.Items.AddRange(new object[] { "To", "CC", "BCC", "Reply To" });
            recType.MinimumWidth = 6;
            recType.Name = "recType";
            recType.Width = 75;
            // 
            // txtBody
            // 
            txtBody.Location = new Point(5, 20);
            txtBody.Margin = new Padding(3, 2, 3, 2);
            txtBody.MaxLength = 10000000;
            txtBody.Multiline = true;
            txtBody.Name = "txtBody";
            txtBody.ScrollBars = ScrollBars.Vertical;
            txtBody.Size = new Size(638, 107);
            txtBody.TabIndex = 1;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(86, 31);
            txtSubject.Margin = new Padding(3, 2, 3, 2);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(537, 23);
            txtSubject.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(25, 34);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 0;
            label2.Text = "Subject:";
            // 
            // grpTrace
            // 
            grpTrace.Controls.Add(txtTrace);
            grpTrace.Location = new Point(770, 66);
            grpTrace.Margin = new Padding(3, 2, 3, 2);
            grpTrace.Name = "grpTrace";
            grpTrace.Padding = new Padding(3, 2, 3, 2);
            grpTrace.Size = new Size(656, 711);
            grpTrace.TabIndex = 2;
            grpTrace.TabStop = false;
            grpTrace.Text = "Trace";
            // 
            // txtTrace
            // 
            txtTrace.BackColor = SystemColors.Window;
            txtTrace.Location = new Point(6, 20);
            txtTrace.Margin = new Padding(3, 2, 3, 2);
            txtTrace.Multiline = true;
            txtTrace.Name = "txtTrace";
            txtTrace.ScrollBars = ScrollBars.Vertical;
            txtTrace.Size = new Size(644, 686);
            txtTrace.TabIndex = 0;
            // 
            // grpBody
            // 
            grpBody.Controls.Add(btnGetBase64String);
            grpBody.Controls.Add(chkIsHtmlBody);
            grpBody.Controls.Add(txtBody);
            grpBody.Location = new Point(6, 103);
            grpBody.Margin = new Padding(3, 2, 3, 2);
            grpBody.Name = "grpBody";
            grpBody.Padding = new Padding(3, 2, 3, 2);
            grpBody.Size = new Size(724, 133);
            grpBody.TabIndex = 2;
            grpBody.TabStop = false;
            grpBody.Text = "Body";
            // 
            // btnGetBase64String
            // 
            btnGetBase64String.Location = new Point(649, 44);
            btnGetBase64String.Name = "btnGetBase64String";
            btnGetBase64String.Size = new Size(69, 57);
            btnGetBase64String.TabIndex = 12;
            btnGetBase64String.Text = "Get Base64 string";
            btnGetBase64String.UseVisualStyleBackColor = true;
            btnGetBase64String.Click += btnGetBase64String_Click;
            // 
            // grpReceipents
            // 
            grpReceipents.Controls.Add(dgReceipeints);
            grpReceipents.Location = new Point(9, 240);
            grpReceipents.Margin = new Padding(3, 2, 3, 2);
            grpReceipents.Name = "grpReceipents";
            grpReceipents.Padding = new Padding(3, 2, 3, 2);
            grpReceipents.Size = new Size(724, 125);
            grpReceipents.TabIndex = 3;
            grpReceipents.TabStop = false;
            grpReceipents.Text = "Receipents";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 61);
            label6.Name = "label6";
            label6.Size = new Size(71, 15);
            label6.TabIndex = 15;
            label6.Text = "Importance:";
            // 
            // cmbImportance
            // 
            cmbImportance.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbImportance.FormattingEnabled = true;
            cmbImportance.ItemHeight = 15;
            cmbImportance.Items.AddRange(new object[] { "None", "1", "2", "3", "4", "5" });
            cmbImportance.Location = new Point(86, 58);
            cmbImportance.Margin = new Padding(3, 2, 3, 2);
            cmbImportance.Name = "cmbImportance";
            cmbImportance.Size = new Size(141, 23);
            cmbImportance.TabIndex = 4;
            // 
            // grpCustomHeders
            // 
            grpCustomHeders.Controls.Add(dgCustomHeaders);
            grpCustomHeders.Location = new Point(6, 369);
            grpCustomHeders.Margin = new Padding(3, 2, 3, 2);
            grpCustomHeders.Name = "grpCustomHeders";
            grpCustomHeders.Padding = new Padding(3, 2, 3, 2);
            grpCustomHeders.Size = new Size(724, 125);
            grpCustomHeders.TabIndex = 4;
            grpCustomHeders.TabStop = false;
            grpCustomHeders.Text = "Custom Headers";
            // 
            // dgCustomHeaders
            // 
            dgCustomHeaders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCustomHeaders.Columns.AddRange(new DataGridViewColumn[] { colHeaderName, colHeaderValue });
            dgCustomHeaders.Location = new Point(6, 20);
            dgCustomHeaders.Margin = new Padding(3, 2, 3, 2);
            dgCustomHeaders.Name = "dgCustomHeaders";
            dgCustomHeaders.RowHeadersWidth = 51;
            dgCustomHeaders.RowTemplate.Height = 29;
            dgCustomHeaders.Size = new Size(574, 100);
            dgCustomHeaders.TabIndex = 0;
            // 
            // colHeaderName
            // 
            colHeaderName.HeaderText = "Name";
            colHeaderName.MinimumWidth = 6;
            colHeaderName.Name = "colHeaderName";
            colHeaderName.Width = 252;
            // 
            // colHeaderValue
            // 
            colHeaderValue.HeaderText = "Value";
            colHeaderValue.MinimumWidth = 6;
            colHeaderValue.Name = "colHeaderValue";
            colHeaderValue.Width = 252;
            // 
            // grpMessageDeliveryStatus
            // 
            grpMessageDeliveryStatus.Controls.Add(btnGetMsgDeliveryStatus);
            grpMessageDeliveryStatus.Controls.Add(txtMessageID);
            grpMessageDeliveryStatus.Controls.Add(label7);
            grpMessageDeliveryStatus.Location = new Point(770, 11);
            grpMessageDeliveryStatus.Margin = new Padding(3, 2, 3, 2);
            grpMessageDeliveryStatus.Name = "grpMessageDeliveryStatus";
            grpMessageDeliveryStatus.Padding = new Padding(3, 2, 3, 2);
            grpMessageDeliveryStatus.Size = new Size(656, 50);
            grpMessageDeliveryStatus.TabIndex = 1;
            grpMessageDeliveryStatus.TabStop = false;
            grpMessageDeliveryStatus.Text = "Message Delivery Status";
            // 
            // btnGetMsgDeliveryStatus
            // 
            btnGetMsgDeliveryStatus.Location = new Point(503, 19);
            btnGetMsgDeliveryStatus.Margin = new Padding(3, 2, 3, 2);
            btnGetMsgDeliveryStatus.Name = "btnGetMsgDeliveryStatus";
            btnGetMsgDeliveryStatus.Size = new Size(150, 22);
            btnGetMsgDeliveryStatus.TabIndex = 2;
            btnGetMsgDeliveryStatus.Text = "Get Delivery Status";
            btnGetMsgDeliveryStatus.UseVisualStyleBackColor = true;
            btnGetMsgDeliveryStatus.Click += btnGetMsgDeliveryStatus_Click;
            // 
            // txtMessageID
            // 
            txtMessageID.Location = new Point(88, 20);
            txtMessageID.Margin = new Padding(3, 2, 3, 2);
            txtMessageID.Name = "txtMessageID";
            txtMessageID.Size = new Size(410, 23);
            txtMessageID.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 22);
            label7.Name = "label7";
            label7.Size = new Size(70, 15);
            label7.TabIndex = 0;
            label7.Text = "Message ID:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(233, 61);
            label8.Name = "label8";
            label8.Size = new Size(186, 15);
            label8.TabIndex = 18;
            label8.Text = "1 is the highest and 5 is the lowest";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(5, 3);
            label11.Name = "label11";
            label11.Size = new Size(116, 15);
            label11.TabIndex = 25;
            label11.Text = "Authentication Type:";
            // 
            // cmbAuthType
            // 
            cmbAuthType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthType.FormattingEnabled = true;
            cmbAuthType.Items.AddRange(new object[] { "ACS Key", "Entra ID Default Auth", "Entra ID Client Auth", "Interactive", "SMTP Username/Password" });
            cmbAuthType.Location = new Point(124, 0);
            cmbAuthType.Margin = new Padding(3, 2, 3, 2);
            cmbAuthType.Name = "cmbAuthType";
            cmbAuthType.Size = new Size(176, 23);
            cmbAuthType.TabIndex = 0;
            cmbAuthType.SelectedIndexChanged += cmbAuthType_SelectedIndexChanged;
            // 
            // cmbClientType
            // 
            cmbClientType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientType.FormattingEnabled = true;
            cmbClientType.Location = new Point(344, 21);
            cmbClientType.Name = "cmbClientType";
            cmbClientType.Size = new Size(121, 23);
            cmbClientType.TabIndex = 3;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(66, 9);
            label9.Name = "label9";
            label9.Size = new Size(88, 15);
            label9.TabIndex = 19;
            label9.Text = "Send wait until:";
            // 
            // cmbSendWaitUntil
            // 
            cmbSendWaitUntil.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSendWaitUntil.FormattingEnabled = true;
            cmbSendWaitUntil.ItemHeight = 15;
            cmbSendWaitUntil.Items.AddRange(new object[] { "Started", "Completed" });
            cmbSendWaitUntil.Location = new Point(160, 6);
            cmbSendWaitUntil.Name = "cmbSendWaitUntil";
            cmbSendWaitUntil.Size = new Size(100, 23);
            cmbSendWaitUntil.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(7, 39);
            label10.Name = "label10";
            label10.Size = new Size(147, 15);
            label10.TabIndex = 21;
            label10.Text = "Number of emails to send:";
            // 
            // numEmailsToSend
            // 
            numEmailsToSend.Location = new Point(160, 34);
            numEmailsToSend.Margin = new Padding(3, 2, 3, 2);
            numEmailsToSend.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numEmailsToSend.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEmailsToSend.Name = "numEmailsToSend";
            numEmailsToSend.Size = new Size(100, 23);
            numEmailsToSend.TabIndex = 2;
            numEmailsToSend.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // panel1
            // 
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtFrom);
            panel1.Controls.Add(txtSubject);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cmbImportance);
            panel1.Controls.Add(label8);
            panel1.Location = new Point(6, 15);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(724, 84);
            panel1.TabIndex = 1;
            // 
            // btnSendEmail
            // 
            btnSendEmail.Location = new Point(478, 3);
            btnSendEmail.Name = "btnSendEmail";
            btnSendEmail.Size = new Size(150, 51);
            btnSendEmail.TabIndex = 4;
            btnSendEmail.Text = "Start sending email(s)";
            btnSendEmail.UseVisualStyleBackColor = true;
            btnSendEmail.Click += btnSendEmail_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnStopSendingEmail);
            panel2.Controls.Add(btnSendEmail);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(cmbClientType);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(numEmailsToSend);
            panel2.Controls.Add(cmbSendWaitUntil);
            panel2.Controls.Add(label10);
            panel2.Location = new Point(0, 654);
            panel2.Name = "panel2";
            panel2.Size = new Size(730, 63);
            panel2.TabIndex = 7;
            // 
            // btnStopSendingEmail
            // 
            btnStopSendingEmail.Location = new Point(634, 3);
            btnStopSendingEmail.Name = "btnStopSendingEmail";
            btnStopSendingEmail.Size = new Size(90, 51);
            btnStopSendingEmail.TabIndex = 5;
            btnStopSendingEmail.Text = "Stop sending email(s)";
            btnStopSendingEmail.UseVisualStyleBackColor = true;
            btnStopSendingEmail.Click += btnStopSendingEmail_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(284, 24);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 22;
            label1.Text = "Send via:";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabConfiguration);
            tabControl1.Controls.Add(tabEmailOperations);
            tabControl1.Location = new Point(8, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(756, 764);
            tabControl1.TabIndex = 0;
            // 
            // tabConfiguration
            // 
            tabConfiguration.Controls.Add(grpEventHandling);
            tabConfiguration.Controls.Add(grpInitializeEmailClients);
            tabConfiguration.Location = new Point(4, 24);
            tabConfiguration.Name = "tabConfiguration";
            tabConfiguration.Padding = new Padding(3);
            tabConfiguration.Size = new Size(748, 736);
            tabConfiguration.TabIndex = 0;
            tabConfiguration.Text = "Configuration";
            tabConfiguration.UseVisualStyleBackColor = true;
            // 
            // grpEventHandling
            // 
            grpEventHandling.Controls.Add(btnServiceBusStopMonitoring);
            grpEventHandling.Controls.Add(btnServiceBusStartMonitoring);
            grpEventHandling.Controls.Add(pnlServiceBusConfig);
            grpEventHandling.Location = new Point(6, 8);
            grpEventHandling.Name = "grpEventHandling";
            grpEventHandling.Size = new Size(736, 133);
            grpEventHandling.TabIndex = 0;
            grpEventHandling.TabStop = false;
            grpEventHandling.Text = "Service Bus Event Handling";
            // 
            // btnServiceBusStopMonitoring
            // 
            btnServiceBusStopMonitoring.Enabled = false;
            btnServiceBusStopMonitoring.Location = new Point(610, 100);
            btnServiceBusStopMonitoring.Name = "btnServiceBusStopMonitoring";
            btnServiceBusStopMonitoring.Size = new Size(120, 23);
            btnServiceBusStopMonitoring.TabIndex = 2;
            btnServiceBusStopMonitoring.Text = "Stop Monitoring";
            btnServiceBusStopMonitoring.UseVisualStyleBackColor = true;
            btnServiceBusStopMonitoring.Click += btnServiceBusStopMonitoring_Click;
            // 
            // btnServiceBusStartMonitoring
            // 
            btnServiceBusStartMonitoring.Location = new Point(485, 100);
            btnServiceBusStartMonitoring.Name = "btnServiceBusStartMonitoring";
            btnServiceBusStartMonitoring.Size = new Size(120, 23);
            btnServiceBusStartMonitoring.TabIndex = 1;
            btnServiceBusStartMonitoring.Text = "Start Monitoring";
            btnServiceBusStartMonitoring.UseVisualStyleBackColor = true;
            btnServiceBusStartMonitoring.Click += btnServiceBusStartMonitoring_Click;
            // 
            // pnlServiceBusConfig
            // 
            pnlServiceBusConfig.Controls.Add(btnShowServiceBusConnString);
            pnlServiceBusConfig.Controls.Add(txtServiceBusQueueName);
            pnlServiceBusConfig.Controls.Add(txtServiceBusConnectionString);
            pnlServiceBusConfig.Controls.Add(label27);
            pnlServiceBusConfig.Controls.Add(label26);
            pnlServiceBusConfig.Location = new Point(6, 22);
            pnlServiceBusConfig.Name = "pnlServiceBusConfig";
            pnlServiceBusConfig.Size = new Size(723, 72);
            pnlServiceBusConfig.TabIndex = 0;
            // 
            // btnShowServiceBusConnString
            // 
            btnShowServiceBusConnString.Location = new Point(638, 11);
            btnShowServiceBusConnString.Margin = new Padding(3, 2, 3, 2);
            btnShowServiceBusConnString.Name = "btnShowServiceBusConnString";
            btnShowServiceBusConnString.Size = new Size(82, 22);
            btnShowServiceBusConnString.TabIndex = 1;
            btnShowServiceBusConnString.Text = "Show";
            btnShowServiceBusConnString.UseVisualStyleBackColor = true;
            btnShowServiceBusConnString.Click += btnShowServiceBusConnString_Click;
            // 
            // txtServiceBusQueueName
            // 
            txtServiceBusQueueName.Location = new Point(124, 39);
            txtServiceBusQueueName.Name = "txtServiceBusQueueName";
            txtServiceBusQueueName.Size = new Size(234, 23);
            txtServiceBusQueueName.TabIndex = 2;
            // 
            // txtServiceBusConnectionString
            // 
            txtServiceBusConnectionString.Location = new Point(124, 10);
            txtServiceBusConnectionString.Name = "txtServiceBusConnectionString";
            txtServiceBusConnectionString.Size = new Size(508, 23);
            txtServiceBusConnectionString.TabIndex = 0;
            txtServiceBusConnectionString.UseSystemPasswordChar = true;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(38, 42);
            label27.Name = "label27";
            label27.Size = new Size(80, 15);
            label27.TabIndex = 2;
            label27.Text = "Queue Name:";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(9, 13);
            label26.Name = "label26";
            label26.Size = new Size(109, 15);
            label26.TabIndex = 1;
            label26.Text = "Connection String: ";
            // 
            // grpInitializeEmailClients
            // 
            grpInitializeEmailClients.Controls.Add(pnlInitializeEmailClients);
            grpInitializeEmailClients.Controls.Add(btnInitializeEmailClients);
            grpInitializeEmailClients.Controls.Add(btnResetEmailClients);
            grpInitializeEmailClients.Location = new Point(6, 147);
            grpInitializeEmailClients.Name = "grpInitializeEmailClients";
            grpInitializeEmailClients.Size = new Size(736, 376);
            grpInitializeEmailClients.TabIndex = 1;
            grpInitializeEmailClients.TabStop = false;
            grpInitializeEmailClients.Text = "Initialize Email Clients";
            // 
            // pnlInitializeEmailClients
            // 
            pnlInitializeEmailClients.Controls.Add(dgEmailClientConfig);
            pnlInitializeEmailClients.Controls.Add(label11);
            pnlInitializeEmailClients.Controls.Add(cmbAuthType);
            pnlInitializeEmailClients.Location = new Point(6, 22);
            pnlInitializeEmailClients.Name = "pnlInitializeEmailClients";
            pnlInitializeEmailClients.Size = new Size(723, 318);
            pnlInitializeEmailClients.TabIndex = 0;
            // 
            // dgEmailClientConfig
            // 
            dgEmailClientConfig.AllowUserToAddRows = false;
            dgEmailClientConfig.AllowUserToDeleteRows = false;
            dgEmailClientConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgEmailClientConfig.Columns.AddRange(new DataGridViewColumn[] { ColName, ColValue, ColNotes });
            dgEmailClientConfig.Location = new Point(5, 28);
            dgEmailClientConfig.Name = "dgEmailClientConfig";
            dgEmailClientConfig.Size = new Size(715, 281);
            dgEmailClientConfig.TabIndex = 3;
            dgEmailClientConfig.CellBeginEdit += dgEmailClientConfig_CellBeginEdit;
            dgEmailClientConfig.CellEndEdit += dgEmailClientConfig_CellEndEdit;
            dgEmailClientConfig.EditingControlShowing += dgEmailClientConfig_EditingControlShowing;
            // 
            // ColName
            // 
            ColName.HeaderText = "Name";
            ColName.Name = "ColName";
            ColName.ReadOnly = true;
            ColName.Width = 130;
            // 
            // ColValue
            // 
            ColValue.HeaderText = "Value";
            ColValue.Name = "ColValue";
            ColValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            ColValue.Width = 300;
            // 
            // ColNotes
            // 
            ColNotes.HeaderText = "Notes";
            ColNotes.Name = "ColNotes";
            ColNotes.ReadOnly = true;
            ColNotes.Width = 240;
            // 
            // btnInitializeEmailClients
            // 
            btnInitializeEmailClients.Location = new Point(483, 345);
            btnInitializeEmailClients.Name = "btnInitializeEmailClients";
            btnInitializeEmailClients.Size = new Size(120, 23);
            btnInitializeEmailClients.TabIndex = 1;
            btnInitializeEmailClients.Text = "Initialize";
            btnInitializeEmailClients.UseVisualStyleBackColor = true;
            btnInitializeEmailClients.Click += btnInitialize_Click;
            // 
            // btnResetEmailClients
            // 
            btnResetEmailClients.Location = new Point(609, 346);
            btnResetEmailClients.Name = "btnResetEmailClients";
            btnResetEmailClients.Size = new Size(120, 23);
            btnResetEmailClients.TabIndex = 2;
            btnResetEmailClients.Text = "Reset";
            btnResetEmailClients.UseVisualStyleBackColor = true;
            btnResetEmailClients.Click += btnReinitialize_Click;
            // 
            // tabEmailOperations
            // 
            tabEmailOperations.Controls.Add(grpSendEmail);
            tabEmailOperations.Location = new Point(4, 24);
            tabEmailOperations.Name = "tabEmailOperations";
            tabEmailOperations.Padding = new Padding(3);
            tabEmailOperations.Size = new Size(748, 736);
            tabEmailOperations.TabIndex = 1;
            tabEmailOperations.Text = "Email Operations";
            tabEmailOperations.UseVisualStyleBackColor = true;
            // 
            // grpSendEmail
            // 
            grpSendEmail.Controls.Add(panel1);
            grpSendEmail.Controls.Add(panel2);
            grpSendEmail.Controls.Add(grpReceipents);
            grpSendEmail.Controls.Add(grpBody);
            grpSendEmail.Controls.Add(grpCustomHeders);
            grpSendEmail.Controls.Add(grpAttachments);
            grpSendEmail.Location = new Point(6, 6);
            grpSendEmail.Name = "grpSendEmail";
            grpSendEmail.Size = new Size(736, 719);
            grpSendEmail.TabIndex = 8;
            grpSendEmail.TabStop = false;
            grpSendEmail.Text = "Send email";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1438, 788);
            Controls.Add(tabControl1);
            Controls.Add(grpMessageDeliveryStatus);
            Controls.Add(grpTrace);
            Margin = new Padding(3, 2, 3, 2);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Email Communication Services Testing Tool";
            grpAttachments.ResumeLayout(false);
            grpAttachments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgAttachments).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgReceipeints).EndInit();
            grpTrace.ResumeLayout(false);
            grpTrace.PerformLayout();
            grpBody.ResumeLayout(false);
            grpBody.PerformLayout();
            grpReceipents.ResumeLayout(false);
            grpCustomHeders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgCustomHeaders).EndInit();
            grpMessageDeliveryStatus.ResumeLayout(false);
            grpMessageDeliveryStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numEmailsToSend).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabConfiguration.ResumeLayout(false);
            grpEventHandling.ResumeLayout(false);
            pnlServiceBusConfig.ResumeLayout(false);
            pnlServiceBusConfig.PerformLayout();
            grpInitializeEmailClients.ResumeLayout(false);
            pnlInitializeEmailClients.ResumeLayout(false);
            pnlInitializeEmailClients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgEmailClientConfig).EndInit();
            tabEmailOperations.ResumeLayout(false);
            grpSendEmail.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtSubject;
        private Label label2;
        private TextBox txtBody;
        private DataGridView dgReceipeints;
        private TextBox txtFrom;
        private Label label5;
        private GroupBox grpTrace;
        private TextBox txtTrace;
        private CheckBox chkIsHtmlBody;
        private Button btnAddAttachment;
        private DataGridView dgAttachments;
        private GroupBox grpAttachments;
        private Label lblAttachmentsCount;
        private GroupBox grpBody;
        private GroupBox grpReceipents;
        private Label lblAttachmentsSize;
        private Label label4;
        private Label label3;
        private Label label6;
        private ComboBox cmbImportance;
        private GroupBox grpCustomHeders;
        private DataGridView dgCustomHeaders;
        private GroupBox grpMessageDeliveryStatus;
        private Button btnGetMsgDeliveryStatus;
        private TextBox txtMessageID;
        private Label label7;
        private Label label8;
        private DataGridViewComboBoxColumn attachType_Old;
        private Label label9;
        private ComboBox cmbSendWaitUntil;
        private Label label10;
        private NumericUpDown numEmailsToSend;
        private ComboBox cmbAuthType;
        private Label label11;
        private Panel panel1;
        private Panel panel2;
        private Button btnSendEmail;
        private ComboBox cmbClientType;
        private Label label1;
        private Button btnStopSendingEmail;
        private Button btnGetBase64String;
        private TabControl tabControl1;
        private TabPage tabConfiguration;
        private TabPage tabEmailOperations;
        private Button btnInitializeEmailClients;
        private Button btnResetEmailClients;
        private GroupBox grpSendEmail;
        private GroupBox grpInitializeEmailClients;
        private GroupBox grpEventHandling;
        private Panel pnlServiceBusConfig;
        private Label label27;
        private Label label26;
        private Button btnShowServiceBusConnString;
        private TextBox txtServiceBusQueueName;
        private TextBox txtServiceBusConnectionString;
        private Button btnServiceBusStartMonitoring;
        private Panel pnlInitializeEmailClients;
        private DataGridViewTextBoxColumn attachFilePath;
        private DataGridViewTextBoxColumn attachMimeType;
        private DataGridViewTextBoxColumn attachSize;
        private DataGridViewTextBoxColumn attachCID;
        private DataGridViewCheckBoxColumn attachInline;
        private DataGridViewTextBoxColumn recEmailAddress;
        private DataGridViewTextBoxColumn recDisplayName;
        private DataGridViewComboBoxColumn recType;
        private DataGridViewTextBoxColumn colHeaderName;
        private DataGridViewTextBoxColumn colHeaderValue;
        private Button btnServiceBusStopMonitoring;
        private DataGridView dgEmailClientConfig;
        private DataGridViewTextBoxColumn ColName;
        private DataGridViewTextBoxColumn ColValue;
        private DataGridViewTextBoxColumn ColNotes;
    }
}