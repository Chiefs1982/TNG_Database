namespace TNG_Database
{
    partial class ViewMasterArchiveForm
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
            this.viewMasterListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewMasterListView = new System.Windows.Forms.ListView();
            this.viewMasterListMainLabel = new System.Windows.Forms.Label();
            this.columnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnClip = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // viewMasterListBox
            // 
            this.viewMasterListBox.FormattingEnabled = true;
            this.viewMasterListBox.Location = new System.Drawing.Point(12, 53);
            this.viewMasterListBox.Name = "viewMasterListBox";
            this.viewMasterListBox.Size = new System.Drawing.Size(233, 446);
            this.viewMasterListBox.TabIndex = 0;
            this.viewMasterListBox.SelectedIndexChanged += new System.EventHandler(this.viewMasterListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "View Master Archive Lists";
            // 
            // viewMasterListView
            // 
            this.viewMasterListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnID,
            this.columnName,
            this.columnClip});
            this.viewMasterListView.FullRowSelect = true;
            this.viewMasterListView.Location = new System.Drawing.Point(304, 53);
            this.viewMasterListView.MultiSelect = false;
            this.viewMasterListView.Name = "viewMasterListView";
            this.viewMasterListView.Size = new System.Drawing.Size(456, 446);
            this.viewMasterListView.TabIndex = 1;
            this.viewMasterListView.UseCompatibleStateImageBehavior = false;
            this.viewMasterListView.View = System.Windows.Forms.View.Details;
            // 
            // viewMasterListMainLabel
            // 
            this.viewMasterListMainLabel.AutoSize = true;
            this.viewMasterListMainLabel.Location = new System.Drawing.Point(301, 32);
            this.viewMasterListMainLabel.Name = "viewMasterListMainLabel";
            this.viewMasterListMainLabel.Size = new System.Drawing.Size(58, 13);
            this.viewMasterListMainLabel.TabIndex = 0;
            this.viewMasterListMainLabel.Text = "Master List";
            // 
            // columnID
            // 
            this.columnID.Text = "Project ID";
            this.columnID.Width = 80;
            // 
            // columnName
            // 
            this.columnName.Text = "Project Name";
            this.columnName.Width = 200;
            // 
            // columnClip
            // 
            this.columnClip.Text = "Clip #";
            this.columnClip.Width = 100;
            // 
            // ViewMasterArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 587);
            this.ControlBox = false;
            this.Controls.Add(this.viewMasterListMainLabel);
            this.Controls.Add(this.viewMasterListView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.viewMasterListBox);
            this.Name = "ViewMasterArchiveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ViewMasterArchiveForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox viewMasterListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label viewMasterListMainLabel;
        private System.Windows.Forms.ListView viewMasterListView;
        private System.Windows.Forms.ColumnHeader columnID;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnClip;
    }
}