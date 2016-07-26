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

namespace TNG_Database
{
    class DataBaseControls
    {
        //TapeDatabaseDB connection string
        private static string database = "Data Source=database/TNG_TapeDatabase.sqlite;Version=3;";

        public static string GetDBName()
        {
            return database;
        }

        /// <summary>
        /// Close connection and command variables, and collects Garbage
        /// </summary>
        /// <param name="command">Active command of SQLiteCommand</param>
        /// <param name="connection">Active connection of SQLiteConnection</param>
        private static void CloseConnections(SQLiteCommand command, SQLiteConnection connection)
        {
            if(command != null) { command.Dispose(); }
            if(connection != null) { connection.Close(); connection.Dispose(); }
            GC.Collect();
        }

        /// <summary>
        /// Gets all users in database
        /// </summary>
        /// <returns>A List of strings of all users</returns>
        public List<string> GetAllUsers()
        {
            List<string> userList;

            //start new sqlite connection
            SQLiteConnection populateConnection = new SQLiteConnection(database);
            populateConnection.Open();

            //Get all users and id and populate a new User class per user
            string query = "select person_name from People";
            SQLiteCommand queryCommand = new SQLiteCommand(query, populateConnection);
            SQLiteDataReader reader = queryCommand.ExecuteReader();
            
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

            //start new sqlite connection
            SQLiteConnection populateConnection = new SQLiteConnection(database);
            populateConnection.Open();

            //Query the database for all items in the Master list
            string query = "select * from MasterList order by master_archive asc";
            SQLiteCommand command = new SQLiteCommand(query, populateConnection);
            SQLiteDataReader reader = command.ExecuteReader();

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
            }else
            {
                masterList = new List<MasterListValues>(1);
                masterList.Add(new MasterListValues("Nothing in database", 2));
            }

            CloseConnections(command, populateConnection);
            return masterList;
        }

        //-------------------------------------------

        public List<TapeDatabaseValues> GetAllTapeValues()
        {
            List<TapeDatabaseValues> tapeList;

            //start new sqlite connection
            SQLiteConnection populateConnection = new SQLiteConnection(database);
            populateConnection.Open();

            //Query the database for all items in the Master list
            string query = "select * from TapeDatabase order by project_id asc";
            SQLiteCommand command = new SQLiteCommand(query, populateConnection);
            SQLiteDataReader reader = command.ExecuteReader();

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

            return tapeList;
        }

        //----------------------------------------------        
        /// <summary>
        /// Gets the person list for people dropdowns.
        /// </summary>
        /// <returns></returns>
        public string[] GetPersonListForDropdown()
        {
            List<string> personList;

            //start new sqlite connection
            SQLiteConnection dropdownConnection = new SQLiteConnection(database);
            dropdownConnection.Open();

            string query = "select person_name from People order by person_name asc";
            SQLiteCommand command = new SQLiteCommand(query, dropdownConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                personList = new List<string>(reader.StepCount);

                while (reader.Read())
                {
                    personList.Add(reader["person_name"].ToString());
                }
            }else
            {
                personList = new List<string>();
                personList.AddRange(new string[] {"Brendan Burghardt", "Brett Snyder", "Aaron Primmer", "Dan Schultz","Jerome Rigoroso", "Kelcy Erbele"});
            }
            

            CloseConnections(command, dropdownConnection);
            return personList.ToArray();
        }

        //----------------------------------------------        
        /// <summary>
        /// Gets the master list for master tape dropdowns.
        /// </summary>
        /// <returns></returns>
        public string[] GetMasterListForDropdown()
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

