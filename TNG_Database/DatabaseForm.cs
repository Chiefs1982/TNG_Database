using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class DatabaseForm : Form
    {
        //reference for the mainform
        private TNG_Database.MainForm mainform;

        //current file selected
        string fileSelected;

        //instance reference
        UpdateStatus updateStatus = UpdateStatus.Instance();

        public DatabaseForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            //disable button
            reinstateDBButton.Enabled = false;

            //load the listview
            PopulateDatabaseList();

        }

        /// <summary>
        /// Populates the database list.
        /// </summary>
        private void PopulateDatabaseList()
        {
            //Clear any items in listview
            databaseListView.Items.Clear();

            //get a string array of files in the backups directory
            string[] databaseList = Directory.GetFiles(@"backups");

            if(databaseList.Length == 0)
            {

            }else
            {
                //iterate over files in the folder of backups
                foreach (string file in databaseList)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string dateFromFile = DateTime.Today.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                    DateTime dateParsed;

                    //Make sure that the string is over needed length
                    if (fileName.Length >= 23)
                    {
                        dateFromFile = fileName.Substring(fileName.Length - 23);
                    }

                    //get date from the filename
                    if (DateTime.TryParseExact(dateFromFile, "yyyy-MM-dd_HH-mm-ss-fff", null,
                                      DateTimeStyles.None, out dateParsed))
                    {
                        dateFromFile = dateParsed.ToString("MM/dd/yyy HH:mm:ss");
                    }

                    //add item to listview and tag with full file
                    databaseListView.Items.Add(new ListViewItem(new string[] { fileName, dateFromFile })).Tag = file;
                }
            }

            updateStatus.UpdateStatusBar("Database Backup List Loaded", mainform);
        }

        /// <summary>
        /// Reinstates the database with the file selected.
        /// </summary>
        /// <param name="file">The file.</param>
        private void ReinstateDatabase(string file)
        {
            if(MessageBox.Show("Are you sure that you want to reinstate this database?", "Reinstate Database", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                mainform.BackupDatabase();

                File.Copy(file, DataBaseControls.FullDatabaseName, true);
            }

            //update status
            updateStatus.UpdateStatusBar("Database Reinstated: " + Path.GetFileName(file), mainform);

            //reload the listview
            PopulateDatabaseList();
        }

        //Reinstate Button click
        private void reinstateDBButton_Click(object sender, EventArgs e)
        {
            ReinstateDatabase(fileSelected);
        }

        private void databaseListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if 1 item is selected
            if(databaseListView.SelectedItems.Count == 1)
            {
                reinstateDBButton.Enabled = true;
                fileSelected = databaseListView.SelectedItems[0].Tag.ToString();
            }else
            {
                //disable button
                reinstateDBButton.Enabled = false;
            }
        }

        private void ribbonButtons1_Click(object sender, EventArgs e)
        {
            PopulateDatabaseList();
        }
    }
}
