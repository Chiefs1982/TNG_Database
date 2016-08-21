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

            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;


        }

        #region Class Methods

        /// <summary>
        /// Populates the ListBox.
        /// </summary>
        private void PopulateListBox()
        {
            List<MasterListValues> values = DataBaseControls.GetAllMasterListItems();

            foreach(MasterListValues value in values)
            {
                viewMasterListBox.Items.Add(value.MasterArchive);
            }
        }

        #endregion
    }
}
