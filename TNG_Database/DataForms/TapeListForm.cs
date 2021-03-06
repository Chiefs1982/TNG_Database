﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNG_Database.Values;

namespace TNG_Database
{
    public partial class TapeListForm : Form
    {
        //location for the groupboxes
        private Point boxLocation = new Point(15, 355);
        //reference for the mainform
        private TNG_Database.MainForm mainform;
        private List<TapeDatabaseValues> tapeListValues;

        //placeholders for Tape Values
        private TapeDatabaseValues tapeValues = new TapeDatabaseValues();
        private TapeDatabaseValues listValues = new TapeDatabaseValues();

        //current listview selection
        private int listViewIndex = -1;

        //Tag Lists
        private List<string> addTagList = new List<string>();
        private List<string> editTagList = new List<string>();
        private List<string> deleteTagList = new List<string>();
        private List<string> defaultTagList = new List<string>();

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();

        //tag default text
        private string tagText = "Seperate each tag with a comma";
        private string defaultNoText = "Select an item from the list to Edit or Delete";

        //List of mulitple items to delete
        List<TapeDatabaseValues> tapesToDelete = null;

        //default tooltip
        ToolTip toolTip = new ToolTip();

        //values for checking if user went to a control
        FirstFocusValues focusValues = new FirstFocusValues();

        //bool to check if button should pressed
        bool buttonToPress = false;

        public TapeListForm()
        {
            InitializeComponent();
        }

        public TapeListForm(TNG_Database.MainForm parent, bool addEntry = false)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            PopulateTapeList();

            tapeListEditEntryButton.Enabled = false;
            tapeListDeleteEntryButton.Enabled = false;

            //set default items to appropriate visibility
            defaultItemsPanel.Visible = false;
            defaultNoItemSelectedLabel.Visible = true;

            //set default label to default value
            defaultNoItemSelectedLabel.Text = defaultNoText;
            defaultNoItemSelectedLabel.Visible = true;

            //disable all groupboxes except default
            addTapeGroupbox.Visible = false;
            deleteTapeGroupbox.Visible = false;
            editTapeGroupbox.Visible = false;
            defaultTapeGroupbox.Visible = true;

            //Populate all dropdowns
            LoadDropdowns();

            //Attach all add textboxes to an event
            addProjectIDTextbox.TextChanged += addTextBoxes_TextChanged;
            addTapeNameTextbox.TextChanged += addTextBoxes_TextChanged;
            addTagsTextbox.TextChanged += addTextBoxes_TextChanged;
            addCameraComboBox.SelectedIndexChanged += addTextBoxes_TextChanged;
            addTapeNumUpDown.ValueChanged += addTextBoxes_TextChanged;
            addDateDateTime.ValueChanged += addTextBoxes_TextChanged;

            //Project ID lost focus
            addProjectIDTextbox.LostFocus += AddProjectIDTextbox_LostFocus;
            editProjectIDTextbox.LostFocus += EditProjectIDTextbox_LostFocus;

            //Attach all edit textboxes to an event
            editProjectIDTextbox.TextChanged += editTextBoxes_TextChanged;
            editTapeNameTextbox.TextChanged += editTextBoxes_TextChanged;
            editTagsTextbox.TextChanged += editTextBoxes_TextChanged;
            editCameraDropdown.SelectedIndexChanged += editTextBoxes_TextChanged;
            editTapeNumberUpDown.ValueChanged += editTextBoxes_TextChanged;
            editDateShotDate.ValueChanged += editTextBoxes_TextChanged;

            //focus given to to tag textbox
            addTagsTextbox.GotFocus += AddTagsTextbox_GotFocus;
            editTagsTextbox.GotFocus += AddTagsTextbox_GotFocus;
            //losing focus
            addTagsTextbox.LostFocus += AddTagsTextbox_LostFocus;
            editTagsTextbox.LostFocus += AddTagsTextbox_LostFocus;

            //set lost focus for all other controls.
            addTapeNameTextbox.LostFocus += AddTapeNameTextbox_LostFocus;
            addCameraComboBox.LostFocus += AddCameraComboBox_LostFocus;
            addTapeNumUpDown.LostFocus += AddTapeNumUpDown_LostFocus;
            addDateDateTime.LostFocus += AddDateDateTime_LostFocus;
            editTapeNameTextbox.LostFocus += EditTapeNameTextbox_LostFocus;
            editCameraDropdown.LostFocus += EditCameraDropdown_LostFocus;
            editTapeNumberUpDown.LostFocus += EditTapeNumberUpDown_LostFocus;
            editDateShotDate.LostFocus += EditDateShotDate_LostFocus;

            //set Got Focus methods
            addProjectIDTextbox.GotFocus += Controls_GotFocus;
            addTapeNameTextbox.GotFocus += Controls_GotFocus;
            addCameraComboBox.GotFocus += Controls_GotFocus;
            addTapeNumUpDown.GotFocus += Controls_GotFocus;
            addDateDateTime.GotFocus += Controls_GotFocus;
            editProjectIDTextbox.GotFocus += Controls_GotFocus;
            editTapeNameTextbox.GotFocus += Controls_GotFocus;
            editCameraDropdown.GotFocus += Controls_GotFocus;
            editTapeNumberUpDown.GotFocus += Controls_GotFocus;
            editDateShotDate.GotFocus += Controls_GotFocus;


            //Tooltips
            //add
            addTagsTextbox.MouseHover += AddTagsTextbox_MouseHover;
            addTagsTextbox.MouseLeave += AddTagsTextbox_MouseLeave;
            //edit
            editTagsTextbox.MouseHover += AddTagsTextbox_MouseHover;
            editTagsTextbox.MouseLeave += AddTagsTextbox_MouseLeave;

            //keep items highlighted
            tapeListListView.HideSelection = false;

            //Clear all controls
            ClearAddControls();
            ClearDeleteLabels();
            ClearDeleteLabels();

            //Event for sorting each column
            CommonMethods.ListViewItemComparer.SortColumn = -1;
            tapeListListView.ColumnClick += new ColumnClickEventHandler(CommonMethods.ListViewItemComparer.SearchListView_ColumnClick);

