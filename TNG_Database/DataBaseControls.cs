using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Text.RegularExpressions;

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
        private void CloseConnections(SQLiteCommand command, SQLiteConnection connection)
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
        public List<MasterListValues> GetAllMasterListItems()
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
            int dbCheck = 0;

            //start new sqlite connection
            SQLiteConnection searchConnection = new SQLiteConnection(database);
            searchConnection.Open();

            string query = "select * from TapeDatabase order by project_id asc";
            SQLiteCommand command = new SQLiteCommand(query, searchConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
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
                }
            }
            else
            {
                if(tapeDBValues != null)
                {
                    tapeDBValues.Clear();
                }
            }

            return tapeDBValues;

        }
    }
}
