using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class MasterListForm : Form, IUpdateApplicationStatus
    {
        private Point boxLocation = new Point(316, 40);
        private TNG_Database.MainForm mainform;
        private List<MasterListValues> masterList;
        private MasterListValues sendValues = new MasterListValues();

        public MasterListForm()
        {
            InitializeComponent();
        }

        public MasterListForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            //Load data into listbox
            PopulateMasterList();

            MakeGroupboxesInvisible();

            //Set Locations for all groupboxes
            addMasterListGroupBox.Location = boxLocation;
            editMasterListGroupBox.Location = boxLocation;
            deleteMasterListGroupBox.Location = boxLocation;

            //make edit and delete buttons disabled
            masterListEditButton.Enabled = false;
            masterListDeleteButton.Enabled = false;

            //make labels for tape invisible
            defaultArchiveMasterListLabel.Visible = false;
            defaultCameraMasterListLabel.Visible = false;
            defaultArchiveNameMasterListLabel.Visible = false;
            defaultCameraNameMasterListLabel.Visible = false;

            //Change name of default GB and make default label visible
            defaultMasterGroupBox.Text = "";
            defaultMasterGroupBox.Height = 41;
            defaultMasterListLabel.Visible = true;
        }

        //------------------------------------------
        //-------------INTERFACE METHODS------------
        //------------------------------------------

        //Interface variable that gets sets mainform
        MainForm IUpdateApplicationStatus.mainform
        {
            get { return mainform; }
            set { mainform = value; }
        }

        /// <summary>
        /// Interface Method that updates the status label on Mainform
        /// </summary>
        /// <param name="update"></param>
        public void UpdateApplicationStatus(string update)
        {
            mainform.applicationStatusLabel.Text = update;
        }

        //-------------------------------------------------
        //---------CLASS METHODS---------------------------
        //-------------------------------------------------

        /// <summary>
        /// Populate Master List Listbox with information from database
        /// </summary>
        private void PopulateMasterList()
        {
            //clear everything from listbox
            masterListListBox.Items.Clear();

            //Run method to get all items in Master list
            DataBaseControls dbControls = new DataBaseControls();
            masterList = dbControls.GetAllMasterListItems();

            //check to see if the first item is the default item
            if(masterList[0].MasterArchive.Equals("Nothing in database"))
            {
                //Nothing in database, default value posted
                masterListListBox.SelectionMode = SelectionMode.None;
                masterListListBox.Items.Add(masterList[0].MasterArchive);

                //Update Application Status
                UpdateApplicationStatus("Nothing in database");
            }
            else
            {
                //List not default, load all values in the listbox
                masterListListBox.SelectionMode = SelectionMode.One;

                //Iterate over everything
                foreach (MasterListValues values in masterList)
                {
                    masterListListBox.Items.Add(values.MasterArchive);
                }

                //Update Application Status
                UpdateApplicationStatus("Database Loaded!");

                masterListListBox.SelectedIndex = -1;
            }

            ShowDefaultGroupboxNothingSelected();
        }

        /// <summary>
        /// Make all groupboxes invisible depending on which one needs to be visible
        /// </summary>
        /// <param name="groupBox"></param>
        private void MakeGroupboxesInvisible(string groupBox = "")
        {
            switch (groupBox)
            {
                case "add":
                    defaultMasterGroupBox.Visible = false;
                    editMasterListGroupBox.Visible = false;
                    deleteMasterListGroupBox.Visible = false;
                    //Update Application Status
                    UpdateApplicationStatus("Add Master Tape Selected");
                    break;
                case "edit":
                    defaultMasterGroupBox.Visible = false;
                    addMasterListGroupBox.Visible = false;
                    deleteMasterListGroupBox.Visible = false;
                    //Update Application Status
                    UpdateApplicationStatus("Edit Master Tape Selected");
                    break;
                case "delete":
                    defaultMasterGroupBox.Visible = false;
                    editMasterListGroupBox.Visible = false;
                    addMasterListGroupBox.Visible = false;
                    //Update Application Status
                    UpdateApplicationStatus("Delete Master Tape Selected");
                    break;
                default:
                    addMasterListGroupBox.Visible = false;
                    editMasterListGroupBox.Visible = false;
                    deleteMasterListGroupBox.Visible = false;
                    defaultMasterGroupBox.Visible = true;
                    //Update Application Status
                    UpdateApplicationStatus("Choose Item");
                    break;
            }
        }

        /// <summary>
        /// Return a string for media device
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        private string GetMediaDevice(Int32 media)
        {
            switch (media)
            {
                case 1:
                    return "XDCam";
                case 2:
                    return "Cannon";
                default:
                    return "Standard";
            }
        }

        /// <summary>
        /// Get name and camera for selected item and make label to reflect that
        /// </summary>
        /// <param name="nameLabel"></param>
        /// <param name="cameraLabel"></param>
        private void LabelSelectedItem(Label nameLabel, Label cameraLabel)
        {
            //check to make sure masterList is not empty
            if (masterList != null)
            {
                //Iterate through master list
                foreach (MasterListValues item in masterList)
                {
                    //If name in master list matches item selected, then get name and camera info
                    if (masterListListBox.GetItemText(masterListListBox.SelectedItem).Equals(item.MasterArchive))
                    {

                        sendValues = item;
                        nameLabel.Text = item.MasterArchive;
                        cameraLabel.Text = GetMediaDevice(Convert.ToInt32(item.MasterMedia));
                    }
                }
            }
            else
            {
                //Update Application Status
                UpdateApplicationStatus("There was a problem");
            }
        }

        /// <summary>
        /// Gets the camera value.
        /// </summary>
        /// <param name="camera">Name of camera selected</param>
        /// <returns>int of the camera to store in db</returns>
        private int GetCameraValue(string camera)
        {
            switch (camera)
            {
                case "Cannon":
                    return 2;
                case "XDCam":
                    return 1;
                case "Beta":
                    return 3;
                case "DVC":
                    return 4;
                default:
                    return 0;

            }
        }

        /// <summary>
        /// Gets the index of the camera.
        /// </summary>
        /// <param name="camera">Name of the camera selected</param>
        /// <returns></returns>
        private int GetCameraIndex(string camera)
        {
            switch (camera)
            {
                case "Cannon":
                    return 0;
                case "XDCam":
                    return 1;
                case "Beta":
                    return 2;
                case "DVC":
                    return 3;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Shows the default groupbox with nothing is selected in the listbox.
        /// </summary>
        private void ShowDefaultGroupboxNothingSelected()
        {
            //Nothing selected
            MakeGroupboxesInvisible();
            masterListDeleteButton.Enabled = false;
            masterListEditButton.Enabled = false;

            //make labels for tape invisible
            defaultArchiveMasterListLabel.Visible = false;
            defaultCameraMasterListLabel.Visible = false;
            defaultArchiveNameMasterListLabel.Visible = false;
            defaultCameraNameMasterListLabel.Visible = false;

            //Change name of default GB and make default label visible
            defaultMasterGroupBox.Text = "";
            defaultMasterGroupBox.Height = 41;
            defaultMasterListLabel.Visible = true;
        }

        //---------------------------------------------------
        //--------ADD, DELETE, UPDATE BUTTONS PRESSED--------
        //---------------------------------------------------

        //Add button pressed
        private void masterListAddButton_Click(object sender, EventArgs e)
        {
            MakeGroupboxesInvisible("add");

            //add components properties to groupbox
            cameraAddMasterCombo.SelectedIndex = 0;
            addMasterListNameTextbox.Clear();
            addMasterListAddButton.Enabled = false;
            
            //Make add GB visible
            addMasterListGroupBox.Visible = true;

        }

        //Edit button pressed
        private void masterListEditButton_Click(object sender, EventArgs e)
        {
            //Make every other groupbox invisible
            MakeGroupboxesInvisible("edit");

            //add components properties to groupbox
            editCameraNewMasterDropdown.SelectedIndex = 0;
            editNewNameMasterTextbox.Clear();
            editMasterEditButton.Enabled = false;

            //Label name and camera of item selected
            LabelSelectedItem(editOldNameMasterLabel, editCameraOldMasterNameLabel);

            //set the text in textbox equal to name of Master tape
            editNewNameMasterTextbox.Text = editOldNameMasterLabel.Text;

            //Set dropdown equal to value of the tape
            editCameraNewMasterDropdown.SelectedIndex = GetCameraIndex(editCameraOldMasterNameLabel.Text);

            //make edit GB visible
            editMasterListGroupBox.Visible = true;
        }

        //Delete button pressed
        private void masterListDeleteButton_Click(object sender, EventArgs e)
        {
            //Make every other groupbox invisible
            MakeGroupboxesInvisible("delete");

            //Label Name and Camera of item selected
            LabelSelectedItem(deleteMasterNameMasterListLabel, deleteCameraNameMasterListLabel);

            //Make delete groupbox visible
            deleteMasterListGroupBox.Visible = true;
        }







        //---------------------------------------------------
        //-------------EDIT GB METHODS-----------------------
        //---------------------------------------------------
        //Edit Camera Dropdown keypress event
        private void editCameraNewMasterDropdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //Edit Camera Dropdown Dropdown closed
        private void editCameraNewMasterDropdown_DropDownClosed(object sender, EventArgs e)
        {
            editNewMasterNameLabel.Focus();
        }

        //Edit Textbox text changed
        private void editNewNameMasterTextbox_TextChanged(object sender, EventArgs e)
        {
            if (editNewNameMasterTextbox.Text.Length > 0)
            {
                editMasterEditButton.Enabled = true;
            }
            else
            {
                editMasterEditButton.Enabled = false;
            }
        }

        //Edit Groupbox edit button pressed
        private void editMasterEditButton_Click(object sender, EventArgs e)
        {
            //Edit Entry button pressed, gather info and update entry
            //check to make sure something is entered in the textbox
            if(editNewNameMasterTextbox.Text.Length > 0)
            {
                AddToDatabase database = new AddToDatabase();

                //Create MasterListValues of old info
                MasterListValues oldValues = new MasterListValues(sendValues.MasterArchive, sendValues.MasterMedia, sendValues.ID);

                //Create MasterListValues of old info
                MasterListValues newValues = new MasterListValues(editNewNameMasterTextbox.Text, GetCameraValue(editCameraNewMasterDropdown.GetItemText(editCameraNewMasterDropdown.SelectedItem)));

                //Send to update method in AddToDatabase class & check if successful
                if(database.UpdateMasterList(oldValues, newValues))
                {
                    //update successful
                    UpdateApplicationStatus("Update of Master Tape successful");

                    //Clear items and close groupbox
                    editNewNameMasterTextbox.Clear();
                    editCameraNewMasterDropdown.SelectedIndex = 0;
                    MakeGroupboxesInvisible();
                    //Load new database info into listbox
                    PopulateMasterList();
                }else
                {
                    //update failed
                    UpdateApplicationStatus("Update Error");
                }

            }
        }

        //Edit Groupbox cancel button pressed
        private void editMasterCancelButton_Click(object sender, EventArgs e)
        {
            MakeGroupboxesInvisible();
        }

        //---------------------------------------------------
        //--------------ADD GB METHODS-----------------------
        //---------------------------------------------------
        //Add Dropdown Keypress event
        private void cameraAddMasterCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //Add Dropdown closed
        private void cameraAddMasterCombo_DropDownClosed(object sender, EventArgs e)
        {
            cameraAddMasterListLabel.Focus();
        }

        //Add Groupbox Add button clicked
        private void addMasterListAddButton_Click(object sender, EventArgs e)
        {
            //Add Entry button pressed, gather info and add entry
            if(addMasterListNameTextbox.Text.Length > 0)
            {
                AddToDatabase database = new AddToDatabase();

                if (database.AddMasterList(addMasterListNameTextbox.Text, GetCameraValue(cameraAddMasterCombo.GetItemText(cameraAddMasterCombo.SelectedItem))))
                {
                    //Add entry success
                    UpdateApplicationStatus("New Master Tape added to database");
                    addMasterListNameTextbox.Clear();
                    cameraAddMasterCombo.SelectedIndex = 0;
                    MakeGroupboxesInvisible();
                    PopulateMasterList();
                }
                else
                {
                    //Add Entry failure
                    UpdateApplicationStatus("Entry was not added to database");
                }
            }
            
        }

        //Add Groupbox Cancel button clicked
        private void addMasterListCancelButton_Click(object sender, EventArgs e)
        {
            MakeGroupboxesInvisible();
        }

        //Add Textbox text changed
        private void addMasterListNameTextbox_TextChanged(object sender, EventArgs e)
        {
            if(addMasterListNameTextbox.Text.Length > 0)
            {
                addMasterListAddButton.Enabled = true;
            }else
            {
                addMasterListAddButton.Enabled = false;
            }
        }



        //---------------------------------------------------
        //-------------DELETE GB METHODS---------------------
        //---------------------------------------------------

        //Delete Groupbox Delete button clicked
        private void deleteMasterListDeleteButton_Click(object sender, EventArgs e)
        {
            //Delete button pressed, gather info and delete entry
            //Show message box to make sure user is to be deleted
            DialogResult deleteMessage = MessageBox.Show("Do you want to delete the user " + deleteMasterNameMasterListLabel.Text + "?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Check to see if user pressed yes or no
            if (deleteMessage == DialogResult.Yes)
            {
                //Yes Pressed, delete user from DB
                Console.WriteLine("Yes Pressed for deletion");

                AddToDatabase deleteDB = new AddToDatabase();

                int _id = 0;
                
                foreach(MasterListValues item in masterList)
                {
                    if (item.MasterArchive.Equals(deleteMasterNameMasterListLabel.Text))
                    {
                        _id = item.ID;
                    }
                }
                
                MasterListValues values = new MasterListValues(deleteMasterNameMasterListLabel.Text, GetCameraIndex(deleteCameraNameMasterListLabel.Text),_id);

                //Delete user from database
                if (deleteDB.DeleteMasterList(values))
                {
                    //deletion success
                    UpdateApplicationStatus(deleteMasterNameMasterListLabel.Text + " deleted!");
                    MakeGroupboxesInvisible("delete");
                    PopulateMasterList();
                }
                else
                {
                    UpdateApplicationStatus("There was an error deleting " + deleteMasterNameMasterListLabel.Text);
                    MakeGroupboxesInvisible("delete");
                }
            }
            else if (deleteMessage == DialogResult.No)
            {
                //No Pressed, nothing will be done
                Console.WriteLine("No Pressed for deletion");
            }
        }

        //Delete Groupbox Cancel button clicked
        private void deleteMasterListCancelButton_Click(object sender, EventArgs e)
        {
            MakeGroupboxesInvisible();
        }




        //-----------------------------------------------------
        //ListBox selection change
        private void masterListListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(masterListListBox.SelectedIndex != -1)
            {
                //Selection made
                //Make everything invisible except default GB
                MakeGroupboxesInvisible();

                //enable edit and delete buttons
                masterListDeleteButton.Enabled = true;
                masterListEditButton.Enabled = true;

                //Change name of default GB and make default label invisible
                defaultMasterGroupBox.Text = "Current Selection";
                defaultMasterGroupBox.Height = 109;
                defaultMasterListLabel.Visible = false;

                //Load selection name and camera into default labels
                LabelSelectedItem(defaultArchiveNameMasterListLabel, defaultCameraNameMasterListLabel);
                
                //Make labels for tape visible
                defaultArchiveMasterListLabel.Visible = true;
                defaultCameraMasterListLabel.Visible = true;
                defaultArchiveNameMasterListLabel.Visible = true;
                defaultCameraNameMasterListLabel.Visible = true;

                //Update Application Status
                UpdateApplicationStatus(sendValues.MasterArchive + " Selected");
            }
            else
            {
                ShowDefaultGroupboxNothingSelected();
            }
        }

        







        //-----------------------------------------------

    }
}
