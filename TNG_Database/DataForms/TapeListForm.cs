using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private Point boxLocation = new Point(12, 317);
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

        public TapeListForm()
        {
            InitializeComponent();
        }

        public TapeListForm(TNG_Database.MainForm parent)
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

            //disable all groupboxes except default
            addTapeGroupbox.Visible = false;
            deleteTapeGroupbox.Visible = false;
            editTapeGroupbox.Visible = false;
            defaultTapeGroupbox.Visible = true;

            //Populate all dropdowns
            LoadDropdowns();

            //Attach all add textboxes to an event
            addProjectIDTextbox.TextChanged += addTextBoxes_TextChanged;
            addProjectNameTextbox.TextChanged += addTextBoxes_TextChanged;
            addTapeNameTextbox.TextChanged += addTextBoxes_TextChanged;
            addTagsTextbox.TextChanged += addTextBoxes_TextChanged;

            //Attach all edit textboxes to an event
            editProjectIDTextbox.TextChanged += editTextBoxes_TextChanged;
            editProjectNameTextbox.TextChanged += editTextBoxes_TextChanged;
            editTapeNameTextbox.TextChanged += editTextBoxes_TextChanged;
            editTagsTextbox.TextChanged += editTextBoxes_TextChanged;

            //Tell user the database is ready for use
            updateStatus.UpdateStatusBar("Database is Loaded and Ready", mainform);

            //keep items highlighted
            tapeListListView.HideSelection = false;

            //Clear all controls
            ClearAddControls();
            ClearDeleteLabels();
            ClearDeleteLabels();
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
                updateStatus.UpdateStatusBar("There are no items in the database", mainform);
            }
            else
            {
                //List returned values, populate listview
                foreach (TapeDatabaseValues values in tapeListValues)
                {
                    tapeListListView.Items.Add(new ListViewItem(new string[] { values.ID.ToString(), values.ProjectId, values.ProjectName, values.TapeName, values.TapeNumber, commonMethod.GetCameraName(values.Camera), values.TapeTags, values.DateShot, values.MasterArchive, values.PersonEntered }));
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
            if (tapeListListView.SelectedItems.Count > 0)
            {
                //set value to current index
                listViewIndex = tapeListListView.SelectedIndices[0];

                //Update Status label
                updateStatus.UpdateStatusBar(tapeListListView.SelectedItems[0].SubItems[1].Text + " item selected", mainform);

                //set items in default panel to item selected
                defaultProjectIDLabel.Text = tapeListListView.SelectedItems[0].SubItems[1].Text;
                defaultProjectNameLabel.Text = tapeListListView.SelectedItems[0].SubItems[2].Text;
                defaultTapeNameLabel.Text = tapeListListView.SelectedItems[0].SubItems[3].Text;
                defaultTapeNumberLabel.Text = tapeListListView.SelectedItems[0].SubItems[4].Text;
                defaultCameraLabel.Text = tapeListListView.SelectedItems[0].SubItems[5].Text;
                //Split csv into list and display
                defaultTagList = tapeListListView.SelectedItems[0].SubItems[6].Text.Split(',').ToList();
                DisplayTags("default", defaultTagFlowLayoutPanel, defaultTagList);
                defaultDateLabel.Text = tapeListListView.SelectedItems[0].SubItems[7].Text;
                defaultMasterArchiveLabel.Text = tapeListListView.SelectedItems[0].SubItems[8].Text;
                defaultPersonLabel.Text = tapeListListView.SelectedItems[0].SubItems[9].Text;

                //swap default views
                defaultNoItemSelectedLabel.Visible = false;
                defaultItemsPanel.Visible = true;

                //make edit and delete buttons enabled
                tapeListEditEntryButton.Enabled = true;
                tapeListDeleteEntryButton.Enabled = true;
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
            DataBaseControls dbControls = new DataBaseControls();
            //Set up method in Control Database to get person and master list to populate dropdowns
            string[] people = dbControls.GetPersonListForDropdown();
            string[] masterTapes = dbControls.GetMasterListForDropdown();
            string[] cameraDropdowns = commonMethod.CameraDropdownItems();

            //load values into camera dropdowns
            addCameraComboBox.Items.AddRange(cameraDropdowns);
            editCameraDropdown.Items.AddRange(cameraDropdowns);
            //load values into person dropdowns
            addTapePersonDropdown.Items.AddRange(people);
            editPersonDropdown.Items.AddRange(people);
            //load values into master list dropdowns
            addTapeMasterArchiveDropdown.Items.AddRange(masterTapes);
            editMasterArchiveDropdown.Items.AddRange(masterTapes);
        }

        /// <summary>
        /// Clears the add controls.
        /// </summary>
        private void ClearAddControls()
        {
            addProjectIDTextbox.Clear();
            addProjectNameTextbox.Clear();
            addTapeNameTextbox.Clear();
            addTagsTextbox.Clear();
            addTapeNumUpDown.Value = 1;
            addCameraComboBox.SelectedIndex = 0;
            addTapeMasterArchiveDropdown.SelectedIndex = 0;
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
            editProjectNameTextbox.Clear();
            editTapeNameTextbox.Clear();
            editTagsTextbox.Clear();
            editTapeNumberUpDown.Value = 1;
            editCameraDropdown.SelectedIndex = 0;
            editMasterArchiveDropdown.SelectedIndex = 0;
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
            tapeValues.ID = Convert.ToInt32(tapeListListView.SelectedItems[0].SubItems[0].Text);
            tapeValues.ProjectId = tapeListListView.SelectedItems[0].SubItems[1].Text;
            tapeValues.ProjectName = tapeListListView.SelectedItems[0].SubItems[2].Text;
            tapeValues.TapeName = tapeListListView.SelectedItems[0].SubItems[3].Text;
            tapeValues.TapeNumber = tapeListListView.SelectedItems[0].SubItems[4].Text;
            tapeValues.Camera = commonMethod.GetCameraNumber(tapeListListView.SelectedItems[0].SubItems[5].Text);
            tapeValues.TapeTags = tapeListListView.SelectedItems[0].SubItems[6].Text;
            tapeValues.DateShot = tapeListListView.SelectedItems[0].SubItems[7].Text;
            tapeValues.MasterArchive = tapeListListView.SelectedItems[0].SubItems[8].Text;
            tapeValues.PersonEntered = tapeListListView.SelectedItems[0].SubItems[9].Text;
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
            addTapePersonDropdown.SelectedIndex = 0;
            addTapeMasterArchiveDropdown.SelectedIndex = 0;
            
        }

        //Edit entry button pressed
        private void tapeListEditEntryButton_Click(object sender, EventArgs e)
        {
            //Place edit tape groupbox and make visible
            MakeBoxesVisible("edit");

            LoadTapeValuesFromList();
            editProjectIDTextbox.Text = tapeValues.ProjectId;
            editProjectNameTextbox.Text = tapeValues.ProjectName;
            editTapeNameTextbox.Text = tapeValues.TapeName;
            editTapeNumberUpDown.Text = tapeValues.TapeNumber;
            editCameraDropdown.SelectedIndex = commonMethod.GetCameraDropdownIndex(commonMethod.GetCameraName(tapeValues.Camera));
            //split csv into a list and display
            editTagList = tapeValues.TapeTags.Split(',').ToList();
            DisplayTags("edit", editTageFlowLayoutPanel, editTagList);
            editDateShotDate.Value = commonMethod.ConvertDateForDatePicker(tapeValues.DateShot);
            editMasterArchiveDropdown.Text = tapeValues.MasterArchive;
            editPersonDropdown.Text = tapeValues.PersonEntered;
        }

        //Delete entry button pressed
        private void tapeListDeleteEntryButton_Click(object sender, EventArgs e)
        {
            //Place delete tape groupbox and make visible

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

        }
        #endregion

        //--------------------------------------------------------
        //-------------ADD GB CONTROLS----------------------------
        //--------------------------------------------------------
        #region AddGBControls

        //Add Button pressed
        private void addTapeAddButton_Click(object sender, EventArgs e)
        {
            //check to make sure that all info is entered
            if(addProjectIDTextbox.Text.Length > 0 && addProjectNameTextbox.Text.Length > 0
                && addTapeNameTextbox.Text.Length > 0 && addTagList.Count > 0
                && addTapeNumUpDown.Value > 0 && addCameraComboBox.Text.Length > 0
                && addTapeMasterArchiveDropdown.Text.Length > 0 && addTapePersonDropdown.Text.Length > 0)
            {
                //load tape values to add to database
                tapeValues.ProjectId = addProjectIDTextbox.Text;
                tapeValues.ProjectName = addProjectNameTextbox.Text;
                tapeValues.TapeName = addTapeNameTextbox.Text;
                tapeValues.TapeNumber = addTapeNumUpDown.Value.ToString();
                tapeValues.Camera = commonMethod.GetCameraNumber(addCameraComboBox.Text);
                tapeValues.TapeTags = String.Join(",", addTagList);
                tapeValues.MasterArchive = addTapeMasterArchiveDropdown.Text;
                tapeValues.DateShot = commonMethod.ConvertDateFromDropdownForDB(addDateDateTime.Value);
                tapeValues.PersonEntered = addTapePersonDropdown.Text;

                //Add to database and check to make sure it is added
                AddToDatabase addDB = new AddToDatabase();
                if (addDB.AddTapeDatabase(tapeValues))
                {
                    //update status and clear all controls and variables
                    PopulateTapeList();
                    tapeValues.Clear();
                    ClearAddControls();
                    MakeBoxesVisible();
                    tapeListListView.Focus();
                    updateStatus.UpdateStatusBar("Tape Added to Database", mainform);
                }
            }else
            {
                updateStatus.UpdateStatusBar("Please Fill Out All Fields", mainform);
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
            AddToDatabase editDB = new AddToDatabase();

            //Create new TapeDatabaseValues for edited entry
            TapeDatabaseValues newTapeValues = new TapeDatabaseValues(
                editTapeNameTextbox.Text,editTapeNumberUpDown.Value.ToString(),editProjectIDTextbox.Text,editProjectNameTextbox.Text,
                commonMethod.GetCameraNumber(editCameraDropdown.Text),String.Join(",",editTagList),commonMethod.ConvertDateFromDropdownForDB(editDateShotDate.Value),
                editMasterArchiveDropdown.Text,editPersonDropdown.Text);

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
            }else
            {
                //User Needs to change a value
                updateStatus.UpdateStatusBar("Must Change At Least One Value", mainform);
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
                Console.WriteLine("Yes Pressed for deletion");

                AddToDatabase deleteDB = new AddToDatabase();

                
                //Delete user from database
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
            if (tapeListListView.SelectedItems.Count > 0)
            {
                listValues.ID = Convert.ToInt32(tapeListListView.SelectedItems[0].SubItems[0].Text);
                listValues.ProjectId = tapeListListView.SelectedItems[0].SubItems[1].Text;
                listValues.ProjectName = tapeListListView.SelectedItems[0].SubItems[2].Text;
                listValues.TapeName = tapeListListView.SelectedItems[0].SubItems[3].Text;
                listValues.TapeNumber = tapeListListView.SelectedItems[0].SubItems[4].Text;
                listValues.Camera = commonMethod.GetCameraNumber(tapeListListView.SelectedItems[0].SubItems[5].Text);
                listValues.TapeTags = tapeListListView.SelectedItems[0].SubItems[6].Text;
                listValues.DateShot = tapeListListView.SelectedItems[0].SubItems[7].Text;
                listValues.MasterArchive = tapeListListView.SelectedItems[0].SubItems[8].Text;
                listValues.PersonEntered = tapeListListView.SelectedItems[0].SubItems[9].Text;
            }else
            {
                if (tapeListListView.SelectedItems.Count == 0)
                {
                    updateStatus.UpdateStatusBar("Nothing Selected", mainform);
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of all the addTextBox controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void addTextBoxes_TextChanged(object sender, EventArgs e)
        {
            if(addProjectIDTextbox.Text.Length > 0 && addProjectNameTextbox.Text.Length >0 && addTapeNameTextbox.Text.Length > 0 && addTagList.Count > 0)
            {
                addTapeAddButton.Enabled = true;
            }else
            {
                addTapeAddButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of all the editTextBox controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void editTextBoxes_TextChanged(object sender, EventArgs e)
        {
            if (editProjectIDTextbox.Text.Length > 0 && editProjectNameTextbox.Text.Length > 0 && editTapeNameTextbox.Text.Length > 0 && editTagList.Count > 0)
            {
                editTapeEditButton.Enabled = true;
            }
            else
            {
                editTapeEditButton.Enabled = false;
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
                if(addTagsTextbox.Text.Length > 0)
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
                if (editTagsTextbox.Text.Length > 0)
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
        }
        #endregion

    }
}
