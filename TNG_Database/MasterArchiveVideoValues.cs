using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    class MasterArchiveVideoValues
    {
        private int id;
        private string project_id;
        private string video_name;
        private string master_tape;
        private int clip_number;

        //For Default no values constructor
        public MasterArchiveVideoValues()
        {
            id = 0;
        }


        public MasterArchiveVideoValues(string projectID, string videoName, string masterTape, int clipNumber, int _id = 0)
        {
            id = _id;
            project_id = projectID;
            video_name = videoName;
            master_tape = masterTape;
            clip_number = clipNumber;
        }

        public void Clear()
        {
            id = 0;
            project_id = null;
            video_name = null;
            clip_number = 0;
        }

        //Getters & Setters
        public int ID
        {
            get { return id; }
            set { id = value; }
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

        public string ProjectId
        {
            get { return project_id; }
            set { project_id = value; }
        }

        public int ClipNumber
        {
            get { return clip_number; }
            set { clip_number = value; }
        }
        
    }
}
