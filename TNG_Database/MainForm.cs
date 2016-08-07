using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using TNG_Database.Values;

namespace TNG_Database
{
    public partial class MainForm : Form
    {
        public TNG_Database.SearchTapeForm searchTapeForm;
        public TNG_Database.PeopleForm peopleForm;
        public TNG_Database.MasterListForm masterListForm;
        public TNG_Database.TapeListForm tapeListForm;
        public TNG_Database.ProjectsForm projectsForm;
        public TNG_Database.MasterArchiveVideosForm masterArchiveForm;
        public TNG_Database.DeletedValuesForm deletedValuesForm;

        private string connect = DataBaseControls.GetDBName();
        OpenFileDialog ofd;

        //Reference to CommonMethods
        CommonMethods commonMethod = CommonMethods.Instance();

        public MainForm()
        {
            

            InitializeComponent();
            
            TNG_Database.SearchTapeForm child = new TNG_Database.SearchTapeForm(this);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            child.Show();
            child.WindowState = FormWindowState.Maximized;

            //check to see if this is the first time the program has ran
            if (Properties.TNG_Settings.Default.FirstRun)
            {
                CreateSQLDatabase();
            }
        }

        private void CreateSQLDatabase()
        {
            if (!File.Exists(@"database\TNG_TapeDatabase.sqlite"))
            {
                try
                {
                    //create directory and file if they do not exist
                    Directory.CreateDirectory(@"database");
                    SQLiteConnection.CreateFile("database/TNG_TapeDatabase.sqlite");
                }catch(Exception e)
                {
                    LogFile(e.Message);
                }

                //create table strings
                string createTapeTable = "CREATE TABLE `TapeDatabase` (" +
                   "`id` INTEGER PRIMARY KEY AUTOINCREMENT,`tape_name` TEXT,`tape_number` TEXT," +
                   "`project_id` TEXT, `project_name` TEXT, `camera` INTEGER, `tape_tags` TEXT," +
                   "`date_shot` TEXT, `master_archive` TEXT, `person_entered` TEXT)";

                string createMasterTable = "CREATE TABLE `MasterList` (`id` INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`master_archive` TEXT UNIQUE, `master_media` INTEGER)";

                string createPeopleTable = "CREATE TABLE `People` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`person_name` TEXT UNIQUE)";

                string createProjectsTable = "CREATE TABLE `Projects` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`project_id` TEXT UNIQUE, `project_name` TEXT)";

                string createMasterArchiveVideos = "CREATE TABLE `MasterArchiveVideos` (`id` INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`project_id` TEXT, `video_name` TEXT, `master_tape` TEXT, `clip_number` TEXT)";

                string createDeleteTapeDatabase = "CREATE TABLE `DeleteTapeDatabase` (`id` INTEGER PRIMARY KEY AUTOINCREMENT,`tape_name` TEXT,`tape_number` TEXT,`project_id` TEXT, `project_name` TEXT, `camera` INTEGER, `tape_tags` TEXT,`date_shot` TEXT, `master_archive` TEXT, `person_entered` TEXT)";

                string createDeleteProjects = "CREATE TABLE `DeleteProjects` (`id` INTEGER PRIMARY KEY AUTOINCREMENT,`project_id` TEXT, `project_name` TEXT)";

                string createDeletePeople = "CREATE TABLE `DeletePeople` (`id` INTEGER PRIMARY KEY AUTOINCREMENT,`person_name` TEXT UNIQUE)";

                string createDeleteMasterList = "CREATE TABLE `DeleteMasterList` (`id` INTEGER PRIMARY KEY AUTOINCREMENT,`master_archive` TEXT UNIQUE, `master_media` INTEGER)";

                string createDeleteMasterArchiveVideos = "CREATE TABLE `DeleteMasterArchiveVideos` (`id` INTEGER PRIMARY KEY AUTOINCREMENT,`project_id` TEXT,`video_name` TEXT,`master_tape` TEXT,`clip_number` TEXT)";

                //put all create strings into string array
                string[] allCreates = { createTapeTable, createMasterTable, createPeopleTable, createProjectsTable, createMasterArchiveVideos, createDeleteTapeDatabase, createDeleteProjects, createDeletePeople, createDeleteMasterList, createDeleteMasterArchiveVideos };
                
                //try to write tables to database
                try
                {
                    //connect to database
                    using (SQLiteConnection createConnection = new SQLiteConnection(DataBaseControls.GetDBName()))
                    {
                        createConnection.Open();
                        
                        //iterate over all tables and add to database
                        foreach (string query in allCreates)
                        {
                            SQLiteCommand command = new SQLiteCommand(query, createConnection);
                            if (command.ExecuteNonQuery() == 0)
                            {
                                //success
                                LogFile("Database Default Table Created");
                            }
                            else
                            {
                                //failed
                                LogFile("Database Default Table Failed to be Created");
                            }
                        }
                        createConnection.Close();
                    }
                        
                }catch(SQLiteException e)
                {
                    LogFile(e.Message);
                }
                
            }
            else
            {

            }
            //Set it so this won't run again
            Properties.TNG_Settings.Default.FirstRun = false;
        }

