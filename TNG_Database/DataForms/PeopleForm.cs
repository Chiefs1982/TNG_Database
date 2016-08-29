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
using TNG_Database.Values;

namespace TNG_Database
{
    public partial class PeopleForm : Form
    {
        private Point groupboxPoint = new Point(345, 94);
        private TNG_Database.MainForm mainform;

        UpdateStatus updateStatus = UpdateStatus.Instance();

        //list to capture multiple selected itemms for deletion
        List<PeopleValues> peopleValues = null;
        
        //Initialize People Form
        public PeopleForm()
        {
            InitializeComponent();

            //populate list with users
            PopulateUserList();

            //disable delete and edit user until user is selected
            editUserPeopleButton.Enabled = false;
            deleteUserPeopleButton.Enabled = false;
        }

        //initialize People Form from the MDI Parent
        public PeopleForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            //populate list with users
            PopulateUserList();

            //disable delete and edit user until user is selected
            editUserPeopleButton.Enabled = false;
            deleteUserPeopleButton.Enabled = false;
            defaultEditGroupBox.Location = groupboxPoint;
            defaultEditGroupBox.Visible = true;

            peopleFormListBox.SelectionMode = SelectionMode.MultiExtended;
        }

        //------------------------------------------------
        //--------PEOPLEFORM METHODS----------------------
        //------------------------------------------------

        //Get all users and populate list on form startup
        public void PopulateUserList()
        {
            //clear everything from listbox first
            peopleFormListBox.Items.Clear();

            //Create list and get all users returned to it
            List<string> userList = DataBaseControls.GetAllUsers();

            if(userList[0].Equals("No Users"))
            {
                //load table with statement saying no entries
                peopleFormListBox.SelectionMode = SelectionMode.None;
                peopleFormListBox.Items.Add(userList[0]);
            }
            else
            {
                //Turn on select only one mode and add each user to listbox
                peopleFormListBox.SelectionMode = SelectionMode.One;
                foreach (string user in userList)
                {
                    peopleFormListBox.Items.Add(user);
                }
            }

            peopleFormListBox.SelectionMode = SelectionMode.MultiExtended;
        }

