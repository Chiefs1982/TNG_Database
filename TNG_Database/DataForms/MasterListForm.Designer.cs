namespace TNG_Database
{
    partial class MasterListForm
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
            this.masterListListBox = new System.Windows.Forms.ListBox();
            this.masterListAddButton = new System.Windows.Forms.Button();
            this.masterListEditButton = new System.Windows.Forms.Button();
            this.masterListDeleteButton = new System.Windows.Forms.Button();
            this.defaultMasterGroupBox = new System.Windows.Forms.GroupBox();
            this.defaultMasterListLabel = new System.Windows.Forms.Label();
            this.defaultCameraNameMasterListLabel = new System.Windows.Forms.Label();
            this.defaultArchiveNameMasterListLabel = new System.Windows.Forms.Label();
            this.defaultCameraMasterListLabel = new System.Windows.Forms.Label();
            this.defaultArchiveMasterListLabel = new System.Windows.Forms.Label();
            this.masterListBoxLabel = new System.Windows.Forms.Label();
            this.addMasterListGroupBox = new System.Windows.Forms.GroupBox();
            this.addMasterListCancelButton = new System.Windows.Forms.Button();
            this.addMasterListAddButton = new System.Windows.Forms.Button();
            this.addMasterListNameTextbox = new System.Windows.Forms.TextBox();
            this.cameraAddMasterCombo = new System.Windows.Forms.ComboBox();
            this.cameraAddMasterListLabel = new System.Windows.Forms.Label();
            this.masterTapeAddNameLabel = new System.Windows.Forms.Label();
            this.editMasterListGroupBox = new System.Windows.Forms.GroupBox();
            this.editMasterCancelButton = new System.Windows.Forms.Button();
            this.editMasterEditButton = new System.Windows.Forms.Button();
            this.editCameraNewMasterDropdown = new System.Windows.Forms.ComboBox();
            this.editNewCameraMasterLabel = new System.Windows.Forms.Label();
            this.editNewNameMasterTextbox = new System.Windows.Forms.TextBox();
            this.editNewMasterNameLabel = new System.Windows.Forms.Label();
            this.editCameraOldMasterNameLabel = new System.Windows.Forms.Label();
            this.cameraOldMasterLabel = new System.Windows.Forms.Label();
            this.editOldNameMasterLabel = new System.Windows.Forms.Label();
            this.editOldMasterNameLabel = new System.Windows.Forms.Label();
            this.deleteMasterListGroupBox = new System.Windows.Forms.GroupBox();
            this.deleteMasterListCancelButton = new System.Windows.Forms.Button();
            this.deleteMasterListDeleteButton = new System.Windows.Forms.Button();
            this.deleteCameraNameMasterListLabel = new System.Windows.Forms.Label();
            this.deleteMasterNameMasterListLabel = new System.Windows.Forms.Label();
            this.deleteCameraMasterListLabel = new System.Windows.Forms.Label();
            this.deleteMasterMasterListLabel = new System.Windows.Forms.Label();
            this.defaultMasterGroupBox.SuspendLayout();
            this.addMasterListGroupBox.SuspendLayout();
            this.editMasterListGroupBox.SuspendLayout();
            this.deleteMasterListGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Master List Form";
            // 
            // masterListListBox
            // 
            this.masterListListBox.FormattingEnabled = true;
            this.masterListListBox.Location = new System.Drawing.Point(12, 67);
            this.masterListListBox.Name = "masterListListBox";
            this.masterListListBox.Size = new System.Drawing.Size(181, 368);
            this.masterListListBox.TabIndex = 4;
            this.masterListListBox.SelectedIndexChanged += new System.EventHandler(this.masterListListBox_SelectedIndexChanged);
            // 
            // masterListAddButton
            // 
            this.masterListAddButton.Location = new System.Drawing.Point(208, 67);
            this.masterListAddButton.Name = "masterListAddButton";
            this.masterListAddButton.Size = new System.Drawing.Size(91, 23);
            this.masterListAddButton.TabIndex = 1;
            this.masterListAddButton.Text = "Add";
            this.masterListAddButton.UseVisualStyleBackColor = true;
            this.masterListAddButton.Click += new System.EventHandler(this.masterListAddButton_Click);
            // 
            // masterListEditButton
            // 
            this.masterListEditButton.Location = new System.Drawing.Point(208, 96);
            this.masterListEditButton.Name = "masterListEditButton";
            this.masterListEditButton.Size = new System.Drawing.Size(91, 23);
            this.masterListEditButton.TabIndex = 2;
            this.masterListEditButton.Text = "Edit";
            this.masterListEditButton.UseVisualStyleBackColor = true;
            this.masterListEditButton.Click += new System.EventHandler(this.masterListEditButton_Click);
            // 
            // masterListDeleteButton
            // 
            this.masterListDeleteButton.Location = new System.Drawing.Point(208, 125);
            this.masterListDeleteButton.Name = "masterListDeleteButton";
            this.masterListDeleteButton.Size = new System.Drawing.Size(91, 23);
            this.masterListDeleteButton.TabIndex = 3;
            this.masterListDeleteButton.Text = "Delete";
            this.masterListDeleteButton.UseVisualStyleBackColor = true;
            this.masterListDeleteButton.Click += new System.EventHandler(this.masterListDeleteButton_Click);
            // 
            // defaultMasterGroupBox
            // 
            this.defaultMasterGroupBox.Controls.Add(this.defaultMasterListLabel);
            this.defaultMasterGroupBox.Controls.Add(this.defaultCameraNameMasterListLabel);
            this.defaultMasterGroupBox.Controls.Add(this.defaultArchiveNameMasterListLabel);
            this.defaultMasterGroupBox.Controls.Add(this.defaultCameraMasterListLabel);
            this.defaultMasterGroupBox.Controls.Add(this.defaultArchiveMasterListLabel);
            this.defaultMasterGroupBox.Location = new System.Drawing.Point(316, 67);
            this.defaultMasterGroupBox.Name = "defaultMasterGroupBox";
            this.defaultMasterGroupBox.Size = new System.Drawing.Size(408, 109);
            this.defaultMasterGroupBox.TabIndex = 5;
            this.defaultMasterGroupBox.TabStop = false;
            // 
            // defaultMasterListLabel
            // 
            this.defaultMasterListLabel.AutoSize = true;
            this.defaultMasterListLabel.Location = new System.Drawing.Point(21, 16);
            this.defaultMasterListLabel.Name = "defaultMasterListLabel";
            this.defaultMasterListLabel.Size = new System.Drawing.Size(220, 13);
            this.defaultMasterListLabel.TabIndex = 4;
            this.defaultMasterListLabel.Text = "<------ Make Selection to Edit or Delete a tape";
            // 
            // defaultCameraNameMasterListLabel
            // 
            this.defaultCameraNameMasterListLabel.AutoSize = true;
            this.defaultCameraNameMasterListLabel.Location = new System.Drawing.Point(128, 58);
            this.defaultCameraNameMasterListLabel.Name = "defaultCameraNameMasterListLabel";
            this.defaultCameraNameMasterListLabel.Size = new System.Drawing.Size(87, 13);
            this.defaultCameraNameMasterListLabel.TabIndex = 3;
            this.defaultCameraNameMasterListLabel.Text = "(Make Selection)";
            this.defaultCameraNameMasterListLabel.Visible = false;
            // 
            // defaultArchiveNameMasterListLabel
            // 
            this.defaultArchiveNameMasterListLabel.AutoSize = true;
            this.defaultArchiveNameMasterListLabel.Location = new System.Drawing.Point(128, 29);
            this.defaultArchiveNameMasterListLabel.Name = "defaultArchiveNameMasterListLabel";
            this.defaultArchiveNameMasterListLabel.Size = new System.Drawing.Size(87, 13);
            this.defaultArchiveNameMasterListLabel.TabIndex = 2;
            this.defaultArchiveNameMasterListLabel.Text = "(Make Selection)";
            this.defaultArchiveNameMasterListLabel.Visible = false;
            // 
            // defaultCameraMasterListLabel
            // 
            this.defaultCameraMasterListLabel.AutoSize = true;
            this.defaultCameraMasterListLabel.Location = new System.Drawing.Point(21, 58);
            this.defaultCameraMasterListLabel.Name = "defaultCameraMasterListLabel";
            this.defaultCameraMasterListLabel.Size = new System.Drawing.Size(46, 13);
            this.defaultCameraMasterListLabel.TabIndex = 1;
            this.defaultCameraMasterListLabel.Text = "Camera:";
            this.defaultCameraMasterListLabel.Visible = false;
            // 
            // defaultArchiveMasterListLabel
            // 
            this.defaultArchiveMasterListLabel.AutoSize = true;
            this.defaultArchiveMasterListLabel.Location = new System.Drawing.Point(21, 29);
            this.defaultArchiveMasterListLabel.Name = "defaultArchiveMasterListLabel";
            this.defaultArchiveMasterListLabel.Size = new System.Drawing.Size(101, 13);
            this.defaultArchiveMasterListLabel.TabIndex = 0;
            this.defaultArchiveMasterListLabel.Text = "Master Tape Name:";
            this.defaultArchiveMasterListLabel.Visible = false;
            // 
            // masterListBoxLabel
            // 
            this.masterListBoxLabel.AutoSize = true;
            this.masterListBoxLabel.Location = new System.Drawing.Point(12, 49);
            this.masterListBoxLabel.Name = "masterListBoxLabel";
            this.masterListBoxLabel.Size = new System.Drawing.Size(89, 13);
            this.masterListBoxLabel.TabIndex = 6;
            this.masterListBoxLabel.Text = "Master Tape List:";
            // 
            // addMasterListGroupBox
            // 
            this.addMasterListGroupBox.Controls.Add(this.addMasterListCancelButton);
            this.addMasterListGroupBox.Controls.Add(this.addMasterListAddButton);
            this.addMasterListGroupBox.Controls.Add(this.addMasterListNameTextbox);
            this.addMasterListGroupBox.Controls.Add(this.cameraAddMasterCombo);
            this.addMasterListGroupBox.Controls.Add(this.cameraAddMasterListLabel);
            this.addMasterListGroupBox.Controls.Add(this.masterTapeAddNameLabel);
            this.addMasterListGroupBox.Location = new System.Drawing.Point(316, 370);
            this.addMasterListGroupBox.Name = "addMasterListGroupBox";
            this.addMasterListGroupBox.Size = new System.Drawing.Size(408, 162);
            this.addMasterListGroupBox.TabIndex = 7;
            this.addMasterListGroupBox.TabStop = false;
            this.addMasterListGroupBox.Text = "Add Master Tape";
            this.addMasterListGroupBox.Visible = false;
            // 
            // addMasterListCancelButton
            // 
            this.addMasterListCancelButton.Location = new System.Drawing.Point(317, 121);
            this.addMasterListCancelButton.Name = "addMasterListCancelButton";
            this.addMasterListCancelButton.Size = new System.Drawing.Size(75, 23);
            this.addMasterListCancelButton.TabIndex = 4;
            this.addMasterListCancelButton.Text = "Cancel";
            this.addMasterListCancelButton.UseVisualStyleBackColor = true;
            this.addMasterListCancelButton.Click += new System.EventHandler(this.addMasterListCancelButton_Click);
            // 
            // addMasterListAddButton
            // 
            this.addMasterListAddButton.Location = new System.Drawing.Point(236, 121);
            this.addMasterListAddButton.Name = "addMasterListAddButton";
            this.addMasterListAddButton.Size = new System.Drawing.Size(75, 23);
            this.addMasterListAddButton.TabIndex = 3;
            this.addMasterListAddButton.Text = "Add";
            this.addMasterListAddButton.UseVisualStyleBackColor = true;
            this.addMasterListAddButton.Click += new System.EventHandler(this.addMasterListAddButton_Click);
            // 
            // addMasterListNameTextbox
            // 
            this.addMasterListNameTextbox.Location = new System.Drawing.Point(128, 36);
            this.addMasterListNameTextbox.MaxLength = 50;
            this.addMasterListNameTextbox.Name = "addMasterListNameTextbox";
            this.addMasterListNameTextbox.Size = new System.Drawing.Size(274, 20);
            this.addMasterListNameTextbox.TabIndex = 1;
            this.addMasterListNameTextbox.TextChanged += new System.EventHandler(this.addMasterListNameTextbox_TextChanged);
            // 
            // cameraAddMasterCombo
            // 
            this.cameraAddMasterCombo.FormattingEnabled = true;
            this.cameraAddMasterCombo.Location = new System.Drawing.Point(24, 104);
            this.cameraAddMasterCombo.Name = "cameraAddMasterCombo";
            this.cameraAddMasterCombo.Size = new System.Drawing.Size(121, 21);
            this.cameraAddMasterCombo.TabIndex = 2;
            this.cameraAddMasterCombo.DropDownClosed += new System.EventHandler(this.cameraAddMasterCombo_DropDownClosed);
            this.cameraAddMasterCombo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cameraAddMasterCombo_KeyPress);
            // 
            // cameraAddMasterListLabel
            // 
            this.cameraAddMasterListLabel.AutoSize = true;
            this.cameraAddMasterListLabel.Location = new System.Drawing.Point(21, 82);
            this.cameraAddMasterListLabel.Name = "cameraAddMasterListLabel";
            this.cameraAddMasterListLabel.Size = new System.Drawing.Size(46, 13);
            this.cameraAddMasterListLabel.TabIndex = 1;
            this.cameraAddMasterListLabel.Text = "Camera:";
            // 
            // masterTapeAddNameLabel
            // 
            this.masterTapeAddNameLabel.AutoSize = true;
            this.masterTapeAddNameLabel.Location = new System.Drawing.Point(21, 39);
            this.masterTapeAddNameLabel.Name = "masterTapeAddNameLabel";
            this.masterTapeAddNameLabel.Size = new System.Drawing.Size(101, 13);
            this.masterTapeAddNameLabel.TabIndex = 0;
            this.masterTapeAddNameLabel.Text = "Master Tape Name:";
            // 
            // editMasterListGroupBox
            // 
            this.editMasterListGroupBox.Controls.Add(this.editMasterCancelButton);
            this.editMasterListGroupBox.Controls.Add(this.editMasterEditButton);
            this.editMasterListGroupBox.Controls.Add(this.editCameraNewMasterDropdown);
            this.editMasterListGroupBox.Controls.Add(this.editNewCameraMasterLabel);
            this.editMasterListGroupBox.Controls.Add(this.editNewNameMasterTextbox);
            this.editMasterListGroupBox.Controls.Add(this.editNewMasterNameLabel);
            this.editMasterListGroupBox.Controls.Add(this.editCameraOldMasterNameLabel);
            this.editMasterListGroupBox.Controls.Add(this.cameraOldMasterLabel);
            this.editMasterListGroupBox.Controls.Add(this.editOldNameMasterLabel);
            this.editMasterListGroupBox.Controls.Add(this.editOldMasterNameLabel);
            this.editMasterListGroupBox.Location = new System.Drawing.Point(316, 554);
            this.editMasterListGroupBox.Name = "editMasterListGroupBox";
            this.editMasterListGroupBox.Size = new System.Drawing.Size(408, 205);
            this.editMasterListGroupBox.TabIndex = 8;
            this.editMasterListGroupBox.TabStop = false;
            this.editMasterListGroupBox.Text = "Edit Master Tape";
            this.editMasterListGroupBox.Visible = false;
            // 
            // editMasterCancelButton
            // 
            this.editMasterCancelButton.Location = new System.Drawing.Point(317, 162);
            this.editMasterCancelButton.Name = "editMasterCancelButton";
            this.editMasterCancelButton.Size = new System.Drawing.Size(75, 23);
            this.editMasterCancelButton.TabIndex = 1;
            this.editMasterCancelButton.Text = "Cancel";
            this.editMasterCancelButton.UseVisualStyleBackColor = true;
            this.editMasterCancelButton.Click += new System.EventHandler(this.editMasterCancelButton_Click);
            // 
            // editMasterEditButton
            // 
            this.editMasterEditButton.Location = new System.Drawing.Point(236, 162);
            this.editMasterEditButton.Name = "editMasterEditButton";
            this.editMasterEditButton.Size = new System.Drawing.Size(75, 23);
            this.editMasterEditButton.TabIndex = 4;
            this.editMasterEditButton.Text = "Edit";
            this.editMasterEditButton.UseVisualStyleBackColor = true;
            this.editMasterEditButton.Click += new System.EventHandler(this.editMasterEditButton_Click);
            // 
            // editCameraNewMasterDropdown
            // 
            this.editCameraNewMasterDropdown.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.editCameraNewMasterDropdown.FormattingEnabled = true;
            this.editCameraNewMasterDropdown.Location = new System.Drawing.Point(24, 136);
            this.editCameraNewMasterDropdown.MaxDropDownItems = 5;
            this.editCameraNewMasterDropdown.Name = "editCameraNewMasterDropdown";
            this.editCameraNewMasterDropdown.Size = new System.Drawing.Size(121, 21);
            this.editCameraNewMasterDropdown.TabIndex = 3;
            this.editCameraNewMasterDropdown.DropDownClosed += new System.EventHandler(this.editCameraNewMasterDropdown_DropDownClosed);
            this.editCameraNewMasterDropdown.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.editCameraNewMasterDropdown_KeyPress);
            // 
            // editNewCameraMasterLabel
            // 
            this.editNewCameraMasterLabel.AutoSize = true;
            this.editNewCameraMasterLabel.Location = new System.Drawing.Point(21, 114);
            this.editNewCameraMasterLabel.Name = "editNewCameraMasterLabel";
            this.editNewCameraMasterLabel.Size = new System.Drawing.Size(46, 13);
            this.editNewCameraMasterLabel.TabIndex = 6;
            this.editNewCameraMasterLabel.Text = "Camera:";
            // 
            // editNewNameMasterTextbox
            // 
            this.editNewNameMasterTextbox.Location = new System.Drawing.Point(90, 67);
            this.editNewNameMasterTextbox.MaxLength = 50;
            this.editNewNameMasterTextbox.Name = "editNewNameMasterTextbox";
            this.editNewNameMasterTextbox.Size = new System.Drawing.Size(312, 20);
            this.editNewNameMasterTextbox.TabIndex = 2;
            this.editNewNameMasterTextbox.TextChanged += new System.EventHandler(this.editNewNameMasterTextbox_TextChanged);
            // 
            // editNewMasterNameLabel
            // 
            this.editNewMasterNameLabel.AutoSize = true;
            this.editNewMasterNameLabel.Location = new System.Drawing.Point(21, 70);
            this.editNewMasterNameLabel.Name = "editNewMasterNameLabel";
            this.editNewMasterNameLabel.Size = new System.Drawing.Size(63, 13);
            this.editNewMasterNameLabel.TabIndex = 4;
            this.editNewMasterNameLabel.Text = "New Name:";
            // 
            // editCameraOldMasterNameLabel
            // 
            this.editCameraOldMasterNameLabel.AutoSize = true;
            this.editCameraOldMasterNameLabel.Location = new System.Drawing.Point(317, 28);
            this.editCameraOldMasterNameLabel.Name = "editCameraOldMasterNameLabel";
            this.editCameraOldMasterNameLabel.Size = new System.Drawing.Size(36, 13);
            this.editCameraOldMasterNameLabel.TabIndex = 3;
            this.editCameraOldMasterNameLabel.Text = "Media";
            // 
            // cameraOldMasterLabel
            // 
            this.cameraOldMasterLabel.AutoSize = true;
            this.cameraOldMasterLabel.Location = new System.Drawing.Point(265, 28);
            this.cameraOldMasterLabel.Name = "cameraOldMasterLabel";
            this.cameraOldMasterLabel.Size = new System.Drawing.Size(46, 13);
            this.cameraOldMasterLabel.TabIndex = 2;
            this.cameraOldMasterLabel.Text = "Camera:";
            // 
            // editOldNameMasterLabel
            // 
            this.editOldNameMasterLabel.AutoSize = true;
            this.editOldNameMasterLabel.Location = new System.Drawing.Point(65, 28);
            this.editOldNameMasterLabel.Name = "editOldNameMasterLabel";
            this.editOldNameMasterLabel.Size = new System.Drawing.Size(117, 13);
            this.editOldNameMasterLabel.TabIndex = 1;
            this.editOldNameMasterLabel.Text = "Master Tape Old Name";
            // 
            // editOldMasterNameLabel
            // 
            this.editOldMasterNameLabel.AutoSize = true;
            this.editOldMasterNameLabel.Location = new System.Drawing.Point(21, 28);
            this.editOldMasterNameLabel.Name = "editOldMasterNameLabel";
            this.editOldMasterNameLabel.Size = new System.Drawing.Size(38, 13);
            this.editOldMasterNameLabel.TabIndex = 0;
            this.editOldMasterNameLabel.Text = "Name:";
            // 
            // deleteMasterListGroupBox
            // 
            this.deleteMasterListGroupBox.Controls.Add(this.deleteMasterListCancelButton);
            this.deleteMasterListGroupBox.Controls.Add(this.deleteMasterListDeleteButton);
            this.deleteMasterListGroupBox.Controls.Add(this.deleteCameraNameMasterListLabel);
            this.deleteMasterListGroupBox.Controls.Add(this.deleteMasterNameMasterListLabel);
            this.deleteMasterListGroupBox.Controls.Add(this.deleteCameraMasterListLabel);
            this.deleteMasterListGroupBox.Controls.Add(this.deleteMasterMasterListLabel);
            this.deleteMasterListGroupBox.Location = new System.Drawing.Point(316, 182);
            this.deleteMasterListGroupBox.Name = "deleteMasterListGroupBox";
            this.deleteMasterListGroupBox.Size = new System.Drawing.Size(408, 143);
            this.deleteMasterListGroupBox.TabIndex = 9;
            this.deleteMasterListGroupBox.TabStop = false;
            this.deleteMasterListGroupBox.Text = "Delete Master Archive Item";
            this.deleteMasterListGroupBox.Visible = false;
            // 
            // deleteMasterListCancelButton
            // 
            this.deleteMasterListCancelButton.Location = new System.Drawing.Point(317, 100);
            this.deleteMasterListCancelButton.Name = "deleteMasterListCancelButton";
            this.deleteMasterListCancelButton.Size = new System.Drawing.Size(75, 23);
            this.deleteMasterListCancelButton.TabIndex = 1;
            this.deleteMasterListCancelButton.Text = "Cancel";
            this.deleteMasterListCancelButton.UseVisualStyleBackColor = true;
            this.deleteMasterListCancelButton.Click += new System.EventHandler(this.deleteMasterListCancelButton_Click);
            // 
            // deleteMasterListDeleteButton
            // 
            this.deleteMasterListDeleteButton.Location = new System.Drawing.Point(236, 100);
            this.deleteMasterListDeleteButton.Name = "deleteMasterListDeleteButton";
            this.deleteMasterListDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteMasterListDeleteButton.TabIndex = 2;
            this.deleteMasterListDeleteButton.Text = "Delete";
            this.deleteMasterListDeleteButton.UseVisualStyleBackColor = true;
            this.deleteMasterListDeleteButton.Click += new System.EventHandler(this.deleteMasterListDeleteButton_Click);
            // 
            // deleteCameraNameMasterListLabel
            // 
            this.deleteCameraNameMasterListLabel.AutoSize = true;
            this.deleteCameraNameMasterListLabel.Location = new System.Drawing.Point(139, 70);
            this.deleteCameraNameMasterListLabel.Name = "deleteCameraNameMasterListLabel";
            this.deleteCameraNameMasterListLabel.Size = new System.Drawing.Size(44, 13);
            this.deleteCameraNameMasterListLabel.TabIndex = 3;
            this.deleteCameraNameMasterListLabel.Text = "Cannon";
            // 
            // deleteMasterNameMasterListLabel
            // 
            this.deleteMasterNameMasterListLabel.AutoSize = true;
            this.deleteMasterNameMasterListLabel.Location = new System.Drawing.Point(139, 31);
            this.deleteMasterNameMasterListLabel.Name = "deleteMasterNameMasterListLabel";
            this.deleteMasterNameMasterListLabel.Size = new System.Drawing.Size(79, 13);
            this.deleteMasterNameMasterListLabel.TabIndex = 2;
            this.deleteMasterNameMasterListLabel.Text = "Delete this item";
            // 
            // deleteCameraMasterListLabel
            // 
            this.deleteCameraMasterListLabel.AutoSize = true;
            this.deleteCameraMasterListLabel.Location = new System.Drawing.Point(21, 70);
            this.deleteCameraMasterListLabel.Name = "deleteCameraMasterListLabel";
            this.deleteCameraMasterListLabel.Size = new System.Drawing.Size(46, 13);
            this.deleteCameraMasterListLabel.TabIndex = 1;
            this.deleteCameraMasterListLabel.Text = "Camera:";
            // 
            // deleteMasterMasterListLabel
            // 
            this.deleteMasterMasterListLabel.AutoSize = true;
            this.deleteMasterMasterListLabel.Location = new System.Drawing.Point(21, 31);
            this.deleteMasterMasterListLabel.Name = "deleteMasterMasterListLabel";
            this.deleteMasterMasterListLabel.Size = new System.Drawing.Size(112, 13);
            this.deleteMasterMasterListLabel.TabIndex = 0;
            this.deleteMasterMasterListLabel.Text = "Master Archive Name:";
            // 
            // MasterListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 756);
            this.ControlBox = false;
            this.Controls.Add(this.deleteMasterListGroupBox);
            this.Controls.Add(this.editMasterListGroupBox);
            this.Controls.Add(this.addMasterListGroupBox);
            this.Controls.Add(this.masterListBoxLabel);
            this.Controls.Add(this.defaultMasterGroupBox);
            this.Controls.Add(this.masterListDeleteButton);
            this.Controls.Add(this.masterListEditButton);
            this.Controls.Add(this.masterListAddButton);
            this.Controls.Add(this.masterListListBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "MasterListForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.defaultMasterGroupBox.ResumeLayout(false);
            this.defaultMasterGroupBox.PerformLayout();
            this.addMasterListGroupBox.ResumeLayout(false);
            this.addMasterListGroupBox.PerformLayout();
            this.editMasterListGroupBox.ResumeLayout(false);
            this.editMasterListGroupBox.PerformLayout();
            this.deleteMasterListGroupBox.ResumeLayout(false);
            this.deleteMasterListGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox masterListListBox;
        private System.Windows.Forms.Button masterListAddButton;
        private System.Windows.Forms.Button masterListEditButton;
        private System.Windows.Forms.Button masterListDeleteButton;
        private System.Windows.Forms.GroupBox defaultMasterGroupBox;
        private System.Windows.Forms.Label masterListBoxLabel;
        private System.Windows.Forms.GroupBox addMasterListGroupBox;
        private System.Windows.Forms.Button addMasterListCancelButton;
        private System.Windows.Forms.Button addMasterListAddButton;
        private System.Windows.Forms.TextBox addMasterListNameTextbox;
        private System.Windows.Forms.ComboBox cameraAddMasterCombo;
        private System.Windows.Forms.Label cameraAddMasterListLabel;
        private System.Windows.Forms.Label masterTapeAddNameLabel;
        private System.Windows.Forms.GroupBox editMasterListGroupBox;
        private System.Windows.Forms.Button editMasterCancelButton;
        private System.Windows.Forms.Button editMasterEditButton;
        private System.Windows.Forms.ComboBox editCameraNewMasterDropdown;
        private System.Windows.Forms.Label editNewCameraMasterLabel;
        private System.Windows.Forms.TextBox editNewNameMasterTextbox;
        private System.Windows.Forms.Label editNewMasterNameLabel;
        private System.Windows.Forms.Label editCameraOldMasterNameLabel;
        private System.Windows.Forms.Label cameraOldMasterLabel;
        private System.Windows.Forms.Label editOldNameMasterLabel;
        private System.Windows.Forms.Label editOldMasterNameLabel;
        private System.Windows.Forms.GroupBox deleteMasterListGroupBox;
        private System.Windows.Forms.Button deleteMasterListCancelButton;
        private System.Windows.Forms.Button deleteMasterListDeleteButton;
        private System.Windows.Forms.Label deleteCameraNameMasterListLabel;
        private System.Windows.Forms.Label deleteMasterNameMasterListLabel;
        private System.Windows.Forms.Label deleteCameraMasterListLabel;
        private System.Windows.Forms.Label deleteMasterMasterListLabel;
        private System.Windows.Forms.Label defaultMasterListLabel;
        private System.Windows.Forms.Label defaultCameraNameMasterListLabel;
        private System.Windows.Forms.Label defaultArchiveNameMasterListLabel;
        private System.Windows.Forms.Label defaultCameraMasterListLabel;
        private System.Windows.Forms.Label defaultArchiveMasterListLabel;
    }
}