        /// <summary>
        /// Logs entries into logfile
        /// </summary>
        /// <param name="log">log entry</param>
        public static void LogFile(string log)
        {
            try
            {
                if (!File.Exists(@"log\TNG_Database_Log.txt"))
                {
                    //Log file did not exist
                    Directory.CreateDirectory(@"log");
                    Console.WriteLine("Log files does not exist");
                    File.Create(@"log\TNG_Database_Log.txt").Close();
                }

                //Write to log file
                using (StreamWriter logWriter = File.AppendText(@"log\TNG_Database_Log.txt"))
                {
                    //Log Format
                    logWriter.WriteLine("{0} {1}: {2}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString(),log);

                    logWriter.Close();
                }
            }catch(Exception e)
            {
                //Error
                Console.WriteLine(e.ToString());
            }
        }

        private void StatusUpdate(string status)
        {
            applicationStatusLabel.Text = status;
        }

        //Click on Close
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //-----------------------------------
        #region Data Toolstrip
        //Opens up people form to add, delete or update user list
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new instance of form
            peopleForm = new PeopleForm(this);

            //close child of mdi if there is one active
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //Show people form and maximize it instantly
            peopleForm.Show();
            peopleForm.WindowState = FormWindowState.Maximized;
        }

        //-----------------------------------
        //Opens up search tape database form to search tape database
        private void tapeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new instance of form SearchTapeForm
            tapeListForm = new TNG_Database.TapeListForm(this);

            //close child of mdi if there is one active
            if(ActiveMdiChild != null){ ActiveMdiChild.Close(); }

            //Show search tape database form and maximize it instantly
            tapeListForm.Show();
            tapeListForm.WindowState = FormWindowState.Maximized;
        }

        //-----------------------------------
        //Opens up Master list form to add, delete, update master archive list
        private void masterArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new instance of form MasterListForm
            masterListForm = new TNG_Database.MasterListForm(this);

            //close chold of mdi if there is one active
            if(ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //show Master list form and maximize it instantly
            masterListForm.Show();
            masterListForm.WindowState = FormWindowState.Maximized;
            
        }

        //Open projects data from menu
        private void projectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new instance of form MasterListForm
            projectsForm = new TNG_Database.ProjectsForm(this);

