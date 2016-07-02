using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class TapeListForm : Form, IUpdateApplicationStatus
    {
        private Point boxLocation = new Point(12, 317);
        private TNG_Database.MainForm mainform;
        private List<TapeDatabaseValues> tapeListValues;
        private TapeDatabaseValues tapeValues = new TapeDatabaseValues();

        public TapeListForm()
        {
            InitializeComponent();
        }

        public TapeListForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;

            PopulateTapeList();

        }

        //----------------------------------------
        //------INTERFACE METHODS-----------------
        //----------------------------------------

        MainForm IUpdateApplicationStatus.mainform
        {
            get{ return mainform; }
            set{ mainform = value; }
        }

        void IUpdateApplicationStatus.UpdateApplicationStatus(string update)
        {
            mainform.applicationStatusLabel.Text = update;
        }

        //-------------------------------------------
        //------------CLASS METHODS------------------
        //-------------------------------------------

        /// <summary>
        /// Populates the tape list in the listview.
        /// </summary>
        private void PopulateTapeList()
        {
            //clear listview
            tapeListListView.Items.Clear();

            //Run method to get all items in Master list
            DataBaseControls database = new DataBaseControls();
            tapeListValues = database.GetAllTapeValues();

            if (tapeListValues[0].ID.Equals(0))
            {
                //nothing was in database to return
                Console.WriteLine("There are NO values in list");
            }
            else
            {
                Console.WriteLine("There are values in list");
                //List returned values, populate listview
                foreach (TapeDatabaseValues values in tapeListValues)
                {
                    tapeListListView.Items.Add(new ListViewItem(new string[] { values.ID.ToString(), values.ProjectId, values.ProjectName, values.TapeName, values.TapeNumber, values.Camera.ToString(), values.TapeTags, values.DateShot, values.MasterArchive }));
                }
            }

            
        }
    }
}
