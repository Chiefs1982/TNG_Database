using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TNG_Database
{
    class UpdateStatus
    {
        private static UpdateStatus instance;

        private static readonly object syncRoot = new object();

        private static List<UpdateStatusValues> statusList = null;

        //background worker for status bar update
        private static BackgroundWorker statusWorker = new BackgroundWorker();
        

        //Mainform reference
        static TNG_Database.MainForm homeForm;

        private UpdateStatus()
        {
            //status worker options
            statusWorker.WorkerReportsProgress = true;
            statusWorker.WorkerSupportsCancellation = true;

            //status worker methods
            statusWorker.DoWork += StatusWorker_DoWork;
            statusWorker.ProgressChanged += StatusWorker_ProgressChanged;
            statusWorker.RunWorkerCompleted += StatusWorker_RunWorkerCompleted;
        }

        public static UpdateStatus Instance()
        {
            if(instance == null)
            {
                lock (syncRoot)
                {
                    if(instance == null)
                    {
                        instance = new UpdateStatus();

                        //status worker options
                        statusWorker.WorkerReportsProgress = true;
                        statusWorker.WorkerSupportsCancellation = true;

                        //status worker methods
                        statusWorker.DoWork += StatusWorker_DoWork;
                        statusWorker.ProgressChanged += StatusWorker_ProgressChanged;
                        statusWorker.RunWorkerCompleted += StatusWorker_RunWorkerCompleted;
                    }
                }
            }
            
            return instance;
        }


        /// <summary>
        /// Enables outside methods to add strings to status bar update list
        /// </summary>
        /// <param name="update">The update.</param>
        /// <param name="mainForm">The main form.</param>
        public void UpdateStatusBar(string update, TNG_Database.MainForm mainForm, int time = 2000)
        {
            //mainForm.applicationStatusLabel.Text = update;
            homeForm = mainForm;

            //set values to be added to list
            UpdateStatusValues statusValues = new UpdateStatusValues(update, time);

            //if list  is null create new list
            if(statusList == null)
            {
                statusList = new List<UpdateStatusValues>();
            }
            
            //add string to list
            statusList.Add(statusValues);

            //start background worker thread
            if (statusWorker.IsBusy != true)
            {
                statusWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Updates the progress bar.
        /// </summary>
        /// <param name="add">The add.</param>
        /// <param name="mainForm">The main form.</param>
        public void UpdateProgressBar(int add, TNG_Database.MainForm mainForm)
        {
            mainForm.mainFormProgressBar.Increment(add);
        }

        /// <summary>
        /// Updates the status bar, static method.
        /// </summary>
        /// <param name="update">The update.</param>
        private static void UpdateStatusBarStatic(string update)
        {
            homeForm.UpdateStatusBarBottom(update);
        }


        #region Worker Functions

        //does the background work
        private static void StatusWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //while list is greater than 0 go through and update status
            while (statusList.Count > 0)
            {
                //update status bar using static method
                UpdateStatusBarStatic(statusList[0].UpdateText);
                //sleep for designated amount of time
                Thread.Sleep(statusList[0].UpdateTime);
                //remove first item in list
                statusList.RemoveAt(0);
            }
        }

        //worker progress changed
        private static void StatusWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
        
        //worker completed
        private static void StatusWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                //cancelled

            }
            else if (e.Error != null)
            {
                //error
                MainForm.LogFile(e.Error.Message);
            }
            else
            {
                //Success
                Debug.WriteLine("Update success and finished");
                statusList.Clear();
            }
        }


        #endregion

        class UpdateStatusValues
        {
            private string updateText = "";
            private int updateTime = 2000;

            public UpdateStatusValues(string update_text, int update_time)
            {
                updateText = update_text;
                if (!update_time.Equals(updateTime))
                {
                    updateTime = update_time;
                }
            }

            public string UpdateText
            {
                get { return updateText; }
            }

            public int UpdateTime
            {
                get { return updateTime; }
            }
        }
        
    }
}
