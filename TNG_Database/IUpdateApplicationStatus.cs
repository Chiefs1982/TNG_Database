using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNG_Database
{
    //Interface to update status label
    interface IUpdateApplicationStatus
    {
        MainForm mainform { get; set; }
        void UpdateApplicationStatus(string update);
    }
}