            buttonToPress = addEntry;
        }

        
        //-------------------------------------------
        //------------CLASS METHODS------------------
        //-------------------------------------------
        #region ClassMethods
        /// <summary>
        /// Populates the tape list in the listview.
        /// </summary>
        private void PopulateTapeList()
        {
            //clear listview
            tapeListListView.Items.Clear();

            //Run method to get all items in Master list
            DataBaseControls database = new DataBaseControls();
            tapeListValues = database.GetAllTapeValues();

            if (tapeListValues[0].ID.Equals(0))
            {
                //nothing was in database to return
                updateStatus.UpdateStatusBar("There are no items in the database", mainform,0);
            }
            else
            {
                //List returned values, populate listview
                foreach (TapeDatabaseValues values in tapeListValues)
                {
                    tapeListListView.Items.Add(new ListViewItem(new string[] { values.ProjectId, values.ProjectName, values.TapeName, values.TapeNumber, commonMethod.GetCameraName(values.Camera), values.TapeTags, values.DateShot, values.MasterArchive, values.PersonEntered })).Tag = Convert.ToInt32(values.ID);
                    //Tell user the database is ready for use
                    updateStatus.UpdateStatusBar("Database is Loaded and Ready", mainform, 0);
                }
            }
            
        }
        //----------------------------------------------------        
        /// <summary>
        /// Makes the box visible and the others invisible.
        /// </summary>
        /// <param name="box">Name of box to make visible</param>
        private void MakeBoxesVisible(string box = "")
        {
            switch (box)
            {
                case "add":
                    addTapeGroupbox.Location = boxLocation;
                    deleteTapeGroupbox.Visible = false;
                    editTapeGroupbox.Visible = false;
                    defaultTapeGroupbox.Visible = false;
                    addTapeAddButton.Enabled = false;
                    addTapeGroupbox.Visible = true;
                    addProjectIDTextbox.Focus();
                    break;
                case "edit":
                    editTapeGroupbox.Location = boxLocation;
                    deleteTapeGroupbox.Visible = false;
                    defaultTapeGroupbox.Visible = false;
                    addTapeGroupbox.Visible = false;
                    editTapeGroupbox.Visible = true;
                    editTapeCancelButton.Focus();
                    break;
                case "delete":
                    deleteTapeGroupbox.Location = boxLocation;
                    defaultTapeGroupbox.Visible = false;
                    addTapeGroupbox.Visible = false;
                    editTapeGroupbox.Visible = false;
                    deleteTapeGroupbox.Visible = true;
                    deleteTapeCancelButton.Focus();
                    break;
                default:
                    defaultTapeGroupbox.Location = boxLocation;
                    addTapeGroupbox.Visible = false;
                    editTapeGroupbox.Visible = false;
                    deleteTapeGroupbox.Visible = false;
                    defaultTapeGroupbox.Visible = true;
                    UpdateDefaultBox();
                    tapeListDeleteEntryButton.Text = "Delete Entry";
                    break;
            }
        }
        //-------------------------------------------------        
        /// <summary>
        /// Updates the default box.
        /// </summary>
        private void UpdateDefaultBox()
        {
            //checks to make sure an item is selected
            if (tapeListListView.SelectedItems.Count == 1)
            {
                //set value to current index
                listViewIndex = tapeListListView.SelectedIndices[0];

                //Update Status label
                updateStatus.UpdateStatusBar(tapeListListView.SelectedItems[0].SubItems[0].Text + " item selected", mainform,0);

                //set items in default panel to item selected
                defaultProjectIDLabel.Text = tapeListListView.SelectedItems[0].SubItems[0].Text;
                defaultProjectNameLabel.Text = tapeListListView.SelectedItems[0].SubItems[1].Text;
                defaultTapeNameLabel.Text = tapeListListView.SelectedItems[0].SubItems[2].Text;
                defaultTapeNumberLabel.Text = tapeListListView.SelectedItems[0].SubItems[3].Text;
                defaultCameraLabel.Text = tapeListListView.SelectedItems[0].SubItems[4].Text;
                //Split csv into list and display
                defaultTagList = tapeListListView.SelectedItems[0].SubItems[5].Text.Split(',').ToList();
                DisplayTags("default", defaultTagFlowLayoutPanel, defaultTagList);
                defaultDateLabel.Text = tapeListListView.SelectedItems[0].SubItems[6].Text;
                defaultMasterArchiveLabel.Text = tapeListListView.SelectedItems[0].SubItems[7].Text;
                defaultPersonLabel.Text = tapeListListView.SelectedItems[0].SubItems[8].Text;

                //swap default views
                defaultNoItemSelectedLabel.Visible = false;
                defaultItemsPanel.Visible = true;

                //make edit and delete buttons enabled
                tapeListEditEntryButton.Enabled = true;
                tapeListDeleteEntryButton.Enabled = true;
            }
            else if (tapeListListView.SelectedItems.Count > 1)
            {
                //make default label visible and default panel invisible
                defaultItemsPanel.Visible = false;
                defaultNoItemSelectedLabel.Visible = true;

                //set default label to display number of items
                defaultNoItemSelectedLabel.Visible = false;
                updateStatus.UpdateStatusBar(tapeListListView.SelectedItems.Count + " items selected", mainform,0);
            }
            else
            {
                //set value to current index
                listViewIndex = -1;

                //update status label
                //updateStatus.UpdateStatusBar("Nothing Selected", mainform);

                //make default label visible and default panel invisible
                defaultItemsPanel.Visible = false;
                defaultNoItemSelectedLabel.Visible = true;

                //set default label to default value
                defaultNoItemSelectedLabel.Text = defaultNoText;
                defaultNoItemSelectedLabel.Visible = true;

                //make edit and delete buttons disabled
                tapeListEditEntryButton.Enabled = false;
                tapeListDeleteEntryButton.Enabled = false;

                //set default values to blank
                defaultProjectIDLabel.Text = "";
                defaultProjectNameLabel.Text = "";
                defaultTapeNameLabel.Text = "";
                defaultTapeNumberLabel.Text = "";
                defaultCameraLabel.Text = "";
                defaultDateLabel.Text = "";
                defaultMasterArchiveLabel.Text = "";
                defaultPersonLabel.Text = "";
                //clear tag controls
                defaultTagList.Clear();
                defaultTagFlowLayoutPanel.Controls.Clear();
            }
        }
        //--------------------------------------------------        
        /// <summary>
        /// Loads all the dropdowns with default values from database.
        /// </summary>
        private void LoadDropdowns()
        {
            //Set up method in Control Database to get person and master list to populate dropdowns
            string[] people = DataBaseControls.GetPersonListForDropdown();
            string[] cameraDropdowns = commonMethod.CameraDropdownItems();

            //load values into camera dropdowns
            addCameraComboBox.Items.AddRange(cameraDropdowns);
            editCameraDropdown.Items.AddRange(cameraDropdowns);
            //load values into person dropdowns
            addTapePersonDropdown.Items.AddRange(people);
            editPersonDropdown.Items.AddRange(people);
        }

