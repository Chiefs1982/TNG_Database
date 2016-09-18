using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class PreferencesForm : Form
    {

        private TNG_Database.MainForm mainform;


        public PreferencesForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            //load stats
            LoadStats();

            importPrefDirTexbox.Text = Path.GetFullPath(Properties.TNG_Settings.Default.ImportFolder);
            exportPrefDirTexbox.Text = Path.GetFullPath(Properties.TNG_Settings.Default.ExportFolder);

            databaseBackupCombobox.Items.AddRange(new string[] { "Day", "Week", "2 Weeks", "30 Days", "90 Days", "180 Days", "Year" });

            databaseBackupCombobox.Text = GetNameOfBackupNumber();
        }

        #region Class Methods

        /// <summary>
        /// Loads the stats.
        /// </summary>
        private void LoadStats()
        {
            List<int> dbEntries = DataBaseControls.CountAllEntries();

            tapeStats.Text = dbEntries[0].ToString();
            projectStats.Text = dbEntries[1].ToString();
            archiveTapesStats.Text = dbEntries[2].ToString();
            archiveVideosStats.Text = dbEntries[3].ToString();
            usersStats.Text = dbEntries[4].ToString();
        }

        /// <summary>
        /// Asks user to pick a folder to save default import folder
        /// </summary>
        private void ImportFolderSave()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            //Add folder attributes
            folder.Description = "Choose folder as default import folder";
            //folder.RootFolder = Environment.SpecialFolder.MyDocuments;
            folder.SelectedPath = Properties.TNG_Settings.Default.ImportFolder;
            folder.ShowNewFolderButton = true;

            DialogResult result = folder.ShowDialog();
            if(result == DialogResult.OK)
            {
                Properties.TNG_Settings.Default.ImportFolder = folder.SelectedPath;
                Properties.TNG_Settings.Default.Save();

                importPrefDirTexbox.Text = Path.GetFullPath(Properties.TNG_Settings.Default.ImportFolder);
            }

        }

        /// <summary>
        /// Asks user to pick a folder to save default export folder
        /// </summary>
        private void ExportFolderSave()
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();

            //Add folder attributes
            folder.Description = "Choose folder as default export folder";
            //folder.RootFolder = Environment.SpecialFolder.MyDocuments;
            folder.SelectedPath = Properties.TNG_Settings.Default.ExportFolder;
            folder.ShowNewFolderButton = true;

            DialogResult result = folder.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.TNG_Settings.Default.ExportFolder = folder.SelectedPath;
                Properties.TNG_Settings.Default.Save();

                exportPrefDirTexbox.Text = Path.GetFullPath(Properties.TNG_Settings.Default.ExportFolder);
            }
        }

        /// <summary>
        /// Converts combobox value to a number.
        /// </summary>
        /// <returns></returns>
        private int BackupToNumbers()
        {
            switch (databaseBackupCombobox.Text.ToLower())
            {
                case "day":
                    return 1;
                case "week":
                    return 7;
                case "2 weeks":
                    return 14;
                case "30 days":
                    return 30;
                case "90 days":
                    return 90;
                case "180 days":
                    return 180;
                case "year":
                default:
                    return 365;
            }
        }

        /// <summary>
        /// Saves the backup number.
        /// </summary>
        private void SaveBackupNumber()
        {
            Properties.TNG_Settings.Default.DBBackupSetting = BackupToNumbers();
            Properties.TNG_Settings.Default.Save();
        }

        private string GetNameOfBackupNumber()
        {
            switch (Properties.TNG_Settings.Default.DBBackupSetting)
            {
                case 1:
                    return "Day";
                case 7:
                    return "Week";
                case 14:
                    return "2 Weeks";
                case 30:
                    return "30 Days";
                case 90:
                    return "90 Days";
                case 180:
                    return "180 Days";
                case 365:
                default:
                    return "Year";
            }
        }

        #endregion

        #region Events

        private void importPrefBrowseButton_Click(object sender, EventArgs e)
        {
            ImportFolderSave();
        }

        private void exportPrefBrowseButton_Click(object sender, EventArgs e)
        {
            ExportFolderSave();
        }

        private void databaseBackupCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveBackupNumber();
        }

        private void databaseBackupCombobox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void databaseBackupCombobox_DropDownClosed(object sender, EventArgs e)
        {
            label4.Focus();
        }

        #endregion

        
    }
}
