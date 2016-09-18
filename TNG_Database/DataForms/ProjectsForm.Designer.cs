namespace TNG_Database
{
    partial class ProjectsForm
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
            this.projectsListView = new System.Windows.Forms.ListView();
            this.columnProjectID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnProjectName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.projectsAddButton = new System.Windows.Forms.Button();
            this.projectsUpdateButton = new System.Windows.Forms.Button();
            this.projectsDeleteButton = new System.Windows.Forms.Button();
            this.projectsDefaultGroupBox = new System.Windows.Forms.GroupBox();
            this.defaultLabelPanel = new System.Windows.Forms.Panel();
            this.defaultProjectNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.defaultProjectIDLabel = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.defaultLabel = new System.Windows.Forms.Label();
            this.projectAddGroupBox = new System.Windows.Forms.GroupBox();
            this.addProjectCancelButton = new System.Windows.Forms.Button();
            this.addProjectAddButton = new System.Windows.Forms.Button();
            this.addProjectNameTextBox = new System.Windows.Forms.TextBox();
            this.addProjectIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.editProjectsGroupBox = new System.Windows.Forms.GroupBox();
            this.editProjectCancelButton = new System.Windows.Forms.Button();
            this.editProjectEditButton = new System.Windows.Forms.Button();
            this.editProjectNameTextBox = new System.Windows.Forms.TextBox();
            this.editProjectIDTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.deleteProjectsGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteProjectNameLabel = new System.Windows.Forms.Label();
            this.deleteProjectIDLabel = new System.Windows.Forms.Label();
            this.deleteProjectCancelButton = new System.Windows.Forms.Button();
            this.deleteProjectDeleteButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.projectsDefaultGroupBox.SuspendLayout();
            this.defaultLabelPanel.SuspendLayout();
            this.projectAddGroupBox.SuspendLayout();
            this.editProjectsGroupBox.SuspendLayout();
            this.deleteProjectsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectsListView
            // 
            this.projectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnProjectID,
            this.columnProjectName});
            this.projectsListView.FullRowSelect = true;
            this.projectsListView.Location = new System.Drawing.Point(12, 68);
            this.projectsListView.Name = "projectsListView";
            this.projectsListView.Size = new System.Drawing.Size(311, 440);
            this.projectsListView.TabIndex = 4;
            this.projectsListView.UseCompatibleStateImageBehavior = false;
            this.projectsListView.View = System.Windows.Forms.View.Details;
            this.projectsListView.SelectedIndexChanged += new System.EventHandler(this.projectsListView_SelectedIndexChanged);
            // 
            // columnProjectID
            // 
            this.columnProjectID.Text = "Project ID";
            this.columnProjectID.Width = 100;
            // 
            // columnProjectName
            // 
            this.columnProjectName.Text = "Project Name";
            this.columnProjectName.Width = 200;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Projects";
            // 
            // projectsAddButton
            // 
            this.projectsAddButton.Location = new System.Drawing.Point(340, 68);
            this.projectsAddButton.Name = "projectsAddButton";
            this.projectsAddButton.Size = new System.Drawing.Size(75, 23);
            this.projectsAddButton.TabIndex = 1;
            this.projectsAddButton.Text = "Add";
            this.projectsAddButton.UseVisualStyleBackColor = true;
            this.projectsAddButton.Click += new System.EventHandler(this.projectsAddButton_Click);
            // 
            // projectsUpdateButton
            // 
            this.projectsUpdateButton.Location = new System.Drawing.Point(340, 97);
            this.projectsUpdateButton.Name = "projectsUpdateButton";
            this.projectsUpdateButton.Size = new System.Drawing.Size(75, 23);
            this.projectsUpdateButton.TabIndex = 2;
            this.projectsUpdateButton.Text = "Update";
            this.projectsUpdateButton.UseVisualStyleBackColor = true;
            this.projectsUpdateButton.Click += new System.EventHandler(this.projectsUpdateButton_Click);
            // 
            // projectsDeleteButton
            // 
            this.projectsDeleteButton.Location = new System.Drawing.Point(340, 126);
            this.projectsDeleteButton.Name = "projectsDeleteButton";
            this.projectsDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.projectsDeleteButton.TabIndex = 3;
            this.projectsDeleteButton.Text = "Delete";
            this.projectsDeleteButton.UseVisualStyleBackColor = true;
            this.projectsDeleteButton.Click += new System.EventHandler(this.projectsDeleteButton_Click);
            // 
            // projectsDefaultGroupBox
            // 
            this.projectsDefaultGroupBox.Controls.Add(this.defaultLabelPanel);
            this.projectsDefaultGroupBox.Controls.Add(this.defaultLabel);
            this.projectsDefaultGroupBox.Location = new System.Drawing.Point(431, 253);
            this.projectsDefaultGroupBox.Name = "projectsDefaultGroupBox";
            this.projectsDefaultGroupBox.Size = new System.Drawing.Size(401, 149);
            this.projectsDefaultGroupBox.TabIndex = 5;
            this.projectsDefaultGroupBox.TabStop = false;
            // 
            // defaultLabelPanel
            // 
            this.defaultLabelPanel.Controls.Add(this.defaultProjectNameLabel);
            this.defaultLabelPanel.Controls.Add(this.label3);
            this.defaultLabelPanel.Controls.Add(this.defaultProjectIDLabel);
            this.defaultLabelPanel.Controls.Add(this.label20);
            this.defaultLabelPanel.Location = new System.Drawing.Point(6, 32);
            this.defaultLabelPanel.Name = "defaultLabelPanel";
            this.defaultLabelPanel.Size = new System.Drawing.Size(389, 111);
            this.defaultLabelPanel.TabIndex = 1;
            // 
            // defaultProjectNameLabel
            // 
            this.defaultProjectNameLabel.AutoSize = true;
            this.defaultProjectNameLabel.Location = new System.Drawing.Point(100, 47);
            this.defaultProjectNameLabel.Name = "defaultProjectNameLabel";
            this.defaultProjectNameLabel.Size = new System.Drawing.Size(116, 13);
            this.defaultProjectNameLabel.TabIndex = 3;
            this.defaultProjectNameLabel.Text = "Selected Project Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Project Name:";
            // 
            // defaultProjectIDLabel
            // 
            this.defaultProjectIDLabel.AutoSize = true;
            this.defaultProjectIDLabel.Location = new System.Drawing.Point(100, 16);
            this.defaultProjectIDLabel.Name = "defaultProjectIDLabel";
            this.defaultProjectIDLabel.Size = new System.Drawing.Size(99, 13);
            this.defaultProjectIDLabel.TabIndex = 1;
            this.defaultProjectIDLabel.Text = "Selected Project ID";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(10, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 13);
            this.label20.TabIndex = 0;
            this.label20.Text = "Project ID:";
            // 
            // defaultLabel
            // 
            this.defaultLabel.AutoSize = true;
            this.defaultLabel.Location = new System.Drawing.Point(16, 16);
            this.defaultLabel.Name = "defaultLabel";
            this.defaultLabel.Size = new System.Drawing.Size(173, 13);
            this.defaultLabel.TabIndex = 0;
            this.defaultLabel.Text = "Select item from list to edit or delete";
            // 
            // projectAddGroupBox
            // 
            this.projectAddGroupBox.Controls.Add(this.addProjectCancelButton);
            this.projectAddGroupBox.Controls.Add(this.addProjectAddButton);
            this.projectAddGroupBox.Controls.Add(this.addProjectNameTextBox);
            this.projectAddGroupBox.Controls.Add(this.addProjectIDTextBox);
            this.projectAddGroupBox.Controls.Add(this.label2);
            this.projectAddGroupBox.Controls.Add(this.label4);
            this.projectAddGroupBox.Location = new System.Drawing.Point(431, 408);
            this.projectAddGroupBox.Name = "projectAddGroupBox";
            this.projectAddGroupBox.Size = new System.Drawing.Size(401, 140);
            this.projectAddGroupBox.TabIndex = 6;
            this.projectAddGroupBox.TabStop = false;
            this.projectAddGroupBox.Text = "Add Project";
            // 
            // addProjectCancelButton
            // 
            this.addProjectCancelButton.Location = new System.Drawing.Point(312, 101);
            this.addProjectCancelButton.Name = "addProjectCancelButton";
            this.addProjectCancelButton.Size = new System.Drawing.Size(75, 23);
            this.addProjectCancelButton.TabIndex = 4;
            this.addProjectCancelButton.Text = "Cancel";
            this.addProjectCancelButton.UseVisualStyleBackColor = true;
            this.addProjectCancelButton.Click += new System.EventHandler(this.addProjectCancelButton_Click);
            // 
            // addProjectAddButton
            // 
            this.addProjectAddButton.Location = new System.Drawing.Point(227, 101);
            this.addProjectAddButton.Name = "addProjectAddButton";
            this.addProjectAddButton.Size = new System.Drawing.Size(75, 23);
            this.addProjectAddButton.TabIndex = 3;
            this.addProjectAddButton.Text = "Add";
            this.addProjectAddButton.UseVisualStyleBackColor = true;
            this.addProjectAddButton.Click += new System.EventHandler(this.addProjectAddButton_Click);
            // 
            // addProjectNameTextBox
            // 
            this.addProjectNameTextBox.Location = new System.Drawing.Point(104, 60);
            this.addProjectNameTextBox.Name = "addProjectNameTextBox";
            this.addProjectNameTextBox.Size = new System.Drawing.Size(291, 20);
            this.addProjectNameTextBox.TabIndex = 2;
            // 
            // addProjectIDTextBox
            // 
            this.addProjectIDTextBox.Location = new System.Drawing.Point(104, 31);
            this.addProjectIDTextBox.Name = "addProjectIDTextBox";
            this.addProjectIDTextBox.Size = new System.Drawing.Size(109, 20);
            this.addProjectIDTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Project Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Project ID:";
            // 
            // editProjectsGroupBox
            // 
            this.editProjectsGroupBox.Controls.Add(this.editProjectCancelButton);
            this.editProjectsGroupBox.Controls.Add(this.editProjectEditButton);
            this.editProjectsGroupBox.Controls.Add(this.editProjectNameTextBox);
            this.editProjectsGroupBox.Controls.Add(this.editProjectIDTextBox);
            this.editProjectsGroupBox.Controls.Add(this.label5);
            this.editProjectsGroupBox.Controls.Add(this.label6);
            this.editProjectsGroupBox.Location = new System.Drawing.Point(431, 563);
            this.editProjectsGroupBox.Name = "editProjectsGroupBox";
            this.editProjectsGroupBox.Size = new System.Drawing.Size(401, 140);
            this.editProjectsGroupBox.TabIndex = 9;
            this.editProjectsGroupBox.TabStop = false;
            this.editProjectsGroupBox.Text = "Edit Project";
            // 
            // editProjectCancelButton
            // 
            this.editProjectCancelButton.Location = new System.Drawing.Point(312, 101);
            this.editProjectCancelButton.Name = "editProjectCancelButton";
            this.editProjectCancelButton.Size = new System.Drawing.Size(75, 23);
            this.editProjectCancelButton.TabIndex = 1;
            this.editProjectCancelButton.Text = "Cancel";
            this.editProjectCancelButton.UseVisualStyleBackColor = true;
            this.editProjectCancelButton.Click += new System.EventHandler(this.editProjectCancelButton_Click);
            // 
            // editProjectEditButton
            // 
            this.editProjectEditButton.Location = new System.Drawing.Point(227, 101);
            this.editProjectEditButton.Name = "editProjectEditButton";
            this.editProjectEditButton.Size = new System.Drawing.Size(75, 23);
            this.editProjectEditButton.TabIndex = 4;
            this.editProjectEditButton.Text = "Edit";
            this.editProjectEditButton.UseVisualStyleBackColor = true;
            this.editProjectEditButton.Click += new System.EventHandler(this.editProjectEditButton_Click);
            // 
            // editProjectNameTextBox
            // 
            this.editProjectNameTextBox.Location = new System.Drawing.Point(104, 60);
            this.editProjectNameTextBox.Name = "editProjectNameTextBox";
            this.editProjectNameTextBox.Size = new System.Drawing.Size(291, 20);
            this.editProjectNameTextBox.TabIndex = 3;
            // 
            // editProjectIDTextBox
            // 
            this.editProjectIDTextBox.Location = new System.Drawing.Point(104, 31);
            this.editProjectIDTextBox.Name = "editProjectIDTextBox";
            this.editProjectIDTextBox.Size = new System.Drawing.Size(109, 20);
            this.editProjectIDTextBox.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Project Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Project ID:";
            // 
            // deleteProjectsGroupBox
            // 
            this.deleteProjectsGroupBox.Controls.Add(this.deleteProjectNameLabel);
            this.deleteProjectsGroupBox.Controls.Add(this.deleteProjectIDLabel);
            this.deleteProjectsGroupBox.Controls.Add(this.deleteProjectCancelButton);
            this.deleteProjectsGroupBox.Controls.Add(this.deleteProjectDeleteButton);
            this.deleteProjectsGroupBox.Controls.Add(this.label7);
            this.deleteProjectsGroupBox.Controls.Add(this.label8);
            this.deleteProjectsGroupBox.Location = new System.Drawing.Point(431, 68);
            this.deleteProjectsGroupBox.Name = "deleteProjectsGroupBox";
            this.deleteProjectsGroupBox.Size = new System.Drawing.Size(401, 140);
            this.deleteProjectsGroupBox.TabIndex = 10;
            this.deleteProjectsGroupBox.TabStop = false;
            this.deleteProjectsGroupBox.Text = "Delete Project";
            // 
            // deleteProjectNameLabel
            // 
            this.deleteProjectNameLabel.AutoSize = true;
            this.deleteProjectNameLabel.Location = new System.Drawing.Point(95, 65);
            this.deleteProjectNameLabel.Name = "deleteProjectNameLabel";
            this.deleteProjectNameLabel.Size = new System.Drawing.Size(162, 13);
            this.deleteProjectNameLabel.TabIndex = 10;
            this.deleteProjectNameLabel.Text = "Selected Project Name to Delete";
            // 
            // deleteProjectIDLabel
            // 
            this.deleteProjectIDLabel.AutoSize = true;
            this.deleteProjectIDLabel.Location = new System.Drawing.Point(95, 34);
            this.deleteProjectIDLabel.Name = "deleteProjectIDLabel";
            this.deleteProjectIDLabel.Size = new System.Drawing.Size(145, 13);
            this.deleteProjectIDLabel.TabIndex = 9;
            this.deleteProjectIDLabel.Text = "Selected Project ID to Delete";
            // 
            // deleteProjectCancelButton
            // 
            this.deleteProjectCancelButton.Location = new System.Drawing.Point(312, 101);
            this.deleteProjectCancelButton.Name = "deleteProjectCancelButton";
            this.deleteProjectCancelButton.Size = new System.Drawing.Size(75, 23);
            this.deleteProjectCancelButton.TabIndex = 1;
            this.deleteProjectCancelButton.Text = "Cancel";
            this.deleteProjectCancelButton.UseVisualStyleBackColor = true;
            this.deleteProjectCancelButton.Click += new System.EventHandler(this.deleteProjectCancelButton_Click);
            // 
            // deleteProjectDeleteButton
            // 
            this.deleteProjectDeleteButton.Location = new System.Drawing.Point(227, 101);
            this.deleteProjectDeleteButton.Name = "deleteProjectDeleteButton";
            this.deleteProjectDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteProjectDeleteButton.TabIndex = 2;
            this.deleteProjectDeleteButton.Text = "Delete";
            this.deleteProjectDeleteButton.UseVisualStyleBackColor = true;
            this.deleteProjectDeleteButton.Click += new System.EventHandler(this.deleteProjectDeleteButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Project Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Project ID:";
            // 
            // ProjectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 587);
            this.ControlBox = false;
            this.Controls.Add(this.deleteProjectsGroupBox);
            this.Controls.Add(this.editProjectsGroupBox);
            this.Controls.Add(this.projectAddGroupBox);
            this.Controls.Add(this.projectsDefaultGroupBox);
            this.Controls.Add(this.projectsDeleteButton);
            this.Controls.Add(this.projectsUpdateButton);
            this.Controls.Add(this.projectsAddButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.projectsListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ProjectsForm";
            this.Shown += new System.EventHandler(this.ProjectsForm_Shown);
            this.projectsDefaultGroupBox.ResumeLayout(false);
            this.projectsDefaultGroupBox.PerformLayout();
            this.defaultLabelPanel.ResumeLayout(false);
            this.defaultLabelPanel.PerformLayout();
            this.projectAddGroupBox.ResumeLayout(false);
            this.projectAddGroupBox.PerformLayout();
            this.editProjectsGroupBox.ResumeLayout(false);
            this.editProjectsGroupBox.PerformLayout();
            this.deleteProjectsGroupBox.ResumeLayout(false);
            this.deleteProjectsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView projectsListView;
        private System.Windows.Forms.ColumnHeader columnProjectID;
        private System.Windows.Forms.ColumnHeader columnProjectName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button projectsAddButton;
        private System.Windows.Forms.Button projectsUpdateButton;
        private System.Windows.Forms.Button projectsDeleteButton;
        private System.Windows.Forms.GroupBox projectsDefaultGroupBox;
        private System.Windows.Forms.Panel defaultLabelPanel;
        private System.Windows.Forms.Label defaultProjectNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label defaultProjectIDLabel;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label defaultLabel;
        private System.Windows.Forms.GroupBox projectAddGroupBox;
        private System.Windows.Forms.Button addProjectCancelButton;
        private System.Windows.Forms.Button addProjectAddButton;
        private System.Windows.Forms.TextBox addProjectNameTextBox;
        private System.Windows.Forms.TextBox addProjectIDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox editProjectsGroupBox;
        private System.Windows.Forms.Button editProjectCancelButton;
        private System.Windows.Forms.Button editProjectEditButton;
        private System.Windows.Forms.TextBox editProjectNameTextBox;
        private System.Windows.Forms.TextBox editProjectIDTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox deleteProjectsGroupBox;
        private System.Windows.Forms.Label deleteProjectNameLabel;
        private System.Windows.Forms.Label deleteProjectIDLabel;
        private System.Windows.Forms.Button deleteProjectCancelButton;
        private System.Windows.Forms.Button deleteProjectDeleteButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}