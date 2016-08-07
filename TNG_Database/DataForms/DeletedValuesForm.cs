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
    public partial class DeletedValuesForm : Form
    {
        //enum to know which database is selected
        enum DeleteForm
        {
            tapeDatabase,
            projects,
            people,
            masterList,
            masterArchiveVideos
        };

        //variable to access current value of enum
        //set initial enum to tapeDatabase
        DeleteForm currentFormState = DeleteForm.tapeDatabase;

        //default point for each panel
        Point panelLocation = new Point(12, 34);

        //reference for the mainform
        private TNG_Database.MainForm mainform;

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();

        //Selected Item Values
        TapeDatabaseValues tapeDBValues = null;
        ProjectValues projectsDBValues = null;
        PeopleValues peopleDBValues = null;
        MasterListValues masterListDBValues = null;
        MasterArchiveVideoValues masterArchiveValues = null;

        public DeletedValuesForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();

            this.MdiParent = parent;
            mainform = parent;

            //Make button invisible and disabled
            ButtonInvisibleAndDisabled();

            //Add values to combobox for picking selected items
            deleteFormSelectCombo.Items.AddRange(new string[] { "Tape Database", "Projects", "People", "Master List", "Master Archive Videos" });
            deleteFormSelectCombo.SelectedIndex = 0;

            //load default tape list values into listview
            PopulateTapeValuesInList();
        }

        //-----------------------------------------------------
        //--------------CLASS METHODS--------------------------
        //-----------------------------------------------------

        #region Class Methods

        /// <summary>
        /// Gets the enum dropdown.
        /// </summary>
        /// <param name="selection">The selection.</param>
        /// <returns>enum of form corresponding to the dropdown</returns>
        DeleteForm GetEnumDropdown(string selection)
        {
            switch (selection)
            {
                case "tapedatabase":
                    return DeleteForm.tapeDatabase;
                case "projects":
                    return DeleteForm.projects;
                case "people":
                    return DeleteForm.people;
                case "masterlist":
                    return DeleteForm.masterList;
                case "masterarchivevideos":
                    return DeleteForm.masterArchiveVideos;
                default:
                    return DeleteForm.tapeDatabase;
            }
        }

        /// <summary>
        /// Clears the flow layouts.
        /// </summary>
        private void ClearFlowLayouts()
        {
            //Clear flow layout panels
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            flowLayoutPanel3.Controls.Clear();
            flowLayoutPanel4.Controls.Clear();
            flowLayoutPanel5.Controls.Clear();
            flowLayoutPanel6.Controls.Clear();
            flowLayoutPanel7.Controls.Clear();
            flowLayoutPanel8.Controls.Clear();
            flowLayoutPanel9.Controls.Clear();
        }

        /// <summary>
        /// Makes button visible and enabled.
        /// </summary>
        private void ButtonVisibleAndEnabled()
        {
            deleteReinstateButton.Visible = true;
            deleteReinstateButton.Enabled = true;
        }

        /// <summary>
        /// Makes button invisible and disabled.
        /// </summary>
        private void ButtonInvisibleAndDisabled()
        {
            deleteReinstateButton.Visible = false;
            deleteReinstateButton.Enabled = false;
        }

        #endregion

        #region Load List View Functions

        private void LoadTapeDBPage()
        {
            //Clear Database of colummns
            databaseListView.Clear();
            databaseListView.Items.Clear();
            ButtonInvisibleAndDisabled();

            //Load COlumns
            ColumnHeader colTapePID = new ColumnHeader();
            colTapePID.Text = "Project ID";
            colTapePID.Width = 60;
            colTapePID.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapePN = new ColumnHeader();
            colTapePN.Text = "Project Name";
            colTapePN.Width = 200;
            colTapePN.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapeTN = new ColumnHeader();
            colTapeTN.Text = "Tape Name";
            colTapeTN.Width = 120;
            colTapeTN.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapeTNum = new ColumnHeader();
            colTapeTNum.Text = "Tape #";
            colTapeTNum.Width = 50;
            colTapeTNum.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapeCam = new ColumnHeader();
            colTapeCam.Text = "Camera";
            colTapeCam.Width = 50;
            colTapeCam.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapeTags = new ColumnHeader();
            colTapeTags.Text = "Tags";
            colTapeTags.Width = 62;
            colTapeTags.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapeDate = new ColumnHeader();
            colTapeDate.Text = "Date Shot";
            colTapeDate.Width = 78;
            colTapeDate.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapeMaster = new ColumnHeader();
            colTapeMaster.Text = "Master Archive";
            colTapeMaster.Width = 95;
            colTapeMaster.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colTapePerson = new ColumnHeader();
            colTapePerson.Text = "Entered By";
            colTapePerson.Width = 60;
            colTapePerson.TextAlign = HorizontalAlignment.Left;

            databaseListView.Columns.AddRange(new ColumnHeader[] { colTapePID, colTapePN, colTapeTN, colTapeTNum, colTapeCam, colTapeTags, colTapeDate, colTapeMaster, colTapePerson });

            //load list values
            PopulateTapeValuesInList();

            databaseListView.Focus();
        }

        private void LoadProjectsPage()
        {
            //Clear Database of colummns
            databaseListView.Clear();
            databaseListView.Items.Clear();
            ButtonInvisibleAndDisabled();

            //Creat columns:
            ColumnHeader colProjectsPID = new ColumnHeader();
            colProjectsPID.Text = "Project ID";
            colProjectsPID.Width = 100;
            colProjectsPID.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colProjectsPN = new ColumnHeader();
            colProjectsPN.Text = "Project Name";
            colProjectsPN.Width = 300;
            colProjectsPN.TextAlign = HorizontalAlignment.Left;

            databaseListView.Columns.AddRange(new ColumnHeader[] { colProjectsPID, colProjectsPN });

            //load values into list view
            PopulateProjectValuesInList();

            //focus on listview
            databaseListView.Focus();

        }

        private void LoadPeoplePage()
        {
            //Clear Database of colummns
            databaseListView.Clear();
            databaseListView.Items.Clear();
            ButtonInvisibleAndDisabled();

            //Load Coluns
            ColumnHeader colPeopleName = new ColumnHeader();
            colPeopleName.Text = "Name";
            colPeopleName.Width = 500;
            colPeopleName.TextAlign = HorizontalAlignment.Left;

            databaseListView.Columns.AddRange(new ColumnHeader[] { colPeopleName });

            //load values into list view
            PopulatePeopleValuesInList();

            //focus on listview
            databaseListView.Focus();
        }

        private void LoadMasterListPage()
        {
            //Clear Database of colummns
            databaseListView.Clear();
            databaseListView.Items.Clear();
            ButtonInvisibleAndDisabled();

            //Creat columns:
            ColumnHeader colMasterListPID = new ColumnHeader();
            colMasterListPID.Text = "Project ID";
            colMasterListPID.Width = 100;
            colMasterListPID.TextAlign = HorizontalAlignment.Left;

            ColumnHeader collMasterListsPN = new ColumnHeader();
            collMasterListsPN.Text = "Project Name";
            collMasterListsPN.Width = 300;
            collMasterListsPN.TextAlign = HorizontalAlignment.Left;

            databaseListView.Columns.AddRange(new ColumnHeader[] { colMasterListPID, collMasterListsPN });

            //load values into list view
            PopulateMasterListValuesInList();

            //focus on listview
            databaseListView.Focus();
        }

        private void LoadMasterArchiveVideosPage()
        {
            //Clear Database of colummns
            databaseListView.Clear();
            databaseListView.Items.Clear();
            ButtonInvisibleAndDisabled();

            //Load COlumns
            ColumnHeader colMasterPID = new ColumnHeader();
            colMasterPID.Text = "Project ID";
            colMasterPID.Width = 100;
            colMasterPID.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colMasterVN = new ColumnHeader();
            colMasterVN.Text = "Video Name";
            colMasterVN.Width = 300;
            colMasterVN.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colMasterMT = new ColumnHeader();
            colMasterMT.Text = "Master Archive";
            colMasterMT.Width = 300;
            colMasterMT.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colMasterClip = new ColumnHeader();
            colMasterClip.Text = "Tape #";
            colMasterClip.Width = 60;
            colMasterClip.TextAlign = HorizontalAlignment.Left;

            databaseListView.Columns.AddRange(new ColumnHeader[] { colMasterPID, colMasterVN, colMasterMT, colMasterClip });

            //load values into list view
            PopulateMasterArchiveValuesInLIst();

            //focus on listview
            databaseListView.Focus();
        }

        #endregion

        #region Load List view with values

        /// <summary>
        /// Populates the tape values into the listview.
        /// </summary>
        private void PopulateTapeValuesInList()
        {

            //get all values for listview
            List<TapeDatabaseValues> tapeValues = DataBaseControls.GetAllDeletedTapeValues();
            if (tapeValues != null)
            {
                //Values returned in list
                //iterate over list and add values to listview
                foreach (TapeDatabaseValues value in tapeValues)
                {
                    databaseListView.Items.Add(new ListViewItem(new string[] { value.ProjectId, value.ProjectName, value.TapeName, value.TapeNumber, commonMethod.GetCameraName(value.Camera), value.TapeTags, value.DateShot, value.MasterArchive, value.PersonEntered })).Tag = Convert.ToInt32(value.ID);
                }

                updateStatus.UpdateStatusBar(tapeValues.Count + " Value(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("Nothing in Database to display", mainform);
            }
        }

        /// <summary>
        /// Populates the project values into the list.
        /// </summary>
        private void PopulateProjectValuesInList()
        {
            //get all values for listview
            List<ProjectValues> projectValues = DataBaseControls.GetAllDeletedProjectValues();
            if (projectValues != null)
            {
                //Values returned in list
                //iterate over list and add values to listview
                foreach (ProjectValues value in projectValues)
                {
                    databaseListView.Items.Add(new ListViewItem(new string[] { value.ProjectID, value.Projectname })).Tag = Convert.ToInt32(value.ID);
                }

                updateStatus.UpdateStatusBar(projectValues.Count + " Value(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("Nothing in Database to display", mainform);
            }
        }

        /// <summary>
        /// Populates the people values into the list.
        /// </summary>
        private void PopulatePeopleValuesInList()
        {
            //get all values for listview
            List<PeopleValues> peopleValues = DataBaseControls.GetAllDeletedPeople();
            if (peopleValues != null)
            {
                //Values returned in list
                //iterate over list and add values to listview
                foreach (PeopleValues value in peopleValues)
                {
                    databaseListView.Items.Add(new ListViewItem(new string[] { value.PersonName })).Tag = Convert.ToInt32(value.ID);
                }

                updateStatus.UpdateStatusBar(peopleValues.Count + " Value(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("Nothing in Database to display", mainform);
            }
        }

        /// <summary>
        /// Populates the master list values into the list.
        /// </summary>
        private void PopulateMasterListValuesInList()
        {
            //get all values for listview
            List<MasterListValues> masterListValues = DataBaseControls.GetAllDeletedMasterListValues();
            if (masterListValues != null)
            {
                //Values returned in list
                //iterate over list and add values to listview
                foreach (MasterListValues value in masterListValues)
                {
                    databaseListView.Items.Add(new ListViewItem(new string[] { value.MasterArchive, commonMethod.GetCameraName(value.MasterMedia) })).Tag = Convert.ToInt32(value.ID);
                }

                updateStatus.UpdateStatusBar(masterListValues.Count + " Value(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("Nothing in Database to display", mainform);
            }
        }

        /// <summary>
        /// Populates the master archive values into the l ist.
        /// </summary>
        private void PopulateMasterArchiveValuesInLIst()
        {
            //get all values for listview
            List<MasterArchiveVideoValues> masterArchiveValues = DataBaseControls.GetAllDeletedMasterArchiveValues();
            if (masterArchiveValues != null)
            {
                //Values returned in list
                //iterate over list and add values to listview
                foreach (MasterArchiveVideoValues value in masterArchiveValues)
                {
                    databaseListView.Items.Add(new ListViewItem(new string[] { value.ProjectId, value.VideoName, value.MasterTape, value.ClipNumber })).Tag = Convert.ToInt32(value.ID);
                }

                updateStatus.UpdateStatusBar(masterArchiveValues.Count + " Value(s) loaded", mainform);
            }
            else
            {
                updateStatus.UpdateStatusBar("Nothing in Database to display", mainform);
            }
        }

        #endregion

        #region Load Panel With Values

        private void LoadTapePanel()
        {
            //Clear flow layout panels
            ClearFlowLayouts();

            //make Button visible and enabled
            ButtonVisibleAndEnabled();

            //load default values
            tapeDBValues = new TapeDatabaseValues();
            tapeDBValues.ID = Convert.ToInt32(databaseListView.SelectedItems[0].Tag);
            tapeDBValues.ProjectId = databaseListView.SelectedItems[0].SubItems[0].Text;
            tapeDBValues.ProjectName = databaseListView.SelectedItems[0].SubItems[1].Text;
            tapeDBValues.TapeName = databaseListView.SelectedItems[0].SubItems[2].Text;
            tapeDBValues.TapeNumber = databaseListView.SelectedItems[0].SubItems[3].Text;
            tapeDBValues.Camera = commonMethod.GetCameraNumber(databaseListView.SelectedItems[0].SubItems[4].Text);
            tapeDBValues.TapeTags = databaseListView.SelectedItems[0].SubItems[5].Text;
            tapeDBValues.DateShot = databaseListView.SelectedItems[0].SubItems[6].Text;
            tapeDBValues.MasterArchive = databaseListView.SelectedItems[0].SubItems[7].Text;
            tapeDBValues.PersonEntered = databaseListView.SelectedItems[0].SubItems[8].Text;

            //create labels to show values
            //set 1
            Label projectTapeID1 = new Label();
            projectTapeID1.Text = "Project ID: ";
            projectTapeID1.Width = 100;

            Label projectTapeID2 = new Label();
            projectTapeID2.Text = tapeDBValues.ProjectId;

            //set 2
            Label projectTapeName1 = new Label();
            projectTapeName1.Text = "Project Name: ";
            projectTapeName1.Width = 100;

            Label projectTapeName2 = new Label();
            projectTapeName2.Text = tapeDBValues.ProjectName;

            //set 3
            Label projectTapeTapeName1 = new Label();
            projectTapeTapeName1.Text = "Tape Name: ";
            projectTapeTapeName1.Width = 100;

            Label projectTapeTapeName2 = new Label();
            projectTapeTapeName2.Text = tapeDBValues.TapeName;

            //set 4
            Label projectTapeTapeNum1 = new Label();
            projectTapeTapeNum1.Text = "Tape Number: ";
            projectTapeTapeNum1.Width = 100;

            Label projectTapeTapeNum2 = new Label();
            projectTapeTapeNum2.Text = tapeDBValues.TapeNumber;

            //set 5
            Label projectTapeCam1 = new Label();
            projectTapeCam1.Text = "Camera: ";
            projectTapeCam1.Width = 100;

            Label projectTapeCam2 = new Label();
            projectTapeCam2.Text = commonMethod.GetCameraName(tapeDBValues.Camera);

            //set 6
            Label projectTapeTags1 = new Label();
            projectTapeTags1.Text = "Tape Tags: ";
            projectTapeTags1.Width = 100;

            Label projectTapeTags2 = new Label();
            projectTapeTags2.Text = tapeDBValues.TapeTags;

            //set 7
            Label projectTapeDate1 = new Label();
            projectTapeDate1.Text = "Date Shot: ";
            projectTapeDate1.Width = 100;

            Label projectTapeDate2 = new Label();
            projectTapeDate2.Text = tapeDBValues.DateShot;

            //set 8
            Label projectTapeMaster1 = new Label();
            projectTapeMaster1.Text = "Master Archive: ";
            projectTapeMaster1.Width = 100;

            Label projectTapeMaster2 = new Label();
            projectTapeMaster2.Text = tapeDBValues.MasterArchive;

            //set 9
            Label projectTapePerson1 = new Label();
            projectTapePerson1.Text = "Entered By: ";
            projectTapePerson1.Width = 100;

            Label projectTapePerson2 = new Label();
            projectTapePerson2.Text = tapeDBValues.PersonEntered;
            
            //Add Labels to corresponding flowlayouts
            flowLayoutPanel1.Controls.AddRange(new Control[] { projectTapeID1, projectTapeID2 });
            flowLayoutPanel2.Controls.AddRange(new Control[] { projectTapeName1, projectTapeName2 });
            flowLayoutPanel3.Controls.AddRange(new Control[] { projectTapeTapeName1, projectTapeTapeName2 });
            flowLayoutPanel4.Controls.AddRange(new Control[] { projectTapeTapeNum1, projectTapeTapeNum2 });
            flowLayoutPanel5.Controls.AddRange(new Control[] { projectTapeCam1, projectTapeCam2 });
            flowLayoutPanel6.Controls.AddRange(new Control[] { projectTapeTags1, projectTapeTags2 });
            flowLayoutPanel7.Controls.AddRange(new Control[] { projectTapeDate1, projectTapeDate2 });
            flowLayoutPanel8.Controls.AddRange(new Control[] { projectTapeMaster1, projectTapeMaster2 });
            flowLayoutPanel9.Controls.AddRange(new Control[] { projectTapePerson1, projectTapePerson2 });
        }

        private void LoadProjectsPanel()
        {
            //Clear flow layout panels
            ClearFlowLayouts();

            //make Button visible and enabled
            ButtonVisibleAndEnabled();

            //load default values
            projectsDBValues = new ProjectValues();
            projectsDBValues.ID = Convert.ToInt32(databaseListView.SelectedItems[0].Tag);
            projectsDBValues.ProjectID = databaseListView.SelectedItems[0].SubItems[0].Text;
            projectsDBValues.Projectname = databaseListView.SelectedItems[0].SubItems[1].Text;

            //create labels to show values
            //set 1
            Label projectID1 = new Label();
            projectID1.Text = "Project ID: ";
            projectID1.Width = 100;

            Label projectID2 = new Label();
            projectID2.Text = projectsDBValues.ProjectID;

            //set 2
            Label projectName1 = new Label();
            projectName1.Text = "Project Name: ";
            projectName1.Width = 100;

            Label projectName2 = new Label();
            projectName2.Text = projectsDBValues.Projectname;

            //Add Labels to corresponding flowlayouts
            flowLayoutPanel1.Controls.AddRange(new Control[] { projectID1, projectID2 });
            flowLayoutPanel2.Controls.AddRange(new Control[] { projectName1, projectName2 });
        }

        private void LoadPeoplePanel()
        {
            //Clear flow layout panels
            ClearFlowLayouts();

            //make Button visible and enabled
            ButtonVisibleAndEnabled();

            //load default values
            peopleDBValues = new PeopleValues();
            peopleDBValues.ID = Convert.ToInt32(databaseListView.SelectedItems[0].Tag);
            peopleDBValues.PersonName = databaseListView.SelectedItems[0].SubItems[0].Text;

            //create labels to show values
            //set 1
            Label personName1 = new Label();
            personName1.Text = "Person Name: ";
            personName1.Width = 100;

            Label personName2 = new Label();
            personName2.Text = peopleDBValues.PersonName;

            //Add Labels to corresponding flowlayouts
            flowLayoutPanel1.Controls.AddRange(new Control[] { personName1, personName2 });
        }

        private void LoadMasterListPanel()
        {
            //Clear flow layout panels
            ClearFlowLayouts();

            //make Button visible and enabled
            ButtonVisibleAndEnabled();

            //load default values
            masterListDBValues = new MasterListValues();
            masterListDBValues.ID = Convert.ToInt32(databaseListView.SelectedItems[0].Tag);
            masterListDBValues.MasterArchive = databaseListView.SelectedItems[0].SubItems[0].Text;
            masterListDBValues.MasterMedia = commonMethod.GetCameraNumber(databaseListView.SelectedItems[0].SubItems[1].Text);

            //create labels to show values
            //set 1
            Label masterArchive1 = new Label();
            masterArchive1.Text = "Project ID: ";
            masterArchive1.Width = 100;

            Label masterArchive2 = new Label();
            masterArchive2.Text = masterListDBValues.MasterArchive;

            //set 2
            Label masterMedia1 = new Label();
            masterMedia1.Text = "Project Name: ";
            masterMedia1.Width = 100;

            Label masterMedia2 = new Label();
            masterMedia2.Text = commonMethod.GetCameraName(masterListDBValues.MasterMedia);

            //Add Labels to corresponding flowlayouts
            flowLayoutPanel1.Controls.AddRange(new Control[] { masterArchive1, masterArchive2 });
            flowLayoutPanel2.Controls.AddRange(new Control[] { masterMedia1, masterMedia2 });
        }

        private void LoadMasterArchivePanel()
        {
            //Clear flow layout panels
            ClearFlowLayouts();

            //make Button visible and enabled
            ButtonVisibleAndEnabled();

            //load default values
            masterArchiveValues = new MasterArchiveVideoValues();
            masterArchiveValues.ID = Convert.ToInt32(databaseListView.SelectedItems[0].Tag);
            masterArchiveValues.ProjectId = databaseListView.SelectedItems[0].SubItems[0].Text;
            masterArchiveValues.VideoName = databaseListView.SelectedItems[0].SubItems[1].Text;
            masterArchiveValues.MasterTape = databaseListView.SelectedItems[0].SubItems[2].Text;
            masterArchiveValues.ClipNumber = databaseListView.SelectedItems[0].SubItems[3].Text;

            //create labels to show values
            //set 1
            Label projectID1 = new Label();
            projectID1.Text = "Project ID: ";
            projectID1.Width = 100;

            Label projectID2 = new Label();
            projectID2.Text = tapeDBValues.ProjectId;

            //set 2
            Label videoName1 = new Label();
            videoName1.Text = "Video Name: ";
            videoName1.Width = 100;

            Label videoName2 = new Label();
            videoName2.Text = tapeDBValues.ProjectName;

            //set 3
            Label masterTape1 = new Label();
            masterTape1.Text = "Master Archive: ";
            masterTape1.Width = 100;

            Label masterTape2 = new Label();
            masterTape2.Text = tapeDBValues.TapeName;

            //set 4
            Label clipNumber1 = new Label();
            clipNumber1.Text = "Clip Number: ";
            clipNumber1.Width = 100;

            Label clipNumber2 = new Label();
            clipNumber2.Text = tapeDBValues.TapeNumber;

            //Add Labels to corresponding flowlayouts
            flowLayoutPanel1.Controls.AddRange(new Control[] { projectID1, projectID2 });
            flowLayoutPanel2.Controls.AddRange(new Control[] { videoName1, videoName2 });
            flowLayoutPanel3.Controls.AddRange(new Control[] { masterTape1, masterTape2 });
            flowLayoutPanel4.Controls.AddRange(new Control[] { clipNumber1, clipNumber2 });
        }

        #endregion

        #region Reinstate values

        private void ReinstateTapevalue()
        {
            if(tapeDBValues != null)
            {
                AddToDatabase addTapeDB = new AddToDatabase();
                if (addTapeDB.AddTapeDatabase(tapeDBValues, true))
                {
                    updateStatus.UpdateStatusBar("Tape Entry Added Back To Database!", mainform);
                    ClearFlowLayouts();
                    ButtonInvisibleAndDisabled();
                    tapeDBValues = null;
                    LoadTapeDBPage();
                }
                else
                {
                    updateStatus.UpdateStatusBar("There Was An Error Adding Tape Back To Database", mainform);
                }
            }
            
        }

        private void ReinstateProject()
        {
            if (projectsDBValues != null)
            {
                AddToDatabase addTapeDB = new AddToDatabase();
                if (addTapeDB.AddProjects(projectsDBValues, true))
                {
                    updateStatus.UpdateStatusBar("Project Entry Added Back To Database!", mainform);
                    ClearFlowLayouts();
                    ButtonInvisibleAndDisabled();
                    projectsDBValues = null;
                    LoadProjectsPage();
                }
                else
                {
                    updateStatus.UpdateStatusBar("There Was An Error Adding Project Back To Database", mainform);
                }
            }
        }

        private void ReinstatePerson()
        {
            if (peopleDBValues != null)
            {
                AddToDatabase addTapeDB = new AddToDatabase();
                if (addTapeDB.AddPerson(peopleDBValues, true))
                {
                    updateStatus.UpdateStatusBar("Person Entry Added Back To Database!", mainform);
                    ClearFlowLayouts();
                    ButtonInvisibleAndDisabled();
                    peopleDBValues = null;
                    LoadPeoplePage();
                }
                else
                {
                    updateStatus.UpdateStatusBar("There Was An Error Adding Person Back To Database", mainform);
                }
            }
        }

        private void ReinstateMasterList()
        {
            if (masterListDBValues != null)
            {
                AddToDatabase addTapeDB = new AddToDatabase();
                if (addTapeDB.AddMasterList(masterListDBValues, true))
                {
                    updateStatus.UpdateStatusBar("Master Archive Entry Added Back To Database!", mainform);
                    ClearFlowLayouts();
                    ButtonInvisibleAndDisabled();
                    masterListDBValues = null;
                    LoadMasterListPage();
                }
                else
                {
                    updateStatus.UpdateStatusBar("There Was An Error Adding Master Archive Back To Database", mainform);
                }
            }
        }

        private void ReinstateMasterArchiveVideo()
        {
            if (masterArchiveValues != null)
            {
                AddToDatabase addTapeDB = new AddToDatabase();
                if (addTapeDB.AddMasterArchiveVideo(masterArchiveValues, true))
                {
                    updateStatus.UpdateStatusBar("Master Archive Video Entry Added Back To Database!", mainform);
                    ClearFlowLayouts();
                    ButtonInvisibleAndDisabled();
                    masterArchiveValues = null;
                    LoadMasterListPage();
                }
                else
                {
                    updateStatus.UpdateStatusBar("There Was An Error Adding Master Archive Video Back To Database", mainform);
                }
            }
        }

        #endregion

        #region Main Combo functions

        //combo selction changed
        private void deleteFormSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (GetEnumDropdown(deleteFormSelectCombo.Text.Replace(" ", "").ToLower()))
            {
                case DeleteForm.tapeDatabase:
                    if (!currentFormState.Equals(DeleteForm.tapeDatabase))
                    {
                        currentFormState = DeleteForm.tapeDatabase;
                        LoadTapeDBPage();
                    }

                    break;
                case DeleteForm.projects:
                    if (!currentFormState.Equals(DeleteForm.projects))
                    {
                        currentFormState = DeleteForm.projects;
                        LoadProjectsPage();
                    }
                    break;
                case DeleteForm.people:
                    if (!currentFormState.Equals(DeleteForm.people))
                    {
                        currentFormState = DeleteForm.people;
                        LoadPeoplePage();
                    }
                    break;
                case DeleteForm.masterList:
                    if (!currentFormState.Equals(DeleteForm.masterList))
                    {
                        currentFormState = DeleteForm.masterList;
                        LoadMasterListPage();
                    }
                    break;
                case DeleteForm.masterArchiveVideos:
                    if (!currentFormState.Equals(DeleteForm.masterArchiveVideos))
                    {
                        currentFormState = DeleteForm.masterArchiveVideos;
                        LoadMasterArchiveVideosPage();
                    }
                    break;
                default:
                    if (!currentFormState.Equals(DeleteForm.tapeDatabase))
                    {
                        currentFormState = DeleteForm.tapeDatabase;
                        LoadTapeDBPage();
                    }
                    break;
            }
        }

        //combo key press
        private void deleteFormSelectCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        //combo closed
        private void deleteFormSelectCombo_DropDownClosed(object sender, EventArgs e)
        {
            databaseListView.Focus();
        }

        #endregion

        #region List View Events

        private void databaseListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (databaseListView.SelectedItems.Count > 0)
            {
                switch (currentFormState)
                {
                    case DeleteForm.tapeDatabase:
                        LoadTapePanel();
                        break;
                    case DeleteForm.projects:
                        LoadProjectsPanel();
                        break;
                    case DeleteForm.people:
                        LoadPeoplePanel();
                        break;
                    case DeleteForm.masterList:
                        LoadMasterListPanel();
                        break;
                    case DeleteForm.masterArchiveVideos:
                        LoadMasterArchivePanel();
                        break;
                    default:
                        LoadTapePanel();
                        break;
                }
            }
            else
            {
                //nothing selected
                ClearFlowLayouts();
                ButtonInvisibleAndDisabled();
            }
        }


        #endregion

        //Reinstate Button clicked
        private void deleteReinstateButton_Click(object sender, EventArgs e)
        {
            switch (currentFormState)
            {
                case DeleteForm.tapeDatabase:
                    ReinstateTapevalue();
                    break;
                case DeleteForm.projects:
                    ReinstateProject();
                    break;
                case DeleteForm.people:
                    ReinstatePerson();
                    break;
                case DeleteForm.masterList:
                    ReinstateMasterList();
                    break;
                case DeleteForm.masterArchiveVideos:
                    ReinstateMasterArchiveVideo();
                    break;
                default:
                    ReinstateTapevalue();
                    break;
            }
        }
    }
}