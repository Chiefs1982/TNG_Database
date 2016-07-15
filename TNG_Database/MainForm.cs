﻿using System;
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
                //put all create strings into string array
                string[] allCreates = { createTapeTable, createMasterTable, createPeopleTable, createProjectsTable };
                
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
                Console.WriteLine("Database does exists already");
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
            }
        }

        private void StatusUpdate(string status)
        {
            applicationStatusLabel.Text = status;
        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            TapeDatabaseValues tbv = new TapeDatabaseValues("Teacher Service Agreement", "1", "16012", "Teacher Service Agreement", 1, "teacher, service, agreement, schools, k12", "DATE", "Master 52", "Aaron Primmer");
            TapeDatabaseValues tbv2 = new TapeDatabaseValues("Teacher Service Agreement", "1", "16015", "Teacher Service Agreement", 1, "teacher, service, agreement, schools, k12", "DATE", "Master 52", "Aaron Primmer");
            //string add_name = "Brett snyder";
            string add_name = "Master 51";
            //string editToName = "Brett Snyder";
            string editToName = "Master 52";
            AddToDatabase addDB = new AddToDatabase();
            if(addDB.UpdateTapeDatabase(tbv2, 3, "16012")){
                Console.WriteLine(add_name + " was updated from Tape DB");
            }else
            {
                Console.WriteLine(add_name + " was not updated from Tape DB");
            }
            
        }
        */

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

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "comma seperated files (*.csv)|*.csv";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.OpenFile() != null)
                {
                    if (backgroundWorker1.IsBusy != true)
                    {
                        backgroundWorker1.RunWorkerAsync();
                    }
                }
            }

                
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Stream importStream = null;
            BackgroundWorker worker = sender as BackgroundWorker;
            if((importStream = ofd.OpenFile()) != null)
            {
                try
                {
                    //items for 
                    string line;
                    string newLine;
                    char[] seperators = ",".ToCharArray();
                    //List of Projectvalues to send to database
                    List<ProjectValues> projectList = new List<ProjectValues>();
                    ProjectValues values = new ProjectValues();

                    //streamReader to read csv file
                    StreamReader textReader = new StreamReader(importStream);
                    while ((line = textReader.ReadLine()) != null)
                    {
                        //newLine = line.Replace('-', ',').Replace(" , ", ",");
                        string[] lineArray = line.Split(seperators, 2);

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

                    //List must have at least one entry in it
                    if (projectList.Count > 0)
                    {
                        //create access point to addToDatabase
                        AddToDatabase addDB = new AddToDatabase();
                        //Async task to add projects
                        Task t = Task.Run(() => {
                            if (AddProjectsFromFile(projectList, worker))
                            {
                                Console.WriteLine("Success");
                            }
                            else
                            {
                                Console.WriteLine("Failed");
                            }
                        });
                        t.Wait();
                    }
                }
                catch (Exception error)
                {
                    LogFile(error.Message);
                }
            }

            
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage > 100)
            {
                mainFormProgressBar.Value = 100;
            }else
            {
                mainFormProgressBar.PerformStep();
            }
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                UpdateStatusBarBottom("Canceled!");
            }
            else if (e.Error != null)
            {
                LogFile(e.Error.Message);
            }
            else
            {
                UpdateStatusBarBottom("Done!");
            }
        }

        /// <summary>
        /// Adds all projects from a file.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>true if succeeded, false if failed</returns>
        private bool AddProjectsFromFile(List<ProjectValues> values, BackgroundWorker worker)
        {

            int counter = 0;
            int progressCounter = 0;
            int queryCounter = values.Count / 100;
            int progress = 0;
            SQLiteConnection projectConnection = new SQLiteConnection(connect);
            projectConnection.Open();

            foreach (ProjectValues value in values)
            {
                string query = "insert into Projects (project_id, project_name) values (@projectID, @projectName)";
                SQLiteCommand command = new SQLiteCommand(query, projectConnection);
                command.Parameters.AddWithValue("@projectID", value.ProjectID);
                command.Parameters.AddWithValue("@projectName", value.Projectname);

                try
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Success
                        counter++;
                        progressCounter++;
                        if (progressCounter >= queryCounter)
                        {
                            progressCounter = 0;
                            progress++;
                            worker.ReportProgress(progress);
                            Console.WriteLine("Added: " + value.ProjectID);
                        }

                    }
                    else
                    {
                        //Failure
                    }
                }
                catch (SQLiteException e)
                {
                    LogFile("Import Projects Error: " + e.Message);
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
    }
}
