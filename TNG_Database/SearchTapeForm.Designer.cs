namespace TNG_Database
{
    partial class SearchTapeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.searchTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchListView = new System.Windows.Forms.ListView();
            this.columnPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTapeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTapeNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCamera = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMaster = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPerson = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnClip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchTotalFoundLabel = new System.Windows.Forms.Label();
            this.defaultTapeGroupbox = new System.Windows.Forms.GroupBox();
            this.searchItemsPanel = new System.Windows.Forms.Panel();
            this.searchClipNameLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.searchTagFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchTapeNumberLabel = new System.Windows.Forms.Label();
            this.searchTapeNameLabel = new System.Windows.Forms.Label();
            this.searchMasterArchiveLabel = new System.Windows.Forms.Label();
            this.searchPersonLabel = new System.Windows.Forms.Label();
            this.searchDateLabel = new System.Windows.Forms.Label();
            this.searchCameraLabel = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.searchProjectIDLabel = new System.Windows.Forms.Label();
            this.searchProjectNameLabel = new System.Windows.Forms.Label();
            this.searchNoItemSelectedLabel = new System.Windows.Forms.Label();
            this.defaultTapeGroupbox.SuspendLayout();
            this.searchItemsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(370, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Tape Form";
            // 
            // searchTextbox
            // 
            this.searchTextbox.Location = new System.Drawing.Point(12, 25);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(675, 20);
            this.searchTextbox.TabIndex = 1;
            this.searchTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchTextbox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Search Database:";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(693, 23);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(139, 23);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchListView
            // 
            this.searchListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPID,
            this.columnProjectName,
            this.columnTapeName,
            this.columnTapeNumber,
            this.columnCamera,
            this.columnTags,
            this.columnDate,
            this.columnMaster,
            this.columnPerson,
            this.columnClip});
            this.searchListView.FullRowSelect = true;
            this.searchListView.Location = new System.Drawing.Point(12, 79);
            this.searchListView.MultiSelect = false;
            this.searchListView.Name = "searchListView";
            this.searchListView.Size = new System.Drawing.Size(820, 250);
            this.searchListView.TabIndex = 5;
            this.searchListView.UseCompatibleStateImageBehavior = false;
            this.searchListView.View = System.Windows.Forms.View.Details;
            this.searchListView.SelectedIndexChanged += new System.EventHandler(this.searchListView_SelectedIndexChanged);
            // 
            // columnPID
            // 
            this.columnPID.Text = "Project ID";
            // 
            // columnProjectName
            // 
            this.columnProjectName.Text = "Project Name";
            this.columnProjectName.Width = 135;
            // 
            // columnTapeName
            // 
            this.columnTapeName.Text = "Tape Name";
            this.columnTapeName.Width = 135;
            // 
            // columnTapeNumber
            // 
            this.columnTapeNumber.Text = "Tape #";
            this.columnTapeNumber.Width = 25;
            // 
            // columnCamera
            // 
            this.columnCamera.Text = "Camera";
            // 
            // columnTags
            // 
            this.columnTags.Text = "Tags";
            this.columnTags.Width = 110;
            // 
            // columnDate
            // 
            this.columnDate.Text = "Date Shot";
            this.columnDate.Width = 78;
            // 
            // columnMaster
            // 
            this.columnMaster.Text = "Master Archive";
            this.columnMaster.Width = 95;
            // 
            // columnPerson
            // 
            this.columnPerson.Text = "Entered By";
            this.columnPerson.Width = 80;
            // 
            // columnClip
            // 
            this.columnClip.Text = "Clip";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(693, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(139, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(640, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Filter By:";
            // 
            // searchTotalFoundLabel
            // 
            this.searchTotalFoundLabel.AutoSize = true;
            this.searchTotalFoundLabel.Location = new System.Drawing.Point(12, 63);
            this.searchTotalFoundLabel.Name = "searchTotalFoundLabel";
            this.searchTotalFoundLabel.Size = new System.Drawing.Size(93, 13);
            this.searchTotalFoundLabel.TabIndex = 8;
            this.searchTotalFoundLabel.Text = "( 0 ) Entries Found";
            // 
            // defaultTapeGroupbox
            // 
            this.defaultTapeGroupbox.Controls.Add(this.searchItemsPanel);
            this.defaultTapeGroupbox.Controls.Add(this.searchNoItemSelectedLabel);
            this.defaultTapeGroupbox.Location = new System.Drawing.Point(12, 335);
            this.defaultTapeGroupbox.Name = "defaultTapeGroupbox";
            this.defaultTapeGroupbox.Size = new System.Drawing.Size(820, 195);
            this.defaultTapeGroupbox.TabIndex = 9;
            this.defaultTapeGroupbox.TabStop = false;
            // 
            // searchItemsPanel
            // 
            this.searchItemsPanel.Controls.Add(this.searchClipNameLabel);
            this.searchItemsPanel.Controls.Add(this.label5);
            this.searchItemsPanel.Controls.Add(this.searchTagFlowLayoutPanel);
            this.searchItemsPanel.Controls.Add(this.searchTapeNumberLabel);
            this.searchItemsPanel.Controls.Add(this.searchTapeNameLabel);
            this.searchItemsPanel.Controls.Add(this.searchMasterArchiveLabel);
            this.searchItemsPanel.Controls.Add(this.searchPersonLabel);
            this.searchItemsPanel.Controls.Add(this.searchDateLabel);
            this.searchItemsPanel.Controls.Add(this.searchCameraLabel);
            this.searchItemsPanel.Controls.Add(this.label34);
            this.searchItemsPanel.Controls.Add(this.label35);
            this.searchItemsPanel.Controls.Add(this.label36);
            this.searchItemsPanel.Controls.Add(this.label37);
            this.searchItemsPanel.Controls.Add(this.label38);
            this.searchItemsPanel.Controls.Add(this.label39);
            this.searchItemsPanel.Controls.Add(this.label40);
            this.searchItemsPanel.Controls.Add(this.label41);
            this.searchItemsPanel.Controls.Add(this.label42);
            this.searchItemsPanel.Controls.Add(this.searchProjectIDLabel);
            this.searchItemsPanel.Controls.Add(this.searchProjectNameLabel);
            this.searchItemsPanel.Location = new System.Drawing.Point(15, 32);
            this.searchItemsPanel.Name = "searchItemsPanel";
            this.searchItemsPanel.Size = new System.Drawing.Size(799, 155);
            this.searchItemsPanel.TabIndex = 45;
            // 
            // searchClipNameLabel
            // 
            this.searchClipNameLabel.AutoSize = true;
            this.searchClipNameLabel.Location = new System.Drawing.Point(490, 135);
            this.searchClipNameLabel.Name = "searchClipNameLabel";
            this.searchClipNameLabel.Size = new System.Drawing.Size(118, 13);
            this.searchClipNameLabel.TabIndex = 47;
            this.searchClipNameLabel.Text = "Clip Number If Selected";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(403, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Clip Number:";
            // 
            // searchTagFlowLayoutPanel
            // 
            this.searchTagFlowLayoutPanel.AutoScroll = true;
            this.searchTagFlowLayoutPanel.AutoScrollMinSize = new System.Drawing.Size(353, 62);
            this.searchTagFlowLayoutPanel.Location = new System.Drawing.Point(443, 62);
            this.searchTagFlowLayoutPanel.Name = "searchTagFlowLayoutPanel";
            this.searchTagFlowLayoutPanel.Size = new System.Drawing.Size(353, 62);
            this.searchTagFlowLayoutPanel.TabIndex = 45;
            // 
            // searchTapeNumberLabel
            // 
            this.searchTapeNumberLabel.AutoSize = true;
            this.searchTapeNumberLabel.Location = new System.Drawing.Point(490, 13);
            this.searchTapeNumberLabel.Name = "searchTapeNumberLabel";
            this.searchTapeNumberLabel.Size = new System.Drawing.Size(115, 13);
            this.searchTapeNumberLabel.TabIndex = 44;
            this.searchTapeNumberLabel.Text = "Tape Number selected";
            // 
            // searchTapeNameLabel
            // 
            this.searchTapeNameLabel.AutoSize = true;
            this.searchTapeNameLabel.Location = new System.Drawing.Point(490, 37);
            this.searchTapeNameLabel.Name = "searchTapeNameLabel";
            this.searchTapeNameLabel.Size = new System.Drawing.Size(106, 13);
            this.searchTapeNameLabel.TabIndex = 43;
            this.searchTapeNameLabel.Text = "Tape Name selected";
            // 
            // searchMasterArchiveLabel
            // 
            this.searchMasterArchiveLabel.AutoSize = true;
            this.searchMasterArchiveLabel.Location = new System.Drawing.Point(92, 135);
            this.searchMasterArchiveLabel.Name = "searchMasterArchiveLabel";
            this.searchMasterArchiveLabel.Size = new System.Drawing.Size(121, 13);
            this.searchMasterArchiveLabel.TabIndex = 41;
            this.searchMasterArchiveLabel.Text = "Master Archive selected";
            // 
            // searchPersonLabel
            // 
            this.searchPersonLabel.AutoSize = true;
            this.searchPersonLabel.Location = new System.Drawing.Point(92, 111);
            this.searchPersonLabel.Name = "searchPersonLabel";
            this.searchPersonLabel.Size = new System.Drawing.Size(83, 13);
            this.searchPersonLabel.TabIndex = 40;
            this.searchPersonLabel.Text = "Person selected";
            // 
            // searchDateLabel
            // 
            this.searchDateLabel.AutoSize = true;
            this.searchDateLabel.Location = new System.Drawing.Point(92, 87);
            this.searchDateLabel.Name = "searchDateLabel";
            this.searchDateLabel.Size = new System.Drawing.Size(73, 13);
            this.searchDateLabel.TabIndex = 39;
            this.searchDateLabel.Text = "Date selected";
            // 
            // searchCameraLabel
            // 
            this.searchCameraLabel.AutoSize = true;
            this.searchCameraLabel.Location = new System.Drawing.Point(92, 63);
            this.searchCameraLabel.Name = "searchCameraLabel";
            this.searchCameraLabel.Size = new System.Drawing.Size(86, 13);
            this.searchCameraLabel.TabIndex = 38;
            this.searchCameraLabel.Text = "Camera selected";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(15, 135);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(81, 13);
            this.label34.TabIndex = 37;
            this.label34.Text = "Master Archive:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(403, 63);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 13);
            this.label35.TabIndex = 36;
            this.label35.Text = "Tags:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(15, 111);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(62, 13);
            this.label36.TabIndex = 35;
            this.label36.Text = "Entered By:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(15, 87);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(58, 13);
            this.label37.TabIndex = 34;
            this.label37.Text = "Date Shot:";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(15, 63);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(46, 13);
            this.label38.TabIndex = 33;
            this.label38.Text = "Camera:";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(403, 37);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(66, 13);
            this.label39.TabIndex = 32;
            this.label39.Text = "Tape Name:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(403, 13);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(75, 13);
            this.label40.TabIndex = 31;
            this.label40.Text = "Tape Number:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(15, 37);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(74, 13);
            this.label41.TabIndex = 30;
            this.label41.Text = "Project Name:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(15, 13);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(57, 13);
            this.label42.TabIndex = 29;
            this.label42.Text = "Project ID:";
            // 
            // searchProjectIDLabel
            // 
            this.searchProjectIDLabel.AutoSize = true;
            this.searchProjectIDLabel.Location = new System.Drawing.Point(92, 13);
            this.searchProjectIDLabel.Name = "searchProjectIDLabel";
            this.searchProjectIDLabel.Size = new System.Drawing.Size(97, 13);
            this.searchProjectIDLabel.TabIndex = 28;
            this.searchProjectIDLabel.Text = "Project ID selected";
            // 
            // searchProjectNameLabel
            // 
            this.searchProjectNameLabel.AutoSize = true;
            this.searchProjectNameLabel.Location = new System.Drawing.Point(92, 37);
            this.searchProjectNameLabel.Name = "searchProjectNameLabel";
            this.searchProjectNameLabel.Size = new System.Drawing.Size(112, 13);
            this.searchProjectNameLabel.TabIndex = 27;
            this.searchProjectNameLabel.Text = "Project name selected";
            // 
            // searchNoItemSelectedLabel
            // 
            this.searchNoItemSelectedLabel.AutoSize = true;
            this.searchNoItemSelectedLabel.Location = new System.Drawing.Point(12, 16);
            this.searchNoItemSelectedLabel.Name = "searchNoItemSelectedLabel";
            this.searchNoItemSelectedLabel.Size = new System.Drawing.Size(200, 13);
            this.searchNoItemSelectedLabel.TabIndex = 0;
            this.searchNoItemSelectedLabel.Text = "Select an item from the list to view details";
            // 
            // SearchTapeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 587);
            this.ControlBox = false;
            this.Controls.Add(this.defaultTapeGroupbox);
            this.Controls.Add(this.searchTotalFoundLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.searchListView);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchTextbox);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "SearchTapeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SearchTapeForm";
            this.TopMost = true;
            this.defaultTapeGroupbox.ResumeLayout(false);
            this.defaultTapeGroupbox.PerformLayout();
            this.searchItemsPanel.ResumeLayout(false);
            this.searchItemsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListView searchListView;
        private System.Windows.Forms.ColumnHeader columnPID;
        private System.Windows.Forms.ColumnHeader columnProjectName;
        private System.Windows.Forms.ColumnHeader columnTapeName;
        private System.Windows.Forms.ColumnHeader columnTapeNumber;
        private System.Windows.Forms.ColumnHeader columnCamera;
        private System.Windows.Forms.ColumnHeader columnTags;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnMaster;
        private System.Windows.Forms.ColumnHeader columnPerson;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label searchTotalFoundLabel;
        private System.Windows.Forms.GroupBox defaultTapeGroupbox;
        private System.Windows.Forms.Panel searchItemsPanel;
        private System.Windows.Forms.FlowLayoutPanel searchTagFlowLayoutPanel;
        private System.Windows.Forms.Label searchTapeNumberLabel;
        private System.Windows.Forms.Label searchTapeNameLabel;
        private System.Windows.Forms.Label searchMasterArchiveLabel;
        private System.Windows.Forms.Label searchPersonLabel;
        private System.Windows.Forms.Label searchDateLabel;
        private System.Windows.Forms.Label searchCameraLabel;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label searchProjectIDLabel;
        private System.Windows.Forms.Label searchProjectNameLabel;
        private System.Windows.Forms.Label searchNoItemSelectedLabel;
        private System.Windows.Forms.Label searchClipNameLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnClip;
    }
}