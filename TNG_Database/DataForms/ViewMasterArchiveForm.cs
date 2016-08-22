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
        
        public ViewMasterArchiveForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();

            this.MdiParent = parent;
            mainform = parent;

            PopulateListBox();

            //Event for sorting each column
            CommonMethods.ListViewItemComparer.SortColumn = -1;
            viewMasterListView.ColumnClick += new ColumnClickEventHandler(CommonMethods.ListViewItemComparer.SearchListView_ColumnClick);
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
    }
}
