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
            var name = adminListBox.GetItemText(adminListBox.SelectedItem);
            if (name !="")
            {
                if (maintenance.DeleteAdmin(name))
                {
                    System.Windows.Forms.MessageBox.Show("Admin " + name + " deleted");
                    LoadAdminListBox();
                }
                else System.Windows.Forms.MessageBox.Show("An error occured while deleting selected admin");
            }
            else System.Windows.Forms.MessageBox.Show("You must select an admin to remove");
        }

        private void addAdminButton_Click(object sender, EventArgs e)
        {
            var userName = nameTextBox.Text;
            var password = passwordTextBox.Text;

            if (maintenance.AddAdmin(userName, password)) {
                LoadAdminListBox();
                nameTextBox.Text = "";
                passwordTextBox.Text = "";
                System.Windows.Forms.MessageBox.Show("Admin added");
            }
            else System.Windows.Forms.MessageBox.Show("Admin username already exists");

        }
        private void LoadAdminListBox() {
            var list = maintenance.GetAllAdmins();
            adminListBox.Items.Clear();
            adminListBox.Items.AddRange(list.ToArray());
        }

        private void modifyAdminButton_Click(object sender, EventArgs e)
        {
            var name = adminListBox.GetItemText(adminListBox.SelectedItem);
            if (name != "")//make sure an admin is selected
            {
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Enter new name", "Modify Administrator name", name);
                if (maintenance.AdminExists(name))//make sure selected admin still exists
                {
                    if (!maintenance.AdminExists(newName))//make sure new name isn't duplicated
                    {
                        maintenance.ModifyAdmin(name, newName);
                        LoadAdminListBox();
                    }
                    else System.Windows.Forms.MessageBox.Show("That name is currently used");

                }
                else System.Windows.Forms.MessageBox.Show("An error occured while deleting selected admin - the selected admin no longer exists");
            }
            else System.Windows.Forms.MessageBox.Show("You must select an admin to rename");
        }
    }
}
