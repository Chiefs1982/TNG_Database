using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace TNG_Database
{
    public partial class MainForm : Form
    {
        public TNG_Database.SearchTapeForm searchTapeForm;
        public TNG_Database.PeopleForm peopleForm;
        public TNG_Database.MasterListForm masterListForm;
        public TNG_Database.TapeListForm tapeListForm;
        
        public MainForm()
        {
            InitializeComponent();
            
            TNG_Database.SearchTapeForm child = new TNG_Database.SearchTapeForm(this);
            child.Show();
            child.WindowState = FormWindowState.Maximized;
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
            TapeDatabaseValues tbv = new TapeDatabaseValues("Teacher Service Agreement", "1", "16012", "Teacher Service Agreement", 1, "teacher, service, agreement, schools, k12", "DATE", "Master 52", "Aaron Primmer");
            TapeDatabaseValues tbv2 = new TapeDatabaseValues("Teacher Service Agreement", "1", "16015", "Teacher Service Agreement", 1, "teacher, service, agreement, schools, k12", "DATE", "Master 52", "Aaron Primmer");
            //string add_name = "Brett snyder";
            string add_name = "Master 51";
            //string editToName = "Brett Snyder";
            string editToName = "Master 52";
            AddToDatabase addDB = new AddToDatabase();
            if(addDB.UpdateTapeDatabase(tbv2, 3, "16012")){
                Console.WriteLine(add_name + " was updated from Tape DB");
            }else
            {
                Console.WriteLine(add_name + " was not updated from Tape DB");
            }
            
        }
        */
        
        //Click on Close
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //-----------------------------------
        //Opens up people form to add, delete or update user list
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new instance of form
            peopleForm = new PeopleForm(this);

            //close child of mdi if there is one active
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //Show people form and maximize it instantly
            peopleForm.Show();
            peopleForm.WindowState = FormWindowState.Maximized;
        }

        //-----------------------------------
        //Opens up search tape database form to search tape database
        private void tapeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new instance of form SearchTapeForm
            tapeListForm = new TNG_Database.TapeListForm(this);

            //close child of mdi if there is one active
            if(ActiveMdiChild != null){ ActiveMdiChild.Close(); }

            //Show search tape database form and maximize it instantly
            tapeListForm.Show();
            tapeListForm.WindowState = FormWindowState.Maximized;
        }

        //-----------------------------------
        //Opens up Master list form to add, delete, update master archive list
        private void masterArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Create new instance of form MasterListForm
            masterListForm = new TNG_Database.MasterListForm(this);

            //close chold of mdi if there is one active
            if(ActiveMdiChild != null) { ActiveMdiChild.Close(); }

            //show Master list form and maximize it instantly
            masterListForm.Show();
            masterListForm.WindowState = FormWindowState.Maximized;
            
        }

        public void UpdateStatusBarBottom(string update)
        {
            applicationStatusLabel.Text = update;  
        }
    }
}
