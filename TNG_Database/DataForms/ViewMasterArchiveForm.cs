using System;
using System.Collections;
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
    public partial class ViewMasterArchiveForm : Form
    {
        //reference for the mainform
        private TNG_Database.MainForm mainform;

        //CommonMethod reference
        CommonMethods commonMethod = CommonMethods.Instance();
        UpdateStatus updateStatus = UpdateStatus.Instance();

        int sortColumn = -1;

        public ViewMasterArchiveForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();

            this.MdiParent = parent;
            mainform = parent;

            PopulateListBox();

            viewMasterListView.ColumnClick += new ColumnClickEventHandler(ViewMasterListView_ColumnClick);
        }

        #region Class Methods

        /// <summary>
        /// Populates the ListBox.
        /// </summary>
        private void PopulateListBox()
        {
            List<MasterListValues> values = DataBaseControls.GetAllMasterListItems();

            foreach (MasterListValues value in values)
            {
                viewMasterListBox.Items.Add(value.MasterArchive);
            }
        }

        private void PopulateListView(string selected)
        {
            //Clear list view
            viewMasterListView.Items.Clear();

            //TODO Create method in DB Controls to get all items in MasterArchiveVideos that match selected Master Tape
            List<MasterArchiveVideoValues> videosList = DataBaseControls.GetAllMasterListValues(selected);

            if (videosList.Count > 0)
            {
                foreach (MasterArchiveVideoValues value in videosList)
                {
                    viewMasterListView.Items.Add(new ListViewItem(new string[] { value.ProjectId, value.VideoName, value.ClipNumber }));
                }
            }
        }

        #endregion

        private void viewMasterListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check to see if something is selected
            if (viewMasterListBox.SelectedIndex != -1)
            {
                //Populate List view
                PopulateListView(viewMasterListBox.GetItemText(viewMasterListBox.SelectedItem));
            }
            else
            {
                viewMasterListView.Items.Clear();
            }
        }

        private void ViewMasterListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                viewMasterListView.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (viewMasterListView.Sorting == SortOrder.Ascending)
                    viewMasterListView.Sorting = SortOrder.Descending;
                else
                    viewMasterListView.Sorting = SortOrder.Ascending;
            }
            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            viewMasterListView.ListViewItemSorter = new ListViewItemComparer(e.Column, viewMasterListView.Sorting);
            // Call the sort method to manually sort.
            viewMasterListView.Sort();
        }

        // Implements the manual sorting of items by column.
        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                // Determine whether the sort order is descending.
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;
                return returnVal;
            }

        }
    }
}
