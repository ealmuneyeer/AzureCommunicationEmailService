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
            label1 = new Label();
            txtConnString = new TextBox();
            btnSendEmail = new Button();
            grpAttachments = new GroupBox();
            lblAttachmentsSize = new Label();
            label4 = new Label();
            label3 = new Label();
            lblAttachmentsCount = new Label();
            dgAttachments = new DataGridView();
            attachFilePath = new DataGridViewTextBoxColumn();
            attachType = new DataGridViewTextBoxColumn();
            attachSize = new DataGridViewTextBoxColumn();
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
            btnInitializeConnString = new Button();
            groupBox1 = new GroupBox();
            btnGetMsgDeliveryStatus = new Button();
            txtMessageID = new TextBox();
            label7 = new Label();
            label8 = new Label();
            chk429AutoRetry = new CheckBox();
            pnlInitialize = new Panel();
            btnAADConfig = new Button();
            label12 = new Label();
            label11 = new Label();
            cmbAuthType = new ComboBox();
            label9 = new Label();
            cmbSendWaitUntil = new ComboBox();
            label10 = new Label();
            numEmailsToSend = new NumericUpDown();
            panel1 = new Panel();
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
            ((System.ComponentModel.ISupportInitialize)numEmailsToSend).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 72);
            label1.Name = "label1";
            label1.Size = new Size(128, 20);
            label1.TabIndex = 0;
            label1.Text = "Connection string:";
            // 
            // txtConnString
            // 
            txtConnString.Location = new Point(160, 69);
            txtConnString.Name = "txtConnString";
            txtConnString.Size = new Size(576, 27);
            txtConnString.TabIndex = 3;
            // 
            // btnSendEmail
            // 
            btnSendEmail.Location = new Point(690, 799);
            btnSendEmail.Name = "btnSendEmail";
            btnSendEmail.Size = new Size(152, 40);
            btnSendEmail.TabIndex = 9;
            btnSendEmail.Text = "Send Email";
            btnSendEmail.UseVisualStyleBackColor = true;
            btnSendEmail.Click += btnSendEmail_Click;
            // 
            // grpAttachments
            // 
            grpAttachments.Controls.Add(lblAttachmentsSize);
            grpAttachments.Controls.Add(label4);
            grpAttachments.Controls.Add(label3);
            grpAttachments.Controls.Add(lblAttachmentsCount);
            grpAttachments.Controls.Add(dgAttachments);
            grpAttachments.Controls.Add(btnAddAttachment);
            grpAttachments.Location = new Point(11, 577);
            grpAttachments.Name = "grpAttachments";
            grpAttachments.Size = new Size(830, 215);
            grpAttachments.TabIndex = 5;
            grpAttachments.TabStop = false;
            grpAttachments.Text = "Attachments";
            // 
            // lblAttachmentsSize
            // 
            lblAttachmentsSize.AutoSize = true;
            lblAttachmentsSize.Location = new Point(459, 31);
            lblAttachmentsSize.Name = "lblAttachmentsSize";
            lblAttachmentsSize.Size = new Size(17, 20);
            lblAttachmentsSize.TabIndex = 15;
            lblAttachmentsSize.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(347, 29);
            label4.Name = "label4";
            label4.Size = new Size(117, 20);
            label4.TabIndex = 14;
            label4.Text = "Total size (Byte):";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(143, 29);
            label3.Name = "label3";
            label3.Size = new Size(86, 20);
            label3.TabIndex = 13;
            label3.Text = "Total count:";
            // 
            // lblAttachmentsCount
            // 
            lblAttachmentsCount.AutoSize = true;
            lblAttachmentsCount.Location = new Point(225, 29);
            lblAttachmentsCount.Name = "lblAttachmentsCount";
            lblAttachmentsCount.Size = new Size(17, 20);
            lblAttachmentsCount.TabIndex = 12;
            lblAttachmentsCount.Text = "0";
            // 
            // dgAttachments
            // 
            dgAttachments.AllowUserToAddRows = false;
            dgAttachments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgAttachments.Columns.AddRange(new DataGridViewColumn[] { attachFilePath, attachType, attachSize });
            dgAttachments.Location = new Point(6, 61);
            dgAttachments.Name = "dgAttachments";
            dgAttachments.RowHeadersWidth = 51;
            dgAttachments.RowTemplate.Height = 29;
            dgAttachments.Size = new Size(818, 147);
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
            attachFilePath.Width = 513;
            // 
            // attachType
            // 
            attachType.HeaderText = "Type";
            attachType.MinimumWidth = 6;
            attachType.Name = "attachType";
            attachType.Width = 125;
            // 
            // attachSize
            // 
            attachSize.HeaderText = "Size (Byte)";
            attachSize.MinimumWidth = 6;
            attachSize.Name = "attachSize";
            attachSize.Width = 125;
            // 
            // btnAddAttachment
            // 
            btnAddAttachment.Location = new Point(6, 27);
            btnAddAttachment.Name = "btnAddAttachment";
            btnAddAttachment.Size = new Size(131, 29);
            btnAddAttachment.TabIndex = 1;
            btnAddAttachment.Text = "Add attachment";
            btnAddAttachment.UseVisualStyleBackColor = true;
            btnAddAttachment.Click += btnAddAttachment_Click;
            // 
            // chkIsHtmlBody
            // 
            chkIsHtmlBody.AutoSize = true;
            chkIsHtmlBody.Location = new Point(734, 28);
            chkIsHtmlBody.Name = "chkIsHtmlBody";
            chkIsHtmlBody.Size = new Size(84, 24);
            chkIsHtmlBody.TabIndex = 2;
            chkIsHtmlBody.Text = "Is HTML";
            chkIsHtmlBody.UseVisualStyleBackColor = true;
            // 
            // txtFrom
            // 
            txtFrom.Location = new Point(160, 3);
            txtFrom.Name = "txtFrom";
            txtFrom.Size = new Size(675, 27);
            txtFrom.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(108, 6);
            label5.Name = "label5";
            label5.Size = new Size(46, 20);
            label5.TabIndex = 6;
            label5.Text = "From:";
            // 
            // dgReceipeints
            // 
            dgReceipeints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgReceipeints.Columns.AddRange(new DataGridViewColumn[] { recEmailAddress, recDisplayName, recType });
            dgReceipeints.Location = new Point(6, 27);
            dgReceipeints.Name = "dgReceipeints";
            dgReceipeints.RowHeadersWidth = 51;
            dgReceipeints.RowTemplate.Height = 29;
            dgReceipeints.Size = new Size(517, 147);
            dgReceipeints.TabIndex = 5;
            // 
            // recEmailAddress
            // 
            recEmailAddress.HeaderText = "Email Address";
            recEmailAddress.MinimumWidth = 6;
            recEmailAddress.Name = "recEmailAddress";
            recEmailAddress.Width = 240;
            // 
            // recDisplayName
            // 
            recDisplayName.HeaderText = "Display Name";
            recDisplayName.MinimumWidth = 6;
            recDisplayName.Name = "recDisplayName";
            recDisplayName.Width = 133;
            // 
            // recType
            // 
            recType.HeaderText = "Type";
            recType.Items.AddRange(new object[] { "To", "CC", "BCC", "Reply To" });
            recType.MinimumWidth = 6;
            recType.Name = "recType";
            recType.Width = 90;
            // 
            // txtBody
            // 
            txtBody.Location = new Point(6, 27);
            txtBody.Multiline = true;
            txtBody.Name = "txtBody";
            txtBody.ScrollBars = ScrollBars.Vertical;
            txtBody.Size = new Size(722, 141);
            txtBody.TabIndex = 1;
            // 
            // txtSubject
            // 
            txtSubject.Location = new Point(160, 37);
            txtSubject.Name = "txtSubject";
            txtSubject.Size = new Size(675, 27);
            txtSubject.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 40);
            label2.Name = "label2";
            label2.Size = new Size(61, 20);
            label2.TabIndex = 0;
            label2.Text = "Subject:";
            // 
            // grpTrace
            // 
            grpTrace.Controls.Add(txtTrace);
            grpTrace.Location = new Point(847, 85);
            grpTrace.Name = "grpTrace";
            grpTrace.Size = new Size(752, 754);
            grpTrace.TabIndex = 11;
            grpTrace.TabStop = false;
            grpTrace.Text = "Trace";
            // 
            // txtTrace
            // 
            txtTrace.BackColor = SystemColors.Window;
            txtTrace.Location = new Point(6, 27);
            txtTrace.Multiline = true;
            txtTrace.Name = "txtTrace";
            txtTrace.ScrollBars = ScrollBars.Vertical;
            txtTrace.Size = new Size(740, 717);
            txtTrace.TabIndex = 0;
            // 
            // grpBody
            // 
            grpBody.Controls.Add(chkIsHtmlBody);
            grpBody.Controls.Add(txtBody);
            grpBody.Location = new Point(11, 209);
            grpBody.Name = "grpBody";
            grpBody.Size = new Size(824, 177);
            grpBody.TabIndex = 2;
            grpBody.TabStop = false;
            grpBody.Text = "Body";
            // 
            // grpReceipents
            // 
            grpReceipents.Controls.Add(dgReceipeints);
            grpReceipents.Location = new Point(11, 392);
            grpReceipents.Name = "grpReceipents";
            grpReceipents.Size = new Size(528, 180);
            grpReceipents.TabIndex = 3;
            grpReceipents.TabStop = false;
            grpReceipents.Text = "Receipents";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(66, 72);
            label6.Name = "label6";
            label6.Size = new Size(88, 20);
            label6.TabIndex = 15;
            label6.Text = "Importance:";
            // 
            // cmbImportance
            // 
            cmbImportance.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbImportance.FormattingEnabled = true;
            cmbImportance.ItemHeight = 20;
            cmbImportance.Items.AddRange(new object[] { "None", "1", "2", "3", "4", "5" });
            cmbImportance.Location = new Point(160, 69);
            cmbImportance.Name = "cmbImportance";
            cmbImportance.Size = new Size(159, 28);
            cmbImportance.TabIndex = 4;
            // 
            // grpCustomHeders
            // 
            grpCustomHeders.Controls.Add(dgCustomHeaders);
            grpCustomHeders.Location = new Point(551, 392);
            grpCustomHeders.Name = "grpCustomHeders";
            grpCustomHeders.Size = new Size(296, 180);
            grpCustomHeders.TabIndex = 4;
            grpCustomHeders.TabStop = false;
            grpCustomHeders.Text = "Custom Headers";
            // 
            // dgCustomHeaders
            // 
            dgCustomHeaders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCustomHeaders.Columns.AddRange(new DataGridViewColumn[] { colHeaderName, colHeaderValue });
            dgCustomHeaders.Location = new Point(6, 27);
            dgCustomHeaders.Name = "dgCustomHeaders";
            dgCustomHeaders.RowHeadersWidth = 51;
            dgCustomHeaders.RowTemplate.Height = 29;
            dgCustomHeaders.Size = new Size(283, 147);
            dgCustomHeaders.TabIndex = 0;
            // 
            // colHeaderName
            // 
            colHeaderName.HeaderText = "Name";
            colHeaderName.MinimumWidth = 6;
            colHeaderName.Name = "colHeaderName";
            colHeaderName.Width = 115;
            // 
            // colHeaderValue
            // 
            colHeaderValue.HeaderText = "Value";
            colHeaderValue.MinimumWidth = 6;
            colHeaderValue.Name = "colHeaderValue";
            colHeaderValue.Width = 115;
            // 
            // btnInitializeConnString
            // 
            btnInitializeConnString.Location = new Point(741, 69);
            btnInitializeConnString.Name = "btnInitializeConnString";
            btnInitializeConnString.Size = new Size(94, 29);
            btnInitializeConnString.TabIndex = 4;
            btnInitializeConnString.Text = "Initialize";
            btnInitializeConnString.UseVisualStyleBackColor = true;
            btnInitializeConnString.Click += btnInitializeConnString_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnGetMsgDeliveryStatus);
            groupBox1.Controls.Add(txtMessageID);
            groupBox1.Controls.Add(label7);
            groupBox1.Location = new Point(847, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(752, 67);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "Message Delivery Status";
            // 
            // btnGetMsgDeliveryStatus
            // 
            btnGetMsgDeliveryStatus.Location = new Point(575, 25);
            btnGetMsgDeliveryStatus.Name = "btnGetMsgDeliveryStatus";
            btnGetMsgDeliveryStatus.Size = new Size(171, 29);
            btnGetMsgDeliveryStatus.TabIndex = 2;
            btnGetMsgDeliveryStatus.Text = "Get Delivery Status";
            btnGetMsgDeliveryStatus.UseVisualStyleBackColor = true;
            btnGetMsgDeliveryStatus.Click += btnGetMsgDeliveryStatus_Click;
            // 
            // txtMessageID
            // 
            txtMessageID.Location = new Point(101, 27);
            txtMessageID.Name = "txtMessageID";
            txtMessageID.Size = new Size(468, 27);
            txtMessageID.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 29);
            label7.Name = "label7";
            label7.Size = new Size(89, 20);
            label7.TabIndex = 0;
            label7.Text = "Message ID:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(325, 72);
            label8.Name = "label8";
            label8.Size = new Size(235, 20);
            label8.TabIndex = 18;
            label8.Text = "1 is the highest and 5 is the lowest";
            // 
            // chk429AutoRetry
            // 
            chk429AutoRetry.AutoSize = true;
            chk429AutoRetry.Location = new Point(160, 46);
            chk429AutoRetry.Name = "chk429AutoRetry";
            chk429AutoRetry.Size = new Size(18, 17);
            chk429AutoRetry.TabIndex = 2;
            chk429AutoRetry.UseVisualStyleBackColor = true;
            // 
            // pnlInitialize
            // 
            pnlInitialize.Controls.Add(btnAADConfig);
            pnlInitialize.Controls.Add(label12);
            pnlInitialize.Controls.Add(label11);
            pnlInitialize.Controls.Add(cmbAuthType);
            pnlInitialize.Controls.Add(chk429AutoRetry);
            pnlInitialize.Controls.Add(btnInitializeConnString);
            pnlInitialize.Controls.Add(txtConnString);
            pnlInitialize.Controls.Add(label1);
            pnlInitialize.Location = new Point(0, 0);
            pnlInitialize.Name = "pnlInitialize";
            pnlInitialize.Size = new Size(841, 102);
            pnlInitialize.TabIndex = 0;
            // 
            // btnAADConfig
            // 
            btnAADConfig.Location = new Point(327, 12);
            btnAADConfig.Name = "btnAADConfig";
            btnAADConfig.Size = new Size(165, 29);
            btnAADConfig.TabIndex = 1;
            btnAADConfig.Text = "AAD Configuration";
            btnAADConfig.UseVisualStyleBackColor = true;
            btnAADConfig.Click += btnAADConfig_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(50, 46);
            label12.Name = "label12";
            label12.Size = new Size(104, 20);
            label12.TabIndex = 26;
            label12.Text = "429 auto retry:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 15);
            label11.Name = "label11";
            label11.Size = new Size(142, 20);
            label11.TabIndex = 25;
            label11.Text = "Authentication type:";
            // 
            // cmbAuthType
            // 
            cmbAuthType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAuthType.FormattingEnabled = true;
            cmbAuthType.Items.AddRange(new object[] { "ACS Key", "AAD Default Auth", "AAD Client Auth" });
            cmbAuthType.Location = new Point(160, 12);
            cmbAuthType.Name = "cmbAuthType";
            cmbAuthType.Size = new Size(161, 28);
            cmbAuthType.TabIndex = 0;
            cmbAuthType.SelectedIndexChanged += cmbAuthType_SelectedIndexChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(17, 809);
            label9.Name = "label9";
            label9.Size = new Size(110, 20);
            label9.TabIndex = 19;
            label9.Text = "Send wait until:";
            // 
            // cmbSendWaitUntil
            // 
            cmbSendWaitUntil.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSendWaitUntil.FormattingEnabled = true;
            cmbSendWaitUntil.Items.AddRange(new object[] { "Started", "Completed" });
            cmbSendWaitUntil.Location = new Point(133, 806);
            cmbSendWaitUntil.Margin = new Padding(3, 4, 3, 4);
            cmbSendWaitUntil.Name = "cmbSendWaitUntil";
            cmbSendWaitUntil.Size = new Size(138, 28);
            cmbSendWaitUntil.TabIndex = 6;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(311, 809);
            label10.Name = "label10";
            label10.Size = new Size(181, 20);
            label10.TabIndex = 21;
            label10.Text = "Number of emails to send";
            // 
            // numEmailsToSend
            // 
            numEmailsToSend.Location = new Point(498, 807);
            numEmailsToSend.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numEmailsToSend.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEmailsToSend.Name = "numEmailsToSend";
            numEmailsToSend.Size = new Size(73, 27);
            numEmailsToSend.TabIndex = 7;
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
            panel1.Location = new Point(0, 103);
            panel1.Name = "panel1";
            panel1.Size = new Size(841, 103);
            panel1.TabIndex = 1;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1611, 851);
            Controls.Add(panel1);
            Controls.Add(numEmailsToSend);
            Controls.Add(label10);
            Controls.Add(cmbSendWaitUntil);
            Controls.Add(label9);
            Controls.Add(pnlInitialize);
            Controls.Add(groupBox1);
            Controls.Add(grpCustomHeders);
            Controls.Add(grpAttachments);
            Controls.Add(grpReceipents);
            Controls.Add(grpBody);
            Controls.Add(grpTrace);
            Controls.Add(btnSendEmail);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Email Communication Services";
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
            ((System.ComponentModel.ISupportInitialize)numEmailsToSend).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtConnString;
        private Button btnSendEmail;
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
        private DataGridViewTextBoxColumn colHeaderName;
        private DataGridViewTextBoxColumn colHeaderValue;
        private DataGridViewTextBoxColumn recEmailAddress;
        private DataGridViewTextBoxColumn recDisplayName;
        private DataGridViewComboBoxColumn recType;
        private Button btnInitializeConnString;
        private GroupBox groupBox1;
        private Button btnGetMsgDeliveryStatus;
        private TextBox txtMessageID;
        private Label label7;
        private Label label8;
        private DataGridViewTextBoxColumn attachFilePath;
        private DataGridViewTextBoxColumn attachType;
        private DataGridViewTextBoxColumn attachSize;
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
    }
}