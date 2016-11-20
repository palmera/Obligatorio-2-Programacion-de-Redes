using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminMaintenance
{
    public partial class AdminMaintenanceMain : Form
    {
        private AdminDataMaitenance maintenance;
        public AdminMaintenanceMain()
        {
            InitializeComponent();
            maintenance = new AdminDataMaitenance();

            //load admin list
            LoadAdminListBox();
            
        }

        private void RemoveAdminButton_Click(object sender, EventArgs e)
        {

        }

        private void addAdminButton_Click(object sender, EventArgs e)
        {
            var userName = nameTextBox.Text;
            var password = passwordTextBox.Text;

            if (maintenance.AddAdmin(userName, password)) {
                LoadAdminListBox();
            }else System.Windows.Forms.MessageBox.Show("Admin username already exists");

        }
        private void LoadAdminListBox() {
            var list = maintenance.GetAllAdmins();
            adminListBox.Items.Clear();
            adminListBox.Items.AddRange(list.ToArray());
        }
    }
}
