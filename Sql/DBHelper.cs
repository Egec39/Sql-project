using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    
    
        public static class DBHelper
        {
            private static readonly string connectionString =
                $"Data Source=.;Initial Catalog=EmployeeDB;Integrated Security=True";

            public static SqlConnection GetConnection()
            {
                return new SqlConnection(connectionString);
            }
        public static bool CheckUserExists(string username)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        
        public static void InsertUser(string username, string password, string role)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, @role)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@role", role);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public static bool ValidateUserLogin(string username, string password)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT COUNT(1) FROM Users WHERE Username = @username AND Password = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        
        public static string GetUserRole(string username)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT Role FROM Users WHERE Username = @username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                return cmd.ExecuteScalar().ToString();
            }
        }

        
        public static DataTable LoadEmployees()
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "SELECT * FROM Employees";
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        
        public static void InsertEmployee(string name, string position, float salary)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "INSERT INTO Employees (Name, Position, Salary) VALUES (@name, @position, @salary)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@salary", salary);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public static void UpdateEmployee(int id, string name, string position, float salary)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "UPDATE Employees SET Name = @name, Position = @position, Salary = @salary WHERE EmployeeID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@position", position);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

       
        public static void DeleteEmployee(int id)
        {
            using (SqlConnection con = GetConnection())
            {
                string query = "DELETE FROM Employees WHERE EmployeeID = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

    

