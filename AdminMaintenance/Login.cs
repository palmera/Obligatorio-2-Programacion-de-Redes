﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminMaintenance
{
    public partial class Login : Form
    {
        private AdminMaintenance maintenance;
        public Login()
        {
            InitializeComponent();
            maintenance = new AdminMaintenance();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void saveAdmin_Click(object sender, EventArgs e)
        {
            string name = Name.Text;
            string password = Password.Text;
            if (maintenance.Login(name, password))
                startMaintenanceScreen();
            else
                MessageBox.Show("Nombre/Password incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void startMaintenanceScreen()
        {

        }
    }
}