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

        //enum for current selection
        enum Selection
        {
            Nothing,
            Tapes,
            Master,
            Projects,
            All
        }

        TNG_Database.MainForm mainForm;
        private List<SearchValues> searchList = null;
        private SearchValues searchValues;
        private List<string> tagList = new List<string>();
        private Filter currentFilter = Filter.All;
        private Selection currentSelection = Selection.Nothing;

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();
        

        public SearchTapeForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainForm = parent;

            NoValuesInListSelected();

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

        #region Class Methods

        private void NoValuesInListSelected()
        {
            //Clear FlowLayoutPanels
            ClearFlowLayouts();

            //Create Default Nothing Selected Label
            Label defaultLabel = new Label();
            defaultLabel.Text = "Select a value to view the details";
            defaultLabel.Width = 100;

            searchFlowPanel1.Controls.Add(defaultLabel);
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
                    return Filter.Projects;
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
            commonMethod.LoadSearchAllListView(searchListView);
            
            if (searchList != null)
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
                searchListView.Clear();
                switch (currentFilter)
                {
                    case Filter.All:
                    default:
                        commonMethod.LoadSearchAllListView(searchListView);
                        break;
                    case Filter.Tapes:
                        commonMethod.LoadTapeListView(searchListView);
                        break;
                    case Filter.Master:
                        commonMethod.LoadMastersListView(searchListView);
                        break;
                    case Filter.Projects:
                        commonMethod.LoadProjectsListView(searchListView);
                        break;
                }

                //Entries returned, iterate over all entries and add them to list
                foreach (SearchValues values in list)
                {
                    //if filter matches filter selected or if there is no filter selected
                    if (values.FilterName.Equals(filter) || filter.Equals(""))
                    {
                        //only load values specific to the filter selected
                        switch (currentFilter)
                        {
                            case Filter.All:
                                searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName, values.TapeName, values.TapeNumber, values.Camera, values.TapeTags, values.DateShot, values.MasterArchive, values.Person, values.ClipNumber })).Tag = Convert.ToInt32(values.ID);
                                break;
                            case Filter.Tapes:
                                searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName, values.TapeName, values.TapeNumber, values.Camera, values.TapeTags, values.DateShot, values.MasterArchive, values.Person })).Tag = Convert.ToInt32(values.ID);
                                break;
                            case Filter.Master:
                                searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName, values.MasterArchive, values.ClipNumber })).Tag = Convert.ToInt32(values.ID);
                                break;
                            case Filter.Projects:
                                searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName })).Tag = Convert.ToInt32(values.ID);
                                break;
                            default:
                                searchListView.Items.Add(new ListViewItem(new string[] { values.ProjectID, values.ProjectName, values.TapeName, values.TapeNumber, values.Camera, values.TapeTags, values.DateShot, values.MasterArchive, values.Person, values.ClipNumber })).Tag = Convert.ToInt32(values.ID);
                                break;
                        }
                        
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
        /// Adds the list item to values.
        /// </summary>
        private void AddListItemToValues()
        {
            searchValues = new SearchValues();
            
            switch (currentFilter)
            {
                case Filter.Tapes:
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
                    break;
                case Filter.Master:
                    searchValues.ID = Convert.ToInt32(searchListView.SelectedItems[0].Tag);
                    searchValues.ProjectID = searchListView.SelectedItems[0].SubItems[0].Text;
                    searchValues.ProjectName = searchListView.SelectedItems[0].SubItems[1].Text;
                    searchValues.MasterArchive = searchListView.SelectedItems[0].SubItems[2].Text;
                    searchValues.ClipNumber = searchListView.SelectedItems[0].SubItems[3].Text;
                    break;
                case Filter.Projects:
                    searchValues.ID = Convert.ToInt32(searchListView.SelectedItems[0].Tag);
                    searchValues.ProjectID = searchListView.SelectedItems[0].SubItems[0].Text;
                    searchValues.ProjectName = searchListView.SelectedItems[0].SubItems[1].Text;
                    break;
                case Filter.All:
                default:
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
                    break;
            }


        }

        /// <summary>
        /// Adds the values to labels.
        /// </summary>
        private void AddValuesToLabels()
        {
            ClearFlowLayouts();

            //Load flow layouts depending on what values are selected in listview
            if(currentSelection.Equals(Selection.Tapes) || currentFilter.Equals(Filter.Tapes))
            {
                //Tapes selected
                commonMethod.LoadTapesFlowValues(new FlowLayoutPanel[] { searchFlowPanel1, searchFlowPanel2, searchFlowPanel3, searchFlowPanel4, searchFlowPanel5, searchFlowPanel6, searchFlowPanel7, searchFlowPanel8, searchFlowPanel9 }, searchValues);
            }else if(currentSelection.Equals(Selection.Master) || currentFilter.Equals(Filter.Master))
            {
                //Master Tapes selected
                commonMethod.LoadMasterTapesFlowValues(new FlowLayoutPanel[] { searchFlowPanel1, searchFlowPanel2, searchFlowPanel3, searchFlowPanel4 }, searchValues);
            }
            else if(currentSelection.Equals(Selection.Projects) || currentFilter.Equals(Filter.Projects))
            {
                //Projects selected
                commonMethod.LoadProjectsFlowValues(new FlowLayoutPanel[] { searchFlowPanel1, searchFlowPanel2 }, searchValues);
            }else
            {
                //default show all values
                commonMethod.LoadSearchAllFlowValues(new FlowLayoutPanel[] { searchFlowPanel1, searchFlowPanel2, searchFlowPanel3, searchFlowPanel4, searchFlowPanel5, searchFlowPanel6, searchFlowPanel7, searchFlowPanel8, searchFlowPanel9 }, searchValues);
            }



            /*
            switch (currentFilter)
            {
                case Filter.All:
                default:
                    break;
                case Filter.Tapes:
                    commonMethod.LoadTapesFlowValues(new FlowLayoutPanel[] { searchFlowPanel1, searchFlowPanel2, searchFlowPanel3, searchFlowPanel4, searchFlowPanel5, searchFlowPanel6, searchFlowPanel7, searchFlowPanel8, searchFlowPanel9 }, searchValues);
                    break;
                case Filter.Master:
                    break;
                case Filter.Projects:
                    commonMethod.LoadProjectsFlowValues(new FlowLayoutPanel[] { searchFlowPanel1,searchFlowPanel2 }, searchValues);
                    break;
            }
            */
        }

        /// <summary>
        /// Clears the labels.
        /// </summary>
        private void ClearLabels()
        {
            tagList.Clear();
            if(searchValues != null)
            {
                searchValues.Clear();
            }
            currentSelection = Selection.Nothing;
        }

        /// <summary>
        /// Clears the flow layouts.
        /// </summary>
        private void ClearFlowLayouts()
        {
            //Clear flow layout panels
            searchFlowPanel1.Controls.Clear();
            searchFlowPanel2.Controls.Clear();
            searchFlowPanel3.Controls.Clear();
            searchFlowPanel4.Controls.Clear();
            searchFlowPanel5.Controls.Clear();
            searchFlowPanel6.Controls.Clear();
            searchFlowPanel7.Controls.Clear();
            searchFlowPanel8.Controls.Clear();
            searchFlowPanel9.Controls.Clear();
        }

        /// <summary>
        /// Sets the current selection of the listview item.
        /// </summary>
        private void SetCurrentSelection()
        {
            switch (currentFilter)
            {
                case Filter.All:
                    //find out based on values in selection which type of item is selected
                    if (searchListView.SelectedItems[0].SubItems[9].Text.Equals("") && searchListView.SelectedItems[0].SubItems[2].Text.Length > 0)
                    {
                        //Tapes selected
                        currentSelection = Selection.Tapes;
                    }
                    else if (searchListView.SelectedItems[0].SubItems[9].Text.Length > 0 && searchListView.SelectedItems[0].SubItems[7].Text.Length > 0)
                    {
                        //Master Tapes selected
                        currentSelection = Selection.Master;
                    }
                    else if (searchListView.SelectedItems[0].SubItems[9].Text.Equals("") && searchListView.SelectedItems[0].SubItems[2].Text.Equals(""))
                    {
                        //Projects selected
                        currentSelection = Selection.Projects;
                    }
                    else
                    {
                        currentSelection = Selection.All;
                    }
                    break;
                case Filter.Master:
                    currentSelection = Selection.Master;
                    break;
                case Filter.Projects:
                    currentSelection = Selection.Projects;
                    break;
                case Filter.Tapes:
                    currentSelection = Selection.Tapes;
                    break;
                default:
                    currentSelection = Selection.Nothing;
                    break;
            }
        }

        #endregion
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
                SetCurrentSelection();
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
                    default:
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
