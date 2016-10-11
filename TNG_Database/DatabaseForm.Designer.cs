namespace TNG_Database
{
    partial class DatabaseForm
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
            this.columnFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.reinstateDBButton = new System.Windows.Forms.Button();
            this.ribbonButtons1 = new TNG_Database.RibbonButtons();
            this.SuspendLayout();
            // 
            // databaseListView
            // 
            this.databaseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFile,
            this.columnDate});
            this.databaseListView.FullRowSelect = true;
            this.databaseListView.HideSelection = false;
            this.databaseListView.Location = new System.Drawing.Point(34, 105);
            this.databaseListView.MultiSelect = false;
            this.databaseListView.Name = "databaseListView";
            this.databaseListView.Size = new System.Drawing.Size(450, 403);
            this.databaseListView.TabIndex = 0;
            this.databaseListView.UseCompatibleStateImageBehavior = false;
            this.databaseListView.View = System.Windows.Forms.View.Details;
            this.databaseListView.SelectedIndexChanged += new System.EventHandler(this.databaseListView_SelectedIndexChanged);
            // 
            // columnFile
            // 
            this.columnFile.Text = "File";
            this.columnFile.Width = 300;
            // 
            // columnDate
            // 
            this.columnDate.Text = "Date Archived";
            this.columnDate.Width = 140;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Archived Databases";
            // 
            // reinstateDBButton
            // 
            this.reinstateDBButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reinstateDBButton.Location = new System.Drawing.Point(523, 105);
            this.reinstateDBButton.Name = "reinstateDBButton";
            this.reinstateDBButton.Size = new System.Drawing.Size(223, 62);
            this.reinstateDBButton.TabIndex = 2;
            this.reinstateDBButton.Text = "Reinstate";
            this.reinstateDBButton.UseVisualStyleBackColor = true;
            this.reinstateDBButton.Click += new System.EventHandler(this.reinstateDBButton_Click);
            // 
            // ribbonButtons1
            // 
            this.ribbonButtons1.ImageType = TNG_Database.RibbonButtons.ImageForButton.Reload;
            this.ribbonButtons1.Location = new System.Drawing.Point(394, 63);
            this.ribbonButtons1.Name = "ribbonButtons1";
            this.ribbonButtons1.Size = new System.Drawing.Size(60, 36);
            this.ribbonButtons1.TabIndex = 3;
            this.ribbonButtons1.Text = "Refresh";
            this.ribbonButtons1.Click += new System.EventHandler(this.ribbonButtons1_Click);
            // 
            // DatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 577);
            this.ControlBox = false;
            this.Controls.Add(this.ribbonButtons1);
            this.Controls.Add(this.reinstateDBButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.databaseListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DatabaseForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DatabaseForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView databaseListView;
        private System.Windows.Forms.ColumnHeader columnFile;
        private System.Windows.Forms.ColumnHeader columnDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button reinstateDBButton;
        private RibbonButtons ribbonButtons1;
    }
}