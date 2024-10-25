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
            grpReceipents = new GroupBox();
            label6 = new Label();
            cmbImportance = new ComboBox();
            grpCustomHeders = new GroupBox();
            dgCustomHeaders = new DataGridView();
            colHeaderName = new DataGridViewTextBoxColumn();
            colHeaderValue = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            btnGetMsgDeliveryStatus = new Button();
            txtMessageID = new TextBox();
            label7 = new Label();
            label8 = new Label();
            chk429AutoRetry = new CheckBox();
            pnlInitialize = new Panel();
            pnlSmtpConfig = new Panel();
            label20 = new Label();
            txtSmtpEndpoint = new TextBox();
            txtSmtpPort = new TextBox();
            label16 = new Label();
            label17 = new Label();
            btnShowAcsKey = new Button();
            label19 = new Label();
            label12 = new Label();
            txtAccessKey = new TextBox();
            label15 = new Label();
            txtAcsEndpoint = new TextBox();
            btnAADConfig = new Button();
            label14 = new Label();
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
            grpAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgAttachments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgReceipeints).BeginInit();
            grpTrace.SuspendLayout();
            grpBody.SuspendLayout();
            grpReceipents.SuspendLayout();
            grpCustomHeders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgCustomHeaders).BeginInit();
            groupBox1.SuspendLayout();
            pnlInitialize.SuspendLayout();
            pnlSmtpConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numEmailsToSend).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
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
            grpAttachments.Location = new Point(10, 539);
            grpAttachments.Margin = new Padding(3, 2, 3, 2);
            grpAttachments.Name = "grpAttachments";
            grpAttachments.Padding = new Padding(3, 2, 3, 2);
            grpAttachments.Size = new Size(758, 161);
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
            dgAttachments.Size = new Size(747, 110);
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
            attachFilePath.Width = 320;
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
            chkIsHtmlBody.Location = new Point(683, 20);
            chkIsHtmlBody.Margin = new Padding(3, 2, 3, 2);
            chkIsHtmlBody.Name = "chkIsHtmlBody";
            chkIsHtmlBody.Size = new Size(69, 19);
            chkIsHtmlBody.TabIndex = 2;
            chkIsHtmlBody.Text = "Is HTML";
            chkIsHtmlBody.UseVisualStyleBackColor = true;
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(140, 3);
            txtFrom.Margin = new Padding(3, 2, 3, 2);
            txtFrom.Name = "txtFrom";
            txtFrom.Size = new Size(537, 23);
            txtFrom.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(90, 6);
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
            dgReceipeints.Size = new Size(480, 110);
            dgReceipeints.TabIndex = 5;
            // 
            // recEmailAddress
            // 
            recEmailAddress.HeaderText = "Email Address";
            recEmailAddress.MinimumWidth = 6;
            recEmailAddress.Name = "recEmailAddress";
            recEmailAddress.Width = 220;
            // 
            // recDisplayName
            // 
            recDisplayName.HeaderText = "Display Name";
            recDisplayName.MinimumWidth = 6;
            recDisplayName.Name = "recDisplayName";
            recDisplayName.Width = 115;
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
            txtBody.Size = new Size(672, 107);
            txtBody.TabIndex = 1;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(140, 30);
            txtSubject.Margin = new Padding(3, 2, 3, 2);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(537, 23);
            txtSubject.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(79, 33);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 0;
            label2.Text = "Subject:";
            // 
            // grpTrace
            // 
            grpTrace.Controls.Add(txtTrace);
            grpTrace.Location = new Point(774, 63);
            grpTrace.Margin = new Padding(3, 2, 3, 2);
            grpTrace.Name = "grpTrace";
            grpTrace.Padding = new Padding(3, 2, 3, 2);
            grpTrace.Size = new Size(658, 705);
            grpTrace.TabIndex = 11;
            grpTrace.TabStop = false;
            grpTrace.Text = "Trace";
            // 
            // txtTrace
            // 
            txtTrace.BackColor = SystemColors.Window;
            txtTrace.Location = new Point(5, 20);
            txtTrace.Margin = new Padding(3, 2, 3, 2);
            txtTrace.Multiline = true;
            txtTrace.Name = "txtTrace";
            txtTrace.ScrollBars = ScrollBars.Vertical;
            txtTrace.Size = new Size(648, 679);
            txtTrace.TabIndex = 0;
            // 
            // grpBody
            // 
            grpBody.Controls.Add(chkIsHtmlBody);
            grpBody.Controls.Add(txtBody);
            grpBody.Location = new Point(10, 263);
            grpBody.Margin = new Padding(3, 2, 3, 2);
            grpBody.Name = "grpBody";
            grpBody.Padding = new Padding(3, 2, 3, 2);
            grpBody.Size = new Size(758, 133);
            grpBody.TabIndex = 2;
            grpBody.TabStop = false;
            grpBody.Text = "Body";
            // 
            // grpReceipents
            // 
            grpReceipents.Controls.Add(dgReceipeints);
            grpReceipents.Location = new Point(10, 400);
            grpReceipents.Margin = new Padding(3, 2, 3, 2);
            grpReceipents.Name = "grpReceipents";
            grpReceipents.Padding = new Padding(3, 2, 3, 2);
            grpReceipents.Size = new Size(490, 135);
            grpReceipents.TabIndex = 3;
            grpReceipents.TabStop = false;
            grpReceipents.Text = "Receipents";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(57, 60);
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
            cmbImportance.Location = new Point(140, 57);
            cmbImportance.Margin = new Padding(3, 2, 3, 2);
            cmbImportance.Name = "cmbImportance";
            cmbImportance.Size = new Size(141, 23);
            cmbImportance.TabIndex = 4;
            // 
            // grpCustomHeders
            // 
            grpCustomHeders.Controls.Add(dgCustomHeaders);
            grpCustomHeders.Location = new Point(506, 400);
            grpCustomHeders.Margin = new Padding(3, 2, 3, 2);
            grpCustomHeders.Name = "grpCustomHeders";
            grpCustomHeders.Padding = new Padding(3, 2, 3, 2);
            grpCustomHeders.Size = new Size(262, 135);
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
            dgCustomHeaders.Size = new Size(250, 110);
            dgCustomHeaders.TabIndex = 0;
            // 
            // colHeaderName
            // 
            colHeaderName.HeaderText = "Name";
            colHeaderName.MinimumWidth = 6;
            colHeaderName.Name = "colHeaderName";
            colHeaderName.Width = 90;
            // 
            // colHeaderValue
            // 
            colHeaderValue.HeaderText = "Value";
            colHeaderValue.MinimumWidth = 6;
            colHeaderValue.Name = "colHeaderValue";
            colHeaderValue.Width = 90;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGetMsgDeliveryStatus);
            groupBox1.Controls.Add(txtMessageID);
            groupBox1.Controls.Add(label7);
            groupBox1.Location = new Point(774, 11);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(658, 50);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Message Delivery Status";
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
            label8.Location = new Point(287, 60);
            label8.Name = "label8";
            label8.Size = new Size(186, 15);
            label8.TabIndex = 18;
            label8.Text = "1 is the highest and 5 is the lowest";
            // 
            // chk429AutoRetry
            // 
            chk429AutoRetry.AutoSize = true;
            chk429AutoRetry.Location = new Point(140, 153);
            chk429AutoRetry.Margin = new Padding(3, 2, 3, 2);
            chk429AutoRetry.Name = "chk429AutoRetry";
            chk429AutoRetry.Size = new Size(15, 14);
            chk429AutoRetry.TabIndex = 6;
            chk429AutoRetry.UseVisualStyleBackColor = true;
            // 
            // pnlInitialize
            // 
            pnlInitialize.Controls.Add(pnlSmtpConfig);
            pnlInitialize.Controls.Add(btnShowAcsKey);
            pnlInitialize.Controls.Add(label19);
            pnlInitialize.Controls.Add(label12);
            pnlInitialize.Controls.Add(txtAccessKey);
            pnlInitialize.Controls.Add(chk429AutoRetry);
            pnlInitialize.Controls.Add(label15);
            pnlInitialize.Controls.Add(txtAcsEndpoint);
            pnlInitialize.Controls.Add(btnAADConfig);
            pnlInitialize.Controls.Add(label14);
            pnlInitialize.Controls.Add(label11);
            pnlInitialize.Controls.Add(cmbAuthType);
            pnlInitialize.Location = new Point(0, 0);
            pnlInitialize.Margin = new Padding(3, 2, 3, 2);
            pnlInitialize.Name = "pnlInitialize";
            pnlInitialize.Size = new Size(768, 171);
            pnlInitialize.TabIndex = 0;
            // 
            // pnlSmtpConfig
            // 
            pnlSmtpConfig.Controls.Add(label20);
            pnlSmtpConfig.Controls.Add(txtSmtpEndpoint);
            pnlSmtpConfig.Controls.Add(txtSmtpPort);
            pnlSmtpConfig.Controls.Add(label16);
            pnlSmtpConfig.Controls.Add(label17);
            pnlSmtpConfig.Location = new Point(8, 94);
            pnlSmtpConfig.Name = "pnlSmtpConfig";
            pnlSmtpConfig.Size = new Size(754, 57);
            pnlSmtpConfig.TabIndex = 5;
            // 
            // label20
            // 
            label20.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label20.Location = new Point(369, 3);
            label20.Name = "label20";
            label20.Size = new Size(300, 52);
            label20.TabIndex = 37;
            label20.Text = "**These configurations are required if only you want to send emails by using SMTP";
            label20.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtSmtpEndpoint
            // 
            txtSmtpEndpoint.Location = new Point(132, 3);
            txtSmtpEndpoint.Name = "txtSmtpEndpoint";
            txtSmtpEndpoint.Size = new Size(212, 23);
            txtSmtpEndpoint.TabIndex = 29;
            // 
            // txtSmtpPort
            // 
            txtSmtpPort.Location = new Point(132, 32);
            txtSmtpPort.Name = "txtSmtpPort";
            txtSmtpPort.Size = new Size(141, 23);
            txtSmtpPort.TabIndex = 31;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(19, 6);
            label16.Name = "label16";
            label16.Size = new Size(101, 15);
            label16.TabIndex = 23;
            label16.Text = "SMTP endpoint**:";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(45, 35);
            label17.Name = "label17";
            label17.Size = new Size(75, 15);
            label17.TabIndex = 30;
            label17.Text = "SMTP port**:";
            // 
            // btnShowAcsKey
            // 
            btnShowAcsKey.Location = new Point(680, 68);
            btnShowAcsKey.Margin = new Padding(3, 2, 3, 2);
            btnShowAcsKey.Name = "btnShowAcsKey";
            btnShowAcsKey.Size = new Size(82, 22);
            btnShowAcsKey.TabIndex = 4;
            btnShowAcsKey.Text = "Show";
            btnShowAcsKey.UseVisualStyleBackColor = true;
            btnShowAcsKey.Click += btnShowAcsKey_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            label19.Location = new Point(436, 13);
            label19.Name = "label19";
            label19.Size = new Size(295, 15);
            label19.TabIndex = 34;
            label19.Text = "*SMTP is only supported with AAD Default/Client Auth";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(46, 152);
            label12.Name = "label12";
            label12.Size = new Size(82, 15);
            label12.TabIndex = 26;
            label12.Text = "429 auto retry:";
            // 
            // txtAccessKey
            // 
            txtAccessKey.Location = new Point(140, 67);
            txtAccessKey.Name = "txtAccessKey";
            txtAccessKey.Size = new Size(537, 23);
            txtAccessKey.TabIndex = 3;
            txtAccessKey.UseSystemPasswordChar = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(75, 70);
            label15.Name = "label15";
            label15.Size = new Size(53, 15);
            label15.TabIndex = 27;
            label15.Text = "ACS key:";
            // 
            // txtAcsEndpoint
            // 
            txtAcsEndpoint.Location = new Point(140, 38);
            txtAcsEndpoint.Name = "txtAcsEndpoint";
            txtAcsEndpoint.Size = new Size(537, 23);
            txtAcsEndpoint.TabIndex = 2;
            // 
            // btnAADConfig
            // 
            btnAADConfig.Location = new Point(286, 9);
            btnAADConfig.Margin = new Padding(3, 2, 3, 2);
            btnAADConfig.Name = "btnAADConfig";
            btnAADConfig.Size = new Size(144, 22);
            btnAADConfig.TabIndex = 1;
            btnAADConfig.Text = "AAD Configuration";
            btnAADConfig.UseVisualStyleBackColor = true;
            btnAADConfig.Click += btnAADConfig_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(45, 41);
            label14.Name = "label14";
            label14.Size = new Size(83, 15);
            label14.TabIndex = 26;
            label14.Text = "ACS endpoint:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(8, 13);
            label11.Name = "label11";
            label11.Size = new Size(120, 15);
            label11.TabIndex = 25;
            label11.Text = "Authentication type*:";
            // 
            // cmbAuthType
            // 
            cmbAuthType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthType.FormattingEnabled = true;
            cmbAuthType.Items.AddRange(new object[] { "ACS Key", "AAD Default Auth", "AAD Client Auth", "Interactive" });
            cmbAuthType.Location = new Point(140, 9);
            cmbAuthType.Margin = new Padding(3, 2, 3, 2);
            cmbAuthType.Name = "cmbAuthType";
            cmbAuthType.Size = new Size(141, 23);
            cmbAuthType.TabIndex = 0;
            cmbAuthType.SelectedIndexChanged += cmbAuthType_SelectedIndexChanged;
            // 
            // cmbClientType
            // 
            cmbClientType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClientType.FormattingEnabled = true;
            cmbClientType.Location = new Point(371, 21);
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
            cmbSendWaitUntil.Size = new Size(121, 23);
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
            numEmailsToSend.Size = new Size(121, 23);
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
            panel1.Location = new Point(0, 175);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(768, 84);
            panel1.TabIndex = 1;
            // 
            // btnSendEmail
            // 
            btnSendEmail.Location = new Point(506, 6);
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
            panel2.Location = new Point(10, 705);
            panel2.Name = "panel2";
            panel2.Size = new Size(758, 63);
            panel2.TabIndex = 7;
            // 
            // btnStopSendingEmail
            // 
            btnStopSendingEmail.Location = new Point(662, 6);
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
            label1.Location = new Point(311, 24);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 22;
            label1.Text = "Send via:";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1438, 783);
            Controls.Add(panel1);
            Controls.Add(pnlInitialize);
            Controls.Add(groupBox1);
            Controls.Add(grpCustomHeders);
            Controls.Add(grpAttachments);
            Controls.Add(grpReceipents);
            Controls.Add(grpBody);
            Controls.Add(grpTrace);
            Controls.Add(panel2);
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
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            pnlInitialize.ResumeLayout(false);
            pnlInitialize.PerformLayout();
            pnlSmtpConfig.ResumeLayout(false);
            pnlSmtpConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numEmailsToSend).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
        private GroupBox groupBox1;
        private Button btnGetMsgDeliveryStatus;
        private TextBox txtMessageID;
        private Label label7;
        private Label label8;
        private DataGridViewComboBoxColumn attachType_Old;
        private CheckBox chk429AutoRetry;
        private Panel pnlInitialize;
        private Label label9;
        private ComboBox cmbSendWaitUntil;
        private Label label10;
        private NumericUpDown numEmailsToSend;
        private Button btnAADConfig;
        private ComboBox cmbAuthType;
        private Label label11;
        private Label label12;
        private Panel panel1;
        private DataGridViewTextBoxColumn colHeaderName;
        private DataGridViewTextBoxColumn colHeaderValue;
        private DataGridViewTextBoxColumn recEmailAddress;
        private DataGridViewTextBoxColumn recDisplayName;
        private DataGridViewComboBoxColumn recType;
        private Panel panel2;
        private Label label14;
        private Label label15;
        private TextBox txtAcsEndpoint;
        private TextBox txtAccessKey;
        private Label label16;
        private TextBox txtSmtpEndpoint;
        private Label label17;
        private TextBox txtSmtpPort;
        private Label label19;
        private Button btnShowAcsKey;
        private Label label20;
        private Panel pnlSmtpConfig;
        private Button btnSendEmail;
        private ComboBox cmbClientType;
        private Label label1;
        private Button btnStopSendingEmail;
        private DataGridViewTextBoxColumn attachFilePath;
        private DataGridViewTextBoxColumn attachMimeType;
        private DataGridViewTextBoxColumn attachSize;
        private DataGridViewTextBoxColumn attachCID;
        private DataGridViewCheckBoxColumn attachInline;
    }
}