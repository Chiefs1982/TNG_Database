namespace TNG_Database
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
            this.databaseListView = new System.Windows.Forms.ListView();
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
            this.deleteReinstateButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.deleteFormSelectCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tapeDatabasePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // databaseListView
            // 
            this.databaseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPID,
            this.columnProjectName,
            this.columnTapeName,
            this.columnTapeNumber,
            this.columnCamera,
            this.columnTags,
            this.columnDate,
            this.columnMaster,
            this.columnPerson});
            this.databaseListView.FullRowSelect = true;
            this.databaseListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.databaseListView.Location = new System.Drawing.Point(12, 73);
            this.databaseListView.MultiSelect = false;
            this.databaseListView.Name = "databaseListView";
            this.databaseListView.Size = new System.Drawing.Size(786, 282);
            this.databaseListView.TabIndex = 2;
            this.databaseListView.UseCompatibleStateImageBehavior = false;
            this.databaseListView.View = System.Windows.Forms.View.Details;
            this.databaseListView.SelectedIndexChanged += new System.EventHandler(this.databaseListView_SelectedIndexChanged);
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
            this.tapeDatabasePanel.Controls.Add(this.deleteReinstateButton);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel9);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel8);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel7);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel6);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel5);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel4);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel3);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel2);
            this.tapeDatabasePanel.Controls.Add(this.flowLayoutPanel1);
            this.tapeDatabasePanel.Location = new System.Drawing.Point(12, 361);
            this.tapeDatabasePanel.Name = "tapeDatabasePanel";
            this.tapeDatabasePanel.Size = new System.Drawing.Size(786, 204);
            this.tapeDatabasePanel.TabIndex = 2;
            // 
            // deleteReinstateButton
            // 
            this.deleteReinstateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteReinstateButton.Location = new System.Drawing.Point(540, 3);
            this.deleteReinstateButton.Name = "deleteReinstateButton";
            this.deleteReinstateButton.Size = new System.Drawing.Size(243, 59);
            this.deleteReinstateButton.TabIndex = 3;
            this.deleteReinstateButton.Text = "Reinstate";
            this.deleteReinstateButton.UseVisualStyleBackColor = true;
            this.deleteReinstateButton.Click += new System.EventHandler(this.deleteReinstateButton_Click);
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 153);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel9.TabIndex = 2;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Location = new System.Drawing.Point(3, 134);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel8.TabIndex = 1;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 116);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel7.TabIndex = 1;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 97);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel6.TabIndex = 1;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 78);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel5.TabIndex = 1;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 59);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel4.TabIndex = 1;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 40);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 22);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(531, 22);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // deleteFormSelectCombo
            // 
            this.deleteFormSelectCombo.FormattingEnabled = true;
            this.deleteFormSelectCombo.Location = new System.Drawing.Point(81, 46);
            this.deleteFormSelectCombo.Name = "deleteFormSelectCombo";
            this.deleteFormSelectCombo.Size = new System.Drawing.Size(181, 21);
            this.deleteFormSelectCombo.TabIndex = 1;
            this.deleteFormSelectCombo.SelectedIndexChanged += new System.EventHandler(this.deleteFormSelectCombo_SelectedIndexChanged);
            this.deleteFormSelectCombo.DropDownClosed += new System.EventHandler(this.deleteFormSelectCombo_DropDownClosed);
            this.deleteFormSelectCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.deleteFormSelectCombo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 49);
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
            this.ControlBox = false;
            this.Controls.Add(this.databaseListView);
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

        private System.Windows.Forms.ListView databaseListView;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button deleteReinstateButton;
    }
}