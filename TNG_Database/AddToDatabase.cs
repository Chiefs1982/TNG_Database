using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.ComponentModel;

namespace TNG_Database
{
    class AddToDatabase
    {

        UpdateStatus updateStatus = UpdateStatus.Instance();

        //private string tngDatabaseConnectString = "Data Source=TNG_TapeDatabase.sqlite;Version=3;";
        //gets string for database connection from DatabaseControls
        private string tngDatabaseConnectString = DataBaseControls.GetDBName();
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

        //----------------------------------------------
        //--TAPE DATABASE ADD, DELETE, UPDATE DATABASE--
        //----------------------------------------------
        #region Tape Database
        /// <summary>
        /// Adds single tape entry to tape database
        /// </summary>
        /// <param name="tapeDBValues">Values of new entry in TapeDatabaseValuesClass</param>
        /// <returns>Boolean of success of database operation</returns>
        public bool AddTapeDatabase(TapeDatabaseValues tapeDBValues)
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
                    command.CommandText = "insert into TapeDatabase (tape_name, tape_number, project_id, project_name, camera, tape_tags, date_shot, master_archive, person_entered) values (@tapeName, @tapeNumber, @projectID, @projectNumber, @camera, @tapeTags, @dateShot, @masterArchive, @personEntered)";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@tapeName", tapeDBValues.TapeName);
                    command.Parameters.AddWithValue("@tapeNumber", tapeDBValues.TapeNumber);
                    command.Parameters.AddWithValue("@projectID", tapeDBValues.ProjectId);
                    command.Parameters.AddWithValue("@projectNumber", tapeDBValues.ProjectName);
                    command.Parameters.AddWithValue("@camera", tapeDBValues.Camera);
                    command.Parameters.AddWithValue("@tapeTags", tapeDBValues.TapeTags);
                    command.Parameters.AddWithValue("@dateShot", tapeDBValues.DateShot);
                    command.Parameters.AddWithValue("@masterArchive", tapeDBValues.MasterArchive);
                    command.Parameters.AddWithValue("@personEntered", tapeDBValues.PersonEntered);


                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Entry inserts successfully
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
                    Console.WriteLine("Tape DB entry already taken");
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

                //create sqlite query to check to see if Tape Database project and tape number is already in database
                string sql = "select count(*) from TapeDatabase where project_id = @project_id and id = @id ";
                SQLiteCommand command = new SQLiteCommand(sql, tapeDBConnection);
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
                        Console.WriteLine("TapeDatabase Row was deleted");
                        CloseConnections(command, tapeDBConnection);
                        return true;
                    }
                    else
                    {
                        //row was not deleted
                        Console.WriteLine("TapeDatabase Row was not deleted");
                        CloseConnections(command, tapeDBConnection);
                        return false;
                    }
                }
                else
                {
                    //There is no entry to delete
                    Console.WriteLine("No Entry to delete");
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
                        Console.WriteLine("Row update was a success");
                        CloseConnections(command, tapeDBConnection);
                        return true;
                    }
                    else
                    {
                        //Row was not updated
                        Console.WriteLine("Row update was not a success");
                        CloseConnections(command, tapeDBConnection);
                        return false;
                    }
                }
                else
                {
                    //No entry was returned to be updated
                    Console.WriteLine("No Entry to update");
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
        public bool AddMasterList(string name, int camera)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection masterConnection = new SQLiteConnection(tngDatabaseConnectString);
                masterConnection.Open();

                //create sqlite query to check to see if Master list name is already in database
                string sql = "select count(*) from MasterList where lower(master_archive) = @m_name";
                SQLiteCommand command = new SQLiteCommand(sql, masterConnection);
                command.Parameters.AddWithValue("@m_name", name.ToLower());
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then Master List is already in database
                if (check == 0)
                {
                    //No rows were returned, so entry can be added
                    command.Parameters.Clear();
                    command.CommandText = "insert into MasterList (master_archive, master_media) values (@m_newName, @m_newMedia)";
                    command.Parameters.AddWithValue("@m_newName", name);
                    command.Parameters.AddWithValue("@m_newMedia", camera);

                    //Execute Insert query and check if it was added
                    if (command.ExecuteNonQuery() == 1)
                    {
                        //Entry added to Master List database
                        Console.WriteLine("Master list inserted correctly");
                        CloseConnections(command, masterConnection);
                        return true;
                    }
                    else
                    {
                        //Entry was not added to database
                        Console.WriteLine("Master List entry not added correctly");
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

                //create sqlite query to check to see if Master list name is already in database
                string sql = "select count(*) from MasterList where lower(master_archive) = @m_name and id = @id";
                SQLiteCommand command = new SQLiteCommand(sql, masterConnection);
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
                        Console.WriteLine("Entry deleted successfully");
                        CloseConnections(command, masterConnection);
                        return true;
                    }
                    else
                    {
                        //Entry was not deleted
                        Console.WriteLine("Entry was not deleted successfully");
                        CloseConnections(command, masterConnection);
                        return false;
                    }
                }
                else
                {
                    //There is no entry for the selected item to delete
                    Console.WriteLine("No Entry that matches name to be deleted");
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
                        Console.WriteLine("Row updated");
                        CloseConnections(command, masterConnection);
                        return true;
                    }
                    else
                    {
                        //Row not updated
                        Console.WriteLine("Row not updated");
                        CloseConnections(command, masterConnection);
                        return false;
                    }
                }
                else
                {
                    //There was nothing to update
                    Console.WriteLine("Nothing to update");
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
        public bool AddPerson(string name)
        {
            try
            {
                //Gets and opens up connection with TNG_TapeDatabase.sqlite
                SQLiteConnection personConnection = new SQLiteConnection(tngDatabaseConnectString);
                personConnection.Open();

                //create sqlite query to check to see if name is already in database
                string sql = "select count(*) from People where lower(person_name) = @p_name";
                SQLiteCommand command = new SQLiteCommand(sql, personConnection);
                command.Parameters.AddWithValue("@p_name", name.ToLower());
                Int32 check = Convert.ToInt32(command.ExecuteScalar());

                //If query returned has any rows of data then name is already in database
                if (check == 0)
                {
                    //person is not in database and needs to be added
                    command.CommandText = "insert into People (person_name) values (@add_name)";
                    command.Parameters.AddWithValue("@add_name", name);

                    //execute adding person query and check to see person was added
                    if (command.ExecuteNonQuery() == 1)
                    {
                        CloseConnections(command, personConnection);
                        return true;
                    }
                    else
                    {
                        CloseConnections(command, personConnection);
                        return false;
                    }

                }
                else
                {
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

                //create sqlite query to check to see if name is already in database
                string sql = "select count(*) from People where person_name = @p_name";
                SQLiteCommand command = new SQLiteCommand(sql, personConnection);
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
        #endregion
        //----------------------------------------------
        //------PROJECT ADD, DELETE, UPDATE DATABASE----
        //----------------------------------------------
        #region Project Database

        public bool AddProjects(ProjectValues project)
        {
            return false;
        }

        #endregion
    }
}
