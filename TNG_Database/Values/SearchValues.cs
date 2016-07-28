using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    class SearchValues
    {
        private int id;
        private string project_id = "";
        private string project_name = "";
        private string tape_name = "";
        private string tape_number = "";
        private string camera = "";
        private string tape_tags = "";
        private string date_shot = "";
        private string masterArchive = "";
        private string person = "";
        private string clip_num = "";

        public SearchValues(int _id = 0)
        {
            id = _id;
        }

        public void Clear()
        {
            id = 0;
            project_id = "";
            project_name = "";
            tape_name = "";
            tape_number = "";
            camera = "";
            tape_tags = "";
            date_shot = "";
            masterArchive = "";
            person = "";
            clip_num = "";
        }

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

        public string ProjectName
        {
            get { return project_name; }
            set { project_name = value; }
        }

        public string TapeName
        {
            get { return tape_name; }
            set { tape_name = value; }
        }

        public string TapeNumber
        {
            get { return tape_number; }
            set { tape_number = value; }
        }

        public string Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public string TapeTags
        {
            get { return tape_tags; }
            set { tape_tags = value; }
        }

        public string DateShot
        {
            get { return date_shot; }
            set { date_shot = value; }
        }

        public string MasterArchive
        {
            get { return masterArchive; }
            set { masterArchive = value; }
        }

        public string Person
        {
            get { return person; }
            set { person = value; }
        }

        public string ClipNumber
        {
            get { return clip_num; }
            set { clip_num = value; }
        }
    }
}
