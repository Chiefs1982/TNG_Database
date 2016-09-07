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

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();

        //list to capture multiple selected itemms for deletion
        List<PeopleValues> peopleValues = null;

        //focus values
        FirstFocusValues focusValues = new FirstFocusValues();
        
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

            //Got Focus For Controls
            addUserNameTextbox.GotFocus += AddUserNameTextbox_GotFocus;
            editUserPeopleTB.GotFocus += EditUserPeopleTB_GotFocus;

            //Lost Focus For Controls
            addUserNameTextbox.LostFocus += AddUserNameTextbox_LostFocus;
            editUserPeopleTB.LostFocus += EditUserPeopleTB_LostFocus;


            peopleFormListBox.SelectionMode = SelectionMode.MultiExtended;

            focusValues.Reset();
        }

        

        

        //------------------------------------------------
        //--------PEOPLEFORM METHODS----------------------
        //------------------------------------------------
        #region Class Methods

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

            updateStatus.UpdateStatusBar("Nothing Selected", mainform,0);
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
                SetDefaultControls("all");
                CloseOpenGroupBox("default");
                updateStatus.UpdateStatusBar(peopleFormListBox.SelectedItem.ToString() + " selected", mainform,0);
            }
            else if (peopleFormListBox.SelectedItems.Count > 1)
            {
                //more than one item selected
                SetDefaultControls("all");
                CloseOpenGroupBox("default");

                editUserPeopleButton.Enabled = false;
                deleteUserPeopleButton.Enabled = true;
                deleteUserPeopleButton.Text = "Delete(" + peopleFormListBox.SelectedItems.Count + ")";
                updateStatus.UpdateStatusBar(peopleFormListBox.SelectedItems.Count + " users selected", mainform,0);
            }
            else if(peopleFormListBox.SelectedItems.Count == 0)
            {
                //more than one item selected
                SetDefaultControls("all");
                CloseOpenGroupBox("default");

                editUserPeopleButton.Enabled = false;
                deleteUserPeopleButton.Enabled = false;

                deleteUserPeopleButton.Text = "Delete User";
            }
        }

        /// <summary>
        /// Updates the ListBox.
        /// </summary>
        public void UpdateListBox()
        {
            PopulateUserList();
            peopleFormListBox.ClearSelected();

            //disable delete and edit user until user is selected
            editUserPeopleButton.Enabled = false;
            deleteUserPeopleButton.Enabled = false;

        }
    
        /// <summary>
        /// Clear and close Open GroupBox.
        /// </summary>
        /// <param name="groupBoxOpen">The group box open.</param>
        private void CloseOpenGroupBox(string groupBoxOpen = "")
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

        /// <summary>
        /// Sets the default controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        private void SetDefaultControls(string controls)
        {
            switch (controls.ToLower())
            {
                case "add":
                    commonMethod.BackColorDefault(addUserNameTextbox);
                    break;
                case "edit":
                    commonMethod.BackColorDefault(editUserPeopleTB);
                    break;
                case "all":
                    commonMethod.BackColorDefault(addUserNameTextbox);
                    commonMethod.BackColorDefault(editUserPeopleTB);
                    break;
            }
        }

        /// <summary>
        /// Sets the error controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        private void SetErrorControls(string controls)
        {
            switch (controls.ToLower())
            {
                case "add":
                    commonMethod.BackColorError(addUserNameTextbox);
                    break;
                case "edit":
                    commonMethod.BackColorError(editUserPeopleTB);
                    break;
            }
        }

        #endregion

        //---------------------------------------------------------
        //---ADD, EDIT, DELETE USER FROM DATABASE BUTTON CLICKS----
        //---------------------------------------------------------
        #region Add, Update, Delte Button pressed

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

            //reset focus values and make all controls default color
            focusValues.Reset();
            SetDefaultControls("edit");
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

            //reset focus values and make all controls default color
            focusValues.Reset();
            SetDefaultControls("add");
        }

        #endregion

        //--------------------------------------------------------
        //--------EDIT USER GROUP BOX CONTROLS--------------------
        //--------------------------------------------------------

        #region Edit Controls
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
            if(editUserPeopleTB.TextLength > 0)
            {
                editUserEditButton.Enabled = true;
                commonMethod.BackColorDefault(editUserPeopleTB);
            }
            else if (editUserPeopleTB.TextLength == 0 && !focusValues.PersonEntered)
            {
                commonMethod.BackColorDefault(editUserPeopleTB);
            }
            else
            {
                if (editUserPeopleTB.TextLength == 0 && focusValues.PersonEntered)
                {
                    commonMethod.BackColorError(editUserPeopleTB);
                }

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

        //Focus Controls
        private void EditUserPeopleTB_LostFocus(object sender, EventArgs e)
        {
            focusValues.PersonEntered = true;

            //set control color based on textbox entry
            if (editUserPeopleTB.TextLength == 0)
            {
                commonMethod.BackColorError(editUserPeopleTB);
            }
            else
            {
                commonMethod.BackColorDefault(editUserPeopleTB);
            }
        }

        private void EditUserPeopleTB_GotFocus(object sender, EventArgs e)
        {
            //set textbox to default color
            commonMethod.BackColorDefault(editUserPeopleTB);
        }

        #endregion

        //--------------------------------------------------------
        //--------ADD USER GROUP BOX CONTROLS---------------------
        //--------------------------------------------------------

        #region Add Controls

        //add user add button clicked
        private void addUserAddButton_Click(object sender, EventArgs e)
        {
            if(addUserNameTextbox.TextLength > 0)
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
            if (addUserNameTextbox.TextLength > 0)
            {
                addUserAddButton.Enabled = true;
                commonMethod.BackColorDefault(addUserNameTextbox);
            }
            else if (addUserNameTextbox.TextLength == 0 && !focusValues.PersonEntered)
            {
                commonMethod.BackColorDefault(addUserNameTextbox);
            }
            else
            {
                if (addUserNameTextbox.TextLength == 0 && focusValues.PersonEntered)
                {
                    commonMethod.BackColorError(addUserNameTextbox);
                }

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

        //Focus Controls
        private void AddUserNameTextbox_LostFocus(object sender, EventArgs e)
        {
            focusValues.PersonEntered = true;

            //set control color based on textbox entry
            if (addUserNameTextbox.TextLength == 0)
            {
                commonMethod.BackColorError(addUserNameTextbox);
            }
            else
            {
                commonMethod.BackColorDefault(addUserNameTextbox);
            }
        }

        private void AddUserNameTextbox_GotFocus(object sender, EventArgs e)
        {
            //set textbox to default color
            commonMethod.BackColorDefault(addUserNameTextbox);
        }

        #endregion

        //--------------------------------------------------------
        //--------DELETE USER GROUP BOX CONTROLS------------------
        //--------------------------------------------------------

        #region Delete Controls

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

        #endregion



        //------------------------------------------
    }
}
