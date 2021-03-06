﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using TNG_Database.Values;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace TNG_Database
{
    class DataBaseControls
    {
        //TapeDatabaseDB connection string
        private static string database = "Data Source=database/TNG_TapeDatabase.sqlite;Version=3;";

        //Reference to CommonMethods
        static CommonMethods commonMethod = CommonMethods.Instance();
        static UpdateStatus updateStatus = UpdateStatus.Instance();
        static ComputerInfo computerInfo = ComputerInfo.Instance();

        //allow access to the full database path
        public static string FullDatabaseName
        {
            get { return @"database/TNG_TapeDatabase.sqlite"; }
        }

        private static string[] tngTables = {
            "TapeDatabase","MasterList","People","DeleteTapeDatabase","DeleteMasterList",
            "DeletePeople","DeleteMasterArchiveVideos","MasterArchiveVideos","Projects",
            "DeleteProjects","ComputerInfo"
        };

        public static string[] TNGTables
        {
            get { return tngTables; }
        }
        
        public static string Database
        {
            get { return database; }
        }

        private static void UpdateProgess(float progress, TNG_Database.MainForm mainForm)
        {
            updateStatus.UpdateProgressBar(Convert.ToInt32(progress), mainForm);
        }

        /// <summary>
        /// Close connection and command variables, and collects Garbage
        /// </summary>
        /// <param name="command">Active command of SQLiteCommand</param>
        /// <param name="connection">Active connection of SQLiteConnection</param>
        private static void CloseConnections(SQLiteCommand command, SQLiteConnection connection)
        {
            if (command != null) { command.Dispose(); }
            if (connection != null) { connection.Close(); connection.Dispose(); }
            GC.Collect();
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="tmp">if set to <c>true</c> [temporary].</param>
        /// <param name="ofd">The ofd.</param>
        private static void DeleteFile(bool tmp,OpenFileDialog ofd, Stream fileStream)
        {
            try {
                fileStream.Close();
                if (tmp)
                {
                    
                    if (File.Exists(Path.GetFullPath(ofd.FileName)))
                    {
                        File.Delete(Path.GetFullPath(ofd.FileName));
                        Debug.WriteLine("File Deleted");
                    }
                }
            }catch(Exception e)
            {
                MainForm.LogFile("Error Deleting File: " + e.Message);
            }
        }

        /// <summary>
        /// Updates the tapes with master upon import.
        /// </summary>
        /// <param name="projectID">The project identifier.</param>
        /// <param name="masterList">The master list.</param>
        /// <param name="connect">The connect.</param>
        private static void UpdateTapesWithMaster(string projectID, string masterList, SQLiteConnection connect)
        {
            Hashtable list = new Hashtable();

            try
            {
                SQLiteCommand command = new SQLiteCommand(connect);

                command.CommandText = "select master_archive from TapeDatabase where project_id = @pid";
                command.Parameters.AddWithValue("@pid", projectID);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                if (!reader["master_archive"].ToString().Equals(masterList))
                                {
                                    list.Add(projectID, reader["master_archive"].ToString() + "," + masterList);
                                }
                                
                            }
                            catch { }
                        }
                    }
                }
                
                foreach(DictionaryEntry value in list)
                {
                    command.Parameters.Clear();
                    command.CommandText = "update TapeDatabase set master_archive = @m_arch where project_id = @pid";
                    command.Parameters.AddWithValue("@m_arch", value.Value);
                    command.Parameters.AddWithValue("@pid", value.Key);

                    command.ExecuteNonQuery();
                }
            }
            catch { }

            

            
        }

        /// <summary>
        /// Gets the master for tapes.
        /// </summary>
        /// <param name="projectID">The project identifier.</param>
        /// <param name="connect">The connect.</param>
        /// <returns></returns>
        public static string GetMasterForTapes(string projectID, SQLiteConnection connect)
        {
            List<string> list = new List<string>();
            bool toClose = false;

            try
            {
                if(connect == null)
                {
                    connect = new SQLiteConnection(database);
                    connect.Open();
                    toClose = true;
                }
                SQLiteCommand command = new SQLiteCommand(connect);

                command.CommandText = "select master_tape from MasterArchiveVideos where project_id = @pid";
                command.Parameters.AddWithValue("@pid", projectID);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (!reader["master_tape"].ToString().Equals(string.Empty))
                            {
                                list.Add(reader["master_tape"].ToString());
                            }
                        }
                    }
                }
                if (toClose)
                {
                    CloseConnections(command, connect);
                }
                
            }
            catch(SQLiteException e) { Debug.WriteLine("Error getting master from db: " + e.Message); }
            
            return String.Join(",", list);
        }

        /// <summary>
        /// Gets the project name using the number in the database.
        /// </summary>
        /// <param name="projectID">The project identifier.</param>
        /// <returns></returns>
        public static string GetProjectNameFromNumber(string projectID)
        {
            string projectName = "";

            try
            {
                SQLiteConnection projectConnection = new SQLiteConnection(database);
                projectConnection.Open();

                SQLiteCommand command = new SQLiteCommand(projectConnection);

                command.CommandText = "select project_name from Projects where project_id = @p_id limit 1";
                command.Parameters.AddWithValue("@p_id", projectID);

                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //data returned
                        while (reader.Read())
                        {
                            projectName = reader["project_name"].ToString();
                        }
                    }
                    else
                    {
                        //nothing returned
                        projectName = null;
                    }
                }
                
                CloseConnections(command, projectConnection);

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }

            return projectName;
        }

        /// <summary>
        /// Gets all users in database
        /// </summary>
        /// <returns>A List of strings of all users</returns>
        public static List<string> GetAllUsers()
        {
            List<string> userList = new List<string>();

            try
            {
                //start new sqlite connection
                SQLiteConnection populateConnection = new SQLiteConnection(database);
                populateConnection.Open();

                //Get all users and id and populate a new User class per user
                string query = "select person_name from People";
                SQLiteCommand queryCommand = new SQLiteCommand(query, populateConnection);

                using(SQLiteDataReader reader = queryCommand.ExecuteReader())
                {
                    //check to make sure query returned a string
                    if (reader.HasRows)
                    {
                        //create list with the number of entries
                        userList = new List<string>(reader.StepCount);

                        //go through each entry and add to listview
                        while (reader.Read())
                        {
                            //add user to list
                            userList.Add(reader["person_name"].ToString());
                        }
                    }
                    else
                    {
                        //start list with one entry
                        userList = new List<string>(1);
                        userList.Add("No Users");
                    }

                    CloseConnections(queryCommand, populateConnection);
                }
            }catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
            
            return userList;
        }

        //---------------------------------------
        /// <summary>
        /// Gets all items in the MasterList database
        /// </summary>
        /// <returns>A List of MasterListValues from the database</returns>
        public static List<MasterListValues> GetAllMasterListItems()
        {
            List<MasterListValues> masterList = new List<MasterListValues>();

            try
            {
                //start new sqlite connection
                SQLiteConnection populateConnection = new SQLiteConnection(database);
                populateConnection.Open();

                //Query the database for all items in the Master list
                string query = "select * from MasterList order by master_archive asc";
                SQLiteCommand command = new SQLiteCommand(query, populateConnection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //check to see if database has rows
                    if (reader.HasRows)
                    {
                        //Set list size as the size of the items returned
                        masterList = new List<MasterListValues>(reader.StepCount);

                        //Iterate through reader
                        while (reader.Read())
                        {
                            masterList.Add(new MasterListValues(reader["master_archive"].ToString(), Convert.ToInt32(reader["master_media"]), Convert.ToInt32(reader["id"])));
                        }
                    }
                    else
                    {
                        masterList = new List<MasterListValues>(1);
                        masterList.Add(new MasterListValues("Nothing in database", 2));
                    }

                    CloseConnections(command, populateConnection);
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }

            return masterList;
        }

        /// <summary>
        /// Gets all project items.
        /// </summary>
        /// <returns></returns>
        public static List<ProjectValues> GetAllProjectItems()
        {
            //List item to return
            List<ProjectValues> values = new List<ProjectValues>();
            ProjectValues projectValue;

            try
            {
                //start SQLite Connection
                SQLiteConnection projectsConnection = new SQLiteConnection(database);
                projectsConnection.Open();
                SQLiteCommand command = new SQLiteCommand(projectsConnection);

                command.CommandText = "select * from Projects order by project_id asc";

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            projectValue = new ProjectValues(reader["project_id"].ToString(), reader["project_name"].ToString(), Convert.ToInt32(reader["id"]));
                            values.Add(projectValue);

                        }
                        CloseConnections(command, projectsConnection);
                    }
                    else
                    {
                        CloseConnections(command, projectsConnection);
                    }
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
            
            return values;
        }
        //-------------------------------------------

        /// <summary>
        /// Gets all tape values.
        /// </summary>
        /// <returns></returns>
        public List<TapeDatabaseValues> GetAllTapeValues()
        {
            List<TapeDatabaseValues> tapeList = new List<TapeDatabaseValues>();

            try
            {
                //start new sqlite connection
                SQLiteConnection populateConnection = new SQLiteConnection(database);
                populateConnection.Open();

                //Query the database for all items in the Master list
                string query = "select * from TapeDatabase order by project_id asc";
                SQLiteCommand command = new SQLiteCommand(query, populateConnection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //check to see if database has rows
                    if (reader.HasRows)
                    {
                        //Set list size as the size of the items returned
                        tapeList = new List<TapeDatabaseValues>(reader.StepCount);

                        //Iterate through reader
                        while (reader.Read())
                        {
                            tapeList.Add(new TapeDatabaseValues(reader["tape_name"].ToString(), reader["tape_number"].ToString(), reader["project_id"].ToString(), reader["project_name"].ToString(), Convert.ToInt32(reader["camera"]), reader["tape_tags"].ToString(), reader["date_shot"].ToString(), reader["master_archive"].ToString(), reader["person_entered"].ToString(), Convert.ToInt32(reader["id"])));
                        }
                    }
                    else
                    {
                        tapeList = new List<TapeDatabaseValues>(1);
                        tapeList.Add(new TapeDatabaseValues());
                    }

                    CloseConnections(command, populateConnection);
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
            
            return tapeList;
        }

        /// <summary>
        /// Gets all archive video values.
        /// </summary>
        /// <returns></returns>
        public static List<MasterTapeValues> GetAllArchiveVideoValues()
        {
            //declare values
            List<MasterTapeValues> mList = new List<MasterTapeValues>();
            MasterTapeValues value;

            try
            {
                //establish connection
                SQLiteConnection archiveConnection = new SQLiteConnection(database);
                archiveConnection.Open();
                //new command
                SQLiteCommand command = new SQLiteCommand(archiveConnection);
                //add query
                command.CommandText = "select * from MasterArchiveVideos order by project_id asc";

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //data has been returned
                        while (reader.Read())
                        {
                            value = new MasterTapeValues(reader["project_id"].ToString(), reader["video_name"].ToString(), reader["master_tape"].ToString(), reader["clip_number"].ToString(), Convert.ToInt32(reader["id"]));
                            mList.Add(value);
                        }
                        CloseConnections(command, archiveConnection);
                    }
                    else
                    {
                        //returned nothing
                        CloseConnections(command, archiveConnection);
                    }
                }
            }catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }

            return mList;
        }

        public static List<MasterArchiveVideoValues> GetAllMasterListValues(string masterList)
        {
            List<MasterArchiveVideoValues> values = new List<MasterArchiveVideoValues>();

            try
            {
                //Open connection to DB
                SQLiteConnection connect = new SQLiteConnection(database);
                connect.Open();
                SQLiteCommand command = new SQLiteCommand(connect);

                //Set up command to query db
                command.CommandText = "select * from MasterArchiveVideos where master_tape = @m_tape";
                command.Parameters.AddWithValue("@m_tape", masterList);

                //query the db
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //add values to list to return
                            MasterArchiveVideoValues videos = new MasterArchiveVideoValues(reader["project_id"].ToString(),reader["video_name"].ToString(),reader["master_tape"].ToString(),reader["clip_number"].ToString(),Convert.ToInt32(reader["id"]));
                            values.Add(videos);
                        }
                    }
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }

            return values;
        }

        //----------------------------------------------        
        /// <summary>
        /// Gets the person list for people dropdowns.
        /// </summary>
        /// <returns></returns>
        public static string[] GetPersonListForDropdown()
        {
            List<string> personList = new List<string>();

            try
            {
                //start new sqlite connection
                SQLiteConnection dropdownConnection = new SQLiteConnection(database);
                dropdownConnection.Open();

                string query = "select person_name from People order by person_name asc";
                SQLiteCommand command = new SQLiteCommand(query, dropdownConnection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        personList = new List<string>(reader.StepCount);

                        while (reader.Read())
                        {
                            personList.Add(reader["person_name"].ToString());
                        }
                    }
                    else
                    {
                        personList = new List<string>();
                        personList.AddRange(new string[] { "Brendan Burghardt", "Brett Snyder", "Aaron Primmer", "Dan Schultz", "Jerome Rigoroso", "Kelcy Erbele" });
                    }
                    
                    CloseConnections(command, dropdownConnection);
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
            
            return personList.ToArray();
        }

        //----------------------------------------------        
        /// <summary>
        /// Gets the master list for master tape dropdowns.
        /// </summary>
        /// <returns></returns>
        public static string[] GetMasterListForDropdown()
        {
            List<string> masterList;

            //start new sqlite connection
            SQLiteConnection dropdownConnection = new SQLiteConnection(database);
            dropdownConnection.Open();

            string query = "select master_archive from MasterList order by master_archive asc";
            SQLiteCommand command = new SQLiteCommand(query, dropdownConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                masterList = new List<string>(reader.StepCount);

                //Add blank MasterList
                masterList.Add("");

                while (reader.Read())
                {
                    masterList.Add(reader["master_archive"].ToString());
                }
            }else
            {
                masterList = new List<string>(1);
                masterList.Add("Not Loaded");
            }

            CloseConnections(command, dropdownConnection);
            return masterList.ToArray();
        }

        public List<SearchValues> SearchAllDB(string[] input)
        {
            //declare values
            List<SearchValues> tapeDBValues = new List<SearchValues>();
            string preQuery = "";
            string query = "";

            try
            {
                //start new sqlite connection
                SQLiteConnection searchConnection = new SQLiteConnection(database);
                searchConnection.Open();
                SQLiteCommand command = new SQLiteCommand(searchConnection);

                //start of query pieces for tape Database
                preQuery = "select * from TapeDatabase";

                //add query based on number of entries
                if (input.Length > 0)
                {
                    //iterate over all entries
                    for (int i = 0; i < input.Length; i++)
                    {
                        //set up value for each input
                        string term = "@input" + i;
                        string value = "'%" + input[i].ToLower() + "%'";
                        if (i == 0)
                        {
                            preQuery += " where";
                        }

                        if (i > 0)
                        {
                            preQuery += " or";
                        }

                        //set up the regex part of the query
                        preQuery += String.Format(" project_id like {0} or project_name like {0} or tape_name like {0} or tape_tags like {0} or date_shot like {0} or master_archive like {0}", value);
                        Debug.WriteLine(input[i].ToLower());
                    }
                }
                preQuery += " order by project_id asc";
                Debug.WriteLine(preQuery);

                //Set assembled query to final query
                query = preQuery;
                command.CommandText = query;
                Debug.WriteLine("Command Text: " + command.CommandText);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //If there are return values then parse them and display them
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SearchValues dbData = new SearchValues();
                            dbData.TapeName = reader["tape_name"].ToString();
                            dbData.TapeNumber = reader["tape_number"].ToString();
                            dbData.ProjectID = reader["project_id"].ToString();
                            dbData.ProjectName = reader["project_name"].ToString();
                            dbData.Camera = commonMethod.GetCameraName(Convert.ToInt32(reader["camera"]));
                            dbData.TapeTags = reader["tape_tags"].ToString();
                            dbData.DateShot = reader["date_shot"].ToString();
                            dbData.MasterArchive = reader["master_archive"].ToString();
                            dbData.Person = reader["person_entered"].ToString();
                            dbData.ID = Convert.ToInt32(reader["id"]);
                            dbData.FilterName = "tapes";

                            tapeDBValues.Add(dbData);
                        }
                    }
                }

                //start of query pieces for Projects
                preQuery = "select * from Projects";

                //add query based on number of entries
                if (input.Length > 0)
                {
                    //iterate over all entries
                    for (int i = 0; i < input.Length; i++)
                    {
                        //set up value for each input
                        string value = "'%" + input[i].ToLower() + "%'";
                        if (i == 0)
                        {
                            preQuery += " where";
                        }

                        if (i > 0)
                        {
                            preQuery += " or";
                        }

                        //set up the regex part of the query
                        preQuery += String.Format(" project_id like {0} or project_name like {0}", value);
                        Debug.WriteLine(input[i].ToLower());
                    }
                }
                preQuery += " order by project_id asc";
                Debug.WriteLine(preQuery);

                //Set assembled query to final query
                query = preQuery;
                command.CommandText = query;
                Debug.WriteLine("Command Text: " + command.CommandText);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //If there are return values then parse them and display them
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Add values to returned list
                            SearchValues dbData = new SearchValues();
                            dbData.ProjectID = reader["project_id"].ToString();
                            dbData.ProjectName = reader["project_name"].ToString();
                            dbData.ID = Convert.ToInt32(reader["id"]);
                            dbData.FilterName = "projects";

                            tapeDBValues.Add(dbData);
                        }
                    }
                }

                //start of query pieces for Master Archive Videos
                preQuery = "select * from MasterArchiveVideos";

                //add query based on number of entries
                if (input.Length > 0)
                {
                    //iterate over all entries
                    for (int i = 0; i < input.Length; i++)
                    {
                        //set up value for each input
                        string value = "'%" + input[i].ToLower() + "%'";
                        if (i == 0)
                        {
                            preQuery += " where";
                        }

                        if (i > 0)
                        {
                            preQuery += " or";
                        }

                        //set up the regex part of the query
                        preQuery += String.Format(" project_id like {0} or video_name like {0} or master_tape like {0}", value);
                        Debug.WriteLine(input[i].ToLower());
                    }
                }
                preQuery += " order by project_id asc";
                Debug.WriteLine(preQuery);

                //Set assembled query to final query
                query = preQuery;
                command.CommandText = query;
                Debug.WriteLine("Command Text: " + command.CommandText);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //If there are return values then parse them and display them
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //Add values to returned list
                            SearchValues dbData = new SearchValues();
                            dbData.ProjectID = reader["project_id"].ToString();
                            dbData.ProjectName = reader["video_name"].ToString();
                            dbData.MasterArchive = reader["master_tape"].ToString();
                            dbData.ClipNumber = reader["clip_number"].ToString();
                            dbData.ID = Convert.ToInt32(reader["id"]);
                            dbData.FilterName = "archive";

                            tapeDBValues.Add(dbData);
                        }
                    }
                }

                //close connection and return final list
                searchConnection.Close();
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
            
            return tapeDBValues;
        }

        /// <summary>
        /// Adds the master tapes from file.
        /// </summary>
        /// <param name="worker">Background worker</param>
        /// <param name="importStream">Import Stream for file</param>
        /// <param name="ofd">The file returned from OpenFileDialog</param>
        /// <returns></returns>
        public static bool AddMasterTapesFromFile(BackgroundWorker worker, Stream importStream, OpenFileDialog ofd, string masterArchive, int media, bool tmp = false)
        {
            //List of Projectvalues to send to database
            List<MasterTapeValues> projectList = new List<MasterTapeValues>();
            MasterTapeValues values = new MasterTapeValues();

            //open file if one was selected
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
                        //parse line and make a line array
                        newLine = Regex.Replace(line, "[\u2013\u2014]", "-");
                        string[] lineArray = newLine.Split(seperators, 3);

                        //check to make sure there are 3 parts to each line
                        if (lineArray.Length > 1)
                        {
                            lineArray[0] = lineArray[0].Trim();
                            lineArray[1] = lineArray[1].Replace(',', '-').Trim();
                            int clipNumber = Convert.ToInt32(lineArray[0]);
                            //CHANGE VALUE LATER
                            values = new MasterTapeValues(lineArray[1], lineArray[2], masterArchive, clipNumber.ToString("000"));
                            projectList.Add(values);
                        }
                        else
                        {
                            //only one part, it will not add this value to database
                        }
                    }


                }
                catch
                {
                    //Index may be out of range, but that is supposed to happen if entry already exists
                }
            }

            //counters for updating the progress bar
            int counter = 0;
            int progressCounter = 0;
            float queryCounter = 100.0f / projectList.Count;
            float progress = 0.0f;


            try
            {
                //connect to DB
                SQLiteConnection masterConnection = new SQLiteConnection(database);
                masterConnection.Open();
                
                //Insert new Archive media if it doesn't already exist
                string masterQuery = "insert or ignore into MasterList(master_archive, master_media) values (@m_archive, @m_media)";
                SQLiteCommand command = new SQLiteCommand(masterQuery, masterConnection);
                command.Parameters.AddWithValue("@m_archive", masterArchive);
                command.Parameters.AddWithValue("@m_media", media);

                if(command.ExecuteNonQuery() == 1)
                {
                    Debug.WriteLine("Master List insert Success");
                }else
                {
                    Debug.WriteLine("Master List insert Failed");
                }

                //iterate over each entry and insert it into the DB
                foreach (MasterTapeValues master in projectList)
                {
                    string query = "insert into MasterArchiveVideos (project_id, video_name, master_tape, clip_number) values (@projectID, @videoName, @masterTape, @clipNumber)";
                    command = new SQLiteCommand(query, masterConnection);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@projectID", master.ProjectID);
                    command.Parameters.AddWithValue("@videoName", master.VideoName);
                    command.Parameters.AddWithValue("@masterTape", master.MasterTape);
                    command.Parameters.AddWithValue("@clipNumber", master.ClipNumber);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Success
                        counter++;
                        progressCounter++;
                        if ((progressCounter * queryCounter) >= 1)
                        {
                            UpdateTapesWithMaster(master.ProjectID, master.MasterTape, masterConnection);

                            progress += (queryCounter * progressCounter);
                            progressCounter = 0;
                            //update the progess bar
                            worker.ReportProgress(Convert.ToInt32(progress));
                            Debug.WriteLine("Added: " + master.ProjectID);
                        }
                    }
                    else
                    {
                        //Failure
                    }
                }

                if (counter > 0)
                {
                    Debug.WriteLine(counter + " items added to database");
                    MainForm.LogFile(counter + " master archive(s) added to database");
                    masterConnection.Close();
                    DeleteFile(tmp, ofd, importStream);
                    return true;
                }
                else
                {
                    //No entries found
                    Debug.WriteLine("No master archive(s) added to database");
                    masterConnection.Close();
                    DeleteFile(tmp, ofd, importStream);
                    return false;
                }

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("Import Projects Error: " + e.Message);
                DeleteFile(tmp, ofd, importStream);
                return false;
            }
        }

        /// <summary>
        /// Adds all projects from a file.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>true if succeeded, false if failed</returns>
        public static bool AddProjectsFromFile(BackgroundWorker worker, Stream importStream, OpenFileDialog ofd)
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
                        newLine = line.Replace("-", ",");
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
                    MainForm.LogFile(error.Message);
                }
            }

            int counter = 0;
            int progressCounter = 0;
            float queryCounter = 100.0f / projectList.Count;
            float progress = 0.0f;

            try
            {
                SQLiteConnection projectConnection = new SQLiteConnection(database);
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
                            
                            progress += (queryCounter * progressCounter);
                            progressCounter = 0;
                            worker.ReportProgress(Convert.ToInt32(progress));
                            Debug.WriteLine("Added: " + value.ProjectID + ", Progress = " + progress);
                        }
                    }
                    else
                    {
                        //Failure
                    }
                }

                if (counter > 0)
                {
                    Debug.WriteLine(counter + " items added to database");
                    MainForm.LogFile(counter + " projects added to database");
                    projectConnection.Close();
                    return true;
                }
                else
                {
                    Debug.WriteLine("No projects added to database");
                    projectConnection.Close();
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("Import Projects Error: " + e.Message);
                return false;
            }


        }

        /// <summary>
        /// Adds the tapes from file.
        /// </summary>
        /// <param name="worker">The worker.</param>
        /// <param name="importStream">The import stream.</param>
        /// <param name="ofd">The ofd.</param>
        /// <returns>true if succeeded, false if failed</returns>
        public static bool AddTapesFromFile(BackgroundWorker worker, Stream importStream, OpenFileDialog ofd)
        {
            List<TapeDatabaseValues> tapeList = new List<TapeDatabaseValues>();
            TapeDatabaseValues tapeValues = new TapeDatabaseValues();

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
                        newLine = line.Replace("-", ",");
                        string[] lineArray = newLine.Split(seperators, 7);

                        //check to make sure there are 2 parts to each line
                        if (lineArray.Length > 1)
                        {
                            lineArray[0] = lineArray[0].Trim();
                            lineArray[1] = lineArray[1].Replace(',', '-').Trim();
                            tapeValues = new TapeDatabaseValues(lineArray[0], lineArray[1], lineArray[2], lineArray[3], commonMethod.GetCameraNumber(lineArray[4]), lineArray[6], commonMethod.ConvertDateFromDropdownForDB(Convert.ToDateTime(lineArray[5])), "", "Imported");
                            tapeList.Add(tapeValues);
                        }
                        else
                        {
                            //only one part, it will not add this value to database
                        }
                    }


                }
                catch (Exception error)
                {
                    MainForm.LogFile(error.Message);
                }
            }

            int counter = 0;
            int progressCounter = 0;
            float queryCounter = 100.0f / tapeList.Count;
            float progress = 0.0f;
            string projectName = "";

            try
            {
                SQLiteConnection projectConnection = new SQLiteConnection(database);
                projectConnection.Open();
                SQLiteCommand command = new SQLiteCommand(projectConnection);

                foreach (TapeDatabaseValues value in tapeList)
                {
                    //list for the master list query
                    List<string> masterList = new List<string>();
                    //query db for names of masterlist for a project
                    command.CommandText = "select master_tape from MasterArchiveVideos where project_id = @pid";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@pid", value.ProjectId);

                    //read results and add values if any to a comma seperated string
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                masterList.Add(reader["master_tape"].ToString());
                            }
                            value.MasterArchive = String.Join(",", masterList);
                        }
                    }

                    //query db for the project name
                    command.CommandText = "select project_name from Projects where project_id = @pid limit 1";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@pid", value.ProjectId);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                projectName = reader["project_name"].ToString();
                            }

                            //add value for addition based on if empty or not
                            if (!projectName.Equals(""))
                            {

                                value.ProjectName = projectName;
                            }
                        }
                    }

                    //add values into tape database table
                    command.Parameters.Clear();
                    command.CommandText = "insert into TapeDatabase (tape_name,tape_number,project_id, project_name,camera,tape_tags,date_shot,master_archive,person_entered) values (@tapeName,@tapeNumber,@projectID, @projectName,@camera,@tapeTags,@dateShot,@masterArchive,@person)";
                    command.Parameters.AddWithValue("@tapeName", value.TapeName);
                    command.Parameters.AddWithValue("@tapeNumber", value.TapeNumber);
                    command.Parameters.AddWithValue("@projectID", value.ProjectId);
                    command.Parameters.AddWithValue("@projectName", value.ProjectName);
                    command.Parameters.AddWithValue("@camera", value.Camera);
                    command.Parameters.AddWithValue("@tapeTags", value.TapeTags);
                    command.Parameters.AddWithValue("@dateShot", value.DateShot);
                    command.Parameters.AddWithValue("@masterArchive", value.MasterArchive);
                    command.Parameters.AddWithValue("@person", ComputerInfo.ComputerUser);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Success
                        counter++;
                        progressCounter++;
                        if ((progressCounter * queryCounter) >= 1)
                        {

                            progress += (queryCounter * progressCounter);
                            progressCounter = 0;
                            worker.ReportProgress(Convert.ToInt32(progress));
                            Debug.WriteLine("Added: " + value.ProjectId + ", Progress = " + progress);
                        }
                    }
                    else
                    {
                        //Failure
                    }

                }

                if (counter > 0)
                {
                    Debug.WriteLine(counter + " items added to database");
                    MainForm.LogFile(counter + " tapes added to database");
                    projectConnection.Close();
                    return true;
                }
                else
                {
                    Debug.WriteLine("No Tapes added to database");
                    projectConnection.Close();
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("Import Tapes Error: " + e.Message);
                return false;
            }
        }

        #region Get Deleted Values

        public static List<TapeDatabaseValues> GetAllDeletedTapeValues()
        {
            List<TapeDatabaseValues> tapeValues = new List<TapeDatabaseValues>();

            try
            {
                //open connection
                SQLiteConnection tapeConnection = new SQLiteConnection(database);
                tapeConnection.Open();
                SQLiteCommand command = new SQLiteCommand(tapeConnection);

                //Quuery for all reasults
                command.CommandText = "select * from DeleteTapeDatabase";
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //rows returned
                    while (reader.Read())
                    {
                        //Load List and return values
                        tapeValues.Add(new TapeDatabaseValues(reader["tape_name"].ToString(), reader["tape_number"].ToString(), reader["project_id"].ToString(), reader["project_name"].ToString(), Convert.ToInt32(reader["camera"]), reader["tape_tags"].ToString(), reader["date_shot"].ToString(), reader["master_archive"].ToString(), reader["person_entered"].ToString(), Convert.ToInt32(reader["id"])));
                    }
                }else
                {
                    //no rows returned
                    tapeValues = null;
                }

            }
            catch(SQLiteException e)
            {
                MainForm.LogFile("SQL Error: " + e.Message);
            }

            return tapeValues;
        }

        public static List<ProjectValues> GetAllDeletedProjectValues()
        {
            List<ProjectValues> projectValues = new List<ProjectValues>();

            try
            {
                //open connection
                SQLiteConnection tapeConnection = new SQLiteConnection(database);
                tapeConnection.Open();
                SQLiteCommand command = new SQLiteCommand(tapeConnection);

                //Quuery for all reasults
                command.CommandText = "select * from DeleteProjects";
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //rows returned
                    while (reader.Read())
                    {
                        //Load List and return values
                        projectValues.Add(new ProjectValues(reader["project_id"].ToString(), reader["project_name"].ToString(), Convert.ToInt32(reader["id"])));
                    }
                }
                else
                {
                    //no rows returned
                    projectValues = null;
                }

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQL Error: " + e.Message);
            }

            return projectValues;
        }

        public static List<PeopleValues> GetAllDeletedPeople()
        {
            List<PeopleValues> peopleValues = new List<PeopleValues>();

            try
            {
                //open connection
                SQLiteConnection peopleConnection = new SQLiteConnection(database);
                peopleConnection.Open();
                SQLiteCommand command = new SQLiteCommand(peopleConnection);

                //Quuery for all reasults
                command.CommandText = "select * from DeletePeople";
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //rows returned
                    while (reader.Read())
                    {
                        //Load List and return values
                        peopleValues.Add(new PeopleValues(reader["person_name"].ToString(), Convert.ToInt32(reader["id"])));
                    }
                }
                else
                {
                    //no rows returned
                    peopleValues = null;
                }

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQL Error: " + e.Message);
            }

            return peopleValues;
        }

        public static List<MasterListValues> GetAllDeletedMasterListValues()
        {
            List<MasterListValues> masterListValues = new List<MasterListValues>();

            try
            {
                //open connection
                SQLiteConnection peopleConnection = new SQLiteConnection(database);
                peopleConnection.Open();
                SQLiteCommand command = new SQLiteCommand(peopleConnection);

                //Quuery for all reasults
                command.CommandText = "select * from DeleteMasterList";
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //rows returned
                    while (reader.Read())
                    {
                        //Load List and return values
                        masterListValues.Add(new MasterListValues(reader["master_archive"].ToString(), Convert.ToInt32(reader["master_media"]), Convert.ToInt32(reader["id"])));
                    }
                }
                else
                {
                    //no rows returned
                    masterListValues = null;
                }

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQL Error: " + e.Message);
            }

            return masterListValues;
        }

        public static List<MasterArchiveVideoValues> GetAllDeletedMasterArchiveValues()
        {
            List<MasterArchiveVideoValues> masterArchiveValues = new List<MasterArchiveVideoValues>();

            try
            {
                //open connection
                SQLiteConnection peopleConnection = new SQLiteConnection(database);
                peopleConnection.Open();
                SQLiteCommand command = new SQLiteCommand(peopleConnection);

                //Quuery for all reasults
                command.CommandText = "select * from DeleteMasterArchiveVideos";
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //rows returned
                    while (reader.Read())
                    {
                        //Load List and return values
                        masterArchiveValues.Add(new MasterArchiveVideoValues(reader["project_id"].ToString(), reader["video_name"].ToString(), reader["master_tape"].ToString(), reader["clip_number"].ToString(), Convert.ToInt32(reader["id"])));
                    }
                }
                else
                {
                    //no rows returned
                    masterArchiveValues = null;
                }

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQL Error: " + e.Message);
            }

            return masterArchiveValues;
        }

        #endregion

        public static void CreateSQLiteDatabase()
        {
            try
            {
                //create directory and file if they do not exist
                Directory.CreateDirectory(@"database");
                SQLiteConnection.CreateFile("database/TNG_TapeDatabase.sqlite");
            }
            catch (Exception e)
            {
                MainForm.LogFile(e.Message);
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
                using (SQLiteConnection createConnection = new SQLiteConnection(Database))
                {
                    createConnection.Open();

                    //iterate over all tables and add to database
                    foreach (string query in allCreates)
                    {
                        SQLiteCommand command = new SQLiteCommand(query, createConnection);
                        if (command.ExecuteNonQuery() == 0)
                        {
                            //success
                            MainForm.LogFile("Database Default Table Created");
                        }
                        else
                        {
                            //failed
                            MainForm.LogFile("Database Default Table Failed to be Created");
                        }
                    }
                    createConnection.Close();
                }

            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("Create Database Error: " + e.Message);
            }
        }

        #region Computer Info Controls

        /// <summary>
        /// Checks the computer information.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="hash">The hash.</param>
        public static string CheckComputerInfo(string name, string hash)
        {
            string userName = "Brendan Burghardt";

            try
            {
                //new connection to sqlite database
                SQLiteConnection computerConnection = new SQLiteConnection(database);
                computerConnection.Open();
                SQLiteCommand command = new SQLiteCommand(computerConnection);

                //check to see if computer info is in database
                command.CommandText = "Select * from ComputerInfo where computer_name = @c_name and computer_hash = @c_hash";
                command.Parameters.AddWithValue("@c_name", name);
                command.Parameters.AddWithValue("@c_hash", hash);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    //check for entry
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userName = reader["computer_user"].ToString();
                            break;
                        }
                    }
                    else
                    {
                        //nothing found in database
                        string[] computers = new string[] { "editer1", "editer2", "editer3", "editer4", "editer5", "editer6" };
                        string[] people = new string[] { "Brendan Burghardt", "Brett Snyder", "Jerome Rigoroso", "Aaron Primmer", "Kelcy Erbele" };

                        foreach (string computer in computers)
                        {
                            int index = name.ToLower().IndexOf(computer);

                            if (index != -1)
                            {
                                switch (computer.ToLower())
                                {
                                    case "edit2":
                                        userName = people[1];
                                        break;
                                    case "edit3":
                                        userName = people[2];
                                        break;
                                    case "edit5":
                                        userName = people[3];
                                        break;
                                    case "edit6":
                                        userName = people[4];
                                        break;
                                    case "edit1":
                                    case "edit4":
                                    default:
                                        userName = people[0];
                                        break;
                                }
                            }
                        }

                        command.CommandText = "insert into ComputerInfo(computer_name, computer_hash, computer_user) values(@c_name, @c_hash, @c_user)";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@c_name", name);
                        command.Parameters.AddWithValue("@c_hash", hash);
                        command.Parameters.AddWithValue("@c_user", userName);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }

            return userName;
        }


        public static void UpdateCurrentUser(string name, string hash, string user)
        {
            try
            {
                //Establish db connection
                SQLiteConnection compConnection = new SQLiteConnection(database);
                compConnection.Open();
                SQLiteCommand command = new SQLiteCommand(compConnection);

                //Set up query and data to manipulate
                command.CommandText = "update ComputerInfo set computer_user = @c_user where computer_name = @c_name and computer_hash = @c_hash";
                command.Parameters.AddWithValue("@c_user", user);
                command.Parameters.AddWithValue("@c_name", name);
                command.Parameters.AddWithValue("@c_hash", hash);

                command.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
        }

        #endregion

        /// <summary>
        /// Counts all entries in each table.
        /// </summary>
        /// <returns></returns>
        public static List<int> CountAllEntries()
        {
            List<int> allEntries = new List<int>(5);
            string[] allDB = new string[] { "TapeDatabase", "Projects", "MasterList", "MasterArchiveVideos", "People" };

            try
            {
                SQLiteConnection connection = new SQLiteConnection(database);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                foreach(string dbName in allDB)
                {
                    command.CommandText = "select count(*) from " + dbName;
                    allEntries.Add(Convert.ToInt32(command.ExecuteScalar()));
                }
                
            }catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }

            return allEntries;
        }

        /// <summary>
        /// Gets all tables from imported database and inserts the information into the default database
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="mainForm">The main form.</param>
        public static void GetTableName(string file, TNG_Database.MainForm mainForm)
        {
            List<string> allTables = new List<string>();

            Debug.WriteLine("Starting to import");

            //new database filename
            string fileDatabase = "Data Source=" + file + ";Version=3;";

            Debug.WriteLine(fileDatabase);

            //progressbar status for each table
            int everyTable = 0;
            int progress = 0;

            try
            {
                //command to get all tables in database
                const string GET_TABLES_QUERY = "select name from sqlite_master where type='table'";
                
                SQLiteConnection connection = new SQLiteConnection(fileDatabase);
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(GET_TABLES_QUERY, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Debug.WriteLine("import has tables");
                        while (reader.Read())
                        {
                            if (TNGTables.Contains(reader.GetString(0)))
                            {
                                allTables.Add(reader.GetString(0));
                            }
                        }
                    }
                }

                //close connection
                connection.Close();
            }catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                Debug.WriteLine("SQLite Error: " + e.Message);
            }

            try
            {
                //check to see if there are any tables returned
                if (!allTables.Count.Equals(0))
                {
                    //set progressbar status to division on count of tables
                    everyTable = 100 / allTables.Count;
                    Debug.WriteLine("Table Number #: " + everyTable);

                    AddToDatabase addDB = new AddToDatabase();

                    foreach (string table in allTables)
                    {
                        //connection for new database
                        SQLiteConnection connect = new SQLiteConnection(fileDatabase);
                        connect.Open();

                        SQLiteCommand command = new SQLiteCommand(connect);
                        command.CommandText = "Select * from " + table;

                        switch (table)
                        {
                            case "TapeDatabase":
                            case "DeleteTapeDatabase":
                                //create values object
                                List<TapeDatabaseValues> tapeList = new List<TapeDatabaseValues>();
                                TapeDatabaseValues tapeValues = new TapeDatabaseValues();
                                
                                //execute query
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            tapeValues.Clear();
                                            tapeValues.ProjectId = reader["project_id"].ToString();
                                            tapeValues.ProjectName = reader["project_name"].ToString();
                                            tapeValues.TapeName = reader["tape_name"].ToString();
                                            tapeValues.TapeNumber = reader["tape_number"].ToString();
                                            tapeValues.Camera = Convert.ToInt32(reader["camera"]);
                                            tapeValues.TapeTags = reader["tape_tags"].ToString();
                                            tapeValues.DateShot = reader["date_shot"].ToString();
                                            tapeValues.MasterArchive = reader["master_archive"].ToString();
                                            tapeValues.PersonEntered = reader["person_entered"].ToString();

                                            //add values to list
                                            tapeList.Add(tapeValues);
                                        }
                                    }
                                }

                                //Close import connection
                                connect.Close();
                                
                                //iterate over values to add to database
                                foreach (TapeDatabaseValues values in tapeList)
                                {
                                    if (table == "TapeDatabase")
                                    {
                                        //add value to database
                                        addDB.AddTapeDatabase(values);
                                    }
                                    else if(table == "DeleteTapeDatabase")
                                    {
                                        //Open up new connection
                                        SQLiteConnection deleteConnect = new SQLiteConnection(database);
                                        deleteConnect.Open();
                                        command.Connection = deleteConnect;

                                        command.CommandText = "select count(*) from DeleteTapeDatabase where lower(project_name) = @projectName and project_id = @projectID";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@projectID", values.ProjectId);
                                        command.Parameters.AddWithValue("@projectName", values.ProjectName.ToLower());
                                        Int32 check = Convert.ToInt32(command.ExecuteScalar());

                                        //Make sure that entry doesn't exist already
                                        if (check < 1)
                                        {
                                            //add value to deleted database
                                            //There is not an entry go ahead and insert new row
                                            command.CommandText = "insert into DeleteTapeDatabase (tape_name, tape_number, project_id, project_name, camera, tape_tags, date_shot, master_archive, person_entered) values (@tapeName, @tapeNumber, @projectID, @projectName, @camera, @tapeTags, @dateShot, @masterArchive, @personEntered)";
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("@tapeName", values.TapeName);
                                            command.Parameters.AddWithValue("@tapeNumber", values.TapeNumber);
                                            command.Parameters.AddWithValue("@projectID", values.ProjectId);
                                            command.Parameters.AddWithValue("@projectName", values.ProjectName);
                                            command.Parameters.AddWithValue("@camera", values.Camera);
                                            command.Parameters.AddWithValue("@tapeTags", values.TapeTags);
                                            command.Parameters.AddWithValue("@dateShot", values.DateShot);
                                            command.Parameters.AddWithValue("@masterArchive", values.MasterArchive);
                                            command.Parameters.AddWithValue("@personEntered", values.PersonEntered);

                                            command.ExecuteNonQuery();
                                        }
                                        
                                        deleteConnect.Close();
                                    }
                                }

                                UpdateProgess(everyTable, mainForm);
                                progress += everyTable;

                                break;
                            case "Projects":
                            case "DeleteProjects":
                                //create values object
                                List<ProjectValues> projectList = new List<ProjectValues>();
                                ProjectValues projectValues = new ProjectValues();
                                
                                //execute query
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {

                                        while (reader.Read())
                                        {
                                            projectValues.Clear();
                                            projectValues.ProjectID = reader["project_id"].ToString();
                                            projectValues.Projectname = reader["project_name"].ToString();

                                            //add values to list
                                            projectList.Add(projectValues);
                                        }
                                    }
                                }
                                
                                //Close import connection
                                connect.Close();

                                //iterate over values to add to database
                                foreach (ProjectValues values in projectList)
                                {
                                    if (table == "Projects")
                                    {
                                        //add value to database
                                        addDB.AddProjects(values);
                                    }
                                    else if (table == "DeleteProjects")
                                    {
                                        //Open up new connection
                                        SQLiteConnection deleteConnect = new SQLiteConnection(database);
                                        deleteConnect.Open();
                                        command.Connection = deleteConnect;

                                        command.CommandText = "select count(*) from DeleteProjects where lower(project_name) = @projectName and project_id = @projectID";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@projectID", values.ProjectID);
                                        command.Parameters.AddWithValue("@projectName", values.Projectname.ToLower());
                                        Int32 check = Convert.ToInt32(command.ExecuteScalar());

                                        //Make sure that entry doesn't exist already
                                        if (check < 1)
                                        {
                                            //add value to deleted database
                                            //There is not an entry go ahead and insert new row
                                            command.CommandText = "insert into DeleteProjects (project_id, project_name) values (@projectID, @projectName)";
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("@projectID", values.ProjectID);
                                            command.Parameters.AddWithValue("@projectName", values.Projectname);

                                            command.ExecuteNonQuery();
                                        }
                                        
                                        deleteConnect.Close();
                                    }
                                }

                                UpdateProgess(everyTable, mainForm);
                                progress += everyTable;

                                break;
                            case "People":
                            case "DeletePeople":
                                //create values object
                                List<PeopleValues> peopleList = new List<PeopleValues>();
                                PeopleValues peopleValues = new PeopleValues();
                                
                                //execute query
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            peopleValues.Clear();
                                            peopleValues.PersonName = reader["person_name"].ToString();

                                            //add values to list
                                            peopleList.Add(peopleValues);
                                        }
                                    }
                                }

                                //Close import connection
                                connect.Close();

                                //iterate over values to add to database
                                foreach (PeopleValues values in peopleList)
                                {
                                    if (table == "People")
                                    {
                                        //add value to database
                                        addDB.AddPerson(values);
                                    }
                                    else if (table == "DeletePeople")
                                    {
                                        //Open up new connection
                                        SQLiteConnection deleteConnect = new SQLiteConnection(database);
                                        deleteConnect.Open();
                                        command.Connection = deleteConnect;

                                        command.CommandText = "select count(*) from DeletePeople where lower(person_name) = @personName";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@personName", values.PersonName.ToLower());
                                        Int32 check = Convert.ToInt32(command.ExecuteScalar());

                                        //Make sure that entry doesn't exist already
                                        if (check < 1)
                                        {
                                            //add value to deleted database
                                            //There is not an entry go ahead and insert new row
                                            command.CommandText = "insert into DeletePeole (person_name) values (@personName)";
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("@personName", values.PersonName);

                                            command.ExecuteNonQuery();
                                        }
                                        
                                        deleteConnect.Close();
                                    }
                                }

                                UpdateProgess(everyTable, mainForm);
                                progress += everyTable;

                                break;
                            case "MasterList":
                            case "DeleteMasterList":
                                //create values object
                                List<MasterListValues> masterTapeList = new List<MasterListValues>();
                                MasterListValues masterTapeValues = new MasterListValues();
                                
                                //execute query
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            masterTapeValues.Clear();
                                            masterTapeValues.MasterArchive = reader["master_archive"].ToString();
                                            masterTapeValues.MasterMedia = Convert.ToInt32(reader["master_media"]);

                                            //add values to list
                                            masterTapeList.Add(masterTapeValues);
                                        }
                                    }
                                }

                                //Close import connection
                                connect.Close();

                                //iterate over values to add to database
                                foreach (MasterListValues values in masterTapeList)
                                {
                                    if (table == "MasterList")
                                    {
                                        //add value to database
                                        addDB.AddMasterList(values);
                                    }
                                    else if (table == "DeleteMasterList")
                                    {
                                        //Open up new connection
                                        SQLiteConnection deleteConnect = new SQLiteConnection(database);
                                        deleteConnect.Open();
                                        command.Connection = deleteConnect;

                                        command.CommandText = "select count(*) from DeleteMasterList where lower(master_archive) = @masterArchive and master_media = @masterMedia";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@masterArchive", values.MasterArchive.ToLower());
                                        command.Parameters.AddWithValue("@masterMedia", values.MasterMedia);
                                        Int32 check = Convert.ToInt32(command.ExecuteScalar());

                                        //Make sure that entry doesn't exist already
                                        if (check < 1)
                                        {
                                            //add value to deleted database
                                            //There is not an entry go ahead and insert new row
                                            command.CommandText = "insert into DeleteMasterList (master_archive, master_media) values (@masterArchive, @masterMedia)";
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("@masterArchive", values.MasterArchive);
                                            command.Parameters.AddWithValue("@masterMedia", values.MasterMedia);

                                            command.ExecuteNonQuery();
                                        }
                                        
                                        deleteConnect.Close();
                                    }
                                }

                                UpdateProgess(everyTable, mainForm);
                                progress += everyTable;

                                break;
                            case "MasterArchiveVideos":
                            case "DeleteMasterArchiveVideos":
                                //create values object
                                List<MasterArchiveVideoValues> masterArchiveVideoList = new List<MasterArchiveVideoValues>();
                                MasterArchiveVideoValues MasterArchiveVideoValues = new MasterArchiveVideoValues();
                                
                                //execute query
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            MasterArchiveVideoValues.Clear();
                                            MasterArchiveVideoValues.ProjectId = reader["project_id"].ToString();
                                            MasterArchiveVideoValues.VideoName = reader["video_name"].ToString();
                                            MasterArchiveVideoValues.MasterTape = reader["master_tape"].ToString();
                                            MasterArchiveVideoValues.ClipNumber = reader["clip_number"].ToString();

                                            //add values to list
                                            masterArchiveVideoList.Add(MasterArchiveVideoValues);
                                        }
                                    }
                                }

                                //Close import connection
                                connect.Close();

                                //iterate over values to add to database
                                foreach (MasterArchiveVideoValues values in masterArchiveVideoList)
                                {
                                    if (table == "MasterArchiveVideos")
                                    {
                                        //add value to database
                                        addDB.AddMasterArchiveVideo(values);
                                    }
                                    else if (table == "DeleteMasterArchiveVideos")
                                    {
                                        //Open up new connection
                                        SQLiteConnection deleteConnect = new SQLiteConnection(database);
                                        deleteConnect.Open();
                                        command.Connection = deleteConnect;

                                        command.CommandText = "select count(*) from DeleteMasterArchiveVideos where project_id = @projectID and video_name = @videoName and clip_number = @clipNumber and master_tape = @masterTape";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@projectID", values.ProjectId);
                                        command.Parameters.AddWithValue("@videoName", values.VideoName);
                                        command.Parameters.AddWithValue("@masterTape", values.MasterTape);
                                        command.Parameters.AddWithValue("@clipNumber", values.ClipNumber);
                                        Int32 check = Convert.ToInt32(command.ExecuteScalar());

                                        //Make sure that entry doesn't exist already
                                        if (check < 1)
                                        {
                                            //add value to deleted database
                                            //There is not an entry go ahead and insert new row
                                            command.CommandText = "insert into DeleteMasterArchiveVideos (project_id, video_name, master_tape, clip_number) values (@projectID, @videoName, @masterTape, @clipNumber)";
                                            command.Parameters.Clear();
                                            command.Parameters.AddWithValue("@projectID", values.ProjectId);
                                            command.Parameters.AddWithValue("@videoName", values.VideoName);
                                            command.Parameters.AddWithValue("@masterTape", values.MasterTape);
                                            command.Parameters.AddWithValue("@clipNumber", values.ClipNumber);

                                            command.ExecuteNonQuery();
                                        }
                                        
                                        deleteConnect.Close();
                                    }
                                }

                                UpdateProgess(everyTable, mainForm);
                                progress += everyTable;

                                break;
                            case "ComputerInfo":
                                //create values object
                                List<ComputerInfoValues> computerInfoList = new List<ComputerInfoValues>();
                                ComputerInfoValues computerInfoValues = new ComputerInfoValues();
                                
                                //execute query
                                using (SQLiteDataReader reader = command.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            computerInfoValues.Clear();
                                            computerInfoValues.UniqueHash = reader["computer_hash"].ToString();
                                            computerInfoValues.ComputerName = reader["computer_name"].ToString();
                                            computerInfoValues.ComputerUser = reader["computer_user"].ToString();

                                            //add values to list
                                            computerInfoList.Add(computerInfoValues);
                                        }
                                    }
                                }

                                //Close import connection
                                connect.Close();

                                //iterate over values to add to database
                                foreach (ComputerInfoValues values in computerInfoList)
                                {
                                    //add value to database
                                    addDB.AddComputerInfo(values);
                                }

                                UpdateProgess(everyTable, mainForm);
                                progress += everyTable;

                                break;
                        }
                    }
                }
            }catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                Debug.WriteLine("SQLite Table Error: " + e.Message);

            }
            
            if((100 - progress) > 0)
            {
                UpdateProgess((100-progress), mainForm);
            }
        }
    }
}
