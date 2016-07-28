using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    class ProjectValues
    {
        private int id;
        private string project_id;
        private string project_name;

        //Blank Constructor
        public ProjectValues(int _id = 0) { }

        //Constructor for class that takes in all variables
        public ProjectValues(string _project_id, string _project_name, int _id = 0)
        {
            id = _id;
            project_id = _project_id;
            project_name = _project_name;
        }

        //------------------------------------
        //------ACCESSOR METHODS--------------
        //------------------------------------

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string ProjectID
        {
            get { return project_id; }
            set { project_id = value; }
        }

        public string Projectname
        {
            get { return project_name; }
            set { project_name = value; }
        }

    }
}
