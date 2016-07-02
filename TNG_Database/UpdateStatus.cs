using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    class UpdateStatus
    {
        private static UpdateStatus instance;

        private static readonly object syncRoot = new object();

        private UpdateStatus()
        {
            
        }

        public static UpdateStatus Instance()
        {
            if(instance == null)
            {
                lock (syncRoot)
                {
                    if(instance == null)
                    {
                        instance = new UpdateStatus();
                    }
                }
            }

            return instance;
        }

        public void UpdateStatusBar(string update, TNG_Database.MainForm mainForm)
        {
            
        }

    }
}
