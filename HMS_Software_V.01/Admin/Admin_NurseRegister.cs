﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS_Software_V._01.Admin
{
    public partial class Admin_NurseRegister : Form
    {
        public Admin_NurseRegister()
        {
            InitializeComponent();
            this.FormClosed += (s, e) => new Admin_Dashboard().Show();
        }
    }
}