using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class SearchTapeForm : Form
    {
        TNG_Database.MainForm mainForm;
        private List<TapeDatabaseValues> searchList;
        private TapeDatabaseValues searchValues;
        private List<string> tagList = new List<string>();

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();
        

        public SearchTapeForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainForm = parent;

            searchItemsPanel.Visible = false;
            searchNoItemSelectedLabel.Visible = true;

            //keep items highlighted
            searchListView.HideSelection = false;

            updateStatus.UpdateStatusBar("Tape Database Ready to Search", mainForm);
        }

        public SearchTapeForm()
        {
            InitializeComponent();

        }

        private void InitializeCompnent()
        {
            this.components = new System.ComponentModel.Container();
        }

        /// <summary>
        /// Populates the search list.
        /// </summary>
        /// <param name="input">The input.</param>
        private void PopulateSearchList(string input)
        {
            searchListView.Items.Clear();
            if(searchList != null)
            {
                searchList.Clear();
            }

            ClearLabels();

            DataBaseControls dbControl = new DataBaseControls();
            searchList = dbControl.SearchAllDB(input.Split(' ')); ;

            if (searchList.Count.Equals(0))
            {
                updateStatus.UpdateStatusBar("No Items Mathced Search, Try Again", mainForm);
                Console.WriteLine("Nothing found");
            }else
            {
                foreach(TapeDatabaseValues values in searchList)
                {
                    searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectId, values.ProjectName, values.TapeName, values.TapeNumber, commonMethod.GetCameraName(values.Camera), values.TapeTags, values.DateShot, values.MasterArchive, values.PersonEntered })).Tag = Convert.ToInt32(values.ID);
                }
                updateStatus.UpdateStatusBar(searchList.Count + " item entries found", mainForm);
                Console.WriteLine(searchList.Count + " item entries found");
            }
            searchListView.Focus();
        }

        /// <summary>
        /// Displays the tags.
        /// </summary>
        /// <param name="gb">groupbox target</param>
        /// <param name="gbPanel">the FlowLayoutPanel to add items to</param>
        /// <param name="tagList">the tag list to use</param>
        private void DisplayTags(FlowLayoutPanel gbPanel, List<string> tagList)
        {
            //clear panel
            gbPanel.Controls.Clear();

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

        /// <summary>
        /// Adds the list item to values.
        /// </summary>
        private void AddListItemToValues()
        {
            searchValues = new TapeDatabaseValues();
            searchValues.ID = Convert.ToInt32(searchListView.SelectedItems[0].Tag);
            searchValues.ProjectId = searchListView.SelectedItems[0].SubItems[0].Text;
            searchValues.ProjectName = searchListView.SelectedItems[0].SubItems[1].Text;
            searchValues.TapeName = searchListView.SelectedItems[0].SubItems[2].Text;
            searchValues.TapeNumber = searchListView.SelectedItems[0].SubItems[3].Text;
            searchValues.Camera = commonMethod.GetCameraNumber(searchListView.SelectedItems[0].SubItems[4].Text);
            searchValues.TapeTags = searchListView.SelectedItems[0].SubItems[5].Text;
            searchValues.DateShot = searchListView.SelectedItems[0].SubItems[6].Text;
            searchValues.MasterArchive = searchListView.SelectedItems[0].SubItems[7].Text;
            searchValues.PersonEntered = searchListView.SelectedItems[0].SubItems[8].Text;
        }

        /// <summary>
        /// Adds the values to labels.
        /// </summary>
        private void AddValuesToLabels()
        {
            //load values to user can see
            searchProjectIDLabel.Text = searchValues.ProjectId;
            searchProjectNameLabel.Text = searchValues.ProjectName;
            searchTapeNameLabel.Text = searchValues.TapeName;
            searchTapeNumberLabel.Text = searchValues.TapeNumber;
            searchCameraLabel.Text = commonMethod.GetCameraName(searchValues.Camera);
            searchDateLabel.Text = searchValues.DateShot;
            searchMasterArchiveLabel.Text = searchValues.MasterArchive;
            searchPersonLabel.Text = searchValues.PersonEntered;
            //set to display tags
            tagList = searchValues.TapeTags.Split(',').ToList();
            DisplayTags(searchTagFlowLayoutPanel, tagList);
            //make the right items display in groupbox
            searchNoItemSelectedLabel.Visible = false;
            searchItemsPanel.Visible = true;
        }

        /// <summary>
        /// Clears the labels.
        /// </summary>
        private void ClearLabels()
        {
            searchProjectIDLabel.Text = "";
            searchProjectNameLabel.Text = "";
            searchTapeNameLabel.Text = "";
            searchTapeNumberLabel.Text = "";
            searchCameraLabel.Text = "";
            searchDateLabel.Text = "";
            searchMasterArchiveLabel.Text = "";
            searchPersonLabel.Text = "";
            searchTagFlowLayoutPanel.Controls.Clear();
            tagList.Clear();
            if(searchValues != null)
            {
                searchValues.Clear();
            }
            //Make appropriate items visible
            searchItemsPanel.Visible = false;
            searchNoItemSelectedLabel.Visible = true;
        }

        //enter pressed in textbox
        private void searchTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals((char)Keys.Enter))
            {
                if(searchTextbox.Text.Length > 0 && !searchTextbox.Text.Equals(" "))
                {
                    PopulateSearchList(searchTextbox.Text.Trim());
                }
                e.Handled = true;
            }
        }

        //Listview item index changed
        private void searchListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (searchListView.SelectedItems.Count > 0)
            {
                AddListItemToValues();
                AddValuesToLabels();
            }
            else
            {
                ClearLabels();
            }
        }

        //Search Button clicked
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchTextbox.Text.Length > 0)
            {
                PopulateSearchList(searchTextbox.Text);
            }
        }
    }
}
