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
    public partial class MasterArchiveVideosForm : Form
    {
        //location for the groupboxes
        private Point boxLocation = new Point(431, 48);

        //current index of item selected in listview
        int listViewIndex = -1;

        //reference for the mainform
        private TNG_Database.MainForm mainform;

        //string reference for Master Dropdown
        string[] masterDropDown;

        //current item selected in list view
        MasterTapeValues listValues = new MasterTapeValues();

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();

        //list to delete multiple selections
        List<MasterArchiveVideoValues> archivesToDelete = null;

        public MasterArchiveVideosForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            //Load listview
            PopulateListView();

            //Form specific actions

            //add textbox changed event
            addArchiveIDTextBox.TextChanged += AddProjectTextBox_TextChanged;
            addArchiveNameTextBox.TextChanged += AddProjectTextBox_TextChanged;
            addArchiveClipNumberTextbox.TextChanged += AddProjectTextBox_TextChanged;

            //max length of clip textboxes
            addArchiveClipNumberTextbox.MaxLength = 3;
            editArchiveClipNumberTextbox.MaxLength = 3;

            //Keypress for digits only
            addArchiveClipNumberTextbox.KeyPress += ArchiveClipNumberTextbox_KeyPress;
            editArchiveClipNumberTextbox.KeyPress += ArchiveClipNumberTextbox_KeyPress;

            //make selected item always visible
            archiveListView.HideSelection = false;

            //get values for master dropdown
            masterDropDown = DataBaseControls.GetMasterListForDropdown();
            addArchiveMasterTapeComboBox.Items.AddRange(masterDropDown);
            editArchiveMasterTapeComboBox.Items.AddRange(masterDropDown);

            //close all groupboxes
            CloseGroupBox();

            //Event for sorting each column
            CommonMethods.ListViewItemComparer.SortColumn = -1;
            archiveListView.ColumnClick += new ColumnClickEventHandler(CommonMethods.ListViewItemComparer.SearchListView_ColumnClick);
        }

        private void ArchiveClipNumberTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void AddProjectTextBox_TextChanged(object sender, EventArgs e)
        {
            //check to make sure both textboxes have something in them
            if (addArchiveIDTextBox.Text.Length > 0 && addArchiveNameTextBox.Text.Length > 0 && addArchiveClipNumberTextbox.Text.Length > 0)
            {
                //Not empty
                addArchiveAddButton.Enabled = true;
            }
            else
            {
                //empty
                addArchiveAddButton.Enabled = false;
            }
        }

        //-------------------------------------------------------
        //-------------------CLASS METHODS-----------------------
        //-------------------------------------------------------
        #region Class Methods
        /// <summary>
        /// Populates the ListView.
        /// </summary>
        private void PopulateListView()
        {
            //clear list
            archiveListView.Items.Clear();

            //Get list of all projects
            List<MasterTapeValues> values = DataBaseControls.GetAllArchiveVideoValues();

            if (values.Any())
            {
                //Iterate over list and add to listview
                foreach (MasterTapeValues value in values)
                {
                    archiveListView.Items.Add(new ListViewItem(new string[] { value.ProjectID, value.VideoName, value.MasterTape, value.ClipNumber.ToString() })).Tag = Convert.ToInt32(value.ID);
                }

                //clear selection
                archiveListView.SelectedIndices.Clear();

                updateStatus.UpdateStatusBar(values.Count + " Archive Videos(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("No Projects in database", mainform);
            }

            archiveDeleteButton.Text = "Delete";
        }

        /// <summary>
        /// Closes the current group box and opens specified groupbox
        /// </summary>
        /// <param name="name">Groupbox to open, default blank</param>
        private void CloseGroupBox(string name = "")
        {
            switch (name.ToLower())
            {
                case "add":
                    archiveAddGroupBox.Location = boxLocation;
                    deleteArchiveGroupBox.Visible = false;
                    editArchiveGroupBox.Visible = false;
                    archiveDefaultGroupBox.Visible = false;
                    addArchiveAddButton.Enabled = false;
                    archiveAddGroupBox.Visible = true;
                    addArchiveIDTextBox.Focus();
                    break;
                case "edit":
                    editArchiveGroupBox.Location = boxLocation;
                    deleteArchiveGroupBox.Visible = false;
                    archiveDefaultGroupBox.Visible = false;
                    archiveAddGroupBox.Visible = false;
                    editArchiveGroupBox.Visible = true;
                    editArchiveCancelButton.Focus();
                    break;
                case "delete":
                    deleteArchiveGroupBox.Location = boxLocation;
                    archiveDefaultGroupBox.Visible = false;
                    archiveAddGroupBox.Visible = false;
                    editArchiveGroupBox.Visible = false;
                    deleteArchiveGroupBox.Visible = true;
                    deleteArchiveCancelButton.Focus();
                    break;
                default:
                    archiveDefaultGroupBox.Location = boxLocation;
                    archiveAddGroupBox.Visible = false;
                    editArchiveGroupBox.Visible = false;
                    deleteArchiveGroupBox.Visible = false;
                    archiveDefaultGroupBox.Visible = true;
                    UpdateDefaultBox();
                    break;
            }

        }

        //-----------------------------------------------

        /// <summary>
        /// Updates the default box.
        /// </summary>
        private void UpdateDefaultBox()
        {
            //checks to make sure an item is selected
            if (archiveListView.SelectedItems.Count > 0)
            {
                //set value to current index
                listViewIndex = Convert.ToInt32(archiveListView.SelectedItems[0].Tag);

                //Update Status label
                updateStatus.UpdateStatusBar(archiveListView.SelectedItems[0].SubItems[0].Text + " item selected", mainform,0);

                //set items in default panel to item selected
                defaultArchiveIDLabel.Text = archiveListView.SelectedItems[0].SubItems[0].Text;
                defaultArchiveNameLabel.Text = archiveListView.SelectedItems[0].SubItems[1].Text;
                defaultArchiveMasterTapeLabel.Text = archiveListView.SelectedItems[0].SubItems[2].Text;
                defaultArchiveClipNumberLabel.Text = archiveListView.SelectedItems[0].SubItems[3].Text;


                //swap default views
                defaultLabel.Visible = false;
                defaultLabelPanel.Visible = true;

                //make edit and delete buttons enabled
                archiveDeleteButton.Enabled = true;
                archiveUpdateButton.Enabled = true;
            }
            else
            {
                //set value to current index
                listViewIndex = -1;

                //update status label
                //updateStatus.UpdateStatusBar("Nothing Selected", mainform);

                //make default label visible and default panel invisible
                defaultLabelPanel.Visible = false;
                defaultLabel.Visible = true;

                //make edit and delete buttons disabled
                archiveUpdateButton.Enabled = false;
                archiveDeleteButton.Enabled = false;

                //set default values to blank
                defaultArchiveIDLabel.Text = "";
                defaultArchiveNameLabel.Text = "";
                defaultArchiveMasterTapeLabel.Text = "";
                defaultArchiveClipNumberLabel.Text = "";
            }
        }

        #endregion

        //---------------------------------------------------------
        //----------ADD, EDIT, DELETE BUTTONS PREsSED--------------
        //---------------------------------------------------------
        #region Add, Edit, Delete Buttons Pressed

        //Add Button Pressed
        private void archiveAddButton_Click(object sender, EventArgs e)
        {
            //clear textboxes
            addArchiveNameTextBox.Clear();
            addArchiveIDTextBox.Clear();
            addArchiveNameTextBox.Clear();
            addArchiveMasterTapeComboBox.SelectedIndex = 0;
            //open groupbox
            CloseGroupBox("add");
        }
        
        //Update Button Pressed
        private void archiveUpdateButton_Click(object sender, EventArgs e)
        {
            //set textboxes to selected values in listview
            editArchiveIDTextBox.Text = listValues.ProjectID;
            editArchiveNameTextBox.Text = listValues.VideoName;
            editArchiveMasterTapeComboBox.Text = listValues.MasterTape;
            editArchiveClipNumberTextbox.Text = listValues.ClipNumber;

            //open edit groupbox and close others
            CloseGroupBox("edit");

            //give focus to the cancel button
            editArchiveCancelButton.Focus();
        }

        //Delete Button Pressed
        private void archiveDeleteButton_Click(object sender, EventArgs e)
        {
            if(archiveListView.SelectedItems.Count == 1)
            {
                //set labels to seleted values in listview
                deleteArchiveIDLabel.Text = listValues.ProjectID;
                deleteArchiveNameLabel.Text = listValues.VideoName;
                deleteArchiveMasterTapeLabel.Text = listValues.MasterTape;
                deleteArchiveClipLabel.Text = listValues.ClipNumber;

                //open delete groupbox
                CloseGroupBox("delete");

                //give the cancel button focus
                deleteArchiveCancelButton.Focus();
            }
            else if (archiveListView.SelectedItems.Count > 1)
            {

                //multiple items selected in listview

                //Show message box to make sure user is to be deleted
                DialogResult deleteMessage = MessageBox.Show("Do you want to delete these " + archiveListView.SelectedItems.Count + " entries?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //Check to see if user pressed yes or no
                if (deleteMessage == DialogResult.Yes)
                {

                    //clear delete list
                    if (archivesToDelete == null)
                    {
                        archivesToDelete = new List<MasterArchiveVideoValues>();
                    }
                    else
                    {
                        archivesToDelete.Clear();
                    }

                    //iterate over each item selected and save data in a value, then a list
                    foreach (ListViewItem item in archiveListView.SelectedItems)
                    {
                        MasterArchiveVideoValues value = new MasterArchiveVideoValues(
                            item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, Convert.ToInt32(item.Tag)
                            );

                        archivesToDelete.Add(value);
                    }



                    if (archiveListView.SelectedItems.Count > 1 && archivesToDelete.Count > 0)
                    {
                        Console.WriteLine("sending " + archivesToDelete.Count + " items to delete");
                        updateStatus.UpdateStatusBar(AddToDatabase.DeleteMultipleMasterArchiveselected(archivesToDelete) + " items deleted", mainform);
                    }

                    PopulateListView();
                    CloseGroupBox();

                }
                else if (deleteMessage == DialogResult.No)
                {
                    //No Pressed, nothing will be done
                }
        }

            
        }

        #endregion

        //----------------------------------------------
        //-----------------ADD PROJECT GROUP BOX--------
        //----------------------------------------------
        #region Add Groupbox
        //Add button pressed
        private void addArchiveAddButton_Click(object sender, EventArgs e)
        {
            //check to make sure something is entered in the textboxes
            if (addArchiveIDTextBox.Text.Length > 0 && addArchiveNameTextBox.Text.Length > 0)
            {
                //instatiate values and database class
                MasterArchiveVideoValues addValues = new MasterArchiveVideoValues(addArchiveIDTextBox.Text, addArchiveNameTextBox.Text, addArchiveMasterTapeComboBox.Text, Convert.ToInt32(addArchiveClipNumberTextbox.Text).ToString("000"));
                AddToDatabase addDB = new AddToDatabase();

                //add to database and check if successful
                if (addDB.AddMasterArchiveVideo(addValues))
                {
                    //success

                    //clear textboxes
                    addArchiveIDTextBox.Clear();
                    addArchiveNameTextBox.Clear();
                    addArchiveClipNumberTextbox.Clear();
                    addArchiveMasterTapeComboBox.SelectedIndex = 0;
                    //populate list view
                    PopulateListView();
                    //open default groupbox
                    CloseGroupBox();

                    //give listview focus
                    archiveListView.Focus();

                    updateStatus.UpdateStatusBar("Project " + addValues.ProjectId + " Added to Database", mainform);
                }
                else
                {
                    //failed
                    updateStatus.UpdateStatusBar("There was a problem, please try again", mainform);
                }
            }
            else
            {
                //at least one text box was empty
                updateStatus.UpdateStatusBar("You Must Enter Values!", mainform);
            }
        }

        //Cancel Button pressed
        private void addArchiveCancelButton_Click(object sender, EventArgs e)
        {
            //clear textboxes
            addArchiveIDTextBox.Clear();
            addArchiveNameTextBox.Clear();
            addArchiveClipNumberTextbox.Clear();
            addArchiveMasterTapeComboBox.SelectedIndex = 0;
            //close groupbox and open default
            CloseGroupBox();
        }

        #endregion

        //----------------------------------------------
        //-----------------Edit PROJECT GROUP BOX-------
        //----------------------------------------------
        #region Edit Groupbox
        //Edit button pressed
        private void editArchiveEditButton_Click(object sender, EventArgs e)
        {
            AddToDatabase editDB = new AddToDatabase();
            MasterTapeValues editValues = new MasterTapeValues(editArchiveIDTextBox.Text, editArchiveNameTextBox.Text, editArchiveMasterTapeComboBox.Text, editArchiveClipNumberTextbox.Text, listValues.ID);

            if (editArchiveIDTextBox.Text.Length > 0 && editArchiveNameTextBox.Text.Length > 0 && editArchiveClipNumberTextbox.Text.Length > 0)
            {
                if (editDB.EditMasterArchiveVideo(listValues, editValues))
                {
                    //success

                    //clear textboxes
                    editArchiveIDTextBox.Clear();
                    editArchiveNameTextBox.Clear();
                    editArchiveClipNumberTextbox.Clear();
                    editArchiveMasterTapeComboBox.SelectedIndex = 0;

                    //update list view
                    PopulateListView();

                    //close edit groupbox and open default
                    CloseGroupBox();

                    //give listview focus
                    archiveListView.Focus();

                    updateStatus.UpdateStatusBar("Project " + editValues.ProjectID + " Updated in Database", mainform);
                }
                else
                {
                    //failed
                    updateStatus.UpdateStatusBar("There was a problem, please try again", mainform);
                }
            }
            else
            {
                //at least one textbox is empty
                updateStatus.UpdateStatusBar("You Must Enter Values!", mainform);
            }
        }
        
        //Cancel button pressed
        private void editArchiveCancelButton_Click(object sender, EventArgs e)
        {
            //clear edit textboxes
            editArchiveIDTextBox.Clear();
            editArchiveNameTextBox.Clear();
            editArchiveClipNumberTextbox.Clear();
            editArchiveMasterTapeComboBox.SelectedIndex = 0;

            //close edit groupbox
            CloseGroupBox();
        }

        #endregion

        //----------------------------------------------
        //--------------DELETE ARCHIVE GROUP BOX--------
        //----------------------------------------------
        #region Delete Groupbox
        //Delete button pressed
        private void deleteArchiveDeleteButton_Click(object sender, EventArgs e)
        {
            //Delete button pressed, gather info and delete entry
            //Show message box to make sure user is to be deleted
            DialogResult deleteMessage = MessageBox.Show("Do you want to delete the entry " + listValues.ProjectID + ": " + listValues.VideoName + "?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Check to see if user pressed yes or no
            if (deleteMessage == DialogResult.Yes)
            {
                //Yes Pressed, delete user from DB
                Console.WriteLine("Yes Pressed for deletion");

                AddToDatabase deleteDB = new AddToDatabase();

                //Delete user from database
                if (deleteDB.DeleteMasterArchiveVideo(listValues))
                {
                    //deletion success
                    listValues.Clear();

                    //clear text labels
                    deleteArchiveIDLabel.Text = "";
                    deleteArchiveNameLabel.Text = "";
                    deleteArchiveMasterTapeLabel.Text = "";
                    deleteArchiveClipLabel.Text = "";

                    PopulateListView();
                    CloseGroupBox();
                    archiveListView.Focus();
                    updateStatus.UpdateStatusBar("Entry deleted", mainform);
                }
                else
                {
                    updateStatus.UpdateStatusBar("There was an error deleting entry", mainform);
                    CloseGroupBox();
                    archiveListView.Focus();
                }
            }
            else if (deleteMessage == DialogResult.No)
            {
                //No Pressed, nothing will be done
            }
        }
        
        //Cancel button pressed
        private void deleteArchiveCancelButton_Click(object sender, EventArgs e)
        {
            //clear text labels
            deleteArchiveIDLabel.Text = "";
            deleteArchiveNameLabel.Text = "";
            deleteArchiveMasterTapeLabel.Text = "";
            deleteArchiveClipLabel.Text = "";

            //close delete groupbox
            CloseGroupBox();
        }

        #endregion


        //Listview index changed
        private void archiveListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //updates default groupbox based on listview selection
            CloseGroupBox();
            if (archiveListView.SelectedItems.Count == 1)
            {
                listValues.ID = Convert.ToInt32(archiveListView.SelectedItems[0].Tag);
                listValues.ProjectID = archiveListView.SelectedItems[0].SubItems[0].Text;
                listValues.VideoName = archiveListView.SelectedItems[0].SubItems[1].Text;
                listValues.MasterTape = archiveListView.SelectedItems[0].SubItems[2].Text;
                listValues.ClipNumber = archiveListView.SelectedItems[0].SubItems[3].Text;

                //make selected panel visible
                defaultLabel.Visible = false;
                defaultLabelPanel.Visible = true;
                archiveDeleteButton.Text = "Delete";
            }
            else if (archiveListView.SelectedItems.Count > 1)
            {
                //more than one item selected
                archiveUpdateButton.Enabled = false;
                archiveDeleteButton.Enabled = true;
                archiveDeleteButton.Text = "Delete(" + archiveListView.SelectedItems.Count + ")";
                updateStatus.UpdateStatusBar("Delete(" + archiveListView.SelectedItems.Count + ") Archive Entries", mainform,0);
                defaultLabelPanel.Visible = false;
                defaultLabel.Visible = true;
            }
            else if(archiveListView.SelectedItems.Count == 0)
            {
                //make default nothing selected label visible
                defaultLabelPanel.Visible = false;
                defaultLabel.Visible = true;
                archiveDeleteButton.Text = "Delete";
            }
        }


    }
}
