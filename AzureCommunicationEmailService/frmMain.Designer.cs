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
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnString = new System.Windows.Forms.TextBox();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.grpAttachments = new System.Windows.Forms.GroupBox();
            this.lblAttachmentsSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAttachmentsCount = new System.Windows.Forms.Label();
            this.dgAttachments = new System.Windows.Forms.DataGridView();
            this.attachFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attachType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.attachSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddAttachment = new System.Windows.Forms.Button();
            this.chkIsHtmlBody = new System.Windows.Forms.CheckBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgReceipeints = new System.Windows.Forms.DataGridView();
            this.recEmailAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recDisplayName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpTrace = new System.Windows.Forms.GroupBox();
            this.txtTrace = new System.Windows.Forms.TextBox();
            this.grpBody = new System.Windows.Forms.GroupBox();
            this.grpReceipents = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbImportance = new System.Windows.Forms.ComboBox();
            this.grpCustomHeders = new System.Windows.Forms.GroupBox();
            this.dgCustomHeaders = new System.Windows.Forms.DataGridView();
            this.colHeaderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeaderValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnInitializeConnString = new System.Windows.Forms.Button();
            this.grpAttachments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipeints)).BeginInit();
            this.grpTrace.SuspendLayout();
            this.grpBody.SuspendLayout();
            this.grpReceipents.SuspendLayout();
            this.grpCustomHeders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomHeaders)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection string:";
            // 
            // txtConnString
            // 
            this.txtConnString.Location = new System.Drawing.Point(146, 14);
            this.txtConnString.Name = "txtConnString";
            this.txtConnString.Size = new System.Drawing.Size(596, 27);
            this.txtConnString.TabIndex = 0;
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Location = new System.Drawing.Point(690, 739);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(152, 40);
            this.btnSendEmail.TabIndex = 9;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.UseVisualStyleBackColor = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // grpAttachments
            // 
            this.grpAttachments.Controls.Add(this.lblAttachmentsSize);
            this.grpAttachments.Controls.Add(this.label4);
            this.grpAttachments.Controls.Add(this.label3);
            this.grpAttachments.Controls.Add(this.lblAttachmentsCount);
            this.grpAttachments.Controls.Add(this.dgAttachments);
            this.grpAttachments.Controls.Add(this.btnAddAttachment);
            this.grpAttachments.Location = new System.Drawing.Point(12, 518);
            this.grpAttachments.Name = "grpAttachments";
            this.grpAttachments.Size = new System.Drawing.Size(830, 215);
            this.grpAttachments.TabIndex = 8;
            this.grpAttachments.TabStop = false;
            this.grpAttachments.Text = "Attachments";
            // 
            // lblAttachmentsSize
            // 
            this.lblAttachmentsSize.AutoSize = true;
            this.lblAttachmentsSize.Location = new System.Drawing.Point(460, 31);
            this.lblAttachmentsSize.Name = "lblAttachmentsSize";
            this.lblAttachmentsSize.Size = new System.Drawing.Size(17, 20);
            this.lblAttachmentsSize.TabIndex = 15;
            this.lblAttachmentsSize.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Total size (Byte):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Total count:";
            // 
            // lblAttachmentsCount
            // 
            this.lblAttachmentsCount.AutoSize = true;
            this.lblAttachmentsCount.Location = new System.Drawing.Point(225, 30);
            this.lblAttachmentsCount.Name = "lblAttachmentsCount";
            this.lblAttachmentsCount.Size = new System.Drawing.Size(17, 20);
            this.lblAttachmentsCount.TabIndex = 12;
            this.lblAttachmentsCount.Text = "0";
            // 
            // dgAttachments
            // 
            this.dgAttachments.AllowUserToAddRows = false;
            this.dgAttachments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAttachments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.attachFilePath,
            this.attachType,
            this.attachSize});
            this.dgAttachments.Location = new System.Drawing.Point(6, 61);
            this.dgAttachments.Name = "dgAttachments";
            this.dgAttachments.RowHeadersWidth = 51;
            this.dgAttachments.RowTemplate.Height = 29;
            this.dgAttachments.Size = new System.Drawing.Size(818, 147);
            this.dgAttachments.TabIndex = 2;
            this.dgAttachments.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgAttachments_RowsAdded);
            this.dgAttachments.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dgAttachments_RowsRemoved);
            // 
            // attachFilePath
            // 
            this.attachFilePath.HeaderText = "File path";
            this.attachFilePath.MinimumWidth = 6;
            this.attachFilePath.Name = "attachFilePath";
            this.attachFilePath.ReadOnly = true;
            this.attachFilePath.Width = 513;
            // 
            // attachType
            // 
            this.attachType.HeaderText = "Type";
            this.attachType.Items.AddRange(new object[] {
            "avi",
            "bmp",
            "doc",
            "dom",
            "docx",
            "gif",
            "jpeg",
            "mp3",
            "one",
            "pdf",
            "png",
            "ppsm",
            "ppsx",
            "ppt",
            "pptm",
            "pptx",
            "pub",
            "rpmsg",
            "rtf",
            "tif",
            "txt",
            "vsd",
            "wav",
            "wma",
            "xls",
            "xlsb",
            "xlsm",
            "xlsx"});
            this.attachType.MinimumWidth = 6;
            this.attachType.Name = "attachType";
            this.attachType.Width = 125;
            // 
            // attachSize
            // 
            this.attachSize.HeaderText = "Size (Byte)";
            this.attachSize.MinimumWidth = 6;
            this.attachSize.Name = "attachSize";
            this.attachSize.Width = 125;
            // 
            // btnAddAttachment
            // 
            this.btnAddAttachment.Location = new System.Drawing.Point(6, 26);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(131, 29);
            this.btnAddAttachment.TabIndex = 1;
            this.btnAddAttachment.Text = "Add attachment";
            this.btnAddAttachment.UseVisualStyleBackColor = true;
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // chkIsHtmlBody
            // 
            this.chkIsHtmlBody.AutoSize = true;
            this.chkIsHtmlBody.Location = new System.Drawing.Point(734, 28);
            this.chkIsHtmlBody.Name = "chkIsHtmlBody";
            this.chkIsHtmlBody.Size = new System.Drawing.Size(84, 24);
            this.chkIsHtmlBody.TabIndex = 2;
            this.chkIsHtmlBody.Text = "Is HTML";
            this.chkIsHtmlBody.UseVisualStyleBackColor = true;
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(146, 49);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(690, 27);
            this.txtFrom.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "From:";
            // 
            // dgReceipeints
            // 
            this.dgReceipeints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReceipeints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.recEmailAddress,
            this.recDisplayName,
            this.recType});
            this.dgReceipeints.Location = new System.Drawing.Point(6, 26);
            this.dgReceipeints.Name = "dgReceipeints";
            this.dgReceipeints.RowHeadersWidth = 51;
            this.dgReceipeints.RowTemplate.Height = 29;
            this.dgReceipeints.Size = new System.Drawing.Size(516, 147);
            this.dgReceipeints.TabIndex = 5;
            // 
            // recEmailAddress
            // 
            this.recEmailAddress.HeaderText = "Email Address";
            this.recEmailAddress.MinimumWidth = 6;
            this.recEmailAddress.Name = "recEmailAddress";
            this.recEmailAddress.Width = 240;
            // 
            // recDisplayName
            // 
            this.recDisplayName.HeaderText = "Display Name";
            this.recDisplayName.MinimumWidth = 6;
            this.recDisplayName.Name = "recDisplayName";
            this.recDisplayName.Width = 133;
            // 
            // recType
            // 
            this.recType.HeaderText = "Type";
            this.recType.Items.AddRange(new object[] {
            "To",
            "CC",
            "BCC",
            "Reply To"});
            this.recType.MinimumWidth = 6;
            this.recType.Name = "recType";
            this.recType.Width = 90;
            // 
            // txtBody
            // 
            this.txtBody.Location = new System.Drawing.Point(6, 26);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBody.Size = new System.Drawing.Size(722, 141);
            this.txtBody.TabIndex = 1;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(146, 82);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(690, 27);
            this.txtSubject.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Subject:";
            // 
            // grpTrace
            // 
            this.grpTrace.Controls.Add(this.txtTrace);
            this.grpTrace.Location = new System.Drawing.Point(848, 12);
            this.grpTrace.Name = "grpTrace";
            this.grpTrace.Size = new System.Drawing.Size(752, 767);
            this.grpTrace.TabIndex = 10;
            this.grpTrace.TabStop = false;
            this.grpTrace.Text = "Trace";
            // 
            // txtTrace
            // 
            this.txtTrace.Location = new System.Drawing.Point(6, 26);
            this.txtTrace.Multiline = true;
            this.txtTrace.Name = "txtTrace";
            this.txtTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTrace.Size = new System.Drawing.Size(740, 735);
            this.txtTrace.TabIndex = 0;
            // 
            // grpBody
            // 
            this.grpBody.Controls.Add(this.chkIsHtmlBody);
            this.grpBody.Controls.Add(this.txtBody);
            this.grpBody.Location = new System.Drawing.Point(12, 149);
            this.grpBody.Name = "grpBody";
            this.grpBody.Size = new System.Drawing.Size(824, 177);
            this.grpBody.TabIndex = 5;
            this.grpBody.TabStop = false;
            this.grpBody.Text = "Body";
            // 
            // grpReceipents
            // 
            this.grpReceipents.Controls.Add(this.dgReceipeints);
            this.grpReceipents.Location = new System.Drawing.Point(12, 332);
            this.grpReceipents.Name = "grpReceipents";
            this.grpReceipents.Size = new System.Drawing.Size(528, 180);
            this.grpReceipents.TabIndex = 6;
            this.grpReceipents.TabStop = false;
            this.grpReceipents.Text = "Receipents";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Importance:";
            // 
            // cmbImportance
            // 
            this.cmbImportance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImportance.FormattingEnabled = true;
            this.cmbImportance.ItemHeight = 20;
            this.cmbImportance.Items.AddRange(new object[] {
            "Default",
            "Low",
            "Normal",
            "High"});
            this.cmbImportance.Location = new System.Drawing.Point(146, 115);
            this.cmbImportance.Name = "cmbImportance";
            this.cmbImportance.Size = new System.Drawing.Size(160, 28);
            this.cmbImportance.TabIndex = 16;
            // 
            // grpCustomHeders
            // 
            this.grpCustomHeders.Controls.Add(this.dgCustomHeaders);
            this.grpCustomHeders.Location = new System.Drawing.Point(546, 332);
            this.grpCustomHeders.Name = "grpCustomHeders";
            this.grpCustomHeders.Size = new System.Drawing.Size(296, 180);
            this.grpCustomHeders.TabIndex = 7;
            this.grpCustomHeders.TabStop = false;
            this.grpCustomHeders.Text = "Custom Headers";
            // 
            // dgCustomHeaders
            // 
            this.dgCustomHeaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCustomHeaders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHeaderName,
            this.colHeaderValue});
            this.dgCustomHeaders.Location = new System.Drawing.Point(6, 26);
            this.dgCustomHeaders.Name = "dgCustomHeaders";
            this.dgCustomHeaders.RowHeadersWidth = 51;
            this.dgCustomHeaders.RowTemplate.Height = 29;
            this.dgCustomHeaders.Size = new System.Drawing.Size(284, 147);
            this.dgCustomHeaders.TabIndex = 0;
            // 
            // colHeaderName
            // 
            this.colHeaderName.HeaderText = "Name";
            this.colHeaderName.MinimumWidth = 6;
            this.colHeaderName.Name = "colHeaderName";
            this.colHeaderName.Width = 115;
            // 
            // colHeaderValue
            // 
            this.colHeaderValue.HeaderText = "Value";
            this.colHeaderValue.MinimumWidth = 6;
            this.colHeaderValue.Name = "colHeaderValue";
            this.colHeaderValue.Width = 115;
            // 
            // btnInitializeConnString
            // 
            this.btnInitializeConnString.Location = new System.Drawing.Point(748, 12);
            this.btnInitializeConnString.Name = "btnInitializeConnString";
            this.btnInitializeConnString.Size = new System.Drawing.Size(94, 29);
            this.btnInitializeConnString.TabIndex = 1;
            this.btnInitializeConnString.Text = "Initialize";
            this.btnInitializeConnString.UseVisualStyleBackColor = true;
            this.btnInitializeConnString.Click += new System.EventHandler(this.btnInitializeConnString_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1613, 793);
            this.Controls.Add(this.btnInitializeConnString);
            this.Controls.Add(this.grpCustomHeders);
            this.Controls.Add(this.cmbImportance);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.grpAttachments);
            this.Controls.Add(this.grpReceipents);
            this.Controls.Add(this.grpBody);
            this.Controls.Add(this.grpTrace);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtConnString);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Communication Services";
            this.grpAttachments.ResumeLayout(false);
            this.grpAttachments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipeints)).EndInit();
            this.grpTrace.ResumeLayout(false);
            this.grpTrace.PerformLayout();
            this.grpBody.ResumeLayout(false);
            this.grpBody.PerformLayout();
            this.grpReceipents.ResumeLayout(false);
            this.grpCustomHeders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomHeaders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DataGridViewTextBoxColumn attachFilePath;
        private DataGridViewComboBoxColumn attachType;
        private DataGridViewTextBoxColumn attachSize;
    }
}