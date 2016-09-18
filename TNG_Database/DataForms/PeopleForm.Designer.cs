namespace TNG_Database
{
    partial class PeopleForm
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
            this.peopleFormListBox = new System.Windows.Forms.ListBox();
            this.currentUsersLabel = new System.Windows.Forms.Label();
            this.addUserPeopleButton = new System.Windows.Forms.Button();
            this.editUserPeopleButton = new System.Windows.Forms.Button();
            this.deleteUserPeopleButton = new System.Windows.Forms.Button();
            this.editUserGroupBox = new System.Windows.Forms.GroupBox();
            this.editUserEditButton = new System.Windows.Forms.Button();
            this.editUserCancelButton = new System.Windows.Forms.Button();
            this.editUserPeopleTB = new System.Windows.Forms.TextBox();
            this.editUserOldPersonName = new System.Windows.Forms.Label();
            this.editPersonNewNameLabel = new System.Windows.Forms.Label();
            this.editUserOldNameLabel = new System.Windows.Forms.Label();
            this.addUserGroupBox = new System.Windows.Forms.GroupBox();
            this.addUserCancelButton = new System.Windows.Forms.Button();
            this.addUserAddButton = new System.Windows.Forms.Button();
            this.addUserLabel = new System.Windows.Forms.Label();
            this.addUserNameTextbox = new System.Windows.Forms.TextBox();
            this.deleteUserGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteUserCancelButton = new System.Windows.Forms.Button();
            this.deleteUserDeleteButton = new System.Windows.Forms.Button();
            this.deleteUserNameLabel = new System.Windows.Forms.Label();
            this.deleteUserLabel = new System.Windows.Forms.Label();
            this.defaultEditGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editUserGroupBox.SuspendLayout();
            this.addUserGroupBox.SuspendLayout();
            this.deleteUserGroupBox.SuspendLayout();
            this.defaultEditGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // peopleFormListBox
            // 
            this.peopleFormListBox.FormattingEnabled = true;
            this.peopleFormListBox.Location = new System.Drawing.Point(12, 69);
            this.peopleFormListBox.Name = "peopleFormListBox";
            this.peopleFormListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.peopleFormListBox.Size = new System.Drawing.Size(156, 238);
            this.peopleFormListBox.TabIndex = 4;
            this.peopleFormListBox.SelectedIndexChanged += new System.EventHandler(this.peopleFormListBox_SelectedIndexChanged);
            // 
            // currentUsersLabel
            // 
            this.currentUsersLabel.AutoSize = true;
            this.currentUsersLabel.Location = new System.Drawing.Point(12, 53);
            this.currentUsersLabel.Name = "currentUsersLabel";
            this.currentUsersLabel.Size = new System.Drawing.Size(74, 13);
            this.currentUsersLabel.TabIndex = 2;
            this.currentUsersLabel.Text = "Current Users:";
            // 
            // addUserPeopleButton
            // 
            this.addUserPeopleButton.Location = new System.Drawing.Point(174, 69);
            this.addUserPeopleButton.Name = "addUserPeopleButton";
            this.addUserPeopleButton.Size = new System.Drawing.Size(117, 23);
            this.addUserPeopleButton.TabIndex = 1;
            this.addUserPeopleButton.Text = "Add User";
            this.addUserPeopleButton.UseVisualStyleBackColor = true;
            this.addUserPeopleButton.Click += new System.EventHandler(this.addUserPeopleButton_Click);
            // 
            // editUserPeopleButton
            // 
            this.editUserPeopleButton.Location = new System.Drawing.Point(174, 98);
            this.editUserPeopleButton.Name = "editUserPeopleButton";
            this.editUserPeopleButton.Size = new System.Drawing.Size(117, 23);
            this.editUserPeopleButton.TabIndex = 2;
            this.editUserPeopleButton.Text = "Edit User";
            this.editUserPeopleButton.UseVisualStyleBackColor = true;
            this.editUserPeopleButton.Click += new System.EventHandler(this.editUserPeopleButton_Click);
            // 
            // deleteUserPeopleButton
            // 
            this.deleteUserPeopleButton.Location = new System.Drawing.Point(174, 127);
            this.deleteUserPeopleButton.Name = "deleteUserPeopleButton";
            this.deleteUserPeopleButton.Size = new System.Drawing.Size(117, 23);
            this.deleteUserPeopleButton.TabIndex = 3;
            this.deleteUserPeopleButton.Text = "Delete User";
            this.deleteUserPeopleButton.UseVisualStyleBackColor = true;
            this.deleteUserPeopleButton.Click += new System.EventHandler(this.deleteUserPeopleButton_Click);
            // 
            // editUserGroupBox
            // 
            this.editUserGroupBox.Controls.Add(this.editUserEditButton);
            this.editUserGroupBox.Controls.Add(this.editUserCancelButton);
            this.editUserGroupBox.Controls.Add(this.editUserPeopleTB);
            this.editUserGroupBox.Controls.Add(this.editUserOldPersonName);
            this.editUserGroupBox.Controls.Add(this.editPersonNewNameLabel);
            this.editUserGroupBox.Controls.Add(this.editUserOldNameLabel);
            this.editUserGroupBox.Location = new System.Drawing.Point(345, 69);
            this.editUserGroupBox.Name = "editUserGroupBox";
            this.editUserGroupBox.Size = new System.Drawing.Size(443, 156);
            this.editUserGroupBox.TabIndex = 6;
            this.editUserGroupBox.TabStop = false;
            this.editUserGroupBox.Text = "Edit User:";
            this.editUserGroupBox.Visible = false;
            // 
            // editUserEditButton
            // 
            this.editUserEditButton.Enabled = false;
            this.editUserEditButton.Location = new System.Drawing.Point(281, 116);
            this.editUserEditButton.Name = "editUserEditButton";
            this.editUserEditButton.Size = new System.Drawing.Size(75, 23);
            this.editUserEditButton.TabIndex = 3;
            this.editUserEditButton.Text = "Edit";
            this.editUserEditButton.UseVisualStyleBackColor = true;
            this.editUserEditButton.Click += new System.EventHandler(this.editUserEditButton_Click);
            // 
            // editUserCancelButton
            // 
            this.editUserCancelButton.Location = new System.Drawing.Point(362, 116);
            this.editUserCancelButton.Name = "editUserCancelButton";
            this.editUserCancelButton.Size = new System.Drawing.Size(75, 23);
            this.editUserCancelButton.TabIndex = 1;
            this.editUserCancelButton.Text = "Cancel";
            this.editUserCancelButton.UseVisualStyleBackColor = true;
            this.editUserCancelButton.Click += new System.EventHandler(this.editUserCancelButton_Click);
            // 
            // editUserPeopleTB
            // 
            this.editUserPeopleTB.Location = new System.Drawing.Point(141, 60);
            this.editUserPeopleTB.Name = "editUserPeopleTB";
            this.editUserPeopleTB.Size = new System.Drawing.Size(296, 20);
            this.editUserPeopleTB.TabIndex = 2;
            this.editUserPeopleTB.TextChanged += new System.EventHandler(this.editUserPeopleTB_TextChanged);
            this.editUserPeopleTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editUserPeopleTB_KeyDown);
            // 
            // editUserOldPersonName
            // 
            this.editUserOldPersonName.AutoSize = true;
            this.editUserOldPersonName.Location = new System.Drawing.Point(138, 34);
            this.editUserOldPersonName.Name = "editUserOldPersonName";
            this.editUserOldPersonName.Size = new System.Drawing.Size(160, 13);
            this.editUserOldPersonName.TabIndex = 2;
            this.editUserOldPersonName.Text = "Old Name Of Person To Change";
            // 
            // editPersonNewNameLabel
            // 
            this.editPersonNewNameLabel.AutoSize = true;
            this.editPersonNewNameLabel.Location = new System.Drawing.Point(27, 63);
            this.editPersonNewNameLabel.Name = "editPersonNewNameLabel";
            this.editPersonNewNameLabel.Size = new System.Drawing.Size(63, 13);
            this.editPersonNewNameLabel.TabIndex = 1;
            this.editPersonNewNameLabel.Text = "New Name:";
            // 
            // editUserOldNameLabel
            // 
            this.editUserOldNameLabel.AutoSize = true;
            this.editUserOldNameLabel.Location = new System.Drawing.Point(27, 34);
            this.editUserOldNameLabel.Name = "editUserOldNameLabel";
            this.editUserOldNameLabel.Size = new System.Drawing.Size(57, 13);
            this.editUserOldNameLabel.TabIndex = 0;
            this.editUserOldNameLabel.Text = "Old Name:";
            // 
            // addUserGroupBox
            // 
            this.addUserGroupBox.Controls.Add(this.addUserCancelButton);
            this.addUserGroupBox.Controls.Add(this.addUserAddButton);
            this.addUserGroupBox.Controls.Add(this.addUserLabel);
            this.addUserGroupBox.Controls.Add(this.addUserNameTextbox);
            this.addUserGroupBox.Location = new System.Drawing.Point(345, 231);
            this.addUserGroupBox.Name = "addUserGroupBox";
            this.addUserGroupBox.Size = new System.Drawing.Size(443, 123);
            this.addUserGroupBox.TabIndex = 7;
            this.addUserGroupBox.TabStop = false;
            this.addUserGroupBox.Text = "Add User:";
            this.addUserGroupBox.Visible = false;
            // 
            // addUserCancelButton
            // 
            this.addUserCancelButton.Location = new System.Drawing.Point(362, 85);
            this.addUserCancelButton.Name = "addUserCancelButton";
            this.addUserCancelButton.Size = new System.Drawing.Size(75, 23);
            this.addUserCancelButton.TabIndex = 3;
            this.addUserCancelButton.Text = "Cancel";
            this.addUserCancelButton.UseVisualStyleBackColor = true;
            this.addUserCancelButton.Click += new System.EventHandler(this.addUserCancelButton_Click);
            // 
            // addUserAddButton
            // 
            this.addUserAddButton.Enabled = false;
            this.addUserAddButton.Location = new System.Drawing.Point(281, 85);
            this.addUserAddButton.Name = "addUserAddButton";
            this.addUserAddButton.Size = new System.Drawing.Size(75, 23);
            this.addUserAddButton.TabIndex = 2;
            this.addUserAddButton.Text = "Add";
            this.addUserAddButton.UseVisualStyleBackColor = true;
            this.addUserAddButton.Click += new System.EventHandler(this.addUserAddButton_Click);
            // 
            // addUserLabel
            // 
            this.addUserLabel.AutoSize = true;
            this.addUserLabel.Location = new System.Drawing.Point(27, 38);
            this.addUserLabel.Name = "addUserLabel";
            this.addUserLabel.Size = new System.Drawing.Size(63, 13);
            this.addUserLabel.TabIndex = 1;
            this.addUserLabel.Text = "User Name:";
            // 
            // addUserNameTextbox
            // 
            this.addUserNameTextbox.Location = new System.Drawing.Point(141, 35);
            this.addUserNameTextbox.Name = "addUserNameTextbox";
            this.addUserNameTextbox.Size = new System.Drawing.Size(296, 20);
            this.addUserNameTextbox.TabIndex = 1;
            this.addUserNameTextbox.TextChanged += new System.EventHandler(this.addUserNameTextbox_TextChanged);
            this.addUserNameTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.addUserNameTextbox_KeyDown);
            // 
            // deleteUserGroupBox
            // 
            this.deleteUserGroupBox.Controls.Add(this.deleteUserCancelButton);
            this.deleteUserGroupBox.Controls.Add(this.deleteUserDeleteButton);
            this.deleteUserGroupBox.Controls.Add(this.deleteUserNameLabel);
            this.deleteUserGroupBox.Controls.Add(this.deleteUserLabel);
            this.deleteUserGroupBox.Location = new System.Drawing.Point(345, 361);
            this.deleteUserGroupBox.Name = "deleteUserGroupBox";
            this.deleteUserGroupBox.Size = new System.Drawing.Size(443, 100);
            this.deleteUserGroupBox.TabIndex = 8;
            this.deleteUserGroupBox.TabStop = false;
            this.deleteUserGroupBox.Text = "Delete User:";
            this.deleteUserGroupBox.Visible = false;
            // 
            // deleteUserCancelButton
            // 
            this.deleteUserCancelButton.Location = new System.Drawing.Point(362, 59);
            this.deleteUserCancelButton.Name = "deleteUserCancelButton";
            this.deleteUserCancelButton.Size = new System.Drawing.Size(75, 23);
            this.deleteUserCancelButton.TabIndex = 1;
            this.deleteUserCancelButton.Text = "Cancel";
            this.deleteUserCancelButton.UseVisualStyleBackColor = true;
            this.deleteUserCancelButton.Click += new System.EventHandler(this.deleteUserCancelButton_Click);
            // 
            // deleteUserDeleteButton
            // 
            this.deleteUserDeleteButton.Location = new System.Drawing.Point(281, 59);
            this.deleteUserDeleteButton.Name = "deleteUserDeleteButton";
            this.deleteUserDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteUserDeleteButton.TabIndex = 2;
            this.deleteUserDeleteButton.Text = "Delete";
            this.deleteUserDeleteButton.UseVisualStyleBackColor = true;
            this.deleteUserDeleteButton.Click += new System.EventHandler(this.deleteUserDeleteButton_Click);
            // 
            // deleteUserNameLabel
            // 
            this.deleteUserNameLabel.AutoSize = true;
            this.deleteUserNameLabel.Location = new System.Drawing.Point(138, 36);
            this.deleteUserNameLabel.Name = "deleteUserNameLabel";
            this.deleteUserNameLabel.Size = new System.Drawing.Size(75, 13);
            this.deleteUserNameLabel.TabIndex = 1;
            this.deleteUserNameLabel.Text = "User to Delete";
            // 
            // deleteUserLabel
            // 
            this.deleteUserLabel.AutoSize = true;
            this.deleteUserLabel.Location = new System.Drawing.Point(27, 36);
            this.deleteUserLabel.Name = "deleteUserLabel";
            this.deleteUserLabel.Size = new System.Drawing.Size(69, 13);
            this.deleteUserLabel.TabIndex = 0;
            this.deleteUserLabel.Text = "Delete User?";
            // 
            // defaultEditGroupBox
            // 
            this.defaultEditGroupBox.Controls.Add(this.label2);
            this.defaultEditGroupBox.Location = new System.Drawing.Point(345, 480);
            this.defaultEditGroupBox.Name = "defaultEditGroupBox";
            this.defaultEditGroupBox.Size = new System.Drawing.Size(443, 54);
            this.defaultEditGroupBox.TabIndex = 9;
            this.defaultEditGroupBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "<-------  Select a user and use buttons to make changes";
            // 
            // PeopleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 533);
            this.ControlBox = false;
            this.Controls.Add(this.defaultEditGroupBox);
            this.Controls.Add(this.deleteUserGroupBox);
            this.Controls.Add(this.addUserGroupBox);
            this.Controls.Add(this.editUserGroupBox);
            this.Controls.Add(this.deleteUserPeopleButton);
            this.Controls.Add(this.editUserPeopleButton);
            this.Controls.Add(this.addUserPeopleButton);
            this.Controls.Add(this.currentUsersLabel);
            this.Controls.Add(this.peopleFormListBox);
            this.MinimizeBox = false;
            this.Name = "PeopleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PeopleForm";
            this.Shown += new System.EventHandler(this.PeopleForm_Shown);
            this.editUserGroupBox.ResumeLayout(false);
            this.editUserGroupBox.PerformLayout();
            this.addUserGroupBox.ResumeLayout(false);
            this.addUserGroupBox.PerformLayout();
            this.deleteUserGroupBox.ResumeLayout(false);
            this.deleteUserGroupBox.PerformLayout();
            this.defaultEditGroupBox.ResumeLayout(false);
            this.defaultEditGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox peopleFormListBox;
        private System.Windows.Forms.Label currentUsersLabel;
        private System.Windows.Forms.Button addUserPeopleButton;
        private System.Windows.Forms.Button editUserPeopleButton;
        private System.Windows.Forms.Button deleteUserPeopleButton;
        private System.Windows.Forms.GroupBox editUserGroupBox;
        private System.Windows.Forms.Button editUserEditButton;
        private System.Windows.Forms.Button editUserCancelButton;
        private System.Windows.Forms.TextBox editUserPeopleTB;
        private System.Windows.Forms.Label editUserOldPersonName;
        private System.Windows.Forms.Label editPersonNewNameLabel;
        private System.Windows.Forms.Label editUserOldNameLabel;
        private System.Windows.Forms.GroupBox addUserGroupBox;
        private System.Windows.Forms.Button addUserCancelButton;
        private System.Windows.Forms.Button addUserAddButton;
        private System.Windows.Forms.Label addUserLabel;
        private System.Windows.Forms.TextBox addUserNameTextbox;
        private System.Windows.Forms.GroupBox deleteUserGroupBox;
        private System.Windows.Forms.Button deleteUserCancelButton;
        private System.Windows.Forms.Button deleteUserDeleteButton;
        private System.Windows.Forms.Label deleteUserNameLabel;
        private System.Windows.Forms.Label deleteUserLabel;
        private System.Windows.Forms.GroupBox defaultEditGroupBox;
        private System.Windows.Forms.Label label2;
    }
}