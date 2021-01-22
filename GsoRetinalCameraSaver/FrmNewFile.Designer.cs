namespace GsoRetinalCameraSaver
{
    partial class FrmNewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewFile));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSurname = new System.Windows.Forms.ComboBox();
            this.cmbFirstname = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.labFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You have generated a new Patient Retinal Camera Report.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Please select the Patient\'s details:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(103, 202);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilename.Location = new System.Drawing.Point(103, 45);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(409, 20);
            this.txtFilename.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "File name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "SURNAME:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "First Name:";
            // 
            // cmbSurname
            // 
            this.cmbSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSurname.FormattingEnabled = true;
            this.cmbSurname.Location = new System.Drawing.Point(103, 123);
            this.cmbSurname.Name = "cmbSurname";
            this.cmbSurname.Size = new System.Drawing.Size(409, 21);
            this.cmbSurname.TabIndex = 9;
            this.cmbSurname.TextChanged += new System.EventHandler(this.cmbSurname_TextChanged);
            // 
            // cmbFirstname
            // 
            this.cmbFirstname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFirstname.FormattingEnabled = true;
            this.cmbFirstname.Location = new System.Drawing.Point(103, 149);
            this.cmbFirstname.Name = "cmbFirstname";
            this.cmbFirstname.Size = new System.Drawing.Size(409, 21);
            this.cmbFirstname.TabIndex = 10;
            this.cmbFirstname.TextChanged += new System.EventHandler(this.cmbFirstname_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(330, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(182, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel (don\'t move this file)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Date:";
            // 
            // txtDate
            // 
            this.txtDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDate.Location = new System.Drawing.Point(103, 71);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(409, 20);
            this.txtDate.TabIndex = 12;
            // 
            // labFolder
            // 
            this.labFolder.AutoSize = true;
            this.labFolder.Location = new System.Drawing.Point(42, 179);
            this.labFolder.Name = "labFolder";
            this.labFolder.Size = new System.Drawing.Size(39, 13);
            this.labFolder.TabIndex = 15;
            this.labFolder.Text = "Folder:";
            // 
            // txtFolder
            // 
            this.txtFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolder.Location = new System.Drawing.Point(103, 176);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.ReadOnly = true;
            this.txtFolder.Size = new System.Drawing.Size(409, 20);
            this.txtFolder.TabIndex = 14;
            // 
            // FrmNewFile
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(524, 236);
            this.Controls.Add(this.labFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbFirstname);
            this.Controls.Add(this.cmbSurname);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNewFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Patient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSurname;
        private System.Windows.Forms.ComboBox cmbFirstname;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label labFolder;
        private System.Windows.Forms.TextBox txtFolder;
    }
}