        /// <summary>
        /// Clears the add controls.
        /// </summary>
        private void ClearAddControls()
        {
            addProjectIDTextbox.Clear();
            addTapeListProjectName.Text = "";
            addTapeNameTextbox.Clear();
            addTagsTextbox.Clear();
            addTapeNumUpDown.Value = 1;
            addCameraComboBox.SelectedIndex = 0;
            addTapeMasterArchiveLabel.Text = "";
            addTapePersonDropdown.SelectedIndex = 0;
            addTagList.Clear();
            addTagDisplayFlowLayout.Controls.Clear();
        }

        /// <summary>
        /// Clears the edit controls.
        /// </summary>
        private void ClearEditControls()
        {
            editProjectIDTextbox.Clear();
            editProjectNameLabel.Text = "";
            editTapeNameTextbox.Clear();
            editTagsTextbox.Clear();
            editTapeNumberUpDown.Value = 1;
            editCameraDropdown.SelectedIndex = 0;
            editTapeMasterListLabel.Text = "";
            editPersonDropdown.SelectedIndex = 0;
            editTageFlowLayoutPanel.Controls.Clear();
        }

        /// <summary>
        /// Clears the delete labels.
        /// </summary>
        private void ClearDeleteLabels()
        {
            //Clear all labels in the delete groupbox
            deleteProjectIDLabel.Text = "";
            deleteProjectNameLabel.Text = "";
            deleteTapeNameLabel.Text = "";
            deleteTapeNumberLabel.Text = "";
            deleteCameraLabel.Text = "";
            deleteDateShotLabel.Text = "";
            deleteMasterArchiveLabel.Text = "";
            deletePersonLabel.Text = "";
            deleteTagList.Clear();
            deleteTagFlowLayoutPanel.Controls.Clear();
        }

        /// <summary>
        /// Loads the tape values from list.
        /// </summary>
        private void LoadTapeValuesFromList()
        {
            tapeValues.ID = Convert.ToInt32(tapeListListView.SelectedItems[0].Tag);
            tapeValues.ProjectId = tapeListListView.SelectedItems[0].SubItems[0].Text;
            tapeValues.ProjectName = tapeListListView.SelectedItems[0].SubItems[1].Text;
            tapeValues.TapeName = tapeListListView.SelectedItems[0].SubItems[2].Text;
            tapeValues.TapeNumber = tapeListListView.SelectedItems[0].SubItems[3].Text;
            tapeValues.Camera = commonMethod.GetCameraNumber(tapeListListView.SelectedItems[0].SubItems[4].Text);
            tapeValues.TapeTags = tapeListListView.SelectedItems[0].SubItems[5].Text;
            tapeValues.DateShot = tapeListListView.SelectedItems[0].SubItems[6].Text;
            tapeValues.MasterArchive = tapeListListView.SelectedItems[0].SubItems[7].Text;
            tapeValues.PersonEntered = tapeListListView.SelectedItems[0].SubItems[8].Text;
        }

