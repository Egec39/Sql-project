using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Sql
{
    public partial class frmEmployee : Form
    {
        private string userRole;

        public frmEmployee(string role)
        {
            InitializeComponent();
            userRole = role;
            LoadEmployees();
            RestrictAccessByRole();
        }
        private void RestrictAccessByRole()
        {
            if (userRole != "Admin")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button2.BackColor = Color.LightGray;
                button3.BackColor = Color.LightGray;
                button2.Cursor = Cursors.No;
                button3.Cursor = Cursors.No;
                MessageBox.Show("You have view-only access. Only Admins can modify employee data.");
            }
        }

        private void LoadEmployees()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "SELECT * FROM Employees";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }
            private void ClearInputs()
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string position = textBox2.Text.Trim();
            if (!float.TryParse(textBox3.Text.Trim(), out float salary))
            {
                MessageBox.Show("Enter a valid salary.");
                return;
            }

            if (name == "" || position == "")
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "INSERT INTO Employees (Name, Position, Salary) VALUES (@name, @position, @salary)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@salary", salary);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee added.");
                LoadEmployees();
                ClearInputs();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to edit.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmployeeID"].Value);
            string name = textBox1.Text.Trim();
            string position = textBox2.Text.Trim();
            if (!float.TryParse(textBox3.Text.Trim(), out float salary))
            {
                MessageBox.Show("Enter a valid salary.");
                return;
            }

            using (SqlConnection con = DBHelper.GetConnection())
            {
                string query = "UPDATE Employees SET Name=@name, Position=@position, Salary=@salary WHERE EmployeeID=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee updated.");
                LoadEmployees();
                ClearInputs();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select an employee to delete.");
                    return;
                }

                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmployeeID"].Value);

                var result = MessageBox.Show("Are you sure to delete this employee?",
                                             "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection con = DBHelper.GetConnection())
                    {
                        string query = "DELETE FROM Employees WHERE EmployeeID=@id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee deleted.");
                        LoadEmployees();
                        ClearInputs();
                    }
                }
            }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells["Name"].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells["Position"].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells["Salary"].Value.ToString();
            }
        }
    }
    }
    
