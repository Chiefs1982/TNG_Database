using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    class UserValues
    {
        int id;
        string name;

        public UserValues(int _id, string _name)
        {
            id = _id;
            name = _name;
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
