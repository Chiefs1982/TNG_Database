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
    public partial class ProjectsForm : Form
    {
        //location for the groupboxes
        private Point boxLocation = new Point(431, 54);

        //current index of item selected in listview
        int listViewIndex = -1;

        //reference for the mainform
        private TNG_Database.MainForm mainform;

        //current item selected in list view
        ProjectValues listValues = new ProjectValues(); 

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();

        //List of mulitple items to delete
        List<ProjectValues> tapesToDelete = null;

        public ProjectsForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            //Load listview
            PopulateListView();

            //Form specific actions

            //add textbox changed event
            addProjectIDTextBox.TextChanged += AddProjectTextBox_TextChanged;
            addProjectNameTextBox.TextChanged += AddProjectTextBox_TextChanged;

            //make selected item always visible
            projectsListView.HideSelection = false;

            //close all groupboxes
            CloseGroupBox();

            //Event for sorting each column
            CommonMethods.ListViewItemComparer.SortColumn = -1;
            projectsListView.ColumnClick += new ColumnClickEventHandler(CommonMethods.ListViewItemComparer.SearchListView_ColumnClick);
        }

        private void AddProjectTextBox_TextChanged(object sender, EventArgs e)
        {
            //check to make sure both textboxes have something in them
            if(addProjectIDTextBox.Text.Length > 0 && addProjectNameTextBox.Text.Length > 0)
            {
                //Not empty
                addProjectAddButton.Enabled = true;
            }else
            {
                //empty
                addProjectAddButton.Enabled = false;
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
            projectsListView.Items.Clear();

            //Get list of all projects
            List<ProjectValues> values = DataBaseControls.GetAllProjectItems();

            if (values.Any())
            {
                //Iterate over list and add to listview
                foreach (ProjectValues value in values)
                {
                    projectsListView.Items.Add(new ListViewItem(new string[] { value.ProjectID, value.Projectname })).Tag = Convert.ToInt32(value.ID);
                }

                //clear selection
                projectsListView.SelectedIndices.Clear();

                updateStatus.UpdateStatusBar(values.Count + " Project(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("No Projects in database", mainform);
            }
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
                    projectAddGroupBox.Location = boxLocation;
                    deleteProjectsGroupBox.Visible = false;
                    editProjectsGroupBox.Visible = false;
                    projectsDefaultGroupBox.Visible = false;
                    addProjectAddButton.Enabled = false;
                    projectAddGroupBox.Visible = true;
                    addProjectIDTextBox.Focus();
                    break;
                case "edit":
                    editProjectsGroupBox.Location = boxLocation;
                    deleteProjectsGroupBox.Visible = false;
                    projectsDefaultGroupBox.Visible = false;
                    projectAddGroupBox.Visible = false;
                    editProjectsGroupBox.Visible = true;
                    editProjectCancelButton.Focus();
                    break;
                case "delete":
                    deleteProjectsGroupBox.Location = boxLocation;
                    projectsDefaultGroupBox.Visible = false;
                    projectAddGroupBox.Visible = false;
                    editProjectsGroupBox.Visible = false;
                    deleteProjectsGroupBox.Visible = true;
                    deleteProjectCancelButton.Focus();
                    break;
                default:
                    projectsDefaultGroupBox.Location = boxLocation;
                    projectAddGroupBox.Visible = false;
                    editProjectsGroupBox.Visible = false;
                    deleteProjectsGroupBox.Visible = false;
                    projectsDefaultGroupBox.Visible = true;
                    UpdateDefaultBox();
                    projectsDeleteButton.Text = "Delete";
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
            if (projectsListView.SelectedItems.Count > 0)
            {
                //set value to current index
                listViewIndex = Convert.ToInt32(projectsListView.SelectedItems[0].Tag);

                //Update Status label
                updateStatus.UpdateStatusBar(projectsListView.SelectedItems[0].SubItems[0].Text + " item selected", mainform,0);

                //set items in default panel to item selected
                defaultProjectIDLabel.Text = projectsListView.SelectedItems[0].SubItems[0].Text;
                defaultProjectNameLabel.Text = projectsListView.SelectedItems[0].SubItems[1].Text;

                //swap default views
                defaultLabel.Visible = false;
                defaultLabelPanel.Visible = true;

                //make edit and delete buttons enabled
                projectsDeleteButton.Enabled = true;
                projectsUpdateButton.Enabled = true;
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
                projectsUpdateButton.Enabled = false;
                projectsDeleteButton.Enabled = false;

                //set default values to blank
                defaultProjectIDLabel.Text = "";
                defaultProjectNameLabel.Text = "";
            }
        }

        #endregion

        //---------------------------------------------------------
        //----------ADD, EDIT, DELETE BUTTONS PREsSED--------------
        //---------------------------------------------------------
        #region Add, Edit, Delete Buttons Pressed

        //Add Button Pressed
        private void projectsAddButton_Click(object sender, EventArgs e)
        {
            //clear textboxes
            addProjectNameTextBox.Clear();
            addProjectIDTextBox.Clear();
            //open groupbox
            CloseGroupBox("add");
        }

        //Update Button Pressed
        private void projectsUpdateButton_Click(object sender, EventArgs e)
        {
            //set textboxes to selected values in listview
            editProjectIDTextBox.Text = listValues.ProjectID;
            editProjectNameTextBox.Text = listValues.Projectname;

            //open edit groupbox and close others
            CloseGroupBox("edit");

            //give focus to the cancel button
            editProjectCancelButton.Focus();
        }

        //Delete Button Pressed
        private void projectsDeleteButton_Click(object sender, EventArgs e)
        {

            if (projectsListView.SelectedItems.Count == 1)
            {
                //set labels to seleted values in listview
                deleteProjectIDLabel.Text = listValues.ProjectID;
                deleteProjectNameLabel.Text = listValues.Projectname;

                //open delete groupbox
                CloseGroupBox("delete");

                //give the cancel button focus
                deleteProjectCancelButton.Focus();
            }
            else if (projectsListView.SelectedItems.Count > 1)
            {
                //multiple items selected in listview

                //Show message box to make sure user is to be deleted
                DialogResult deleteMessage = MessageBox.Show("Do you want to delete these " + projectsListView.SelectedItems.Count + " entries?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //Check to see if user pressed yes or no
                if (deleteMessage == DialogResult.Yes)
                {

                    //clear delete list
                    if (tapesToDelete == null)
                    {
                        tapesToDelete = new List<ProjectValues>();
                    }
                    else
                    {
                        tapesToDelete.Clear();
                    }

                    //iterate over each item selected and save data in a value, then a list
                    foreach (ListViewItem item in projectsListView.SelectedItems)
                    {
                        ProjectValues value = new ProjectValues(item.SubItems[0].Text, item.SubItems[1].Text, Convert.ToInt32(item.Tag));

                        tapesToDelete.Add(value);
                    }



                    if (projectsListView.SelectedItems.Count > 1 && tapesToDelete.Count > 0)
                    {
                        Console.WriteLine("sending " + tapesToDelete.Count + " items to delete");
                        updateStatus.UpdateStatusBar(AddToDatabase.DeleteMultipleProjectSelected(tapesToDelete) + " items deleted", mainform);
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
        private void addProjectAddButton_Click(object sender, EventArgs e)
        {
            //check if project ID is a number
            if (commonMethod.StringIsANumber(addProjectIDTextBox.Text))
            {
                //Project ID is a number
                //check to make sure something is entered in the textboxes
                if (addProjectIDTextBox.Text.Length > 0 && addProjectNameTextBox.Text.Length > 0)
                {
                    //instatiate values and database class
                    ProjectValues addValues = new ProjectValues(addProjectIDTextBox.Text, addProjectNameTextBox.Text);
                    AddToDatabase addDB = new AddToDatabase();

                    //add to database and check if successful
                    if (addDB.AddProjects(addValues))
                    {
                        //success

                        //clear textboxes
                        addProjectIDTextBox.Clear();
                        addProjectNameTextBox.Clear();
                        //populate list view
                        PopulateListView();
                        //open default groupbox
                        CloseGroupBox();

                        //give listview focus
                        projectsListView.Focus();

                        updateStatus.UpdateStatusBar("Project " + addValues.ProjectID + " Added to Database", mainform);
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
            else
            {
                //project id was NOT a number
                updateStatus.UpdateStatusBar("Project ID must be a number", mainform);
            }

            
        }

        //Cancel Button pressed
        private void addProjectCancelButton_Click(object sender, EventArgs e)
        {
            //clear textboxes
            addProjectIDTextBox.Clear();
            addProjectNameTextBox.Clear();
            //close groupbox and open default
            CloseGroupBox();
        }

        #endregion

        //----------------------------------------------
        //-----------------Edit PROJECT GROUP BOX-------
        //----------------------------------------------
        #region Edit Groupbox
        //Edit button pressed
        private void editProjectEditButton_Click(object sender, EventArgs e)
        {
            //check if project ID is a number
            if (commonMethod.StringIsANumber(editProjectIDTextBox.Text))
            {
                //Project ID is a number
                AddToDatabase editDB = new AddToDatabase();
                ProjectValues editValues = new ProjectValues(editProjectIDTextBox.Text, editProjectNameTextBox.Text, listValues.ID);

                if (editProjectIDTextBox.Text.Length > 0 && editProjectNameTextBox.Text.Length > 0)
                {
                    if (editDB.EditProject(listValues, editValues))
                    {
                        //success

                        //clear textboxes
                        editProjectIDTextBox.Clear();
                        editProjectNameTextBox.Clear();

                        //update list view
                        PopulateListView();

                        //close edit groupbox and open default
                        CloseGroupBox();

                        //give listview focus
                        projectsListView.Focus();

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
            else
            {
                //project id was NOT a number
                updateStatus.UpdateStatusBar("Project ID must be a number", mainform);
            }
        }

        //Cancel button pressed
        private void editProjectCancelButton_Click(object sender, EventArgs e)
        {
            //clear edit textboxes
            editProjectIDTextBox.Clear();
            editProjectNameTextBox.Clear();

            //close edit groupbox
            CloseGroupBox();
        }
        
        #endregion

        //----------------------------------------------
        //--------------DELETE PROJECT GROUP BOX--------
        //----------------------------------------------
        #region Delete Groupbox
        //Delete button pressed
        private void deleteProjectDeleteButton_Click(object sender, EventArgs e)
        {
            //Delete button pressed, gather info and delete entry
            //Show message box to make sure user is to be deleted
            DialogResult deleteMessage = MessageBox.Show("Do you want to delete the entry " + listValues.ProjectID + ": " + listValues.Projectname + "?", "Deletion Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            //Check to see if user pressed yes or no
            if (deleteMessage == DialogResult.Yes)
            {
                //Yes Pressed, delete user from DB
                Console.WriteLine("Yes Pressed for deletion");

                AddToDatabase deleteDB = new AddToDatabase();
                
                //Delete user from database
                if (deleteDB.DeleteProjects(listValues))
                {
                    //deletion success
                    listValues.Clear();

                    //clear text labels
                    deleteProjectIDLabel.Text = "";
                    deleteProjectNameLabel.Text = "";

                    PopulateListView();
                    CloseGroupBox();
                    projectsListView.Focus();
                    updateStatus.UpdateStatusBar("Entry deleted", mainform);
                }
                else
                {
                    updateStatus.UpdateStatusBar("There was an error deleting entry", mainform);
                    CloseGroupBox();
                    projectsListView.Focus();
                }
            }
            else if (deleteMessage == DialogResult.No)
            {
                //No Pressed, nothing will be done
            }
        }

        //Cancel button pressed
        private void deleteProjectCancelButton_Click(object sender, EventArgs e)
        {
            //clear text labels
            deleteProjectIDLabel.Text = "";
            deleteProjectNameLabel.Text = "";

            //close delete groupbox
            CloseGroupBox();
        }

        #endregion


        //Listview index changed
        private void projectsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //updates default groupbox based on listview selection
            CloseGroupBox();
            if (projectsListView.SelectedItems.Count == 1)
            {
                listValues.ID = Convert.ToInt32(projectsListView.SelectedItems[0].Tag);
                listValues.ProjectID = projectsListView.SelectedItems[0].SubItems[0].Text;
                listValues.Projectname = projectsListView.SelectedItems[0].SubItems[1].Text;

                //make selected panel visible
                defaultLabel.Visible = false;
                defaultLabelPanel.Visible = true;
                //make button say delete
                projectsDeleteButton.Text = "Delete";
            }
            else if(projectsListView.SelectedItems.Count > 1)
            {
                //more than one item selected
                CloseGroupBox();

                projectsUpdateButton.Enabled = false;
                projectsDeleteButton.Enabled = true;
                updateStatus.UpdateStatusBar(projectsListView.SelectedItems.Count + " projects selected", mainform);
                projectsDeleteButton.Text = "Delete(" + projectsListView.SelectedItems.Count + ")";
            }
            else if(projectsListView.SelectedItems.Count == 0)
            {
                //make default nothing selected label visible
                defaultLabelPanel.Visible = false;
                defaultLabel.Visible = true;
                //make button say delete
                projectsDeleteButton.Text = "Delete";
            }
        }

        
    }
}
