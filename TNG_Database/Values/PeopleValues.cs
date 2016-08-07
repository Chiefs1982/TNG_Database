using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    class PeopleValues
    {
        private int id;
        private string person_name;

        //Blank Constructor
        public PeopleValues(int _id = 0) { }

        //Constructor for class that takes in all variables
        public PeopleValues(string _person_name, int _id = 0)
        {
            id = _id;
            person_name = _person_name;
        }

        public void Clear()
        {
            id = 0;
            person_name = null;
        }

        //------------------------------------
        //------ACCESSOR METHODS--------------
        //------------------------------------

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string PersonName
        {
            get { return person_name; }
            set { person_name = value; }
        }
    }
}
