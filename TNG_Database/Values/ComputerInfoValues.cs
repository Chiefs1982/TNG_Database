using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database.Values
{
    class ComputerInfoValues
    {
        private string uniqueHash;
        private string computerName;
        private string computerUser;

        public ComputerInfoValues() { }

        public ComputerInfoValues(string _uniqueHash, string _computerName, string _computerUser)
        {
            uniqueHash = _uniqueHash;
            computerName = _computerName;
            computerUser = _computerUser;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            uniqueHash = "";
            computerName = "";
            computerUser = "";
        }

        //Getters and Setters
        public string UniqueHash
        {
            get { return uniqueHash; }
            set { uniqueHash = value; }
        }

        public string ComputerName
        {
            get { return computerName; }
            set { computerName = value; }
        }

        public string ComputerUser
        {
            get { return computerUser; }
            set { computerUser = value; }
        }

    }
}
