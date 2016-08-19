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
            this.searchFilterCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.searchTotalFoundLabel = new System.Windows.Forms.Label();
            this.defaultTapeGroupbox = new System.Windows.Forms.GroupBox();
            this.searchFlowPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.defaultTapeGroupbox.SuspendLayout();
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
            // searchFilterCombo
            // 
            this.searchFilterCombo.FormattingEnabled = true;
            this.searchFilterCombo.Location = new System.Drawing.Point(693, 52);
            this.searchFilterCombo.Name = "searchFilterCombo";
            this.searchFilterCombo.Size = new System.Drawing.Size(139, 21);
            this.searchFilterCombo.TabIndex = 6;
            this.searchFilterCombo.SelectedIndexChanged += new System.EventHandler(this.searchFilterCombo_SelectedIndexChanged);
            this.searchFilterCombo.DropDownClosed += new System.EventHandler(this.searchFilterCombo_DropDownClosed);
            this.searchFilterCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchFilterCombo_KeyPress);
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
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel9);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel8);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel7);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel6);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel5);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel4);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel3);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel2);
            this.defaultTapeGroupbox.Controls.Add(this.searchFlowPanel1);
            this.defaultTapeGroupbox.Location = new System.Drawing.Point(12, 335);
            this.defaultTapeGroupbox.Name = "defaultTapeGroupbox";
            this.defaultTapeGroupbox.Size = new System.Drawing.Size(820, 251);
            this.defaultTapeGroupbox.TabIndex = 9;
            this.defaultTapeGroupbox.TabStop = false;
            // 
            // searchFlowPanel1
            // 
            this.searchFlowPanel1.Location = new System.Drawing.Point(6, 22);
            this.searchFlowPanel1.Name = "searchFlowPanel1";
            this.searchFlowPanel1.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel1.TabIndex = 0;
            // 
            // searchFlowPanel3
            // 
            this.searchFlowPanel3.Location = new System.Drawing.Point(6, 62);
            this.searchFlowPanel3.Name = "searchFlowPanel3";
            this.searchFlowPanel3.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel3.TabIndex = 1;
            // 
            // searchFlowPanel2
            // 
            this.searchFlowPanel2.Location = new System.Drawing.Point(6, 42);
            this.searchFlowPanel2.Name = "searchFlowPanel2";
            this.searchFlowPanel2.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel2.TabIndex = 1;
            // 
            // searchFlowPanel4
            // 
            this.searchFlowPanel4.Location = new System.Drawing.Point(6, 82);
            this.searchFlowPanel4.Name = "searchFlowPanel4";
            this.searchFlowPanel4.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel4.TabIndex = 1;
            // 
            // searchFlowPanel5
            // 
            this.searchFlowPanel5.Location = new System.Drawing.Point(6, 102);
            this.searchFlowPanel5.Name = "searchFlowPanel5";
            this.searchFlowPanel5.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel5.TabIndex = 1;
            // 
            // searchFlowPanel6
            // 
            this.searchFlowPanel6.Location = new System.Drawing.Point(6, 122);
            this.searchFlowPanel6.Name = "searchFlowPanel6";
            this.searchFlowPanel6.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel6.TabIndex = 1;
            // 
            // searchFlowPanel7
            // 
            this.searchFlowPanel7.Location = new System.Drawing.Point(6, 142);
            this.searchFlowPanel7.Name = "searchFlowPanel7";
            this.searchFlowPanel7.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel7.TabIndex = 2;
            // 
            // searchFlowPanel8
            // 
            this.searchFlowPanel8.Location = new System.Drawing.Point(6, 162);
            this.searchFlowPanel8.Name = "searchFlowPanel8";
            this.searchFlowPanel8.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel8.TabIndex = 1;
            // 
            // searchFlowPanel9
            // 
            this.searchFlowPanel9.Location = new System.Drawing.Point(6, 182);
            this.searchFlowPanel9.Name = "searchFlowPanel9";
            this.searchFlowPanel9.Size = new System.Drawing.Size(808, 20);
            this.searchFlowPanel9.TabIndex = 3;
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
            this.Controls.Add(this.searchFilterCombo);
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
        private System.Windows.Forms.ComboBox searchFilterCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label searchTotalFoundLabel;
        private System.Windows.Forms.GroupBox defaultTapeGroupbox;
        private System.Windows.Forms.ColumnHeader columnClip;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel9;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel8;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel7;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel6;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel5;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel4;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel3;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel2;
        private System.Windows.Forms.FlowLayoutPanel searchFlowPanel1;
    }
}