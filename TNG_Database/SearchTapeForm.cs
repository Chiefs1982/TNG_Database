﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TNG_Database.Values;

namespace TNG_Database
{
    public partial class SearchTapeForm : Form
    {
        //enum to determin filter by results
        enum Filter
        {
            All,
            Tapes,
            Master,
            Projects
        }

        TNG_Database.MainForm mainForm;
        private List<SearchValues> searchList = null;
        private SearchValues searchValues;
        private List<string> tagList = new List<string>();
        private Filter currentFilter = Filter.All;

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

            //Load filter combo with defaults
            searchFilterCombo.Items.AddRange(new string[] { "All","Tapes","Master Archive","Projects" });
            searchFilterCombo.SelectedIndex = 0;
            searchFilterCombo.Cursor = Cursors.Default;
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
        /// Gets the enum Filter value.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        private Filter GetFilterValue(string filter)
        {
            switch (filter.Replace(" ","").ToLower())
            {
                case "all":
                    return Filter.All;
                case "tapes":
                    return Filter.Tapes;
                case "masterarchive":
                    return Filter.Master;
                case "projects":
                    return Filter.Master;
                default:
                    return Filter.All;
            }
        }

        /// <summary>
        /// What to filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        private void WhatToFilter(Filter filter)
        {
            switch (filter)
            {
                case Filter.All:
                    PopulateSearchView(searchList);
                    break;
                case Filter.Tapes:
                    PopulateSearchView(searchList, "tapes");
                    break;
                case Filter.Master:
                    PopulateSearchView(searchList, "archive");
                    break;
                case Filter.Projects:
                    PopulateSearchView(searchList, "projects");
                    break;
                default:
                    PopulateSearchView(searchList);
                    break;
            }
        }

        /// <summary>
        /// Populates the search list.
        /// </summary>
        /// <param name="input">The input.</param>
        private void PopulateSearchList(string input)
        {
            //Clear List items and returned list
            searchListView.Items.Clear();
            if(searchList != null)
            {
                searchList.Clear();
            }

            //Clear all display labels
            ClearLabels();

            //Search all databases for entries
            DataBaseControls dbControl = new DataBaseControls();
            searchList = dbControl.SearchAllDB(input.Split(' ')); ;

            //populate search view with searchlist
            //Check if no entries where returned
            if (searchList.Count.Equals(0))
            {
                //No entries returned
                updateStatus.UpdateStatusBar("No Items Mathced Search, Try Again", mainForm);
                Console.WriteLine("Nothing found");
            }
            else
            {
                //Entries returned, iterate over all entries and add them to list
                foreach (SearchValues values in searchList)
                {
                    searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName, values.TapeName, values.TapeNumber, values.Camera, values.TapeTags, values.DateShot, values.MasterArchive, values.Person, values.ClipNumber })).Tag = Convert.ToInt32(values.ID);
                }
                updateStatus.UpdateStatusBar(searchList.Count + " item entries found", mainForm);
                Console.WriteLine(searchList.Count + " item entries found");
            }
            //set entries returned number
            if (searchList.Count != 1)
            {
                searchTotalFoundLabel.Text = "( " + searchList.Count + " ) entries found";
            }
            else
            {
                searchTotalFoundLabel.Text = "( " + searchList.Count + " ) entry found";
            }

            searchListView.Focus();
        }

        /// <summary>
        /// Populates the search view from filter.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="filter">The filter.</param>
        private void PopulateSearchView(List<SearchValues> list, string filter = "")
        {
            //Check if no entries where returned
            if (list == null)
            {
                //No entries returned
                updateStatus.UpdateStatusBar("Nothing to filter", mainForm);
            }
            else
            {
                searchListView.Items.Clear();
                //Entries returned, iterate over all entries and add them to list
                foreach (SearchValues values in list)
                {
                    if (values.FilterName.Equals(filter) || filter.Equals(""))
                    {
                        searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName, values.TapeName, values.TapeNumber, values.Camera, values.TapeTags, values.DateShot, values.MasterArchive, values.Person, values.ClipNumber })).Tag = Convert.ToInt32(values.ID);
                    }
                }
            }

            //set focus to the listview
            searchListView.Focus();
        }

        /// <summary>
        /// Resets the search values and filter.
        /// </summary>
        private void ResetSearchValuesAndFilter()
        {
            currentFilter = Filter.All;
            searchList = null;
            searchFilterCombo.SelectedIndex = 0;
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

            if(tagList.Count > 0 && !tagList[0].Equals(""))
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

        /// <summary>
        /// Adds the list item to values.
        /// </summary>
        private void AddListItemToValues()
        {
            searchValues = new SearchValues();
            searchValues.ID = Convert.ToInt32(searchListView.SelectedItems[0].Tag);
            searchValues.ProjectID = searchListView.SelectedItems[0].SubItems[0].Text;
            searchValues.ProjectName = searchListView.SelectedItems[0].SubItems[1].Text;
            searchValues.TapeName = searchListView.SelectedItems[0].SubItems[2].Text;
            searchValues.TapeNumber = searchListView.SelectedItems[0].SubItems[3].Text;
            searchValues.Camera = searchListView.SelectedItems[0].SubItems[4].Text;
            searchValues.TapeTags = searchListView.SelectedItems[0].SubItems[5].Text;
            searchValues.DateShot = searchListView.SelectedItems[0].SubItems[6].Text;
            searchValues.MasterArchive = searchListView.SelectedItems[0].SubItems[7].Text;
            searchValues.Person = searchListView.SelectedItems[0].SubItems[8].Text;
            searchValues.ClipNumber = searchListView.SelectedItems[0].SubItems[9].Text;
        }

        /// <summary>
        /// Adds the values to labels.
        /// </summary>
        private void AddValuesToLabels()
        {
            //load values to user can see
            searchProjectIDLabel.Text = searchValues.ProjectID;
            searchProjectNameLabel.Text = searchValues.ProjectName;
            searchTapeNameLabel.Text = searchValues.TapeName;
            searchTapeNumberLabel.Text = searchValues.TapeNumber;
            searchCameraLabel.Text = searchValues.Camera;
            searchDateLabel.Text = searchValues.DateShot;
            searchMasterArchiveLabel.Text = searchValues.MasterArchive;
            searchPersonLabel.Text = searchValues.Person;
            searchClipNameLabel.Text = searchValues.ClipNumber;
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
            searchClipNameLabel.Text = "";
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
                    ResetSearchValuesAndFilter();
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
                ResetSearchValuesAndFilter();
                PopulateSearchList(searchTextbox.Text);
            }
        }

        #region Filter Combobox

        private void searchFilterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(searchList != null)
            {
                switch (GetFilterValue(searchFilterCombo.Text))
                {
                    case Filter.All:
                        currentFilter = Filter.All;
                        break;
                    case Filter.Tapes:
                        currentFilter = Filter.Tapes;
                        break;
                    case Filter.Master:
                        currentFilter = Filter.Master;
                        break;
                    case Filter.Projects:
                        currentFilter = Filter.Projects;
                        break;
                    default:
                        currentFilter = Filter.All;
                        break;
                }
                WhatToFilter(currentFilter);
            }
        }

        private void searchFilterCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void searchFilterCombo_DropDownClosed(object sender, EventArgs e)
        {
            searchListView.Focus();
        }

        #endregion


    }
}
