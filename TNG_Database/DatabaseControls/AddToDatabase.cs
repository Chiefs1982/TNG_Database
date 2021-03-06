﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel;
using TNG_Database.Values;
using System.Diagnostics;

namespace TNG_Database
{
    class AddToDatabase
    {

        UpdateStatus updateStatus = UpdateStatus.Instance();

        //private string tngDatabaseConnectString = "Data Source=TNG_TapeDatabase.sqlite;Version=3;";
        //gets string for database connection from DatabaseControls
        private string tngDatabaseConnectString = DataBaseControls.Database;
        private static string staticDatabase = DataBaseControls.Database;
        //-------------------------------------------------
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

        private static void CloseConnection(SQLiteConnection connection)
        {
            if (connection != null) { connection.Close(); connection.Dispose(); }
            GC.Collect();
        }

        private static void CloseCommand(SQLiteCommand command)
        {
            if (command != null) { command.Dispose(); }
            GC.Collect();
        }

        //----------------------------------------------
        //--TAPE DATABASE ADD, DELETE, UPDATE DATABASE--
        //----------------------------------------------
        #region Tape Database
        /// <summary>
        /// Adds single tape entry to tape database
        /// </summary>
        /// <param name="tapeDBValues">Values of new entry in TapeDatabaseValuesClass</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool AddTapeDatabase(TapeDatabaseValues tapeDBValues, bool reinstate = false)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(tngDatabaseConnectString);
                tapeDBConnection.Open();

                //create sqlite query to check to see if Tape Database project and tape number is already in database
                string sql = "select count(*) from TapeDatabase where project_id = @t_pID and lower(project_name) = @t_pName and tape_number = @t_tNumber";
                SQLiteCommand command = new SQLiteCommand(sql, tapeDBConnection);
                command.Parameters.AddWithValue("@t_pID", tapeDBValues.ProjectId);
                command.Parameters.AddWithValue("@t_pName", tapeDBValues.ProjectName.ToLower());
                command.Parameters.AddWithValue("@t_tNumber", tapeDBValues.TapeNumber);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then Master List is already in database
                if (check == 0)
                {
                    //There is not an entry go ahead and insert new row
                    command.CommandText = "insert into TapeDatabase (tape_name, tape_number, project_id, project_name, camera, tape_tags, date_shot, master_archive, person_entered) values (@tapeName, @tapeNumber, @projectID, @projectName, @camera, @tapeTags, @dateShot, @masterArchive, @personEntered)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@tapeName", tapeDBValues.TapeName);
                    command.Parameters.AddWithValue("@tapeNumber", tapeDBValues.TapeNumber);
                    command.Parameters.AddWithValue("@projectID", tapeDBValues.ProjectId);
                    command.Parameters.AddWithValue("@projectName", tapeDBValues.ProjectName);
                    command.Parameters.AddWithValue("@camera", tapeDBValues.Camera);
                    command.Parameters.AddWithValue("@tapeTags", tapeDBValues.TapeTags);
                    command.Parameters.AddWithValue("@dateShot", tapeDBValues.DateShot);
                    command.Parameters.AddWithValue("@masterArchive", tapeDBValues.MasterArchive);
                    command.Parameters.AddWithValue("@personEntered", tapeDBValues.PersonEntered);
                    
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Entry inserts successfully
                        
                        //Delete entry from deleted database
                        if (reinstate)
                        {
                            command.CommandText = "delete from DeleteTapeDatabase where id = @t_id";
                            command.Parameters.AddWithValue("@t_id", tapeDBValues.ID);

                            if(command.ExecuteNonQuery() == 1)
                            {
                                //Deleted from deleted db success
                            }else
                            {
                                //Deleted from deleted db failure
                            }
                        }

