using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sql
{
    public partial class frmMain : Form
    {
        private string currentUser;
        private string currentRole;
        public frmMain(string username, string role)
        {
            InitializeComponent();
            currentUser = username;
            currentRole = role;
            label1.Text = "Welcome, " + currentUser;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?",
                                    "Confirm Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                Login loginForm = new Login();
                loginForm.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            
            Application.Exit();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEmployee empForm = new frmEmployee(currentRole);
            empForm.ShowDialog();  
            this.Show();

        }
    }
}