        /// <summary>
        /// Searches all database.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public List<TapeDatabaseValues> SearchAllDB(string[] input)
        {
            List<TapeDatabaseValues> tapeDBValues = new List<TapeDatabaseValues>();
            //int dbCheck = 0;

            //start new sqlite connection
            SQLiteConnection searchConnection = new SQLiteConnection(database);
            searchConnection.Open();
            SQLiteCommand command = new SQLiteCommand(searchConnection);

            //start of query pieces
            string preQuery = "select * from TapeDatabase";

            //add query based on number of entries
            if(input.Length > 0)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    string term = "@input" + i;
                    string value = "'%" + input[i].ToLower() + "%'";
                    if (i == 0)
                    {
                        preQuery += " where";
                    }

                    if(i > 0)
                    {
                        preQuery += " or";
                    }

                    //preQuery += String.Format(" project_name LIKE {0}",value);
                    preQuery += String.Format(" project_id like {0} or project_name like {0} or tape_name like {0} or tape_tags like {0} or date_shot like {0} or master_archive like {0}",value);
                    //command.Parameters.AddWithValue(term,value);
                    Console.WriteLine(input[i].ToLower());
                }
            }
            preQuery += " order by project_id asc";
            Console.WriteLine(preQuery);

            //Set assembled query to final query
            string query = preQuery;
            command.CommandText = query;
            Console.WriteLine("Command Text: "+command.CommandText);
            SQLiteDataReader reader = command.ExecuteReader();

            //If there are return values then parse them and display them
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TapeDatabaseValues dbData = new TapeDatabaseValues(reader["tape_name"].ToString(),
                                    reader["tape_number"].ToString(), reader["project_id"].ToString(), reader["project_name"].ToString(),
                                    Convert.ToInt32(reader["camera"]), reader["tape_tags"].ToString(), reader["date_shot"].ToString(),
                                    reader["master_archive"].ToString(), reader["person_entered"].ToString(), Convert.ToInt32(reader["id"]));
                    tapeDBValues.Add(dbData);

                    /*
                    for(int i = 0;i < reader.FieldCount; i++)
                    {
                        dbCheck = tapeDBValues.Count;
                        foreach(string user in input)
                        {
                            if (Regex.IsMatch(reader.GetValue(i).ToString(), user,RegexOptions.IgnoreCase))
                            {
                                Console.WriteLine(reader.GetValue(i) + " matched user input");
                                TapeDatabaseValues dbData = new TapeDatabaseValues(reader["tape_name"].ToString(),
                                    reader["tape_number"].ToString(),reader["project_id"].ToString(), reader["project_name"].ToString(),
                                    Convert.ToInt32(reader["camera"]), reader["tape_tags"].ToString(), reader["date_shot"].ToString(),
                                    reader["master_archive"].ToString(), reader["person_entered"].ToString(), Convert.ToInt32(reader["id"]));
                                tapeDBValues.Add(dbData);
                                break;
                            }
                        }
                        if(dbCheck < tapeDBValues.Count)
                        {
                            break;
                        }
                    }
                    */
                }
            }
            else
            {
                if(tapeDBValues != null)
                {
                    tapeDBValues.Clear();
                }
            }
            searchConnection.Close();
            return tapeDBValues;

        }

        /// <summary>
        /// Adds the master tapes from file.
        /// </summary>
        /// <param name="worker">Background worker</param>
        /// <param name="importStream">Import Stream for file</param>
        /// <param name="ofd">The file returned from OpenFileDialog</param>
        /// <returns></returns>
        public static bool AddMasterTapesFromFile(BackgroundWorker worker, Stream importStream, OpenFileDialog ofd, string masterArchive)
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
                catch (Exception error)
                {
                    MainForm.LogFile(error.Message);
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

                //iterate over each entry and insert it into the DB
                foreach (MasterTapeValues master in projectList)
                {
                    string query = "insert into MasterArchiveVideos (project_id, video_name, master_tape, clip_number) values (@projectID, @videoName, @masterTape, @clipNumber)";
                    SQLiteCommand command = new SQLiteCommand(query, masterConnection);
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
                            progress += (queryCounter * progressCounter);
                            progressCounter = 0;
                            //update the progess bar
                            worker.ReportProgress(Convert.ToInt32(progress));
                            Console.WriteLine("Added: " + master.ProjectID);
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
                    MainForm.LogFile(counter + " master archive(s) added to database");
                    masterConnection.Close();
                    return true;
                }
                else
                {
                    //No entries found
                    Console.WriteLine("No master archive(s) added to database");
                    masterConnection.Close();
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
                            Console.WriteLine("Added: " + value.ProjectID + ", Progress = " + progress);
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
                    MainForm.LogFile(counter + " projects added to database");
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
                MainForm.LogFile("Import Projects Error: " + e.Message);
                return false;
            }


        }
    }
}
