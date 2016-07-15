using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    class CommonMethods
    {
        private static CommonMethods instance;

        private static readonly object syncRoot = new object();

        //Date variables
        private string dbDatePattern = "MM/dd/yyyy";
        private DateTime dropdownParsedDate;

        private CommonMethods()
        {

        }

        //Instantiates one and only one reference to this class
        public static CommonMethods Instance()
        {
            if(instance == null)
            {
                lock (syncRoot)
                {
                    if(instance == null)
                    {
                        instance = new CommonMethods();
                    }
                }
            }

            return instance;
        }

        //---------------------------------------------
        //--------CLASS METHODS------------------------
        //---------------------------------------------


        /// <summary>
        /// Return a string for media device
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public string GetCameraName(Int32 camera)
        {
            switch (camera)
            {
                case 1:
                    return "XDCam";
                case 2:
                    return "Cannon";
                case 3:
                    return "Beta";
                case 4:
                    return "DVC";
                default:
                    return "Standard";
            }
        }

        /// <summary>
        /// Gets the camera value.
        /// </summary>
        /// <param name="camera">Name of camera selected</param>
        /// <returns>int of the camera to store in db</returns>
        public int GetCameraNumber(string camera)
        {
            switch (camera)
            {
                case "Cannon":
                    return 2;
                case "XDCam":
                    return 1;
                case "Beta":
                    return 3;
                case "DVC":
                    return 4;
                default:
                    return 0;

            }
        }

        /// <summary>
        /// Gets the index of the camera.
        /// </summary>
        /// <param name="camera">Name of the camera selected</param>
        /// <returns></returns>
        public int GetCameraDropdownIndex(string camera)
        {
            switch (camera)
            {
                case "Cannon":
                    return 0;
                case "XDCam":
                    return 1;
                case "Beta":
                    return 2;
                case "DVC":
                    return 3;
                default:
                    return 0;
            }
        }
        //---------------------------------------

        /// <summary>
        /// Default dropdown items for the Camera
        /// </summary>
        /// <returns></returns>
        public string[] CameraDropdownItems()
        {
            return new string[] { "Cannon", "XDCam", "Beta", "DVC", "Other" };
        }

        /// <summary>
        /// Converts the date from dropdown for database.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public string ConvertDateFromDropdownForDB(DateTime date)
        {
            return date.ToString("MM/dd/yyyy");
        }

        public DateTime ConvertDateForDatePicker(string dateFromDB)
        {
            if (DateTime.TryParseExact(dateFromDB, dbDatePattern, null,
                                  DateTimeStyles.None, out dropdownParsedDate))
            {
                return dropdownParsedDate;
            }
            else
            {
                return DateTime.Now;
            }
        }

    }
}
