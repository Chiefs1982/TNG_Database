using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    //This class holds information regarding tape info to be inserted into database
    class TapeDatabaseValues
    {
        private int id;
        private string tape_name;
        private string tape_number;
        private string project_id;
        private string project_name;
        private int camera;
        private string tape_tags;
        private string date_shot;
        private string master_archive;
        private string person_entered;

        //For Default no values constructor
        public TapeDatabaseValues()
        {
            id = 0;
        }


        public TapeDatabaseValues(string tapeName, string tapeNumber, string projectID,
                                    string projectName, int cameraNumber, string tapeTags,
                                    string dateShot, string masterArchive, string personEntered, int _id = 0)
        {
            id = _id;
            tape_name = tapeName;
            tape_number = tapeNumber;
            project_id = projectID;
            project_name = projectName;
            camera = cameraNumber;
            tape_tags = tapeTags;
            date_shot = dateShot;
            master_archive = masterArchive;
            person_entered = personEntered;
        }

        //Getters & Setters
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string TapeName
        {
            get{ return tape_name; }
            set{ tape_name = value; }
        }

        public string TapeNumber
        {
            get{ return tape_number; }
            set{ tape_number = value; }
        }

        public string ProjectId
        {
            get{ return project_id; }
            set{ project_id = value; }
        }

        public string ProjectName
        {
            get{ return project_name; }
            set{ project_name = value; }
        }

        public int Camera
        {
            get{ return camera; }
            set{ camera = value; }
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
            get { return master_archive; }
            set { master_archive = value; }
        }

        public string PersonEntered
        {
            get { return person_entered; }
            set { person_entered = value; }
        }
    }
}