        //--------------------------------------------
        //When a user is clicked this gets called
        private void peopleFormListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If edit & delete buttons are not enabled AND selection is not on white space then enable them
            if(peopleFormListBox.SelectedIndex != -1 && peopleFormListBox.SelectedItems.Count == 1)
            {
                editUserPeopleButton.Enabled = true;
                deleteUserPeopleButton.Enabled = true;

                deleteUserPeopleButton.Text = "Delete User";
                updateStatus.UpdateStatusBar(peopleFormListBox.SelectedItem.ToString() + " selected", mainform);
            }
            else if (peopleFormListBox.SelectedItems.Count > 1)
            {
                //more than one item selected
                CloseOpenGroupBox("default");

                editUserPeopleButton.Enabled = false;
                deleteUserPeopleButton.Enabled = true;
                deleteUserPeopleButton.Text = "Delete(" + peopleFormListBox.SelectedItems.Count + ")";
                updateStatus.UpdateStatusBar(peopleFormListBox.SelectedItems.Count + " users selected", mainform);
            }
            else if(peopleFormListBox.SelectedItems.Count == 0)
            {
                updateStatus.UpdateStatusBar("Nothing Selected", mainform);
                editUserPeopleButton.Enabled = false;
                deleteUserPeopleButton.Enabled = false;

                deleteUserPeopleButton.Text = "Delete User";
            }
        }

        //update listbox
        public void UpdateListBox()
        {
            PopulateUserList();
            peopleFormListBox.ClearSelected();

            //disable delete and edit user until user is selected
            editUserPeopleButton.Enabled = false;
            deleteUserPeopleButton.Enabled = false;

        }

        //Clear and close Open GroupBox
        private void CloseOpenGroupBox(string groupBoxOpen)
        {
            //determine which groupbox is open, clear and close appropriate box, and show default box
            switch (groupBoxOpen)
            {
                case "add":
                    addUserNameTextbox.Clear();
                    addUserGroupBox.Visible = false;
                    defaultEditGroupBox.Visible = true;
                    break;
                case "delete":
                    deleteUserGroupBox.Visible = false;
                    defaultEditGroupBox.Visible = true;
                    break;
                case "edit":
                    editUserPeopleTB.Clear();
                    editUserGroupBox.Visible = false;
                    defaultEditGroupBox.Visible = true;
                    break;
                default:
                    editUserGroupBox.Visible = false;
                    deleteUserGroupBox.Visible = false;
                    addUserGroupBox.Visible = false;
                    defaultEditGroupBox.Visible = true;
                    break;
            }
            peopleFormListBox.SelectionMode = SelectionMode.MultiExtended;
        }

        //---------------------------------------------------------
        //---ADD, EDIT, DELETE USER FROM DATABASE BUTTON CLICKS----
        //---------------------------------------------------------

        //Edit user button click
        private void editUserPeopleButton_Click(object sender, EventArgs e)
        {
            //Check to make sure a user is selected in the listbox
            if(peopleFormListBox.SelectedIndex != -1)
            {
                //Check to make sure other Group Boxes aren't visible
                if (addUserGroupBox.Visible || deleteUserGroupBox.Visible || defaultEditGroupBox.Visible)
                {
                    addUserGroupBox.Visible = false;
                    deleteUserGroupBox.Visible = false;
                    defaultEditGroupBox.Visible = false;

                }

                //Show controls on same page in editGroup
                editUserOldPersonName.Text = peopleFormListBox.GetItemText(peopleFormListBox.SelectedItem);
                editUserGroupBox.Location = groupboxPoint;
                editUserGroupBox.Visible = true;
                editUserPeopleTB.Focus();
            }
        }

        //Delete user button click
        private void deleteUserPeopleButton_Click(object sender, EventArgs e)
        {
            //Check to make sure a user is selected in the listbox
            if (peopleFormListBox.SelectedIndex != -1 && peopleFormListBox.SelectedItems.Count == 1)
            {
                //Check to make sure other Group Boxes aren't visible
                if (editUserGroupBox.Visible || addUserGroupBox.Visible || defaultEditGroupBox.Visible)
                {
                    editUserGroupBox.Visible = false;
                    addUserGroupBox.Visible = false;
                    defaultEditGroupBox.Visible = false;
                }

                //Show Delete User Group box
                deleteUserGroupBox.Location = groupboxPoint;
                deleteUserNameLabel.Text = peopleFormListBox.GetItemText(peopleFormListBox.SelectedItem);
                deleteUserGroupBox.Visible = true;
                deleteUserCancelButton.Focus();
            }else if (peopleFormListBox.SelectedItems.Count > 1)
            {
                //multiple items selected in listview

                //Show message box to make sure user is to be deleted
                DialogResult deleteMessage = MessageBox.Show("Do you want to delete these " + peopleFormListBox.SelectedItems.Count + " entries?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //Check to see if user pressed yes or no
                if (deleteMessage == DialogResult.Yes)
                {

                    //clear delete list
                    if (peopleValues == null)
                    {
                        peopleValues = new List<PeopleValues>();
                    }
                    else
                    {
                        peopleValues.Clear();
                    }

                    //iterate over each item selected and save data in a value, then a list
                    foreach (var item in peopleFormListBox.SelectedItems)
                    {
                        PeopleValues value = new PeopleValues(item.ToString());

                        peopleValues.Add(value);
                    }



                    if (peopleFormListBox.SelectedItems.Count > 1 && peopleValues.Count > 0)
                    {
                        Console.WriteLine("sending " + peopleValues.Count + " people to delete");
                        updateStatus.UpdateStatusBar(AddToDatabase.DeleteMultiplePeopleSelected(peopleValues) + " people deleted", mainform);
                    }

                    PopulateUserList();
                    CloseOpenGroupBox("default");
                    deleteUserPeopleButton.Enabled = false;
                    deleteUserPeopleButton.Text = "Delete User";

                }
                else if (deleteMessage == DialogResult.No)
                {
                    //No Pressed, nothing will be done
                }

            }
        }

        //Add user button click
        private void addUserPeopleButton_Click(object sender, EventArgs e)
        {
            //Check to make sure other Group Boxes aren't visible
            if (editUserGroupBox.Visible || deleteUserGroupBox.Visible || defaultEditGroupBox.Visible)
            {
                editUserGroupBox.Visible = false;
                deleteUserGroupBox.Visible = false;
                defaultEditGroupBox.Visible = false;
            }

            //Show Add user controls on the screen
            addUserGroupBox.Location = groupboxPoint;
            addUserGroupBox.Visible = true;
            addUserNameTextbox.Focus();
        }

        //--------------------------------------------------------
        //--------EDIT USER GROUP BOX CONTROLS--------------------
        //--------------------------------------------------------
        
        //Edit user button clicked
        private void editUserEditButton_Click(object sender, EventArgs e)
        {
            //Show message box to make sure user is to be edited
            DialogResult editMessage = MessageBox.Show("Do you want to edit the user " + editUserOldPersonName.Text + "?", "Edit User?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            //Check to see if user pressed yes or no
            if (editMessage == DialogResult.Yes)
            {
                //Yes Pressed, edit user in DB, check to make sure a name was entered
                if (editUserPeopleTB.Text.Length > 0 && !editUserPeopleTB.Text.Equals(editUserOldPersonName.Text))
                {
                    //Yes pressed and edit person
                    AddToDatabase editDB = new AddToDatabase();
                    if (editDB.EditPerson(editUserOldPersonName.Text, editUserPeopleTB.Text))
                    {
                        updateStatus.UpdateStatusBar("User " + editUserOldPersonName.Text + " updated successfully!",mainform);
                        CloseOpenGroupBox("edit");
                        UpdateListBox();
                    }else
                    {
                        updateStatus.UpdateStatusBar("User " + editUserOldPersonName.Text + " not updated", mainform);
                        CloseOpenGroupBox("edit");
                    }
                }
                
            }
            else if (editMessage == DialogResult.No)
            {
                //No Pressed, nothing will be done
                
            }
        }

        //Cancel edit button clicked
        private void editUserCancelButton_Click(object sender, EventArgs e)
        {
            CloseOpenGroupBox("edit");
        }

        //Text Changed in edit group box
        private void editUserPeopleTB_TextChanged(object sender, EventArgs e)
        {
            if(editUserPeopleTB.Text.Length > 0)
            {
                editUserEditButton.Enabled = true;
            }else
            {
                editUserEditButton.Enabled = false;
            }
        }
        
        //Enter pressed on edit textbox
        private void editUserPeopleTB_KeyDown(object sender, KeyEventArgs e)
        {
            //check to make sure enter was pressed
            if(e.KeyCode == Keys.Enter)
            {
                if(editUserGroupBox.Visible && editUserPeopleTB.Text.Length > 0 && editUserEditButton.Enabled)
                {
                    editUserEditButton.PerformClick();
                }
            }
        }

        //--------------------------------------------------------
        //--------ADD USER GROUP BOX CONTROLS---------------------
        //--------------------------------------------------------

        //add user add button clicked
        private void addUserAddButton_Click(object sender, EventArgs e)
        {
            if(addUserNameTextbox.Text.Length > 0)
            {
                AddToDatabase addUser = new AddToDatabase();
                PeopleValues peopleValues = new PeopleValues(addUserNameTextbox.Text);
                //Add user to database
                if (addUser.AddPerson(peopleValues))
                {
                    //user added successfully
                    updateStatus.UpdateStatusBar(addUserNameTextbox.Text + " added successfully", mainform);
                    CloseOpenGroupBox("add");
                    UpdateListBox();
                }else
                {
                    //user not added
                    updateStatus.UpdateStatusBar(addUserNameTextbox.Text + " not added", mainform);
                }
            }
        }

        //add user cancel button clicked
        private void addUserCancelButton_Click(object sender, EventArgs e)
        {
            CloseOpenGroupBox("add");
        }

        //add user textbox changed event
        private void addUserNameTextbox_TextChanged(object sender, EventArgs e)
        {
            if (addUserNameTextbox.Text.Length > 0)
            {
                addUserAddButton.Enabled = true;
            }
            else
            {
                addUserAddButton.Enabled = false;
            }
        }

        //Add user textbox enter pressed
        private void addUserNameTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            //check to make sure enter was pressed
            if(e.KeyCode == Keys.Enter)
            {
                //check to make sure GB is visible, textbox is not empty, add button is enabled
                if (addUserGroupBox.Visible && addUserNameTextbox.Text.Length > 0 && addUserAddButton.Enabled)
                {
                    addUserAddButton.PerformClick();
                }
            }
        }

        //--------------------------------------------------------
        //--------DELETE USER GROUP BOX CONTROLS------------------
        //--------------------------------------------------------

        //delete user Delete button clicked
        private void deleteUserDeleteButton_Click(object sender, EventArgs e)
        {
            //Show message box to make sure user is to be deleted
            DialogResult deleteMessage = MessageBox.Show("Do you want to delete the user " + deleteUserNameLabel.Text + "?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Check to see if user pressed yes or no
            if(deleteMessage == DialogResult.Yes)
            {
                //Yes Pressed, delete user from DB
                AddToDatabase deleteDB = new AddToDatabase();

                //Delete user from database
                if (deleteDB.DeletePerson(deleteUserNameLabel.Text))
                {
                    //deleteion success
                    updateStatus.UpdateStatusBar(deleteUserNameLabel.Text + " deleted!", mainform);
                    CloseOpenGroupBox("delete");
                    UpdateListBox();
                }else
                {
                    updateStatus.UpdateStatusBar("There was an error deleting " + deleteUserNameLabel.Text, mainform);
                    CloseOpenGroupBox("delete");
                }
            }else if(deleteMessage == DialogResult.No){
                //No Pressed, nothing will be done
            }
        }

        //delete user cancel button clicked
        private void deleteUserCancelButton_Click(object sender, EventArgs e)
        {
            CloseOpenGroupBox("delete");
        }




        //------------------------------------------
    }
}