            //close chold of mdi if there is one active
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //show Master list form and maximize it instantly
            projectsForm.Show();
            projectsForm.WindowState = FormWindowState.Maximized;
        }

        //Open Archive videos data from menu
        private void archiveVideosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new instance of form MasterListForm
            masterArchiveForm = new TNG_Database.MasterArchiveVideosForm(this);

            //close chold of mdi if there is one active
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //show Master list form and maximize it instantly
            masterArchiveForm.Show();
            masterArchiveForm.WindowState = FormWindowState.Maximized;
        }

        private void deletedDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new instance of form MasterListForm
            deletedValuesForm = new TNG_Database.DeletedValuesForm(this);

            //close chold of mdi if there is one active
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //show Master list form and maximize it instantly
            deletedValuesForm.Show();
            deletedValuesForm.WindowState = FormWindowState.Maximized;
        }

        #endregion

        /// <summary>
        /// Updates the status bar bottom.
        /// </summary>
        /// <param name="update">The update.</param>
        public void UpdateStatusBarBottom(string update)
        {
            applicationStatusLabel.Text = update;
        }


        public delegate void UpdateProgressBarCallback(int add);

        private void UpdateProgressBar(int add)
        {
            mainFormProgressBar.Increment(add);
        }

        //Import->Projects
        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string importSetting = "projects";
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Properties.TNG_Settings.Default.LastFolder;
            ofd.Filter = "comma seperated files (*.csv)|*.csv|text files (*.txt)|*.txt";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.OpenFile() != null)
                {
                    Properties.TNG_Settings.Default.LastFolder = Path.GetDirectoryName(ofd.FileName);
                    Console.WriteLine(Properties.TNG_Settings.Default.LastFolder);
                    if (backgroundWorker1.IsBusy != true)
                    {
                        mainFormProgressBar.Value = 0;
                        backgroundWorker1.RunWorkerAsync(importSetting);
                    }
                }
            }
        }

        /// <summary>
        /// Checks the import function and calls appropriate import function.
        /// </summary>
        /// <param name="worker">The background worker.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void CheckImportFunction(BackgroundWorker worker, DoWorkEventArgs e)
        {
            Stream importStream = null;

            switch (e.Argument.ToString())
            {
                case "projects":
                    DataBaseControls.AddProjectsFromFile(worker, importStream, ofd);
                    break;
                case "masters":
                    //Master List import, has a popup to enter Master Tape to add to
                    List<MasterListValues> masterListValues = DataBaseControls.GetAllMasterListItems();
                    string[] cameraValues = commonMethod.CameraDropdownItems();
                    string masterTapeName = "";
                    string cameraMasterName = "";
                    bool addMasters = false;
                    //create a new form for user to enter tape name
                    Form masterPrompt = new Form();
                    masterPrompt.Height = 200;
                    masterPrompt.Width = 500;
                    masterPrompt.Text = "Enter Tape Name";

                    //Set up items to add to popup box
                    Label textLabel = new Label() { Left = 50, Top = 20, Text = "Master Archive to Import" };
                    ComboBox inputBox = new ComboBox() { Left = 50, Top = 50, Width = 400 };
                    //add items to combobox
                    foreach (MasterListValues values in masterListValues)
                    {
                        inputBox.Items.Add(values.MasterArchive);
                    }
                    inputBox.SelectedIndex = 0;
                    //add media combobox
                    ComboBox mediaCombo = new ComboBox() { Left = 50, Top = 75, Width = 200 };
                    foreach(string mediaValue in cameraValues)
                    {
                        mediaCombo.Items.Add(mediaValue);
                    }
                    mediaCombo.SelectedIndex = 1;
                    mediaCombo.KeyPress += (senderCombo, eCombo) => { eCombo.Handled = true; };
                    mediaCombo.SelectedIndexChanged += (senderCombo, eCombo) => { textLabel.Focus(); };
                    //Set up buttons to add
                    Button confirmation = new Button() { Text = "OK", Left = 240, Width = 100, Top = 120 };
                    Button cancelButton = new Button() { Text = "Cancel", Left = 350, Width = 100, Top = 120 };
                    //button actions
                    cancelButton.Click += (senderPrompt, ePrompt) => { addMasters = false; masterPrompt.Close(); };
                    confirmation.Click += (senderPrompt, ePrompt) => { addMasters = true; masterTapeName = inputBox.Text; cameraMasterName = mediaCombo.Text; masterPrompt.Close(); };
                    //Add items to form
                    masterPrompt.Controls.Add(textLabel);
                    masterPrompt.Controls.Add(inputBox);
                    masterPrompt.Controls.Add(mediaCombo);
                    masterPrompt.Controls.Add(confirmation);
                    masterPrompt.Controls.Add(cancelButton);
                    masterPrompt.ShowDialog();
                    //Add entries or Cancel depending on button clicked
                    if (addMasters)
                    {
                        StatusUpdate("Importing " + masterTapeName + " Entries");
                        DataBaseControls.AddMasterTapesFromFile(worker, importStream, ofd, masterTapeName, commonMethod.GetCameraNumber(cameraMasterName));
                    }
                    else
                    {
                        worker.CancelAsync();
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                    break;
            }
        }

        //does the background work
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Stream importStream = null;
            BackgroundWorker worker = sender as BackgroundWorker;

            if ((importStream = ofd.OpenFile()) != null)
            {
                CheckImportFunction(worker, e);
            }
        }

        //Update progress of adding projects
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage > 100)
            {
                mainFormProgressBar.Value = 100;
            }else
            {
                //Increment the progressbar by number provided
                //mainFormProgressBar.Increment(e.ProgressPercentage);
                mainFormProgressBar.Value = e.ProgressPercentage;
            }
            
        }

        //Background process completed
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                //cancelled
                UpdateStatusBarBottom("Cancelled!");
            }
            else if (e.Error != null)
            {
                //error
                LogFile(e.Error.Message);
            }
            else
            {
                //Success
                UpdateStatusBarBottom("Done!");
                //make sure progress bar is the max after completion
                mainFormProgressBar.Value = mainFormProgressBar.Maximum;
            }
        }
        

        //Import a XDCam master csv file click
        private void xDCamMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string importMaster = "masters";
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Properties.TNG_Settings.Default.LastFolder;
            ofd.Filter = "comma seperated files (*.csv)|*.csv";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.OpenFile() != null)
                {
                    Properties.TNG_Settings.Default.LastFolder = Path.GetDirectoryName(ofd.FileName);
                    Console.WriteLine(Properties.TNG_Settings.Default.LastFolder);
                    if (backgroundWorker1.IsBusy != true)
                    {
                        mainFormProgressBar.Value = 0;
                        backgroundWorker1.RunWorkerAsync(importMaster);
                    }
                }
            }
        }

        private void searchTapeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new instance of form
            searchTapeForm = new SearchTapeForm(this);

            //close child of mdi if there is one active
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //Show people form and maximize it instantly
            searchTapeForm.Show();
            searchTapeForm.WindowState = FormWindowState.Maximized;
        }
    }
}
