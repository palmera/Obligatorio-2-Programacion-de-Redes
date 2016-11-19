using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotingConsumer
{
    public partial class Form1 : Form
    {
        IRemotingAdminService.IRemotingAdminService adminService;
        public Form1()
        {
            InitializeComponent();
            adminService = (IRemotingAdminService.IRemotingAdminService)Activator.GetObject
                (typeof(IRemotingAdminService.IRemotingAdminService),
                "tcp://192.168.1.47:5000/RemotingAdminService");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = "agus";
            string password = "agus";
            Administrator admin = new Administrator()
            {
                Name = name,
                Password = password,
            };
            adminService.AddAdmin(1);
        }
    }
}
