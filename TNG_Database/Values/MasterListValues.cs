using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    //Class that keeps values for Master List Archive for Records
    class MasterListValues
    {
        private int id;
        private string master_archive;
        private int master_media;

        //Blacnk Constructor
        public MasterListValues() { }

        //Constructor for class that takes in all variables
        public MasterListValues(string _masterArchive, int _masterMedia, int _id = 0)
        {
            id = _id;
            master_archive = _masterArchive;
            master_media = _masterMedia;
        }

        public void Clear()
        {
            id = 0;
            master_archive = "";
            master_media = 0;
        }

        //------------------------------------
        //------ACCESSOR METHODS--------------
        //------------------------------------

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string MasterArchive
        {
            get { return master_archive; }
            set { master_archive = value; }
        }

        public int MasterMedia
        {
            get { return master_media; }
            set { master_media = value; }
        }

    }
}
