using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database.DatabaseControls
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
        
        public DeletedValuesForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();

            this.MdiParent = parent;
            mainform = parent;

            
            
        }

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

        #region Main Combo functions

        //combo selction changed
        private void deleteFormSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (GetEnumDropdown(deleteFormSelectCombo.Text.ToLower()))
            {
                
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

        }

        #endregion
    }
}
