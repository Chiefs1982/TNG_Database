﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TNG_Database
{
    public partial class PreferencesForm : Form
    {

        private TNG_Database.MainForm mainform;


        public PreferencesForm(TNG_Database.MainForm parent)
        {
            InitializeComponent();
            this.MdiParent = parent;
            mainform = parent;
        }
        
    }
}