        /// <summary>
        /// Compares the old edit values.
        /// </summary>
        /// <param name="newValues">The new tape values.</param>
        /// <returns>True if they do not match</returns>
        private bool CompareOldEditValues(TapeDatabaseValues newValues)
        {
            //check to see if old and new values match
            if(tapeValues.ProjectId.Equals(newValues.ProjectId) && tapeValues.ProjectName.Equals(newValues.ProjectName) &&
                tapeValues.TapeName.Equals(newValues.TapeName) && tapeValues.TapeNumber.Equals(newValues.TapeNumber) &&
                tapeValues.Camera.Equals(newValues.Camera) && tapeValues.DateShot.Equals(newValues.DateShot) &&
                tapeValues.TapeTags.Equals(newValues.TapeTags) && tapeValues.MasterArchive.Equals(newValues.MasterArchive) &&
                tapeValues.PersonEntered.Equals(newValues.PersonEntered))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Displays the tags.
        /// </summary>
        /// <param name="gb">groupbox target</param>
        /// <param name="gbPanel">the FlowLayoutPanel to add items to</param>
        /// <param name="tagList">the tag list to use</param>
        private void DisplayTags(string gb, FlowLayoutPanel gbPanel, List<string> tagList)
        {
            //clear panel
            gbPanel.Controls.Clear();
            PictureBox pb;

            //iterate over all items in list and add them to the panel
            for (int i = 0; i < tagList.Count(); i++)
            {
                //create Picturebox variable
                

                //Create FLP for individual tags, set properties
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.AutoSize = true;
                flp.BackColor = Color.FromArgb(77, 77, 76);
                flp.ForeColor = Color.White;
                flp.Margin = new Padding(5, 1, 5, 1);
                flp.Padding = new Padding(2);

                //add Label of the tag and set properties
                Label addTagLabel = new Label();
                addTagLabel.Text = tagList[i].ToString();
                addTagLabel.Margin = new Padding(5, 0, 2, 0);
                addTagLabel.AutoSize = true;
                addTagLabel.Tag = i; //tag set to value of the index

                //add label to the FLP
                flp.Controls.Add(addTagLabel);

                //check to see if call is for delete or default
                if (gb.Equals("add") || gb.Equals("edit"))
                {
                    //add a picturebox for the closing feature and set properties
                    pb = new PictureBox();
                    pb.ImageLocation = @"icons\closeBox.png";
                    pb.Size = new Size(16, 12);
                    pb.Margin = new Padding(0);
                    pb.Padding = new Padding(0);
                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.Cursor = Cursors.Hand;
                    pb.Tag = i;  //tag set to value of the index

                    //check to see what function gets the call
                    switch (gb)
                    {
                        case "add":
                            pb.Click += PbAdd_Click;
                            break;
                        case "edit":
                            pb.Click += PbEdit_Click;
                            break;
                        default:
                            break;
                    }

                    flp.Controls.Add(pb);
                }
                
                //add the FLP to the larger FLP to display tags as seperate items
                gbPanel.Controls.Add(flp);
            }
        }

        private void SetDefaultColors(string controls)
        {
            switch (controls.ToLower())
            {
                case "edit":
                    //set default colors for edit controls
                    commonMethod.BackColorDefault(editProjectIDTextbox);
                    commonMethod.BackColorDefault(editTapeNameTextbox);
                    commonMethod.BackColorDefault(editTagsTextbox);
                    commonMethod.BackColorDefault(editTapeNumberUpDown);
                    commonMethod.BackColorDefault(editDateShotDate);
                    commonMethod.BackColorDefault(editCameraDropdown);
                    break;
                case "add":
                    //set default colors for add controls
                    commonMethod.BackColorDefault(addProjectIDTextbox);
                    commonMethod.BackColorDefault(addTapeNameTextbox);
                    commonMethod.BackColorDefault(addTagsTextbox);
                    commonMethod.BackColorDefault(addTapeNumUpDown);
                    commonMethod.BackColorDefault(addDateDateTime);
                    commonMethod.BackColorDefault(addCameraComboBox);
                    break;
            }
        }

        private void SetBackColorError(string controls)
        {
            switch (controls.ToLower())
            {
                case "edit":
                    //set error colors for edit controls
                    commonMethod.BackColorError(editProjectIDTextbox);
                    commonMethod.BackColorError(editTapeNameTextbox);
                    commonMethod.BackColorError(editTagsTextbox);
                    commonMethod.BackColorError(editTapeNumberUpDown);
                    commonMethod.BackColorError(editDateShotDate);
                    commonMethod.BackColorError(editCameraDropdown);
            break;
                case "add":
                    //change color of control based on the error
                    commonMethod.BackColorError(addProjectIDTextbox);
                    commonMethod.BackColorError(addTapeNameTextbox);
                    commonMethod.BackColorError(addTagsTextbox);
                    commonMethod.BackColorError(addTapeNumUpDown);
                    commonMethod.BackColorError(addDateDateTime);
                    commonMethod.BackColorError(addCameraComboBox);
                    break;
            }
        }
        
        #endregion

        //--------------------------------------------------
        //-------ADD, EDIT & DELETE BUTTONS PRESSED---------
        //--------------------------------------------------
        #region AddEditDeleteButtons

        //Add new entry button pressed
        private void tapeListAddNewButton_Click(object sender, EventArgs e)
        {
            //Place add tape groupbox and set add button to disabled until data is entered
            MakeBoxesVisible("add");
            addCameraComboBox.SelectedIndex = 0;
            addTapeNumUpDown.Value = 1;
            addTapePersonDropdown.Text = ComputerInfo.ComputerUser;
            //default text and color
            addTagsTextbox.ForeColor = SystemColors.GrayText;
            addTagsTextbox.Text = tagText;

            //set controls to default colors
            SetDefaultColors("add");

            //set focus values
            focusValues.Reset();
        }

        //Edit entry button pressed
        private void tapeListEditEntryButton_Click(object sender, EventArgs e)
        {
            //Place edit tape groupbox and make visible
            MakeBoxesVisible("edit");

            LoadTapeValuesFromList();
            editProjectIDTextbox.Text = tapeValues.ProjectId;
            editProjectNameLabel.Text = tapeValues.ProjectName;
            editTapeNameTextbox.Text = tapeValues.TapeName;
            editTapeNumberUpDown.Text = tapeValues.TapeNumber;
            editCameraDropdown.SelectedIndex = commonMethod.GetCameraDropdownIndex(commonMethod.GetCameraName(tapeValues.Camera));
            //split csv into a list and display
            editTagList = tapeValues.TapeTags.Split(',').ToList();
            DisplayTags("edit", editTageFlowLayoutPanel, editTagList);
            editDateShotDate.Value = commonMethod.ConvertDateForDatePicker(tapeValues.DateShot);
            editTapeMasterListLabel.Text = tapeValues.MasterArchive;
            editPersonDropdown.Text = tapeValues.PersonEntered;
            //default text and color
            editTagsTextbox.ForeColor = SystemColors.GrayText;
            editTagsTextbox.Text = tagText;

            //set controls to default colors
            SetDefaultColors("edit");

            //set focus values
            focusValues.Reset();
        }

        //Delete entry button pressed
        private void tapeListDeleteEntryButton_Click(object sender, EventArgs e)
        {
            //Place delete tape groupbox and make visible
            if (tapeListListView.SelectedItems.Count == 1)
            {

                LoadTapeValuesFromList();
                deleteProjectIDLabel.Text = tapeValues.ProjectId;
                deleteProjectNameLabel.Text = tapeValues.ProjectName;
                deleteTapeNameLabel.Text = tapeValues.TapeName;
                deleteTapeNumberLabel.Text = tapeValues.TapeNumber;
                deleteCameraLabel.Text = commonMethod.GetCameraName(tapeValues.Camera);
                //split csv int a list and display
                deleteTagList = tapeValues.TapeTags.Split(',').ToList();
                DisplayTags("delete", deleteTagFlowLayoutPanel, deleteTagList);
                deleteDateShotLabel.Text = tapeValues.DateShot;
                deleteMasterArchiveLabel.Text = tapeValues.MasterArchive;
                deletePersonLabel.Text = tapeValues.PersonEntered;
                MakeBoxesVisible("delete");
            }else if(tapeListListView.SelectedItems.Count > 1)
            {
                //multiple items selected in listview

                //Show message box to make sure user is to be deleted
                DialogResult deleteMessage = MessageBox.Show("Do you want to delete these " + tapeListListView.SelectedItems.Count + " entries?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //Check to see if user pressed yes or no
                if (deleteMessage == DialogResult.Yes)
                {

                    //clear delete list
                    if (tapesToDelete == null)
                    {
                        tapesToDelete = new List<TapeDatabaseValues>();
                    }
                    else
                    {
                        tapesToDelete.Clear();
                    }

                    //iterate over each item selected and save data in a value, then a list
                    foreach (ListViewItem item in tapeListListView.SelectedItems)
                    {
                        TapeDatabaseValues value = new TapeDatabaseValues(
                            item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[0].Text, item.SubItems[1].Text,
                            commonMethod.GetCameraNumber(item.SubItems[4].Text), item.SubItems[5].Text, item.SubItems[6].Text,
                            item.SubItems[7].Text, item.SubItems[8].Text, Convert.ToInt32(item.Tag)
                            );

                        tapesToDelete.Add(value);
                    }



                    if (tapeListListView.SelectedItems.Count > 1 && tapesToDelete.Count > 0)
                    {
                        Debug.WriteLine("sending " + tapesToDelete.Count + " items to delete");
                        updateStatus.UpdateStatusBar(AddToDatabase.DeleteMultipleTapeSelected(tapesToDelete) + " items deleted", mainform);
                    }

                    PopulateTapeList();
                    MakeBoxesVisible();

                }
                else if (deleteMessage == DialogResult.No)
                {
                    //No Pressed, nothing will be done
                }
            }

        }
        #endregion

        //--------------------------------------------------------
        //-------------ADD GB CONTROLS----------------------------
        //--------------------------------------------------------
        #region AddGBControls

        //Add Button pressed
        private void addTapeAddButton_Click(object sender, EventArgs e)
        {
            //check if project id is a number
            if (commonMethod.StringIsANumber(addProjectIDTextbox.Text))
            {
                
                //check to make sure that all info is entered
                if (addProjectIDTextbox.Text.Length > 0
                    && addTapeNameTextbox.Text.Length > 0 && (addTagList.Count > 0 || addTagsTextbox.TextLength > 0)
                    && addTapeNumUpDown.Value > 0 && addCameraComboBox.Text.Length > 0
                    && addTapePersonDropdown.Text.Length > 0)
                {
                    //load tape values to add to database
                    tapeValues.ProjectId = addProjectIDTextbox.Text;
                    if (addTapeListProjectName.Text.Length > 0)
                    {
                        tapeValues.ProjectName = addTapeListProjectName.Text;
                    }
                    else
                    {
                        tapeValues.ProjectName = addTapeNameTextbox.Text;
                    }

                    tapeValues.TapeName = addTapeNameTextbox.Text;
                    tapeValues.TapeNumber = addTapeNumUpDown.Value.ToString();
                    tapeValues.Camera = commonMethod.GetCameraNumber(addCameraComboBox.Text);
                    //if there is text in tags textbox then add it on the end of the tag string
                    if (addTagsTextbox.TextLength > 0 && !addTagList.Contains(addTagsTextbox.Text.ToLower().Replace(",", "")))
                    {
                        addTagList.Add(addTagsTextbox.Text);
                    }
                    tapeValues.TapeTags = String.Join(",", addTagList);
                    tapeValues.MasterArchive = addTapeMasterArchiveLabel.Text;
                    tapeValues.DateShot = commonMethod.ConvertDateFromDropdownForDB(addDateDateTime.Value);
                    tapeValues.PersonEntered = addTapePersonDropdown.Text;

                    //Add to database and check to make sure it is added
                    AddToDatabase addDB = new AddToDatabase();
                    if (addDB.AddTapeDatabase(tapeValues))
                    {
                        //update status and clear all controls and variables
                        updateStatus.UpdateStatusBar("Tape Added to Database", mainform);
                        PopulateTapeList();
                        tapeValues.Clear();
                        ClearAddControls();
                        MakeBoxesVisible();
                        tapeListListView.Focus();

                    }
                }
                else
                {
                    updateStatus.UpdateStatusBar("Please Fill Out All Fields", mainform);
                }
            }else
            {
                updateStatus.UpdateStatusBar("Project ID must be a number", mainform);
            }
        }
        //Cancel Button pressed
        private void addTapeCancelButton_Click(object sender, EventArgs e)
        {
            ClearAddControls();
            MakeBoxesVisible();
            tapeListListView.Focus();
        }

        //Add Dropdown Controls
        #region AddDropdownControls
        //Camera combobox closed
        private void addCameraComboBox_DropDownClosed(object sender, EventArgs e)
        {
            label3.Focus();
        }
        //Camera combobox key press negate
        private void addCameraComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        //Tape Master combo closed
        private void addTapeMasterArchiveDropdown_DropDownClosed(object sender, EventArgs e)
        {
            label6.Focus();
        }
        //Tape Master combobox key press negate
        private void addTapeMasterArchiveDropdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        //Person combo closed
        private void addTapePersonDropdown_DropDownClosed(object sender, EventArgs e)
        {
            label7.Focus();
        }
        //Person combobox key press negate
        private void addTapePersonDropdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        #endregion

        #endregion

        //-----------------------------------------------------
        //------------EDIT GB CONTROLS-------------------------
        //-----------------------------------------------------
        #region EditGBControls

        //Edit button pressed
        private void editTapeEditButton_Click(object sender, EventArgs e)
        {
            //check to see if project ID is a number
            if (commonMethod.StringIsANumber(editProjectIDTextbox.Text))
            {
                AddToDatabase editDB = new AddToDatabase();
                string projectNameEdit = "";

                if (editProjectNameLabel.Text.Length > 0)
                {
                    projectNameEdit = editProjectNameLabel.Text;
                }
                else
                {
                    projectNameEdit = editTapeNameTextbox.Text;
                }

                //if there is text in tags textbox then add it on the end of the tag string
                if (editTagsTextbox.TextLength > 0 && !editTagList.Contains(editTagsTextbox.Text.ToLower().Replace(",", "")))
                {
                    editTagList.Add(editTagsTextbox.Text);
                }

                //Create new TapeDatabaseValues for edited entry
                TapeDatabaseValues newTapeValues = new TapeDatabaseValues(
                    editTapeNameTextbox.Text, editTapeNumberUpDown.Value.ToString(), editProjectIDTextbox.Text, projectNameEdit,
                    commonMethod.GetCameraNumber(editCameraDropdown.Text), String.Join(",", editTagList), commonMethod.ConvertDateFromDropdownForDB(editDateShotDate.Value),
                    editTapeMasterListLabel.Text, editPersonDropdown.Text);

                //Check if user made a change
                if (CompareOldEditValues(newTapeValues))
                {
                    //Update entry and check to make sure it is updated
                    if (editDB.UpdateTapeDatabase(newTapeValues, tapeValues))
                    {
                        //Enrty Update Successful
                        ClearEditControls();
                        tapeValues.Clear();
                        PopulateTapeList();
                        MakeBoxesVisible();
                        tapeListListView.Focus();
                        updateStatus.UpdateStatusBar("Values Updated in Database", mainform);

                    }
                    else
                    {
                        //Entry Update Unsuccessful
                        updateStatus.UpdateStatusBar("There Was A Problem Updated Entry", mainform);
                    }
                }
                else
                {
                    //User Needs to change a value
                    updateStatus.UpdateStatusBar("Must Change At Least One Value", mainform);
                }
            }else
            {
                //project ID was NOT a number
                updateStatus.UpdateStatusBar("Project ID must be a number", mainform);
            }
            
        }
        //Edit cancel button pressed
        private void editTapeCancelButton_Click(object sender, EventArgs e)
        {
            ClearEditControls();
            MakeBoxesVisible();
            tapeListListView.Focus();
        }

        //Key press and dropdown focus
        #region KeyAndDropdown
        private void editCameraDropdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void editCameraDropdown_DropDownClosed(object sender, EventArgs e)
        {
            label21.Focus();
        }

        private void editMasterArchiveDropdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void editMasterArchiveDropdown_DropDownClosed(object sender, EventArgs e)
        {
            label9.Focus();
        }

        private void editPersonDropdown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void editPersonDropdown_DropDownClosed(object sender, EventArgs e)
        {
            label8.Focus();
        }
        #endregion

        #endregion

        //---------------------------------------------------
        //------------DELETE GB CONTROLS---------------------
        //---------------------------------------------------
        #region DeleteGBControls

        //Delete Button Pressed
        private void deleteTapeDeleteButton_Click(object sender, EventArgs e)
        {
            //Delete button pressed, gather info and delete entry
            //Show message box to make sure user is to be deleted
            DialogResult deleteMessage = MessageBox.Show("Do you want to delete the entry " + tapeValues.ProjectId + ": "+ tapeValues.ProjectName + "?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Check to see if user pressed yes or no
            if (deleteMessage == DialogResult.Yes)
            {
                //Yes Pressed, delete user from DB
                Debug.WriteLine("Yes Pressed for deletion");

                AddToDatabase deleteDB = new AddToDatabase();

                if(tapeListListView.SelectedItems.Count == 1)
                {
                    //Delete tape from database
                    if (deleteDB.DeleteTapeDatabase(tapeValues))
                    {
                        //deletion success
                        tapeValues.Clear();
                        ClearDeleteLabels();
                        PopulateTapeList();
                        MakeBoxesVisible();
                        tapeListListView.Focus();
                        updateStatus.UpdateStatusBar("Entry deleted", mainform);
                    }
                    else
                    {
                        updateStatus.UpdateStatusBar("There was an error deleting entry", mainform);
                        MakeBoxesVisible();
                        tapeListListView.Focus();
                    }
                }
                
            }
            else if (deleteMessage == DialogResult.No)
            {
                //No Pressed, nothing will be done
            }
        }

        //Cancel Button Pressed
        private void deleteTapeCancelButton_Click(object sender, EventArgs e)
        {
            ClearDeleteLabels();
            MakeBoxesVisible();
            tapeListListView.Focus();
        }

        #endregion

        //---------------------------------------------------
        //------------------ALL OTHER CONTROL METHODS--------
        //---------------------------------------------------
        #region AllOtherMethods
        //listview item selected changed
        private void tapeListListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //updates default groupbox based on listview selection
            MakeBoxesVisible();
            if (tapeListListView.SelectedItems.Count == 1)
            {
                listValues.ID = Convert.ToInt32(tapeListListView.SelectedItems[0].Tag);
                listValues.ProjectId = tapeListListView.SelectedItems[0].SubItems[0].Text;
                listValues.ProjectName = tapeListListView.SelectedItems[0].SubItems[1].Text;
                listValues.TapeName = tapeListListView.SelectedItems[0].SubItems[2].Text;
                listValues.TapeNumber = tapeListListView.SelectedItems[0].SubItems[3].Text;
                listValues.Camera = commonMethod.GetCameraNumber(tapeListListView.SelectedItems[0].SubItems[4].Text);
                listValues.TapeTags = tapeListListView.SelectedItems[0].SubItems[5].Text;
                listValues.DateShot = tapeListListView.SelectedItems[0].SubItems[6].Text;
                listValues.MasterArchive = tapeListListView.SelectedItems[0].SubItems[7].Text;
                listValues.PersonEntered = tapeListListView.SelectedItems[0].SubItems[8].Text;
                tapesToDelete = null;
            }
            else if (tapeListListView.SelectedItems.Count > 1)
            {
                //more than one item selected
                MakeBoxesVisible();

                tapeListEditEntryButton.Enabled = false;
                tapeListDeleteEntryButton.Enabled = true;
                tapeListDeleteEntryButton.Text = "Delete(" + tapeListListView.SelectedItems.Count + ")";
            }
            else if (tapeListListView.SelectedItems.Count == 0)
            {
                //no items selected
                tapesToDelete = null;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of all the addTextBox controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addTextBoxes_TextChanged(object sender, EventArgs e)
        {
            if(addProjectIDTextbox.TextLength > 0 && addTapeNameTextbox.TextLength > 0 && (addTagList.Count > 0 || addTagsTextbox.TextLength > 0) && addTapeNumUpDown.Value > 0 && !addDateDateTime.Value.Equals(string.Empty) && !addCameraComboBox.Text.Equals(string.Empty) && commonMethod.StringIsANumber(addProjectIDTextbox.Text))
            {
                addTapeAddButton.Enabled = true;

                //Change to default color backgrounds
                SetDefaultColors("add");
            }
            else if (addProjectIDTextbox.TextLength > 0 && focusValues.ProjectID)
            {
                commonMethod.BackColorDefault(addProjectIDTextbox);
            }
            else if(addTapeNameTextbox.TextLength > 0 && focusValues.TapeName)
            {
                commonMethod.BackColorDefault(addTapeNameTextbox);
            }
            else if((addTagList.Count > 0 || addTagsTextbox.TextLength > 0) && focusValues.Tags && !addTagsTextbox.Text.Equals(tagText))
            {
                commonMethod.BackColorDefault(addTagsTextbox);
            }
            else if(addTapeNumUpDown.Value > 0 && focusValues.TapeNumber)
            {
                commonMethod.BackColorDefault(addTapeNumUpDown);
            }
            else if(!addDateDateTime.Value.Equals(string.Empty) && focusValues.DateShot)
            {
                commonMethod.BackColorDefault(addDateDateTime);
            }
            else if (!addCameraComboBox.Text.Equals(string.Empty) && focusValues.Camera)
            {
                commonMethod.BackColorDefault(addCameraComboBox);
            }
            else
            {
                addTapeAddButton.Enabled = false;

                //change color of control based on the error
                if((addProjectIDTextbox.TextLength == 0 || !commonMethod.StringIsANumber(addProjectIDTextbox.Text)) && focusValues.ProjectID) { commonMethod.BackColorError(addProjectIDTextbox); }
                if(addTapeNameTextbox.TextLength == 0 && focusValues.TapeName){ commonMethod.BackColorError(addTapeNameTextbox); }
                if(addTagList.Count == 0 && (addTagsTextbox.TextLength == 0 || addTagsTextbox.Text.Equals(tagText)) && focusValues.Tags){ commonMethod.BackColorError(addTagsTextbox); }
                if(addTapeNumUpDown.Value == 0 && focusValues.TapeName){ commonMethod.BackColorError(addTapeNumUpDown); }
                if (addDateDateTime.Value.Equals(string.Empty) && focusValues.DateShot){ commonMethod.BackColorError(addDateDateTime); }
                if (addCameraComboBox.Text.Equals(string.Empty) && focusValues.Camera){ commonMethod.BackColorError(addCameraComboBox); }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of all the editTextBox controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editTextBoxes_TextChanged(object sender, EventArgs e)
        {
            if (editProjectIDTextbox.Text.Length > 0 && editTapeNameTextbox.Text.Length > 0 && (editTagList.Count > 0 || editTagsTextbox.TextLength > 0) && editTapeNumberUpDown.Value > 0 && !editDateShotDate.Value.Equals(string.Empty) && !editCameraDropdown.Text.Equals(string.Empty) && commonMethod.StringIsANumber(editProjectIDTextbox.Text))
            {
                editTapeEditButton.Enabled = true;

                //Change to default color backgrounds
                SetDefaultColors("edit");
            }
            else if (editProjectIDTextbox.TextLength > 0 && focusValues.ProjectID)
            {
                commonMethod.BackColorDefault(editProjectIDTextbox);
            }
            else if (editTapeNameTextbox.TextLength > 0 && focusValues.TapeName)
            {
                commonMethod.BackColorDefault(editTapeNameTextbox);
            }
            else if ((editTagList.Count > 0 || editTagsTextbox.TextLength > 0) && focusValues.Tags && !editTagsTextbox.Text.Equals(tagText))
            {
                commonMethod.BackColorDefault(editTagsTextbox);
            }
            else if (editTapeNumberUpDown.Value > 0 && focusValues.TapeNumber)
            {
                commonMethod.BackColorDefault(editTapeNumberUpDown);
            }
            else if (!editDateShotDate.Value.Equals(string.Empty) && focusValues.DateShot)
            {
                commonMethod.BackColorDefault(editDateShotDate);
            }
            else if (!editCameraDropdown.Text.Equals(string.Empty) && focusValues.Camera)
            {
                commonMethod.BackColorDefault(editCameraDropdown);
            }
            else
            {
                editTapeEditButton.Enabled = false;

                //change color of control based on the error
                if (editProjectIDTextbox.TextLength == 0 || !commonMethod.StringIsANumber(editProjectIDTextbox.Text)) { commonMethod.BackColorError(editProjectIDTextbox); }
                if (editTapeNameTextbox.TextLength == 0) { commonMethod.BackColorError(editTapeNameTextbox); }
                if (editTagList.Count == 0 && editTagsTextbox.TextLength == 0) { commonMethod.BackColorError(editTagsTextbox); }
                if (editTapeNumberUpDown.Value == 0) { commonMethod.BackColorError(editTapeNumberUpDown); }
                if (editDateShotDate.Value.Equals(string.Empty)) { commonMethod.BackColorError(editDateShotDate); }
                if (editCameraDropdown.Text.Equals(string.Empty)) { commonMethod.BackColorError(editCameraDropdown); }
            }
        }

        //Project label lost Focus
        private void AddProjectIDTextbox_LostFocus(object sender, EventArgs e)
        {
            focusValues.ProjectID = true;

            if(addProjectIDTextbox.Text.Length > 0 && commonMethod.StringIsANumber(addProjectIDTextbox.Text))
            {
                string projectName = DataBaseControls.GetProjectNameFromNumber(addProjectIDTextbox.Text);
                if(projectName != null)
                {
                    //A value was returned
                    addTapeListProjectName.Text = projectName;
                }else
                {
                    //no value was returned set Name to Tape Name Value
                    addTapeListProjectName.Text = "";
                }

                //Make Master list label
                addTapeMasterArchiveLabel.Text = DataBaseControls.GetMasterForTapes(addProjectIDTextbox.Text, null);

                commonMethod.BackColorDefault(addProjectIDTextbox);
            }
            else
            {
                //project ID is empty or not a entered
                commonMethod.BackColorError(addProjectIDTextbox);
                updateStatus.UpdateStatusBar("You must enter a Project ID that is a number", mainform);
            }

        }

        //Project Label lost focus
        private void EditProjectIDTextbox_LostFocus(object sender, EventArgs e)
        {
            focusValues.ProjectID = true;

            if(editProjectIDTextbox.Text.Length > 0 && commonMethod.StringIsANumber(editProjectIDTextbox.Text))
            {
                string projectName = DataBaseControls.GetProjectNameFromNumber(editProjectIDTextbox.Text);
                if(projectName != null)
                {
                    //A value was returned
                    editProjectNameLabel.Text = projectName;
                }else
                {
                    //no value was returned set Name to Tape Name Value
                    editProjectNameLabel.Text = "";
                }

                editTapeMasterListLabel.Text = DataBaseControls.GetMasterForTapes(editProjectIDTextbox.Text, null);

                commonMethod.BackColorDefault(editProjectIDTextbox);
            }
            else
            {
                //project ID is empty or not a number
                commonMethod.BackColorError(editProjectIDTextbox);
                updateStatus.UpdateStatusBar("You must enter a Project ID that is a number", mainform);
            }
        }

        //Focus given to tag textbox
        private void AddTagsTextbox_GotFocus(object sender, EventArgs e)
        {
            focusValues.Tags = true;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Equals(tagText))
            {
                textBox.Clear();
                commonMethod.BackColorDefault(textBox);
                textBox.ForeColor = SystemColors.WindowText;
            }
        }

        //Focus lost from tag textbox
        private void AddTagsTextbox_LostFocus(object sender, EventArgs e)
        {
            focusValues.Tags = true;

            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Equals(string.Empty))
            {
                textBox.ForeColor = SystemColors.GrayText;
                commonMethod.BackColorDefault(textBox);
                textBox.Text = tagText;
            }
        }

        private void AddTapeNameTextbox_LostFocus(object sender, EventArgs e)
        {
            focusValues.TapeName = true;

            if(addTapeNameTextbox.TextLength == 0)
            {
                commonMethod.BackColorError(addTapeNameTextbox);
            }else
            {
                commonMethod.BackColorDefault(addTapeNameTextbox);
            }
        }

        private void AddCameraComboBox_LostFocus(object sender, EventArgs e)
        {
            focusValues.Camera = true;
        }


        private void AddTapeNumUpDown_LostFocus(object sender, EventArgs e)
        {
            focusValues.TapeNumber = true;
        }

        private void AddDateDateTime_LostFocus(object sender, EventArgs e)
        {
            focusValues.DateShot = true;
        }


        private void EditTapeNameTextbox_LostFocus(object sender, EventArgs e)
        {
            focusValues.TapeName = true;

            if (editTapeNameTextbox.TextLength == 0)
            {
                commonMethod.BackColorError(editTapeNameTextbox);
            }
            else
            {
                commonMethod.BackColorDefault(editTapeNameTextbox);
            }
        }


        private void EditCameraDropdown_LostFocus(object sender, EventArgs e)
        {
            focusValues.Camera = true;
        }


        private void EditTapeNumberUpDown_LostFocus(object sender, EventArgs e)
        {
            focusValues.TapeNumber = true;
        }

        private void EditDateShotDate_LostFocus(object sender, EventArgs e)
        {
            focusValues.DateShot = true;
        }

        //Got Focus methods
        private void Controls_GotFocus(object sender, EventArgs e)
        {
            Control control = sender as Control;

            commonMethod.BackColorDefault(control);
        }

        private void addTapeNumUpDown_ValueChanged(object sender, EventArgs e)
        {
            if(addTapeNumUpDown.Value == 0)
            {
                commonMethod.BackColorError(addTapeNumUpDown);
            }
            else
            {
                commonMethod.BackColorDefault(addTapeNumUpDown);
            }
        }

        private void editTapeNumberUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (editTapeNumberUpDown.Value == 0)
            {
                commonMethod.BackColorError(editTapeNumberUpDown);
            }
            else
            {
                commonMethod.BackColorDefault(editTapeNumberUpDown);
            }
        }


        #endregion


        #region Tag Events
        //Add Tag comma to seperate tags pressed
        private void addTagsTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check to see if the , or enter has been pressed
            if (e.KeyChar.Equals(',') || e.KeyChar.Equals((char)Keys.Enter))
            {
                //check to see if there is anything in the textbox
                if(addTagsTextbox.Text.Length > 0 && !addTagList.Contains(addTagsTextbox.Text.ToLower().Replace(",","")))
                {
                    //use text from textbox, remove the ',' and add it to a list, and display tags
                    string text = addTagsTextbox.Text;
                    text = text.Replace(",", "");
                    addTagList.Add(text.ToLower());
                    DisplayTags("add", addTagDisplayFlowLayout, addTagList);
                }
                //Handle the key press and clear the textbox
                addTagsTextbox.Clear();
                e.Handled = true;
            }
        }

        //Add Tag comma to seperate tags pressed
        private void editTagsTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check to see if the , or enter has been pressed
            if (e.KeyChar.Equals(',') || e.KeyChar.Equals((char)Keys.Enter))
            {
                //check to see if there is anything in the textbox
                if (editTagsTextbox.Text.Length > 0 && !editTagList.Contains(editTagsTextbox.Text.ToLower().Replace(",", "")))
                {
                    //use text from textbox, remove the ',' and add it to a list, and display tags
                    string text = editTagsTextbox.Text;
                    text = text.Replace(",", "");
                    editTagList.Add(text.ToLower());
                    DisplayTags("edit", editTageFlowLayoutPanel, editTagList);
                }
                //Handle the key press and clear the textbox
                editTagsTextbox.Clear();
                e.Handled = true;
            }
        }

        //Event for when edit tag close is clicked
        private void PbEdit_Click(object sender, EventArgs e)
        {
            //cast sender as picturebox
            PictureBox pBox = (PictureBox)sender;
            //remove item in list
            editTagList.RemoveAt(Convert.ToInt32(pBox.Tag));
            //redraw tags from new list
            DisplayTags("edit", editTageFlowLayoutPanel, editTagList);
            editTapeEditButton.Enabled = true;
            
        }

        //Event for when add tag close is clicked
        private void PbAdd_Click(object sender, EventArgs e)
        {
            //cast sender as picturebox
            PictureBox pBox = (PictureBox)sender;
            //remove item in list
            addTagList.RemoveAt(Convert.ToInt32(pBox.Tag));
            //redraw tags from new list
            DisplayTags("add",addTagDisplayFlowLayout,addTagList);

            if(addTagList.Count == 0 && (addTagsTextbox.TextLength == 0 || addTagsTextbox.Text.Equals(tagText)))
            {
                commonMethod.BackColorError(addTagsTextbox);
            }else
            {
                commonMethod.BackColorDefault(addTagsTextbox);
            }
        }

        //MouseOver Event
        private void AddTagsTextbox_MouseHover(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!textBox.Text.Equals(tagText))
            {
                toolTip.InitialDelay = 1000;
                toolTip.Show(tagText, textBox, -30, -22, 2000);
            }
        }

        //Mouse leave event
        private void AddTagsTextbox_MouseLeave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (toolTip.Active)
            {
                toolTip.Hide(textBox);
            }
        }


        #endregion

        private void TapeListForm_Shown(object sender, EventArgs e)
        {
            if (buttonToPress)
            {
                tapeListAddNewButton.PerformClick();
            }
        }
    }
}
