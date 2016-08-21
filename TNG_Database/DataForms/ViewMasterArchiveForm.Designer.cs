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
            this.panel1 = new System.Windows.Forms.Panel();
            this.viewMasterListMainLabel = new System.Windows.Forms.Label();
            this.viewMastersFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewMasterListBox
            // 
            this.viewMasterListBox.FormattingEnabled = true;
            this.viewMasterListBox.Location = new System.Drawing.Point(12, 53);
            this.viewMasterListBox.Name = "viewMasterListBox";
            this.viewMasterListBox.Size = new System.Drawing.Size(233, 446);
            this.viewMasterListBox.TabIndex = 0;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.viewMastersFlowPanel);
            this.panel1.Controls.Add(this.viewMasterListMainLabel);
            this.panel1.Location = new System.Drawing.Point(285, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(465, 446);
            this.panel1.TabIndex = 2;
            // 
            // viewMasterListMainLabel
            // 
            this.viewMasterListMainLabel.AutoSize = true;
            this.viewMasterListMainLabel.Location = new System.Drawing.Point(3, 0);
            this.viewMasterListMainLabel.Name = "viewMasterListMainLabel";
            this.viewMasterListMainLabel.Size = new System.Drawing.Size(35, 13);
            this.viewMasterListMainLabel.TabIndex = 0;
            this.viewMasterListMainLabel.Text = "label2";
            // 
            // viewMastersFlowPanel
            // 
            this.viewMastersFlowPanel.Location = new System.Drawing.Point(6, 16);
            this.viewMastersFlowPanel.Name = "viewMastersFlowPanel";
            this.viewMastersFlowPanel.Size = new System.Drawing.Size(456, 427);
            this.viewMastersFlowPanel.TabIndex = 1;
            // 
            // ViewMasterArchiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 587);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.viewMasterListBox);
            this.Name = "ViewMasterArchiveForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ViewMasterArchiveForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox viewMasterListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel viewMastersFlowPanel;
        private System.Windows.Forms.Label viewMasterListMainLabel;
    }
}