using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    public class ComputerInfo
    {
        //create only one reference to this class
        private static ComputerInfo instance;

        private static readonly object syncRoot = new object();

        private static string uniqueHash = HashAString(GetMacAddress());
        private static string computerName = Environment.MachineName;
        private static string computerUser;
        private MainForm mainForm = null;

        public ComputerInfo()
        {

        }

        //Instantiates one and only one reference to this class
        public static ComputerInfo Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new ComputerInfo();
                    }
                }
            }

            ComputerUser = DataBaseControls.CheckComputerInfo(computerName, uniqueHash);

            Debug.WriteLine("This is the computer info instance");
            Debug.WriteLine("Computer Name: " + ComputerName);
            Debug.WriteLine("Computer Hash: " + UniqueHash);
            Debug.WriteLine("Computer User: " + ComputerUser);
            
            return instance;
        }

        public void UpdateUserName(MainForm form)
        {
            if (mainForm == null)
            {
                mainForm = form;
            }

            mainForm.UpdatePersonStatus(ComputerUser);
        }

        public void UpdateUserInDB(string name)
        {
            if(name.Length > 0)
            {
                ComputerUser = name;
                DataBaseControls.UpdateCurrentUser(ComputerName, UniqueHash, ComputerUser);
                UpdateUserName(mainForm);
            }
        }
        
        /// <summary>
        /// Gets or sets the unique hash.
        /// </summary>
        /// <value>
        /// The unique hash of the computer.
        /// </value>
        public static string UniqueHash
        {
            get { return uniqueHash; }
        }

        public static string ComputerName
        {
            get { return computerName; }
        }

        public static string ComputerUser
        {
            get { return computerUser; }
            set { computerUser = value; }
        }

        /// <summary>
        /// Gets the mac address.
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string macAddr =
                (
                    from nic in NetworkInterface.GetAllNetworkInterfaces()
                    where nic.OperationalStatus == OperationalStatus.Up
                    select nic.GetPhysicalAddress().ToString()
                ).FirstOrDefault();

            return macAddr;
        }

        /// <summary>
        /// Hashes a string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string HashAString(string input)
        {
            return input.GetHashCode().ToString();
        }
    }
}
