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
using TNG_Database.Values;

namespace TNG_Database
{
    public partial class MainForm : Form
    {
        //Form references to open each form
        public TNG_Database.SearchTapeForm searchTapeForm;
        public TNG_Database.PeopleForm peopleForm;
        public TNG_Database.MasterListForm masterListForm;
        public TNG_Database.TapeListForm tapeListForm;
        public TNG_Database.ProjectsForm projectsForm;
        public TNG_Database.MasterArchiveVideosForm masterArchiveForm;
        public TNG_Database.DeletedValuesForm deletedValuesForm;

        //the current form
        Form currentForm;

        //gets database name for db operations
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
            searchTapeForm = child;
            currentForm = searchTapeForm;

            //check to see if this is the first time the program has ran
            if (Properties.TNG_Settings.Default.FirstRun)
            {
                CreateSQLDatabase();
            }
        }

        #region Class Methods

        /// <summary>
        /// Creates the SQL database.
        /// </summary>
        private void CreateSQLDatabase()
        {
            //check to see if database file exists
            if (!File.Exists(@"database\TNG_TapeDatabase.sqlite"))
            {
                //Creates database if there is not one already
                DataBaseControls.CreateSQLiteDatabase();
            }
            else
            {
                //already a database file
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

        /// <summary>
        /// Imports projects from file.
        /// </summary>
        /// <param name="worker">The worker.</param>
        private void ImportProjects(BackgroundWorker worker)
        {
            Stream importStream = null;
            DataBaseControls.AddProjectsFromFile(worker, importStream, ofd);
        }

        /// <summary>
        /// Imports masters tapes from file.
        /// </summary>
        /// <param name="worker">The worker.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
        private void ImportMasters(BackgroundWorker worker, DoWorkEventArgs e)
        {
            Stream importStream = null;

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
            foreach (string mediaValue in cameraValues)
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
                switch (GetExtensionOfFile(ofd))
                {
                    case "csv":
                        StatusUpdate("Importing " + masterTapeName + " Entries");
                        DataBaseControls.AddMasterTapesFromFile(worker, importStream, ofd, masterTapeName, commonMethod.GetCameraNumber(cameraMasterName));
                        break;
                    case "txt":
                        ofd.FileName = @"" + TempConvertToCSV(ofd);
                        StatusUpdate("Importing " + masterTapeName + " Entries");
                        DataBaseControls.AddMasterTapesFromFile(worker, importStream, ofd, masterTapeName, commonMethod.GetCameraNumber(cameraMasterName),true);
                        break;
                    default:
                        Console.WriteLine("File was not a txt or a csv");
                        break;
                }
                
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
        }

        //
        private void ImportTapes(BackgroundWorker worker)
        {
            Stream importStream = null;
            DataBaseControls.AddTapesFromFile(worker, importStream, ofd);
        }

        /// <summary>
        /// Gets the extension of file without the period.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private string GetExtensionOfFile(OpenFileDialog file)
        {
            //extension string to return
            string extension = "";

            if(file != null)
            {
                extension = Path.GetExtension(file.FileName).ToString().Replace(".", "");
            }

            return extension;
        }

        /// <summary>
        /// Convert to temporary csv file
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        private string TempConvertToCSV(OpenFileDialog file)
        {
            //string of the filename
            string filename = "";

            Stream importStream = null;

            //Convert to csv
            if ((importStream = file.OpenFile()) != null)
            {
                try
                {
                    //items for 
                    filename = @"tmp\" + Path.GetFileNameWithoutExtension(file.FileName) + "_tmp.csv";
                    string line;
                    string newLine;
                    string finalLine = "";
                    List<string> lineList = new List<string>();
                    char[] seperators = ",".ToCharArray();

                    //check to see if file exists, if it does clear it
                    if (!File.Exists(@"" + filename))
                    {
                        //Log file did not exist
                        Directory.CreateDirectory(@"tmp");
                        File.Create(@"" + filename).Close();
                        Console.WriteLine("File doesn't exist");
                    }
                    else
                    {
                        //Clear File
                        File.WriteAllText(@"" + filename, string.Empty);
                        Console.WriteLine("File Cleared");
                    }
                    
                    //streamReader to read csv file
                    StreamReader textReader = new StreamReader(importStream);
                    while ((line = textReader.ReadLine()) != null)
                    {
                        finalLine = "";
                        lineList.Clear();
                        newLine = line.Trim().Replace("\t", ",").Replace(",,", ",").Replace(",,,", ",");
                        while (newLine[newLine.Length - 1].Equals(","))
                        {
                            newLine = newLine.Remove(newLine.Length - 1, 1);
                        }

                        string[] lineArray = newLine.Split(seperators, 15);

                        foreach (string value in lineArray)
                        {
                            lineList.Add(value.Trim());
                        }

                        finalLine = string.Join(",", lineList);

                        
                        //Write to tmp file
                        using (StreamWriter csvWriter = File.AppendText(@"" + filename))
                        {
                            //Log Format
                            csvWriter.WriteLine("{0}", finalLine);

                            csvWriter.Close();
                        }
                    }


                }
                catch (Exception error)
                {
                    MainForm.LogFile(error.Message);
                }
            }

            return filename;
        }

        /// <summary>
        /// Converts the text file to a CSV file.
        /// </summary>
        /// <param name="worker">The worker.</param>
        /// <param name="file">The file.</param>
        private void ConvertTextToCSVFile(BackgroundWorker worker, OpenFileDialog file)
        {
            Stream importStream = null;

            if ((importStream = file.OpenFile()) != null)
            {
                try
                {
                    //items for 
                    string line;
                    string newLine;
                    string finalLine = "";
                    List<string> lineList = new List<string>();
                    char[] seperators = ",".ToCharArray();


                    //streamReader to read csv file
                    StreamReader textReader = new StreamReader(importStream);
                    while ((line = textReader.ReadLine()) != null)
                    {
                        finalLine = "";
                        lineList.Clear();
                        newLine = line.Trim().Replace("\t", ",").Replace(",,", ",").Replace(",,,", ",");
                        while (newLine[newLine.Length - 1].Equals(","))
                        {
                            newLine = newLine.Remove(newLine.Length - 1, 1);
                        }

                        string[] lineArray = newLine.Split(seperators, 15);

                        foreach (string value in lineArray)
                        {
                            lineList.Add(value.Trim());
                        }

                        finalLine = string.Join(",", lineList);

                        if (!File.Exists(@"outputs\" + Path.GetFileNameWithoutExtension(file.FileName) + "_Fixed.csv"))
                        {
                            //Log file did not exist
                            Directory.CreateDirectory(@"outputs");
                            Console.WriteLine("Log files does not exist");
                            File.Create(@"outputs\" + Path.GetFileNameWithoutExtension(file.FileName) + "_Fixed.csv").Close();
                        }

                        //Write to log file
                        using (StreamWriter csvWriter = File.AppendText(@"outputs\" + Path.GetFileNameWithoutExtension(file.FileName) + "_Fixed.csv"))
                        {
                            //Log Format
                            csvWriter.WriteLine("{0}", finalLine);

                            csvWriter.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MainForm.LogFile(error.Message);
                }
            }
            //Opens file browser to folder of outputted file
            System.Diagnostics.Process.Start(@"outputs");
        }

        #endregion

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

        //Open projects data form menu
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

        //Open Archive videos data form menu
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

        //Open Deleted Database data form
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
            

            switch (e.Argument.ToString())
            {
                case "projects":
                    //Import Projects
                    ImportProjects(worker);
                    break;
                case "masters":
                    //Import Master Tapes
                    ImportMasters(worker, e);
                    break;
                case "tapes":
                    //tapes file selected for import
                    ImportTapes(worker);
                    break;
                case "tocsv":
                    ConvertTextToCSVFile(worker, ofd);
                    break;
                case "check":
                    //TODO delete this before release
                    Stream importStream = null;

                    if ((importStream = ofd.OpenFile()) != null)
                    {
                        Console.WriteLine(Path.GetExtension(ofd.FileName).ToString().Replace(".",""));
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
                        backgroundWorker1.RunWorkerAsync(importMaster);
                    }
                }
            }
        }

        //Import a tapes csv file
        private void tapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string importTapes = "tapes";
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
                        backgroundWorker1.RunWorkerAsync(importTapes);
                    }
                }
            }
        }

        //Open Search Tape Database
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

        #region Cut, Copy and Paste commands
        //Copy Text from toolstrip selected
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check if control is active
            if(ActiveMdiChild.ActiveControl != null)
            {
                //set control to active control
                Control ctrl = ActiveMdiChild.ActiveControl;

                //check to see if control is null
                if (ctrl != null)
                {
                    //control is a textbox
                    if (ctrl is TextBox)
                    {
                        TextBox tb = (TextBox)ctrl;
                        Clipboard.SetText(tb.SelectedText);
                    }

                    //control is a combobox
                    if(ctrl is ComboBox)
                    {
                        ComboBox cb = (ComboBox)ctrl;
                        Clipboard.SetText(cb.Text);
                    }

                    //control is a numeric up down
                    if(ctrl is NumericUpDown)
                    {
                        NumericUpDown nud = (NumericUpDown)ctrl;
                        Clipboard.SetText(nud.Value.ToString());
                    }
                }
            }
            
        }

        //Paste Text from toolstrip selected
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check if control is active
            if (ActiveMdiChild.ActiveControl != null)
            {
                //set control to active control
                Control ctrl = ActiveMdiChild.ActiveControl;

                //check to see if control is null
                if (ctrl != null)
                {
                    //control is a textbox
                    if (ctrl is TextBox)
                    {
                        TextBox tb = (TextBox)ctrl;
                        string paste = Clipboard.GetText();
                        tb.Text = tb.Text.Insert(tb.SelectionStart, paste);
                    }
                }
            }
        }

        //Cut Text from toolstrip selected
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //check if control is active
            if (ActiveMdiChild.ActiveControl != null)
            {
                //set control to active control
                Control ctrl = ActiveMdiChild.ActiveControl;

                //check to see if control is null
                if (ctrl != null)
                {
                    //control is a textbox
                    if (ctrl is TextBox)
                    {
                        TextBox tb = (TextBox)ctrl;
                        Clipboard.SetText(tb.SelectedText);
                        tb.SelectedText = string.Empty;
                    }
                }
            }
        }

        #endregion

        //Convert a txt to csv toolstrip click
        private void tocsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string to tell which button was clicked
            string importMaster = "tocsv";
            //open file dialog to ask user which file to convert
            ofd = new OpenFileDialog();
            ofd.InitialDirectory = Properties.TNG_Settings.Default.LastFolder;
            ofd.Filter = "text file (*.txt)|*.txt";
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
