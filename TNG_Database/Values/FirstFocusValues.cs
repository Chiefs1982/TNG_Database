using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    class FirstFocusValues
    {

        private bool projectID = false;
        private bool projectName = false;
        private bool tapeName = false;
        private bool tapeNumber = false;
        private bool dateShot = false;
        private bool masterTape = false;
        private bool personEntered = false;
        private bool clipNumber = false;
        private bool videoName = false;
        private bool camera = false;
        private bool tags = false;

        //Constructor to set all values to false
        public FirstFocusValues()
        {
            Reset();
        }

        /// <summary>
        /// Resets the values to false
        /// </summary>
        public void Reset()
        {
            projectID = false;
            projectName = false;
            tapeName = false;
            tapeNumber = false;
            dateShot = false;
            masterTape = false;
            personEntered = false;
            clipNumber = false;
            videoName = false;
            camera = false;
            tags = false;
        }


        #region Variable Methods

        public bool ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public bool ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public bool TapeName
        {
            get { return tapeName; }
            set { tapeName = value; }
        }

        public bool TapeNumber
        {
            get { return tapeNumber; }
            set { tapeNumber = value; }
        }

        public bool DateShot
        {
            get { return dateShot; }
            set { dateShot = value; }
        }

        public bool MasterTape
        {
            get { return masterTape; }
            set { masterTape = value; }
        }

        public bool PersonEntered
        {
            get { return personEntered; }
            set { personEntered = value; }
        }

        public bool ClipNumber
        {
            get { return clipNumber; }
            set { clipNumber = value; }
        }

        public bool VideoName
        {
            get { return videoName; }
            set { videoName = value; }
        }

        public bool Camera
        {
            get { return camera; }
            set { camera = value; }
        }

        public bool Tags
        {
            get { return tags; }
            set { tags = value; }
        }

        #endregion
    }
}
