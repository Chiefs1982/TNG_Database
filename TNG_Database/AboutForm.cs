using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace TNG_Database
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            versionLabel.Text = "v" + GetVersionNumber();
            aboutCloseButton.Focus();
        }
        
        private string GetVersionNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fileVersionInfo.ProductVersion;
        }

        private void aboutCloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
