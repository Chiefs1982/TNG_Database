namespace TNG_Database.DatabaseControls
{
    partial class DeletedValuesForm
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
            this.tapeListListView = new System.Windows.Forms.ListView();
            this.columnPID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTapeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTapeNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCamera = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnTags = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMaster = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPerson = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tapeDatabasePanel = new System.Windows.Forms.Panel();
            this.deleteFormSelectCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tapeDatabasePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tapeListListView
            // 
            this.tapeListListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPID,
            this.columnProjectName,
            this.columnTapeName,
            this.columnTapeNumber,
            this.columnCamera,
            this.columnTags,
            this.columnDate,
            this.columnMaster,
            this.columnPerson});
            this.tapeListListView.FullRowSelect = true;
            this.tapeListListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.tapeListListView.Location = new System.Drawing.Point(13, 15);
            this.tapeListListView.MultiSelect = false;
            this.tapeListListView.Name = "tapeListListView";
            this.tapeListListView.Size = new System.Drawing.Size(786, 282);
            this.tapeListListView.TabIndex = 1;
            this.tapeListListView.UseCompatibleStateImageBehavior = false;
            this.tapeListListView.View = System.Windows.Forms.View.Details;
            // 
            // columnPID
            // 
            this.columnPID.Text = "Project ID";
            // 
            // columnProjectName
            // 
            this.columnProjectName.Text = "Project Name";
            this.columnProjectName.Width = 200;
            // 
            // columnTapeName
            // 
            this.columnTapeName.Text = "Tape Name";
            this.columnTapeName.Width = 120;
            // 
            // columnTapeNumber
            // 
            this.columnTapeNumber.Text = "Tape #";
            this.columnTapeNumber.Width = 50;
            // 
            // columnCamera
            // 
            this.columnCamera.Text = "Camera";
            this.columnCamera.Width = 50;
            // 
            // columnTags
            // 
            this.columnTags.Text = "Tags";
            this.columnTags.Width = 62;
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
            // 
            // tapeDatabasePanel
            // 
            this.tapeDatabasePanel.Controls.Add(this.tapeListListView);
            this.tapeDatabasePanel.Location = new System.Drawing.Point(12, 34);
            this.tapeDatabasePanel.Name = "tapeDatabasePanel";
            this.tapeDatabasePanel.Size = new System.Drawing.Size(810, 531);
            this.tapeDatabasePanel.TabIndex = 2;
            // 
            // deleteFormSelectCombo
            // 
            this.deleteFormSelectCombo.FormattingEnabled = true;
            this.deleteFormSelectCombo.Location = new System.Drawing.Point(81, 7);
            this.deleteFormSelectCombo.Name = "deleteFormSelectCombo";
            this.deleteFormSelectCombo.Size = new System.Drawing.Size(181, 21);
            this.deleteFormSelectCombo.TabIndex = 3;
            this.deleteFormSelectCombo.SelectedIndexChanged += new System.EventHandler(this.deleteFormSelectCombo_SelectedIndexChanged);
            this.deleteFormSelectCombo.DropDownClosed += new System.EventHandler(this.deleteFormSelectCombo_DropDownClosed);
            this.deleteFormSelectCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.deleteFormSelectCombo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Select Form:";
            // 
            // DeletedValuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 577);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteFormSelectCombo);
            this.Controls.Add(this.tapeDatabasePanel);
            this.Name = "DeletedValuesForm";
            this.Text = "DeletedValuesForm";
            this.tapeDatabasePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView tapeListListView;
        private System.Windows.Forms.ColumnHeader columnPID;
        private System.Windows.Forms.ColumnHeader columnProjectName;
        private System.Windows.Forms.ColumnHeader columnTapeName;
        private System.Windows.Forms.ColumnHeader columnTapeNumber;
        private System.Windows.Forms.ColumnHeader columnCamera;
        private System.Windows.Forms.ColumnHeader columnTags;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.ColumnHeader columnMaster;
        private System.Windows.Forms.ColumnHeader columnPerson;
        private System.Windows.Forms.Panel tapeDatabasePanel;
        private System.Windows.Forms.ComboBox deleteFormSelectCombo;
        private System.Windows.Forms.Label label1;
    }
}