                        CloseConnections(command, tapeDBConnection);
                        return true;
                    }
                    else
                    {
                        //Entry not added to database
                        CloseConnections(command, tapeDBConnection);
                        return false;
                    }
                }
                else
                {
                    //There is already an entry
                    Debug.WriteLine("Tape DB entry already taken");
                    if (tapeDBConnection != null) { tapeDBConnection.Close(); }
                    return false;
                }
            }catch(Exception e)
            {
                //Error
                MainForm.LogFile("Add Tape List Error: " + e.Message);
                return false;
            }
        }

        //----------------------------------------------
        /// <summary>
        /// Delete entry from tape database
        /// </summary>
        /// <param name="tapeValues">Values of entry to delete in TapeDatabaseValuesClass</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool DeleteTapeDatabase(TapeDatabaseValues tapeValues)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(tngDatabaseConnectString);
                tapeDBConnection.Open();

                //Insert entry to delete into Deleted Tape Database DB
                SQLiteCommand command = new SQLiteCommand(tapeDBConnection);
                command.CommandText = "insert into DeleteTapeDatabase (tape_name, tape_number, project_id, project_name, camera, tape_tags, date_shot, master_archive, person_entered) values (@tapeName, @tapeNumber, @projectID, @projectNumber, @camera, @tapeTags, @dateShot, @masterArchive, @personEntered)";
                command.Parameters.AddWithValue("@tapeName", tapeValues.TapeName);
                command.Parameters.AddWithValue("@tapeNumber", tapeValues.TapeNumber);
                command.Parameters.AddWithValue("@projectID", tapeValues.ProjectId);
                command.Parameters.AddWithValue("@projectNumber", tapeValues.ProjectName);
                command.Parameters.AddWithValue("@camera", tapeValues.Camera);
                command.Parameters.AddWithValue("@tapeTags", tapeValues.TapeTags);
                command.Parameters.AddWithValue("@dateShot", tapeValues.DateShot);
                command.Parameters.AddWithValue("@masterArchive", tapeValues.MasterArchive);
                command.Parameters.AddWithValue("@personEntered", tapeValues.PersonEntered);
                
                //check if entry was added
                if(command.ExecuteNonQuery() == 1)
                {
                    //Entry added
                    //create sqlite query to check to see if Tape Database project and tape number is already in database
                    command.Parameters.Clear();
                    command.CommandText = "select count(*) from TapeDatabase where project_id = @project_id and id = @id ";
                    command.Parameters.AddWithValue("@project_id", tapeValues.ProjectId);
                    command.Parameters.AddWithValue("@id", tapeValues.ID);
                    Int32 check = Convert.ToInt32(command.ExecuteScalar());

                    //If query returned has any rows of data then Master List is already in database
                    if (check == 1)
                    {
                        //Entry double checked to match
                        command.CommandText = "delete from TapeDatabase where id = @id and project_id = @project_id";

                        //Execute query and check to see that row was deleted
                        if (command.ExecuteNonQuery() == 1)
                        {
                            //row was deleted
                            Debug.WriteLine("TapeDatabase Row was deleted");
                            CloseConnections(command, tapeDBConnection);
                            return true;
                        }
                        else
                        {
                            //row was not deleted
                            Debug.WriteLine("TapeDatabase Row was not deleted");
                            CloseConnections(command, tapeDBConnection);
                            return false;
                        }
                    }
                    else
                    {
                        //There is no entry to delete
                        Debug.WriteLine("No Entry to delete");
                        CloseConnections(command, tapeDBConnection);
                        return false;
                    }
                }else
                {
                    //Add deletion item to deleted db not valid
                    CloseConnections(command, tapeDBConnection);
                    return false;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Delete Tape List Error: " + e.Message);
                return false;
            }
        }

        //----------------------------------------------
        /// <summary>
        /// Update entry in tape database
        /// </summary>
        /// <param name="tapeDBValues">Values of new entry to edit in TapeDatabaseValuesClass</param>
        /// <param name="id">ID of old item to update</param>
        /// <param name="projectID">Project ID of old item to update</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool UpdateTapeDatabase(TapeDatabaseValues tapeDBValues, TapeDatabaseValues oldTapeValues)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(tngDatabaseConnectString);
                tapeDBConnection.Open();

                //create sqlite query to check to see if Tape Database project and tape number is already in database
                string sql = "select count(*) from TapeDatabase where project_id = @t_pID and id = @id";
                SQLiteCommand command = new SQLiteCommand(sql, tapeDBConnection);
                command.Parameters.AddWithValue("@t_pID", oldTapeValues.ProjectId);
                command.Parameters.AddWithValue("@id", oldTapeValues.ID);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then Master List is already in database
                if (check == 1)
                {
                    //There is an entry to be updated
                    command.Parameters.Clear();
                    command.CommandText = "update TapeDatabase set tape_name = @tapeName, tape_number = @tapeNumber," +
                                            "project_id = @projectID, project_name = @projectName, camera = @camera," +
                                            "tape_tags = @tapeTags, date_shot = @dateShot, master_archive = @masterArchive," +
                                            " person_entered = @personEntered where id = @id";
                    command.Parameters.AddWithValue("@tapeName", tapeDBValues.TapeName);
                    command.Parameters.AddWithValue("@tapeNumber", tapeDBValues.TapeNumber);
                    command.Parameters.AddWithValue("@projectID", tapeDBValues.ProjectId);
                    command.Parameters.AddWithValue("@projectName", tapeDBValues.ProjectName);
                    command.Parameters.AddWithValue("@camera", tapeDBValues.Camera);
                    command.Parameters.AddWithValue("@tapeTags", tapeDBValues.TapeTags);
                    command.Parameters.AddWithValue("@dateShot", tapeDBValues.DateShot);
                    command.Parameters.AddWithValue("@masterArchive", tapeDBValues.MasterArchive);
                    command.Parameters.AddWithValue("@personEntered", tapeDBValues.PersonEntered);
                    command.Parameters.AddWithValue("@id", oldTapeValues.ID);

                    //Execute query and return if row was updated
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Row was updated
                        Debug.WriteLine("Row update was a success");
                        CloseConnections(command, tapeDBConnection);
                        return true;
                    }
                    else
                    {
                        //Row was not updated
                        Debug.WriteLine("Row update was not a success");
                        CloseConnections(command, tapeDBConnection);
                        return false;
                    }
                }
                else
                {
                    //No entry was returned to be updated
                    Debug.WriteLine("No Entry to update");
                    CloseConnections(command, tapeDBConnection);
                    return false;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Update Tape List Error: " + e.Message);
                return false;
            }
            
        }

        /// <summary>
        /// Deletes the multiple tape selected.
        /// </summary>
        /// <param name="listValues">The list values.</param>
        /// <returns></returns>
        public static int DeleteMultipleTapeSelected(List<TapeDatabaseValues> listValues)
        {
            int deleted = 0;

            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(staticDatabase);
                tapeDBConnection.Open();
                
                foreach (TapeDatabaseValues tapeValues in listValues)
                {
                    //Insert entry to delete into Deleted Tape Database DB
                    SQLiteCommand command = new SQLiteCommand(tapeDBConnection);
                    command.CommandText = "insert into DeleteTapeDatabase (tape_name, tape_number, project_id, project_name, camera, tape_tags, date_shot, master_archive, person_entered) values (@tapeName, @tapeNumber, @projectID, @projectName, @camera, @tapeTags, @dateShot, @masterArchive, @personEntered); delete from TapeDatabase where id = @id and project_id = @projectID";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", tapeValues.ID);
                    command.Parameters.AddWithValue("@tapeName", tapeValues.TapeName);
                    command.Parameters.AddWithValue("@tapeNumber", tapeValues.TapeNumber);
                    command.Parameters.AddWithValue("@projectID", tapeValues.ProjectId);
                    command.Parameters.AddWithValue("@projectName", tapeValues.ProjectName);
                    command.Parameters.AddWithValue("@camera", tapeValues.Camera);
                    command.Parameters.AddWithValue("@tapeTags", tapeValues.TapeTags);
                    command.Parameters.AddWithValue("@dateShot", tapeValues.DateShot);
                    command.Parameters.AddWithValue("@masterArchive", tapeValues.MasterArchive);
                    command.Parameters.AddWithValue("@personEntered", tapeValues.PersonEntered);
                    if(command.ExecuteNonQuery() == 1) { /* Success */ }
                    deleted++;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Delete Tape List Error: " + e.Message);
            }

            return deleted;
        }
        
        #endregion
        //----------------------------------------------
        //---MASTER LIST ADD, DELETE, UPDATE DATABASE---
        //----------------------------------------------
        #region Master List Database
        /// <summary>
        /// Add an entry to the Master List database
        /// </summary>
        /// <param name="name">Name of tape to add</param>
        /// <param name="camera">Camera number</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool AddMasterList(MasterListValues masterValues, bool reinstate = false)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection masterConnection = new SQLiteConnection(tngDatabaseConnectString);
                masterConnection.Open();

                //create sqlite query to check to see if Master list name is already in database
                string sql = "select count(*) from MasterList where lower(master_archive) = @m_name";
                SQLiteCommand command = new SQLiteCommand(sql, masterConnection);
                command.Parameters.AddWithValue("@m_name", masterValues.MasterArchive.ToLower());
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then Master List is already in database
                if (check == 0)
                {
                    //No rows were returned, so entry can be added
                    command.Parameters.Clear();
                    command.CommandText = "insert into MasterList (master_archive, master_media) values (@m_newName, @m_newMedia)";
                    command.Parameters.AddWithValue("@m_newName", masterValues.MasterArchive);
                    command.Parameters.AddWithValue("@m_newMedia", masterValues.MasterMedia);

                    //Execute Insert query and check if it was added
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Entry added to Master List database

                        //Delete entry from deleted database
                        if (reinstate)
                        {
                            command.CommandText = "delete from DeleteMasterList where id = @t_id";
                            command.Parameters.AddWithValue("@t_id", masterValues.ID);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                //Deleted from deleted db success
                            }
                            else
                            {
                                //Deleted from deleted db failure
                            }
                        }
                        Debug.WriteLine("Master list inserted correctly");
                        CloseConnections(command, masterConnection);
                        return true;
                    }
                    else
                    {
                        //Entry was not added to database
                        Debug.WriteLine("Master List entry not added correctly");
                        CloseConnections(command, masterConnection);
                        return false;
                    }
                }
                else
                {
                    //At least 1 row was returned, so entry already exists
                    CloseConnections(command, masterConnection);
                    return false;
                }
            }catch(Exception e)
            {
                MainForm.LogFile("Master List Add Error: " + e.Message);
                return false;
            }
        }

        //-----------------------------------
        /// <summary>
        /// Delete an entry from Master List database
        /// </summary>
        /// <param name="deleteValues">Entry values to delete as MasterListValues class</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool DeleteMasterList(MasterListValues deleteValues)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection masterConnection = new SQLiteConnection(tngDatabaseConnectString);
                masterConnection.Open();

                //Insert deleted entry into Deleted Master List DB
                SQLiteCommand command = new SQLiteCommand(masterConnection);
                command.CommandText = "insert into DeleteMasterList (master_archive, master_media) values (@m_newName, @m_newMedia)";
                command.Parameters.AddWithValue("@m_newName", deleteValues.MasterArchive);
                command.Parameters.AddWithValue("@m_newMedia", deleteValues.MasterMedia);

                if(command.ExecuteNonQuery() == 1)
                {
                    //create sqlite query to check to see if Master list name is already in database
                    command.Parameters.Clear();
                    command.CommandText = "select count(*) from MasterList where lower(master_archive) = @m_name and id = @id";
                    command.Parameters.AddWithValue("@m_name", deleteValues.MasterArchive.ToLower());
                    command.Parameters.AddWithValue("@id", deleteValues.ID);
                    Int32 check = Convert.ToInt32(command.ExecuteScalar());

                    //If query returned has any rows of data then Master List is already in database
                    if (check == 1)
                    {
                        //There is an entry to be deleted
                        command.CommandText = "delete from MasterList where id = @id and lower(master_archive) = @m_name";

                        if (command.ExecuteNonQuery() == 1)
                        {
                            //Entry deleted
                            Debug.WriteLine("Entry deleted successfully");
                            CloseConnections(command, masterConnection);
                            return true;
                        }
                        else
                        {
                            //Entry was not deleted
                            Debug.WriteLine("Entry was not deleted successfully");
                            CloseConnections(command, masterConnection);
                            return false;
                        }
                    }
                    else
                    {
                        //There is no entry for the selected item to delete
                        Debug.WriteLine("No Entry that matches name to be deleted");
                        CloseConnections(command, masterConnection);
                        return false;
                    }
                }
                else
                {
                    //Could not insert into Delete DB
                    Debug.WriteLine("No Entry that matches name to be deleted");
                    CloseConnections(command, masterConnection);
                    return false;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Master List Delete Error: " + e.Message);
                return false;
            }
        }

        //----------------------------------------------
        /// <summary>
        /// Update entry in Master List database
        /// </summary>
        /// <param name="oldValues">Old entry values to edit as MasterListValues class</param>
        /// <param name="updateValues">New entry to update the old as MasterListValues class</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool UpdateMasterList(MasterListValues oldValues, MasterListValues updateValues)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection masterConnection = new SQLiteConnection(tngDatabaseConnectString);
                masterConnection.Open();

                //create sqlite query to check to see if Master list name is already in database
                string sql = "select count(*) from MasterList where lower(master_archive) = @m_name and id = @id";
                SQLiteCommand command = new SQLiteCommand(sql, masterConnection);
                command.Parameters.AddWithValue("@m_name", oldValues.MasterArchive.ToLower());
                command.Parameters.AddWithValue("@id", oldValues.ID);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then Master List is already in database
                if (check == 1)
                {
                    //Update entry
                    command.CommandText = "update MasterList set master_archive = @m_newName, master_media = @m_newMedia where id = @id";
                    command.Parameters.AddWithValue("@m_newName", updateValues.MasterArchive);
                    command.Parameters.AddWithValue("@m_newMedia", updateValues.MasterMedia);

                    //Execute query and check to make sure row was updated
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Row updated
                        Debug.WriteLine("Row updated");
                        CloseConnections(command, masterConnection);
                        return true;
                    }
                    else
                    {
                        //Row not updated
                        Debug.WriteLine("Row not updated");
                        CloseConnections(command, masterConnection);
                        return false;
                    }
                }
                else
                {
                    //There was nothing to update
                    Debug.WriteLine("Nothing to update");
                    CloseConnections(command, masterConnection);
                    return false;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Master List Update Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes the multiple master list that are selected.
        /// </summary>
        /// <param name="listValues">The list values.</param>
        /// <returns></returns>
        public static int DeleteMultipleMasterListselected(List<MasterListValues> listValues)
        {
            int deleted = 0;

            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(staticDatabase);
                tapeDBConnection.Open();

                foreach (MasterListValues tapeValues in listValues)
                {
                    //Insert entry to delete into Deleted Tape Database DB
                    SQLiteCommand command = new SQLiteCommand(tapeDBConnection);
                    command.CommandText = "insert into DeleteMasterList (master_archive, master_media) values (@masterArchive, @masterMedia); delete from MasterList where master_archive = @masterArchive";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", tapeValues.ID);
                    command.Parameters.AddWithValue("@masterArchive", tapeValues.MasterArchive);
                    command.Parameters.AddWithValue("@masterMedia", tapeValues.MasterMedia);
                    if (command.ExecuteNonQuery() == 1) { /* Success */ }
                    /*
                    //Entry double checked to match
                    command.CommandText = "delete from TapeDatabase where id = @id and project_id = @projectID";
                    command.ExecuteNonQuery();
                    */
                    deleted++;
                }

                return deleted;
            }
            catch (Exception e)
            {
                MainForm.LogFile("Delete Tape List Error: " + e.Message);
                return deleted;
            }
        }

        #endregion
        //----------------------------------------------
        //------PERSON ADD, DELETE, UPDATE DATABASE-----
        //----------------------------------------------
        #region Person Database
        /// <summary>
        /// Adds a person to the People Database
        /// </summary>
        /// <param name="name">Name of person to add to database</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool AddPerson(PeopleValues peopleValues, bool reinstate = false)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection personConnection = new SQLiteConnection(tngDatabaseConnectString);
                personConnection.Open();

                //create sqlite query to check to see if name is already in database
                string sql = "select count(*) from People where lower(person_name) = @p_name";
                SQLiteCommand command = new SQLiteCommand(sql, personConnection);
                command.Parameters.AddWithValue("@p_name", peopleValues.PersonName.ToLower());
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then name is already in database
                if (check == 0)
                {
                    //person is not in database and needs to be added
                    command.CommandText = "insert into People (person_name) values (@add_name)";
                    command.Parameters.AddWithValue("@add_name", peopleValues.PersonName);

                    //execute adding person query and check to see person was added
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //add successful

                        //Delete entry from deleted database
                        if (reinstate || true)
                        {
                            command.CommandText = "delete from DeletePeople where person_name = @personName";
                            command.Parameters.AddWithValue("@personName", peopleValues.PersonName);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                //Deleted from deleted db success
                            }
                            else
                            {
                                //Deleted from deleted db failure
                            }
                        }

                        CloseConnections(command, personConnection);
                        return true;
                    }
                    else
                    {
                        //add failed
                        CloseConnections(command, personConnection);
                        return false;
                    }
                }
                else
                {
                    //entry already exists
                    CloseConnections(command, personConnection);
                    return false;
                }
            }
            catch(Exception e)
            {
                MainForm.LogFile("Person Add Error" + e.Message);
                return false;
            }
        }

        //----------------------------------
        /// <summary>
        /// Delete this person from the People database
        /// </summary>
        /// <param name="name">Name of person to delete from database</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool DeletePerson(string name)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection personConnection = new SQLiteConnection(tngDatabaseConnectString);
                personConnection.Open();

                //Insert entry to be deleted into Deleted Person DB
                SQLiteCommand command = new SQLiteCommand(personConnection);
                command.CommandText = "insert into DeletePeople (person_name) values (@add_name)";
                command.Parameters.AddWithValue("@add_name", name);
                command.ExecuteNonQuery();

                //create sqlite query to check to see if name is already in database
                command.Parameters.Clear();
                command.CommandText = "select count(*) from People where person_name = @p_name";
                command.Parameters.AddWithValue("@p_name", name);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then name is already in database
                if (check == 1)
                {
                    //Name matched in database
                    command.CommandText = "delete from People where person_name = @p_name";

                    //execute delete query and check to see row was deleted
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Person deleted from database
                        CloseConnections(command, personConnection);
                        return true;
                    }
                    else
                    {
                        //Person not deleted from database
                        CloseConnections(command, personConnection);
                        return false;
                    }
                }
                else
                {
                    //there was no name in the database to delete
                    CloseConnections(command, personConnection);
                    return false;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Person Delete Error" + e.Message);
                return false;
            }
        }

        //--------------------------------------------
        /// <summary>
        /// Edit persons name in the People database
        /// </summary>
        /// <param name="name">Old name of person to edit in database</param>
        /// <param name="edit_name">New name of person to edit in database</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool EditPerson(string name, string edit_name)
        {
            try
            {
                //make name lowercase to check DB
                string lower_name = name.ToLower();

                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection personConnection = new SQLiteConnection(tngDatabaseConnectString);
                personConnection.Open();

                //create sqlite query to check to see if name is already in database
                string sql = "select count(*) from People where person_name = @p_name";
                SQLiteCommand command = new SQLiteCommand(sql, personConnection);
                command.Parameters.AddWithValue("@p_name", name);
                //Execute count on query
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //check to make sure it returned 1 row
                if (check == 1)
                {
                    //set update parameters
                    command.CommandText = "update People set person_name = @up_name where person_name = @p_name";
                    command.Parameters.AddWithValue("@up_name", edit_name);

                    //execute query
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //update success, close and return true
                        CloseConnections(command, personConnection);
                        return true;
                    }
                    else
                    {
                        //update failed, close and return false
                        CloseConnections(command, personConnection);
                        return false;
                    }
                }
                else
                {
                    //No match returned, close and return false
                    CloseConnections(command, personConnection);
                    return false;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Person Update Error" + e.Message);
                return false;
            }
        }


        public static int DeleteMultiplePeopleSelected(List<PeopleValues> listValues)
        {
            int deleted = 0;

            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(staticDatabase);
                tapeDBConnection.Open();

                foreach (PeopleValues peopleValues in listValues)
                {
                    //Insert entry to delete into Deleted Tape Database DB
                    SQLiteCommand command = new SQLiteCommand(tapeDBConnection);
                    command.CommandText = "insert into DeletePeople (person_name) values (@personName); delete from People where person_name = @personName";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@personName", peopleValues.PersonName);
                    if (command.ExecuteNonQuery() == 1) { /* Success */ }
                    /*
                    //Entry double checked to match
                    command.CommandText = "delete from TapeDatabase where id = @id and project_id = @projectID";
                    command.ExecuteNonQuery();
                    */
                    deleted++;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Delete Project List Error: " + e.Message);
            }

            return deleted;
        }

        #endregion
        //----------------------------------------------
        //------PROJECT ADD, DELETE, UPDATE DATABASE----
        //----------------------------------------------
        #region Project Database

        /// <summary>
        /// Adds Entry to the projects database.
        /// </summary>
        /// <param name="project">project to add</param>
        /// <returns>true if successful, false if it failed</returns>
        public bool AddProjects(ProjectValues project, bool reinstate = false)
        {
            try
            {
                SQLiteConnection projectsConnection = new SQLiteConnection(tngDatabaseConnectString);
                projectsConnection.Open();
                
                SQLiteCommand command = new SQLiteCommand(projectsConnection);

                //create sqlite query to check to see if name is already in database
                command.CommandText = "select count(*) from Projects where project_id = @p_id";
                command.Parameters.AddWithValue("@p_id", project.ProjectID);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                if (check == 0)
                {
                    //No matches, add entry to DB
                    command.Parameters.Clear();
                    command.CommandText = "insert or ignore into Projects(project_id, project_name) values(@p_id, @p_name)";
                    command.Parameters.AddWithValue("@p_id", project.ProjectID);
                    command.Parameters.AddWithValue("@p_name", project.Projectname);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //insert successful

                        //Delete entry from deleted database
                        if (reinstate)
                        {
                            command.CommandText = "delete from DeleteMasterList where id = @t_id";
                            command.Parameters.AddWithValue("@t_id", project.ID);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                //Deleted from deleted db success
                            }
                            else
                            {
                                //Deleted from deleted db failure
                            }
                        }

                        CloseConnections(command, projectsConnection);
                        return true;
                    }
                    else
                    {
                        //insert failed
                        CloseConnections(command, projectsConnection);
                        return false;
                    }
                }else
                {
                    //Already an entry, abort
                    CloseConnections(command, projectsConnection);
                    return false;
                }
            }
            catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes Entry in the projects database.
        /// </summary>
        /// <param name="project">project to delete</param>
        /// <returns>true if successful, false if it failed</returns>
        public bool DeleteProjects(ProjectValues project)
        {
            try
            {
                SQLiteConnection projectsConnection = new SQLiteConnection(tngDatabaseConnectString);
                projectsConnection.Open();
                SQLiteCommand command = new SQLiteCommand(projectsConnection);

                //Insert delete entry into Delete Projects DB
                command.CommandText = "insert into DeleteProjects(project_id, project_name) values(@p_id, @p_name)";
                command.Parameters.AddWithValue("@p_id", project.ProjectID);
                command.Parameters.AddWithValue("@p_name", project.Projectname);

                if(command.ExecuteNonQuery() == 1)
                {
                    //Insert into Deletion DB successful
                    //Delete from Projects DB
                    command.Parameters.Clear();
                    command.CommandText = "delete from Projects where id = @id";
                    command.Parameters.AddWithValue("@id", project.ID);

                    if(command.ExecuteNonQuery() == 1)
                    {
                        //deletion successful
                        CloseConnections(command, projectsConnection);
                        return true;
                    }
                    else
                    {
                        //deletion failed
                        CloseConnections(command, projectsConnection);
                        return false;
                    }
                }else
                {
                    //Insert into Deletion DB failed
                    CloseConnections(command, projectsConnection);
                    return false;
                }
            }
            catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Edits Entry in the projects database.
        /// </summary>
        /// <param name="oldProject">old project to update</param>
        /// <param name="newProject">new values to update the old</param>
        /// <returns>true if successful, false if it failed</returns>
        public bool EditProject(ProjectValues oldProject, ProjectValues newProject)
        {
            try
            {
                SQLiteConnection projectsConnection = new SQLiteConnection(tngDatabaseConnectString);
                projectsConnection.Open();
                SQLiteCommand command = new SQLiteCommand(projectsConnection);

                command.CommandText = "select count(*) from Projects where id = @id";
                command.Parameters.AddWithValue("@id", oldProject.ID);
                

                if(Convert.ToInt32(command.ExecuteScalar()) == 1)
                {
                    //entry found to update
                    command.Parameters.Clear();
                    command.CommandText = "update Projects set project_id = @p_id, project_name = @p_name where id = @id";
                    command.Parameters.AddWithValue("@p_id", newProject.ProjectID);
                    command.Parameters.AddWithValue("@p_name", newProject.Projectname);
                    command.Parameters.AddWithValue("@id", oldProject.ID);

                    if(command.ExecuteNonQuery() == 1)
                    {
                        //Update successful
                        CloseConnections(command, projectsConnection);
                        return true;
                    }else
                    {
                        //Update Failed
                        CloseConnections(command, projectsConnection);
                        return false;
                    }
                }else
                {
                    //No entry found to update
                    CloseConnections(command, projectsConnection);
                    return false;
                }
            }
            catch(SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes the multiple projects selected.
        /// </summary>
        /// <param name="listValues">The list values.</param>
        /// <returns></returns>
        public static int DeleteMultipleProjectSelected(List<ProjectValues> listValues)
        {
            int deleted = 0;

            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(staticDatabase);
                tapeDBConnection.Open();

                foreach (ProjectValues projectValues in listValues)
                {
                    //Insert entry to delete into Deleted Tape Database DB
                    SQLiteCommand command = new SQLiteCommand(tapeDBConnection);
                    command.CommandText = "insert into DeleteProjects (project_id, project_name) values (@projectID, @projectName); delete from Projects where id = @id and project_id = @projectID";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", projectValues.ID);
                    command.Parameters.AddWithValue("@projectName", projectValues.Projectname);
                    command.Parameters.AddWithValue("@projectID", projectValues.ProjectID);
                    if (command.ExecuteNonQuery() == 1) { /* Success */ }
                    /*
                    //Entry double checked to match
                    command.CommandText = "delete from TapeDatabase where id = @id and project_id = @projectID";
                    command.ExecuteNonQuery();
                    */
                    deleted++;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Delete Project List Error: " + e.Message);
            }

            return deleted;
        }

        #endregion
        //--------------------------------------------------------
        //---MASTER ARCHIVE VIDEO ADD, DELETE, UPDATE DATABASE----
        //--------------------------------------------------------
        #region MasterArchiveVideos Database

        /// <summary>
        /// Adds Entry to the MasterArchiveVideos database.
        /// </summary>
        /// <param name="video">video to add</param>
        /// <returns>true if successful, false if it failed</returns>
        public bool AddMasterArchiveVideo(MasterArchiveVideoValues video, bool reinstate = false)
        {
            try
            {
                SQLiteConnection videoConnection = new SQLiteConnection(tngDatabaseConnectString);
                videoConnection.Open();
                SQLiteCommand command = new SQLiteCommand(videoConnection);

                command.CommandText = "insert or ignore into MasterList(master_archive, master_media) values (@m_archive, @m_media)";
                command.Parameters.AddWithValue("@m_archive", video.MasterTape);
                command.Parameters.AddWithValue("@m_media", 2);

                if(command.ExecuteNonQuery() == 1)
                {
                    //new Master Tape added successfully
                    Debug.WriteLine("Added master list success");
                }else
                {
                    //new Master Tape added failure
                    Debug.WriteLine("Added master list did not happen");
                }

                //create sqlite query to check to see if name is already in database
                command.Parameters.Clear();
                command.CommandText = "select count(*) from MasterArchiveVideos where video_name = @v_name";
                command.Parameters.AddWithValue("@v_name", video.VideoName);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                if (check == 0)
                {
                    //No matches, add entry to DB
                    command.Parameters.Clear();
                    command.CommandText = "insert into MasterArchiveVideos(project_id, video_name, master_tape, clip_number) values(@p_id, @v_name, @m_tape, @c_number)";
                    command.Parameters.AddWithValue("@p_id", video.ProjectId);
                    command.Parameters.AddWithValue("@v_name", video.VideoName);
                    command.Parameters.AddWithValue("@m_tape", video.MasterTape);
                    command.Parameters.AddWithValue("@c_number", video.ClipNumber);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //insert successful

                        //Delete entry from deleted database
                        if (reinstate)
                        {
                            command.CommandText = "delete from DeleteMasterArchiveVideos where id = @t_id";
                            command.Parameters.AddWithValue("@t_id", video.ID);

                            if (command.ExecuteNonQuery() == 1)
                            {
                                //Deleted from deleted db success
                            }
                            else
                            {
                                //Deleted from deleted db failure
                            }
                        }

                        CloseConnections(command, videoConnection);
                        return true;
                    }
                    else
                    {
                        //insert failed
                        CloseConnections(command, videoConnection);
                        return false;
                    }
                }
                else
                {
                    //Already an entry, abort
                    CloseConnections(command, videoConnection);
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes Entry in the projects database.
        /// </summary>
        /// <param name="project">project to delete</param>
        /// <returns>true if successful, false if it failed</returns>
        public bool DeleteMasterArchiveVideo(MasterTapeValues video)
        {
            try
            {
                SQLiteConnection videoConnection = new SQLiteConnection(tngDatabaseConnectString);
                videoConnection.Open();
                SQLiteCommand command = new SQLiteCommand(videoConnection);

                //Insert delete entry into Delete Projects DB
                command.CommandText = "insert into DeleteMasterArchiveVideos(project_id, video_name, master_tape, clip_number) values(@p_id, @v_name, @m_tape, @c_number)";
                command.Parameters.AddWithValue("@p_id", video.ProjectID);
                command.Parameters.AddWithValue("@v_name", video.VideoName);
                command.Parameters.AddWithValue("@m_tape", video.MasterTape);
                command.Parameters.AddWithValue("@c_number", video.ClipNumber);

                if (command.ExecuteNonQuery() == 1)
                {
                    //Insert into Deletion DB successful
                    //Delete from Projects DB
                    command.Parameters.Clear();
                    command.CommandText = "delete from MasterArchiveVideos where id = @id";
                    command.Parameters.AddWithValue("@id", video.ID);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //deletion successful
                        CloseConnections(command, videoConnection);
                        return true;
                    }
                    else
                    {
                        //deletion failed
                        CloseConnections(command, videoConnection);
                        return false;
                    }
                }
                else
                {
                    //Insert into Deletion DB failed
                    CloseConnections(command, videoConnection);
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Edits Entry in the projects database.
        /// </summary>
        /// <param name="oldVideo">old project to update</param>
        /// <param name="newVideo">new values to update the old</param>
        /// <returns>true if successful, false if it failed</returns>
        public bool EditMasterArchiveVideo(MasterTapeValues oldVideo, MasterTapeValues newVideo)
        {
            try
            {
                SQLiteConnection videoConnection = new SQLiteConnection(tngDatabaseConnectString);
                videoConnection.Open();
                SQLiteCommand command = new SQLiteCommand(videoConnection);

                command.CommandText = "select count(*) from MasterArchiveVideos where id = @id";
                command.Parameters.AddWithValue("@id", oldVideo.ID);
                
                if (Convert.ToInt32(command.ExecuteScalar()) == 1)
                {
                    //entry found to update
                    command.Parameters.Clear();
                    command.CommandText = "update MasterArchiveVideos set project_id = @p_id, video_name = @v_name, master_tape = @m_tape, clip_number = @c_number where id = @id";
                    command.Parameters.AddWithValue("@p_id", newVideo.ProjectID);
                    command.Parameters.AddWithValue("@v_name", newVideo.VideoName);
                    command.Parameters.AddWithValue("@m_tape", newVideo.MasterTape);
                    command.Parameters.AddWithValue("@c_number", newVideo.ClipNumber);
                    command.Parameters.AddWithValue("@id", oldVideo.ID);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Update successful
                        CloseConnections(command, videoConnection);
                        return true;
                    }
                    else
                    {
                        //Update Failed
                        CloseConnections(command, videoConnection);
                        return false;
                    }
                }
                else
                {
                    //No entry found to update
                    CloseConnections(command, videoConnection);
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes the multiple master archiveselected.
        /// </summary>
        /// <param name="listValues">The list values.</param>
        /// <returns></returns>
        public static int DeleteMultipleMasterArchiveselected(List<MasterArchiveVideoValues> listValues)
        {
            int deleted = 0;

            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection tapeDBConnection = new SQLiteConnection(staticDatabase);
                tapeDBConnection.Open();

                foreach (MasterArchiveVideoValues tapeValues in listValues)
                {
                    //Insert entry to delete into Deleted Tape Database DB
                    SQLiteCommand command = new SQLiteCommand(tapeDBConnection);
                    command.CommandText = "insert into DeleteMasterArchiveVideos (project_id, video_name, master_tape, clip_number) values (@projectID, @videoName, @masterTape, @clipNumber); delete from MasterArchiveVideos where id = @id and project_id = @projectID";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@id", tapeValues.ID);
                    command.Parameters.AddWithValue("@projectID", tapeValues.ProjectId);
                    command.Parameters.AddWithValue("@videoName", tapeValues.VideoName);
                    command.Parameters.AddWithValue("@masterTape", tapeValues.MasterTape);
                    command.Parameters.AddWithValue("@clipNumber", tapeValues.ClipNumber);
                    if (command.ExecuteNonQuery() == 1) { /* Success */ }
                    /*
                    //Entry double checked to match
                    command.CommandText = "delete from TapeDatabase where id = @id and project_id = @projectID";
                    command.ExecuteNonQuery();
                    */
                    deleted++;
                }
            }
            catch (Exception e)
            {
                MainForm.LogFile("Delete Tape List Error: " + e.Message);
            }

            return deleted;
        }

        #endregion
        //--------------------------------------------------------
        //----COMPUTER INFO VIDEO ADD, DELETE, UPDATE DATABASE----
        //--------------------------------------------------------
        #region ComputerInfo Database

        /// <summary>
        /// Adds entry to the computer information database.
        /// </summary>
        /// <param name="computerInfo">Computer information values.</param>
        /// <returns></returns>
        public bool AddComputerInfo(ComputerInfoValues computerInfo)
        {
            try
            {
                SQLiteConnection projectsConnection = new SQLiteConnection(tngDatabaseConnectString);
                projectsConnection.Open();

                SQLiteCommand command = new SQLiteCommand(projectsConnection);

                //create sqlite query to check to see if name is already in database
                command.CommandText = "select count(*) from ComputerInfo where computer_name = @c_name and computer_hash = @c_hash and computer_user = @c_user";
                command.Parameters.AddWithValue("@c_name", computerInfo.ComputerName);
                command.Parameters.AddWithValue("@c_hash", computerInfo.UniqueHash);
                command.Parameters.AddWithValue("@c_user", computerInfo.ComputerUser);
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                if (check == 0)
                {
                    //No matches, add entry to DB
                    command.Parameters.Clear();
                    command.CommandText = "insert into ComputerInfo(computer_name, computer_hash, computer_user) values(@c_name, @c_hash, @c_user)";
                    command.Parameters.AddWithValue("@c_name", computerInfo.ComputerName);
                    command.Parameters.AddWithValue("@c_hash", computerInfo.UniqueHash);
                    command.Parameters.AddWithValue("@c_user", computerInfo.ComputerUser);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        //insert successful
                        
                        CloseConnections(command, projectsConnection);
                        return true;
                    }
                    else
                    {
                        //insert failed
                        CloseConnections(command, projectsConnection);
                        return false;
                    }
                }
                else
                {
                    //Already an entry, abort
                    CloseConnections(command, projectsConnection);
                    return false;
                }
            }
            catch (SQLiteException e)
            {
                MainForm.LogFile("SQLite Error: " + e.Message);
                return false;
            }
        }

        #endregion
    }
}
