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

namespace TNG_Database
{
    public partial class MainForm : Form
    {
        public TNG_Database.SearchTapeForm searchTapeForm;
        public TNG_Database.PeopleForm peopleForm;
        public TNG_Database.MasterListForm masterListForm;
        public TNG_Database.TapeListForm tapeListForm;

        private string connect = DataBaseControls.GetDBName();
        OpenFileDialog ofd;
        
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

                string createMasterTable = "CREATE TABLE `MasterList` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`master_archive` TEXT UNIQUE, `master_media` INTEGER)";

                string createPeopleTable = "CREATE TABLE `People` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`person_name` TEXT UNIQUE)";

                string createProjectsTable = "CREATE TABLE `Projects` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`project_id` TEXT, `project_name` TEXT)";

                string createMasterArchiveVideos = "CREATE TABLE `MasterArchiveVideos` (`id` INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "`project_id` TEXT, `video_name` TEXT, `master_tape` TEXT, `clip_number` TEXT)";

                //put all create strings into string array
                string[] allCreates = { createTapeTable, createMasterTable, createPeopleTable, createProjectsTable,createMasterArchiveVideos };
                
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

        //does the background work
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Stream importStream = null;
            BackgroundWorker worker = sender as BackgroundWorker;

            if ((importStream = ofd.OpenFile()) != null)
            {

                switch (e.Argument.ToString())
                {
                    case "projects":
                        DataBaseControls.AddProjectsFromFile(worker, importStream,ofd);
                        break;
                    case "masters":
                        //Master List import, has a popup to enter Master Tape to add to
                        List<MasterListValues> masterListValues = DataBaseControls.GetAllMasterListItems();
                        string masterTapeName = "";
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
                        //Set up buttons to add
                        Button confirmation = new Button() { Text = "OK", Left = 225, Width = 100, Top = 100 };
                        Button cancelButton = new Button() { Text = "Cancel", Left = 350, Width = 100, Top = 100};
                        //button actions
                        cancelButton.Click += (senderPrompt, ePrompt) => { addMasters = false; masterPrompt.Close(); };
                        confirmation.Click += (senderPrompt, ePrompt) => { addMasters = true; masterTapeName = inputBox.Text;masterPrompt.Close();};
                        //Add items to form
                        masterPrompt.Controls.Add(textLabel);
                        masterPrompt.Controls.Add(inputBox);
                        masterPrompt.Controls.Add(confirmation);
                        masterPrompt.Controls.Add(cancelButton);
                        masterPrompt.ShowDialog();
                        //Add entries or Cancel depending on button clicked
                        if (addMasters)
                        {
                            StatusUpdate("Importing " + masterTapeName + " Entries");
                            DataBaseControls.AddMasterTapesFromFile(worker, importStream, ofd, masterTapeName);
                        }else
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

        //CHECK AND DELETE IF IT WORKS
        /// <summary>
        /// Adds all projects from a file.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>true if succeeded, false if failed</returns>
        private bool AddProjectsFromFile(BackgroundWorker worker, Stream importStream)
        {
            //List of Projectvalues to send to database
            List<ProjectValues> projectList = new List<ProjectValues>();
            ProjectValues values = new ProjectValues();


            if ((importStream = ofd.OpenFile()) != null)
            {
                
                try
                {
                    //items for 
                    string line;
                    string newLine;
                    char[] seperators = ",".ToCharArray();
                    

                    //streamReader to read csv file
                    StreamReader textReader = new StreamReader(importStream);
                    while ((line = textReader.ReadLine()) != null)
                    {
                        newLine = line.Replace(" - ", ",");
                        string[] lineArray = newLine.Split(seperators, 2);

                        //check to make sure there are 2 parts to each line
                        if (lineArray.Length > 1)
                        {
                            lineArray[0] = lineArray[0].Trim();
                            lineArray[1] = lineArray[1].Replace(',', '-').Trim();
                            values = new ProjectValues(lineArray[0], lineArray[1]);
                            projectList.Add(values);
                        }
                        else
                        {
                            //only one part, it will not add this value to database
                        }
                    }


                }
                catch (Exception error)
                {
                    LogFile(error.Message);
                }
            }

            int counter = 0;
            int progressCounter = 0;
            float queryCounter = 100.0f / projectList.Count;
            float progress = 0.0f;

            try
            {
                SQLiteConnection projectConnection = new SQLiteConnection(connect);
                projectConnection.Open();

                foreach (ProjectValues value in projectList)
                {
                    string query = "insert into Projects (project_id, project_name) values (@projectID, @projectName)";
                    SQLiteCommand command = new SQLiteCommand(query, projectConnection);
                    command.Parameters.AddWithValue("@projectID", value.ProjectID);
                    command.Parameters.AddWithValue("@projectName", value.Projectname);


                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Success
                        counter++;
                        progressCounter++;
                        if ((progressCounter * queryCounter) >= 1)
                        {
                            progressCounter = 0;
                            progress += queryCounter;
                            worker.ReportProgress(Convert.ToInt32(progress));
                            Console.WriteLine("Added: " + value.ProjectID);
                        }
                    }
                    else
                    {
                        //Failure
                    }
                }

                if (counter > 0)
                {
                    Console.WriteLine(counter + " items added to database");
                    LogFile(counter + " projects added to database");
                    projectConnection.Close();
                    return true;
                }
                else
                {
                    Console.WriteLine("No projects added to database");
                    projectConnection.Close();
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                LogFile("Import Projects Error: " + e.Message);
                return false;
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
    }
}
