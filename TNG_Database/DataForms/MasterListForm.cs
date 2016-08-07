using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNG_Database.Values;

namespace TNG_Database
{
    public partial class MasterListForm : Form
    {
        private Point boxLocation = new Point(316, 40);
        private TNG_Database.MainForm mainform;
        private List<MasterListValues> masterList;
        private MasterListValues sendValues = new MasterListValues();

        UpdateStatus updateStatus = UpdateStatus.Instance();

        //Reference to CommonMethods
        CommonMethods commonMethod = CommonMethods.Instance();

        public MasterListForm()
        {
            InitializeComponent();
        }

        //Open Form Constructor
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

            //Load all the dropdowns
            LoadDropdowns();
        }

        //-------------------------------------------------
        //---------CLASS METHODS---------------------------
        //-------------------------------------------------
        #region ClassMethods
        /// <summary>
        /// Populate Master List Listbox with information from database
        /// </summary>
        private void PopulateMasterList()
        {
            //clear everything from listbox
            masterListListBox.Items.Clear();

            //Run method to get all items in Master list
            //DataBaseControls dbControls = new DataBaseControls();
            masterList = DataBaseControls.GetAllMasterListItems();

            //check to see if the first item is the default item
            if(masterList[0].MasterArchive.Equals("Nothing in database"))
            {
                //Nothing in database, default value posted
                masterListListBox.SelectionMode = SelectionMode.None;
                masterListListBox.Items.Add(masterList[0].MasterArchive);

                //Update Application Status
                updateStatus.UpdateStatusBar("Nothing in database", mainform);
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
                    updateStatus.UpdateStatusBar("Add Master Tape Selected", mainform);
                    break;
                case "edit":
                    defaultMasterGroupBox.Visible = false;
                    addMasterListGroupBox.Visible = false;
                    deleteMasterListGroupBox.Visible = false;
                    //Update Application Status
                    updateStatus.UpdateStatusBar("Edit Master Tape Selected", mainform);
                    break;
                case "delete":
                    defaultMasterGroupBox.Visible = false;
                    editMasterListGroupBox.Visible = false;
                    addMasterListGroupBox.Visible = false;
                    //Update Application Status
                    updateStatus.UpdateStatusBar("Delete Master Tape Selected", mainform);
                    break;
                default:
                    addMasterListGroupBox.Visible = false;
                    editMasterListGroupBox.Visible = false;
                    deleteMasterListGroupBox.Visible = false;
                    defaultMasterGroupBox.Visible = true;
                    //Update Application Status
                    updateStatus.UpdateStatusBar("Choose Item", mainform);
                    break;
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
                        cameraLabel.Text = commonMethod.GetCameraName(Convert.ToInt32(item.MasterMedia));
                    }
                }
            }
            else
            {
                //Update Application Status
                updateStatus.UpdateStatusBar("There was a problem", mainform);
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

        /// <summary>
        /// Loads all the dropdowns.
        /// </summary>
        private void LoadDropdowns()
        {
            string[] camera = commonMethod.CameraDropdownItems();

            cameraAddMasterCombo.Items.AddRange(camera);
            editCameraNewMasterDropdown.Items.AddRange(camera);
        }
        #endregion ClassMethods
        //---------------------------------------------------
        //--------ADD, DELETE, UPDATE BUTTONS PRESSED--------
        //---------------------------------------------------
        #region Add Edit Delete Buttons Pressed
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
            editCameraNewMasterDropdown.SelectedIndex = commonMethod.GetCameraDropdownIndex(editCameraOldMasterNameLabel.Text);

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
        #endregion
        //---------------------------------------------------
        //-------------EDIT GB METHODS-----------------------
        //---------------------------------------------------
        #region Edit GB Controls
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
                MasterListValues newValues = new MasterListValues(editNewNameMasterTextbox.Text, commonMethod.GetCameraNumber(editCameraNewMasterDropdown.GetItemText(editCameraNewMasterDropdown.SelectedItem)));

                //Send to update method in AddToDatabase class & check if successful
                if(database.UpdateMasterList(oldValues, newValues))
                {
                    //update successful
                    updateStatus.UpdateStatusBar("Update of Master Tape successful", mainform);

                    //Clear items and close groupbox
                    editNewNameMasterTextbox.Clear();
                    editCameraNewMasterDropdown.SelectedIndex = 0;
                    MakeGroupboxesInvisible();
                    //Load new database info into listbox
                    PopulateMasterList();
                }else
                {
                    //update failed
                    updateStatus.UpdateStatusBar("Update Error", mainform);
                }

            }
        }

        //Edit Groupbox cancel button pressed
        private void editMasterCancelButton_Click(object sender, EventArgs e)
        {
            MakeGroupboxesInvisible();
        }
        #endregion
        //---------------------------------------------------
        //--------------ADD GB METHODS-----------------------
        //---------------------------------------------------
        #region Add GB Controls
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
                sendValues = new MasterListValues(addMasterListNameTextbox.Text, commonMethod.GetCameraNumber(cameraAddMasterCombo.GetItemText(cameraAddMasterCombo.SelectedItem)));

                if (database.AddMasterList(sendValues))
                {
                    //Add entry success
                    updateStatus.UpdateStatusBar("New Master Tape added to database", mainform);
                    addMasterListNameTextbox.Clear();
                    cameraAddMasterCombo.SelectedIndex = 0;
                    MakeGroupboxesInvisible();
                    PopulateMasterList();
                }
                else
                {
                    //Add Entry failure
                    updateStatus.UpdateStatusBar("Entry was not added to database", mainform);
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
        #endregion
        //---------------------------------------------------
        //-------------DELETE GB METHODS---------------------
        //---------------------------------------------------
        #region Delete GB Controls
        //Delete Groupbox Delete button clicked
        private void deleteMasterListDeleteButton_Click(object sender, EventArgs e)
        {
            //Delete button pressed, gather info and delete entry
            //Show message box to make sure user is to be deleted
            DialogResult deleteMessage = MessageBox.Show("Do you want to delete " + deleteMasterNameMasterListLabel.Text + "?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                
                MasterListValues values = new MasterListValues(deleteMasterNameMasterListLabel.Text, commonMethod.GetCameraDropdownIndex(deleteCameraNameMasterListLabel.Text),_id);

                //Delete user from database
                if (deleteDB.DeleteMasterList(values))
                {
                    //deletion success
                    updateStatus.UpdateStatusBar(deleteMasterNameMasterListLabel.Text + " deleted!", mainform);
                    MakeGroupboxesInvisible("delete");
                    PopulateMasterList();
                }
                else
                {
                    updateStatus.UpdateStatusBar("There was an error deleting " + deleteMasterNameMasterListLabel.Text, mainform);
                    MakeGroupboxesInvisible("delete");
                }
            }
            else if (deleteMessage == DialogResult.No)
            {
                //No Pressed, nothing will be done
            }
        }

        //Delete Groupbox Cancel button clicked
        private void deleteMasterListCancelButton_Click(object sender, EventArgs e)
        {
            MakeGroupboxesInvisible();
        }
        #endregion



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
                updateStatus.UpdateStatusBar(sendValues.MasterArchive + " Selected", mainform);
            }
            else
            {
                ShowDefaultGroupboxNothingSelected();
            }
        }
        //-----------------------------------------------

    }
}
