using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNG_Database.Values;

namespace TNG_Database
{
    class CommonMethods
    {
        private static CommonMethods instance;

        private static readonly object syncRoot = new object();

        private List<string> tagList = new List<string>();

        //Date variables
        private string dbDatePattern = "MM/dd/yyyy";
        private DateTime dropdownParsedDate;

        private CommonMethods()
        {

        }

        //Instantiates one and only one reference to this class
        public static CommonMethods Instance()
        {
            if(instance == null)
            {
                lock (syncRoot)
                {
                    if(instance == null)
                    {
                        instance = new CommonMethods();
                    }
                }
            }

            return instance;
        }

        //---------------------------------------------
        //--------CLASS METHODS------------------------
        //---------------------------------------------


        /// <summary>
        /// Return a string for media device
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public string GetCameraName(Int32 camera)
        {
            switch (camera)
            {
                case 1:
                    return "XDCam";
                case 2:
                    return "Canon";
                case 3:
                    return "Beta";
                case 4:
                    return "DVC";
                default:
                    return "Standard";
            }
        }

        /// <summary>
        /// Gets the camera value.
        /// </summary>
        /// <param name="camera">Name of camera selected</param>
        /// <returns>int of the camera to store in db</returns>
        public int GetCameraNumber(string camera)
        {
            switch (camera.ToLower())
            {
                case "cannon":
                    return 2;
                case "xdcam":
                    return 1;
                case "beta":
                    return 3;
                case "dvc":
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
        public int GetCameraDropdownIndex(string camera)
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
        //---------------------------------------

        /// <summary>
        /// Default dropdown items for the Camera
        /// </summary>
        /// <returns></returns>
        public string[] CameraDropdownItems()
        {
            return new string[] { "Canon", "XDCam", "Beta", "DVC", "Other" };
        }

        /// <summary>
        /// Converts the date from dropdown for database.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public string ConvertDateFromDropdownForDB(DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Converts the date for the date picker.
        /// </summary>
        /// <param name="dateFromDB">The date from database.</param>
        /// <returns></returns>
        public DateTime ConvertDateForDatePicker(string dateFromDB)
        {
            if (DateTime.TryParseExact(dateFromDB, dbDatePattern, null,
                                  DateTimeStyles.None, out dropdownParsedDate))
            {
                return dropdownParsedDate;
            }
            else
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Displays the tags.
        /// </summary>
        /// <param name="gb">groupbox target</param>
        /// <param name="gbPanel">the FlowLayoutPanel to add items to</param>
        /// <param name="tagList">the tag list to use</param>
        public void DisplayTags(FlowLayoutPanel gbPanel, List<string> tagList)
        {
            if (tagList.Count > 0 && !tagList[0].Equals(""))
            {
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

                    //add the FLP to the larger FLP to display tags as seperate items
                    gbPanel.Controls.Add(flp);
                }
            }
        }

        //Load column values into a listview
        #region ListView Values Load

        /// <summary>
        /// Loads columns for the tape ListView.
        /// </summary>
        /// <param name="listView">The list view.</param>
        public void LoadTapeListView(ListView listView)
        {
            //Clear Database of colummns
            listView.Clear();
            listView.Items.Clear();

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
            colTapeTNum.Width = 30;
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
            colTapePerson.Width = 70;
            colTapePerson.TextAlign = HorizontalAlignment.Left;

            listView.Columns.AddRange(new ColumnHeader[] { colTapePID, colTapePN, colTapeTN, colTapeTNum, colTapeCam, colTapeTags, colTapeDate, colTapeMaster, colTapePerson });
        }

        /// <summary>
        /// Loads columns for the masters ListView.
        /// </summary>
        /// <param name="listView">The list view.</param>
        public void LoadMastersListView(ListView listView)
        {
            //Clear Database of colummns
            listView.Clear();
            listView.Items.Clear();

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
            colMasterMT.Width = 100;
            colMasterMT.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colMasterClip = new ColumnHeader();
            colMasterClip.Text = "Clip #";
            colMasterClip.Width = 60;
            colMasterClip.TextAlign = HorizontalAlignment.Left;

            listView.Columns.AddRange(new ColumnHeader[] { colMasterPID, colMasterVN, colMasterMT, colMasterClip });
        }

        /// <summary>
        /// Loads columns for the projects ListView.
        /// </summary>
        /// <param name="listView">The list view.</param>
        public void LoadProjectsListView(ListView listView)
        {
            //Clear Database of colummns
            listView.Clear();
            listView.Items.Clear();

            //Creat columns:
            ColumnHeader colProjectsPID = new ColumnHeader();
            colProjectsPID.Text = "Project ID";
            colProjectsPID.Width = 75;
            colProjectsPID.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colProjectsPN = new ColumnHeader();
            colProjectsPN.Text = "Project Name";
            colProjectsPN.Width = 300;
            colProjectsPN.TextAlign = HorizontalAlignment.Left;

            listView.Columns.AddRange(new ColumnHeader[] { colProjectsPID, colProjectsPN });
        }

        /// <summary>
        /// Loads columns for the search all ListView.
        /// </summary>
        /// <param name="listView">The list view.</param>
        public void LoadSearchAllListView(ListView listView)
        {
            //Clear Database of colummns
            listView.Clear();
            listView.Items.Clear();

            //Load COlumns
            ColumnHeader colSearchID = new ColumnHeader();
            colSearchID.Text = "Project ID";
            colSearchID.Width = 60;
            colSearchID.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchPN = new ColumnHeader();
            colSearchPN.Text = "Project Name";
            colSearchPN.Width = 135;
            colSearchPN.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchTN = new ColumnHeader();
            colSearchTN.Text = "Tape Name";
            colSearchTN.Width = 135;
            colSearchTN.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchTNum = new ColumnHeader();
            colSearchTNum.Text = "Tape #";
            colSearchTNum.Width = 25;
            colSearchTNum.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchCam = new ColumnHeader();
            colSearchCam.Text = "Camera";
            colSearchCam.Width = 60;
            colSearchCam.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchTags = new ColumnHeader();
            colSearchTags.Text = "Tags";
            colSearchTags.Width = 110;
            colSearchTags.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchDate = new ColumnHeader();
            colSearchDate.Text = "Date Shot";
            colSearchDate.Width = 78;
            colSearchDate.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchMaster = new ColumnHeader();
            colSearchMaster.Text = "Master Archive";
            colSearchMaster.Width = 75;
            colSearchMaster.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchPerson = new ColumnHeader();
            colSearchPerson.Text = "Entered By";
            colSearchPerson.Width = 80;
            colSearchPerson.TextAlign = HorizontalAlignment.Left;

            ColumnHeader colSearchClip = new ColumnHeader();
            colSearchClip.Text = "Clip #";
            colSearchClip.Width = 60;
            colSearchClip.TextAlign = HorizontalAlignment.Left;

            listView.Columns.AddRange(new ColumnHeader[] { colSearchID, colSearchPN, colSearchTN, colSearchTNum, colSearchCam, colSearchTags, colSearchDate, colSearchMaster, colSearchPerson, colSearchClip });
        }

        #endregion

        //Load column values into a flowlayout panel
        #region FlowLayout Panel Values Load

        public void LoadSearchAllFlowValues(FlowLayoutPanel[] panels, SearchValues values)
        {
            //create labels to show values
            //set 1
            Label projectTapeID1 = new Label();
            projectTapeID1.Text = "Project ID: ";
            projectTapeID1.Width = 100;

            Label projectTapeID2 = new Label();
            projectTapeID2.Text = values.ProjectID;
            projectTapeID2.AutoSize = true;

            //set 2
            Label projectTapeName1 = new Label();
            projectTapeName1.Text = "Project Name: ";
            projectTapeName1.Width = 100;

            Label projectTapeName2 = new Label();
            projectTapeName2.Text = values.ProjectName;
            projectTapeName2.AutoSize = true;

            //set 3
            Label projectTapeTapeName1 = new Label();
            projectTapeTapeName1.Text = "Tape Name: ";
            projectTapeTapeName1.Width = 100;

            Label projectTapeTapeName2 = new Label();
            projectTapeTapeName2.Text = values.TapeName;
            projectTapeTapeName2.AutoSize = true;

            //set 4
            Label projectTapeTapeNum1 = new Label();
            projectTapeTapeNum1.Text = "Tape Number: ";
            projectTapeTapeNum1.Width = 100;

            Label projectTapeTapeNum2 = new Label();
            projectTapeTapeNum2.Text = values.TapeNumber;
            projectTapeTapeNum2.AutoSize = true;

            //set 5
            Label projectTapeCam1 = new Label();
            projectTapeCam1.Text = "Camera: ";
            projectTapeCam1.Width = 100;

            Label projectTapeCam2 = new Label();
            projectTapeCam2.Text = values.Camera;
            projectTapeCam2.AutoSize = true;

            //set 6
            Label projectTapeTags1 = new Label();
            projectTapeTags1.Text = "Tape Tags: ";
            projectTapeTags1.Width = 100;

            tagList = values.TapeTags.Split(',').ToList();

            //set 7
            Label projectTapeDate1 = new Label();
            projectTapeDate1.Text = "Date Shot: ";
            projectTapeDate1.Width = 100;

            Label projectTapeDate2 = new Label();
            projectTapeDate2.Text = values.DateShot;
            projectTapeDate2.AutoSize = true;

            //set 8
            Label projectTapeMaster1 = new Label();
            projectTapeMaster1.Text = "Master Archive: ";
            projectTapeMaster1.Width = 100;

            Label projectTapeMaster2 = new Label();
            projectTapeMaster2.Text = values.MasterArchive + "     Clip #: " + values.ClipNumber;
            projectTapeMaster2.AutoSize = true;

            //set 9
            Label projectTapePerson1 = new Label();
            projectTapePerson1.Text = "Entered By: ";
            projectTapePerson1.Width = 100;

            Label projectTapePerson2 = new Label();
            projectTapePerson2.Text = values.Person;
            projectTapePerson2.AutoSize = true;

            //Add Labels to corresponding flowlayouts
            panels[0].Controls.AddRange(new Control[] { projectTapeID1, projectTapeID2 });
            panels[1].Controls.AddRange(new Control[] { projectTapeName1, projectTapeName2 });
            panels[2].Controls.AddRange(new Control[] { projectTapeTapeName1, projectTapeTapeName2 });
            panels[3].Controls.AddRange(new Control[] { projectTapeTapeNum1, projectTapeTapeNum2 });
            panels[4].Controls.AddRange(new Control[] { projectTapeCam1, projectTapeCam2 });
            panels[5].Controls.AddRange(new Control[] { projectTapeTags1 });
            DisplayTags(panels[5], tagList);
            panels[6].Controls.AddRange(new Control[] { projectTapeDate1, projectTapeDate2 });
            panels[7].Controls.AddRange(new Control[] { projectTapeMaster1, projectTapeMaster2 });
            panels[8].Controls.AddRange(new Control[] { projectTapePerson1, projectTapePerson2 });
        }

        public void LoadTapesFlowValues(FlowLayoutPanel[] panels, SearchValues values)
        {
            //create labels to show values
            //set 1
            Label projectTapeID1 = new Label();
            projectTapeID1.Text = "Project ID: ";
            projectTapeID1.Width = 100;

            Label projectTapeID2 = new Label();
            projectTapeID2.Text = values.ProjectID;
            projectTapeID2.AutoSize = true;

            //set 2
            Label projectTapeName1 = new Label();
            projectTapeName1.Text = "Project Name: ";
            projectTapeName1.Width = 100;

            Label projectTapeName2 = new Label();
            projectTapeName2.Text = values.ProjectName;
            projectTapeName2.AutoSize = true;

            //set 3
            Label projectTapeTapeName1 = new Label();
            projectTapeTapeName1.Text = "Tape Name: ";
            projectTapeTapeName1.Width = 100;

            Label projectTapeTapeName2 = new Label();
            projectTapeTapeName2.Text = values.TapeName;
            projectTapeTapeName2.AutoSize = true;

            //set 4
            Label projectTapeTapeNum1 = new Label();
            projectTapeTapeNum1.Text = "Tape Number: ";
            projectTapeTapeNum1.Width = 100;

            Label projectTapeTapeNum2 = new Label();
            projectTapeTapeNum2.Text = values.TapeNumber;
            projectTapeTapeNum2.AutoSize = true;

            //set 5
            Label projectTapeCam1 = new Label();
            projectTapeCam1.Text = "Camera: ";
            projectTapeCam1.Width = 100;

            Label projectTapeCam2 = new Label();
            projectTapeCam2.Text = values.Camera;
            projectTapeCam2.AutoSize = true;

            //set 6
            Label projectTapeTags1 = new Label();
            projectTapeTags1.Text = "Tape Tags: ";
            projectTapeTags1.Width = 100;

            tagList = values.TapeTags.Split(',').ToList();

            //set 7
            Label projectTapeDate1 = new Label();
            projectTapeDate1.Text = "Date Shot: ";
            projectTapeDate1.Width = 100;

            Label projectTapeDate2 = new Label();
            projectTapeDate2.Text = values.DateShot;
            projectTapeDate2.AutoSize = true;

            //set 8
            Label projectTapeMaster1 = new Label();
            projectTapeMaster1.Text = "Master Archive: ";
            projectTapeMaster1.Width = 100;

            Label projectTapeMaster2 = new Label();
            projectTapeMaster2.Text = values.MasterArchive;
            projectTapeMaster2.AutoSize = true;

            //set 9
            Label projectTapePerson1 = new Label();
            projectTapePerson1.Text = "Entered By: ";
            projectTapePerson1.Width = 100;

            Label projectTapePerson2 = new Label();
            projectTapePerson2.Text = values.Person;
            projectTapePerson2.AutoSize = true;

            //Add Labels to corresponding flowlayouts
            panels[0].Controls.AddRange(new Control[] { projectTapeID1, projectTapeID2 });
            panels[1].Controls.AddRange(new Control[] { projectTapeName1, projectTapeName2 });
            panels[2].Controls.AddRange(new Control[] { projectTapeTapeName1, projectTapeTapeName2 });
            panels[3].Controls.AddRange(new Control[] { projectTapeTapeNum1, projectTapeTapeNum2 });
            panels[4].Controls.AddRange(new Control[] { projectTapeCam1, projectTapeCam2 });
            panels[5].Controls.AddRange(new Control[] { projectTapeTags1 });
            DisplayTags(panels[5], tagList);
            panels[6].Controls.AddRange(new Control[] { projectTapeDate1, projectTapeDate2 });
            panels[7].Controls.AddRange(new Control[] { projectTapeMaster1, projectTapeMaster2 });
            panels[8].Controls.AddRange(new Control[] { projectTapePerson1, projectTapePerson2 });
        }

        public void LoadMasterTapesFlowValues(FlowLayoutPanel[] panels, SearchValues values)
        {
            //create labels to show values
            //set 1
            Label projectID1 = new Label();
            projectID1.Text = "Project ID: ";
            projectID1.Width = 100;

            Label projectID2 = new Label();
            projectID2.Text = values.ProjectID;
            projectID2.AutoSize = true;

            //set 2
            Label videoName1 = new Label();
            videoName1.Text = "Video Name: ";
            videoName1.Width = 100;

            Label videoName2 = new Label();
            videoName2.Text = values.ProjectName;
            videoName2.AutoSize = true;

            //set 3
            Label masterTape1 = new Label();
            masterTape1.Text = "Master Archive: ";
            masterTape1.Width = 100;

            Label masterTape2 = new Label();
            masterTape2.Text = values.MasterArchive;
            masterTape2.AutoSize = true;

            //set 4
            Label clipNumber1 = new Label();
            clipNumber1.Text = "Clip Number: ";
            clipNumber1.Width = 100;

            Label clipNumber2 = new Label();
            clipNumber2.Text = values.ClipNumber;
            clipNumber2.AutoSize = true;

            //Add Labels to corresponding flowlayouts
            panels[0].Controls.AddRange(new Control[] { projectID1, projectID2 });
            panels[1].Controls.AddRange(new Control[] { videoName1, videoName2 });
            panels[2].Controls.AddRange(new Control[] { masterTape1, masterTape2 });
            panels[3].Controls.AddRange(new Control[] { clipNumber1, clipNumber2 });
        }

        public void LoadProjectsFlowValues(FlowLayoutPanel[] panels, SearchValues values)
        {
            //create labels to show values
            //set 1
            Label projectID1 = new Label();
            projectID1.Text = "Project ID: ";
            projectID1.Width = 100;

            Label projectID2 = new Label();
            projectID2.Text = values.ProjectID;
            projectID2.AutoSize = true;

            //set 2
            Label projectName1 = new Label();
            projectName1.Text = "Project Name: ";
            projectName1.Width = 100;

            Label projectName2 = new Label();
            projectName2.Text = values.ProjectName;
            projectName2.AutoSize = true;

            //Add Labels to corresponding flowlayouts
            panels[0].Controls.AddRange(new Control[] { projectID1, projectID2 });
            panels[1].Controls.AddRange(new Control[] { projectName1, projectName2 });
        }

        #endregion


    }
}
