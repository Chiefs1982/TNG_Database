namespace TNG_Database
{
    partial class PreferencesForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.databaseBackupCombobox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.usersStats = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.archiveVideosStats = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.projectStats = new System.Windows.Forms.Label();
            this.archiveTapesStats = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tapeStats = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.exportPrefBrowseButton = new System.Windows.Forms.Button();
            this.importPrefBrowseButton = new System.Windows.Forms.Button();
            this.exportPrefDirTexbox = new System.Windows.Forms.TextBox();
            this.importPrefDirTexbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.databaseBackupCombobox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.exportPrefBrowseButton);
            this.groupBox1.Controls.Add(this.importPrefBrowseButton);
            this.groupBox1.Controls.Add(this.exportPrefDirTexbox);
            this.groupBox1.Controls.Add(this.importPrefDirTexbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(820, 397);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preferences";
            // 
            // databaseBackupCombobox
            // 
            this.databaseBackupCombobox.FormattingEnabled = true;
            this.databaseBackupCombobox.Location = new System.Drawing.Point(225, 121);
            this.databaseBackupCombobox.Name = "databaseBackupCombobox";
            this.databaseBackupCombobox.Size = new System.Drawing.Size(108, 21);
            this.databaseBackupCombobox.TabIndex = 9;
            this.databaseBackupCombobox.SelectedIndexChanged += new System.EventHandler(this.databaseBackupCombobox_SelectedIndexChanged);
            this.databaseBackupCombobox.DropDownClosed += new System.EventHandler(this.databaseBackupCombobox_DropDownClosed);
            this.databaseBackupCombobox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.databaseBackupCombobox_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(185, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Every";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Backup Database Timeframe:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.usersStats);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.archiveVideosStats);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.projectStats);
            this.groupBox2.Controls.Add(this.archiveTapesStats);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tapeStats);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(586, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(228, 184);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stats";
            // 
            // usersStats
            // 
            this.usersStats.AutoSize = true;
            this.usersStats.Location = new System.Drawing.Point(147, 142);
            this.usersStats.Name = "usersStats";
            this.usersStats.Size = new System.Drawing.Size(41, 13);
            this.usersStats.TabIndex = 9;
            this.usersStats.Text = "label12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 142);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Users:";
            // 
            // archiveVideosStats
            // 
            this.archiveVideosStats.AutoSize = true;
            this.archiveVideosStats.Location = new System.Drawing.Point(147, 114);
            this.archiveVideosStats.Name = "archiveVideosStats";
            this.archiveVideosStats.Size = new System.Drawing.Size(41, 13);
            this.archiveVideosStats.TabIndex = 7;
            this.archiveVideosStats.Text = "label10";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Archived Videos:";
            // 
            // projectStats
            // 
            this.projectStats.AutoSize = true;
            this.projectStats.Location = new System.Drawing.Point(147, 61);
            this.projectStats.Name = "projectStats";
            this.projectStats.Size = new System.Drawing.Size(35, 13);
            this.projectStats.TabIndex = 5;
            this.projectStats.Text = "label8";
            // 
            // archiveTapesStats
            // 
            this.archiveTapesStats.AutoSize = true;
            this.archiveTapesStats.Location = new System.Drawing.Point(147, 87);
            this.archiveTapesStats.Name = "archiveTapesStats";
            this.archiveTapesStats.Size = new System.Drawing.Size(35, 13);
            this.archiveTapesStats.TabIndex = 4;
            this.archiveTapesStats.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Archive Tapes:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Projects:";
            // 
            // tapeStats
            // 
            this.tapeStats.AutoSize = true;
            this.tapeStats.Location = new System.Drawing.Point(147, 36);
            this.tapeStats.Name = "tapeStats";
            this.tapeStats.Size = new System.Drawing.Size(35, 13);
            this.tapeStats.TabIndex = 1;
            this.tapeStats.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tapes:";
            // 
            // exportPrefBrowseButton
            // 
            this.exportPrefBrowseButton.Location = new System.Drawing.Point(680, 75);
            this.exportPrefBrowseButton.Name = "exportPrefBrowseButton";
            this.exportPrefBrowseButton.Size = new System.Drawing.Size(95, 23);
            this.exportPrefBrowseButton.TabIndex = 5;
            this.exportPrefBrowseButton.Text = "Browse";
            this.exportPrefBrowseButton.UseVisualStyleBackColor = true;
            this.exportPrefBrowseButton.Click += new System.EventHandler(this.exportPrefBrowseButton_Click);
            // 
            // importPrefBrowseButton
            // 
            this.importPrefBrowseButton.Location = new System.Drawing.Point(680, 44);
            this.importPrefBrowseButton.Name = "importPrefBrowseButton";
            this.importPrefBrowseButton.Size = new System.Drawing.Size(95, 23);
            this.importPrefBrowseButton.TabIndex = 4;
            this.importPrefBrowseButton.Text = "Browse";
            this.importPrefBrowseButton.UseVisualStyleBackColor = true;
            this.importPrefBrowseButton.Click += new System.EventHandler(this.importPrefBrowseButton_Click);
            // 
            // exportPrefDirTexbox
            // 
            this.exportPrefDirTexbox.Location = new System.Drawing.Point(117, 77);
            this.exportPrefDirTexbox.Name = "exportPrefDirTexbox";
            this.exportPrefDirTexbox.Size = new System.Drawing.Size(557, 20);
            this.exportPrefDirTexbox.TabIndex = 3;
            // 
            // importPrefDirTexbox
            // 
            this.importPrefDirTexbox.Location = new System.Drawing.Point(117, 46);
            this.importPrefDirTexbox.Name = "importPrefDirTexbox";
            this.importPrefDirTexbox.Size = new System.Drawing.Size(557, 20);
            this.importPrefDirTexbox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Export Directory:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Import Directory:";
            // 
            // PreferencesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 587);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PreferencesForm";
            this.Text = "PreferencesForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button exportPrefBrowseButton;
        private System.Windows.Forms.Button importPrefBrowseButton;
        private System.Windows.Forms.TextBox exportPrefDirTexbox;
        private System.Windows.Forms.TextBox importPrefDirTexbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox databaseBackupCombobox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label usersStats;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label archiveVideosStats;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label projectStats;
        private System.Windows.Forms.Label archiveTapesStats;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label tapeStats;
        private System.Windows.Forms.Label label3;
    }
}