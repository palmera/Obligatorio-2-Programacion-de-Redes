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
            var list = maintenance.GetAllAdmins();

            adminListBox.Items.AddRange(list.ToArray());
            
        }

        private void RemoveAdminButton_Click(object sender, EventArgs e)
        {

        }

        private void addAdminButton_Click(object sender, EventArgs e)
        {
            var userName = nameTextBox.Text;
            var password = passwordTextBox.Text;
            
        }
    }
}
