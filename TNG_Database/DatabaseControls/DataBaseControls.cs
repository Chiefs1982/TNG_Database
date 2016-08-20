using System;
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

namespace TNG_Database
{
    class DataBaseControls
    {
        //TapeDatabaseDB connection string
        private static string database = "Data Source=database/TNG_TapeDatabase.sqlite;Version=3;";

        //Reference to CommonMethods
        static CommonMethods commonMethod = CommonMethods.Instance();

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
                        Console.WriteLine("File Deleted");
                    }
                }
            }catch(Exception e)
            {
                MainForm.LogFile("Error Deleting File: " + e.Message);
            }
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
                SQLiteDataReader reader = command.ExecuteReader();

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
            } else
            {
                masterList = new List<MasterListValues>(1);
                masterList.Add(new MasterListValues("Nothing in database", 2));
            }

            CloseConnections(command, populateConnection);
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

            //start SQLite Connection
            SQLiteConnection projectsConnection = new SQLiteConnection(database);
            projectsConnection.Open();
            SQLiteCommand command = new SQLiteCommand(projectsConnection);

            command.CommandText = "select * from Projects order by project_id asc";
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    projectValue = new ProjectValues(reader["project_id"].ToString(), reader["project_name"].ToString(), Convert.ToInt32(reader["id"]));
                    values.Add(projectValue);

                }
                CloseConnections(command, projectsConnection);
            } else
            {
                CloseConnections(command, projectsConnection);
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

                SQLiteDataReader reader = command.ExecuteReader();

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

                
            }catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
            }
            return mList;
        }

        //----------------------------------------------        
        /// <summary>
        /// Gets the person list for people dropdowns.
        /// </summary>
        /// <returns></returns>
        public static string[] GetPersonListForDropdown()
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

        /// <summary>
        /// Searches all database.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public List<SearchValues> SearchAllDB(string[] input)
        {
            //declare values
            List<SearchValues> tapeDBValues = new List<SearchValues>();
            string preQuery = "";
            string query = "";

            //start new sqlite connection
            SQLiteConnection searchConnection = new SQLiteConnection(database);
            searchConnection.Open();
            SQLiteCommand command = new SQLiteCommand(searchConnection);

            //start of query pieces for tape Database
            preQuery = "select * from TapeDatabase";

            //add query based on number of entries
            if(input.Length > 0)
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

                    if(i > 0)
                    {
                        preQuery += " or";
                    }

                    //set up the regex part of the query
                    preQuery += String.Format(" project_id like {0} or project_name like {0} or tape_name like {0} or tape_tags like {0} or date_shot like {0} or master_archive like {0}",value);
                    Console.WriteLine(input[i].ToLower());
                }
            }
            preQuery += " order by project_id asc";
            Console.WriteLine(preQuery);

            //Set assembled query to final query
            query = preQuery;
            command.CommandText = query;
            Console.WriteLine("Command Text: "+command.CommandText);
            SQLiteDataReader reader = command.ExecuteReader();

            //If there are return values then parse them and display them
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SearchValues dbData = new SearchValues();
                    dbData.TapeName = reader["tape_name"].ToString();
                    dbData.TapeNumber =  reader["tape_number"].ToString();
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

            //close reader for next query
            reader.Close();

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
                    Console.WriteLine(input[i].ToLower());
                }
            }
            preQuery += " order by project_id asc";
            Console.WriteLine(preQuery);

            //Set assembled query to final query
            query = preQuery;
            command.CommandText = query;
            Console.WriteLine("Command Text: " + command.CommandText);
            reader = command.ExecuteReader();

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

            //close reader for next query
            reader.Close();

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
                    Console.WriteLine(input[i].ToLower());
                }
            }
            preQuery += " order by project_id asc";
            Console.WriteLine(preQuery);

            //Set assembled query to final query
            query = preQuery;
            command.CommandText = query;
            Console.WriteLine("Command Text: " + command.CommandText);
            reader = command.ExecuteReader();

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

            //close reader for next query
            reader.Close();

            //close connection and return final list
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
                    Console.WriteLine("Master List insert Success");
                }else
                {
                    Console.WriteLine("Master List insert Failed");
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
                    DeleteFile(tmp, ofd, importStream);
                    return true;
                }
                else
                {
                    //No entries found
                    Console.WriteLine("No master archive(s) added to database");
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
                        string[] lineArray = newLine.Split(seperators, 8);

                        //check to make sure there are 2 parts to each line
                        if (lineArray.Length > 1)
                        {
                            lineArray[0] = lineArray[0].Trim();
                            lineArray[1] = lineArray[1].Replace(',', '-').Trim();
                            tapeValues = new TapeDatabaseValues(lineArray[0], lineArray[1], lineArray[2], lineArray[3], commonMethod.GetCameraNumber(lineArray[4]), lineArray[7], commonMethod.ConvertDateFromDropdownForDB(Convert.ToDateTime(lineArray[5])), lineArray[6], "Imported");
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
                    command.CommandText = "select project_name from Projects where project_id = @pid limit 1";
                    command.Parameters.AddWithValue("@pid", value.ProjectId);
                    SQLiteDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            projectName = reader["project_name"].ToString();
                        }
                    }

                    reader.Close();
                    command.Parameters.Clear();
                    command.CommandText = "insert into TapeDatabase (tape_name,tape_number,project_id, project_name,camera,tape_tags,date_shot,master_archive,person_entered) values (@tapeName,@tapeNumber,@projectID, @projectName,@camera,@tapeTags,@dateShot,@masterArchive,@person)";
                    command.Parameters.AddWithValue("@tapeName", value.TapeName);
                    command.Parameters.AddWithValue("@tapeNumber", value.TapeNumber);
                    command.Parameters.AddWithValue("@projectID", value.ProjectId);
                    if (projectName.Equals(""))
                    {
                        command.Parameters.AddWithValue("@projectName", value.ProjectName);
                    }else
                    {
                        command.Parameters.AddWithValue("@projectName", projectName);
                    }
                    command.Parameters.AddWithValue("@camera", value.Camera);
                    command.Parameters.AddWithValue("@tapeTags", value.TapeTags);
                    command.Parameters.AddWithValue("@dateShot",value.DateShot);
                    command.Parameters.AddWithValue("@masterArchive", value.MasterArchive);
                    command.Parameters.AddWithValue("@person", value.PersonEntered);
                    
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
                            Console.WriteLine("Added: " + value.ProjectId + ", Progress = " + progress);
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
                    MainForm.LogFile(counter + " tapes added to database");
                    projectConnection.Close();
                    return true;
                }
                else
                {
                    Console.WriteLine("No Tapes added to database");
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

        /// <summary>
        /// Creates the sqlite database.
        /// </summary>
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
                MainForm.LogFile(e.Message);
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
                SQLiteDataReader reader = command.ExecuteReader();

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

                    foreach(string computer in computers)
                    {
                        int index = name.ToLower().IndexOf(computer);

                        if (index != -1)
                        {
                            switch (computer.ToLower())
                            {
                                case "editer2":
                                    userName = people[1];
                                    break;
                                case "editer3":
                                    userName = people[2];
                                    break;
                                case "editer5":
                                    userName = people[3];
                                    break;
                                case "editer6":
                                    userName = people[4];
                                    break;
                                case "editer1":
                                case "editer4":
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
            catch { }
            
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
            catch { }
        }

        #endregion
    }
}
