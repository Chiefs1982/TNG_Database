using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    class MasterTapeValues
    {
        private int id;
        private string project_id;
        private string video_name;
        private string master_tape;
        private string clip_number;

        //Blank Constructor
        public MasterTapeValues(int _id = 0) { }

        //Constructor for class that takes in all variables
        public MasterTapeValues(string _project_id, string _video_name, string _master_tape, string _clip_number, int _id = 0)
        {
            id = _id;
            project_id = _project_id;
            video_name = _video_name;
            master_tape = _master_tape;
            clip_number = _clip_number;
        }

        //Clear MasterTapeValues
        public void Clear()
        {
            id = 0;
            project_id = null;
            video_name = null;
            master_tape = null;
            clip_number = null;
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

        public string VideoName
        {
            get { return video_name; }
            set { video_name = value; }
        }

        public string MasterTape
        {
            get { return master_tape; }
            set { master_tape = value; }
        }

        public string ClipNumber
        {
            get { return clip_number; }
            set { clip_number = value; }
        }
    }
}
