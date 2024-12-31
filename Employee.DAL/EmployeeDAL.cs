using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using Employee.Domain;
using System;
using System.Data;

namespace Employee.DAL
{
    public class EmployeeDAL
    {
        public SqlConnection _connection { set; get; }
        private SqlCommand _command;
        public List<EmployeeModel> Employees { get; set; }
        public List<EmployeeModel> SortEmployees { get; set; }
        public EmployeeDAL()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EmployeeCon"].ConnectionString);
            Employees = new List<EmployeeModel>();
            SortEmployees = new List<EmployeeModel>();
        }
        public List<EmployeeModel> GetEmployees()
        {
            _command = new SqlCommand("select * from Employee", _connection);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EmployeeModel emp = new EmployeeModel()
                    {
                        Eno = Convert.ToInt32(dr["Eno"]),
                        Ename = Convert.ToString(dr["Ename"]),
                        Job = Convert.ToString(dr["Job"]),
                        Salary = Convert.ToDouble(dr["Salary"]),
                        Dname = Convert.ToString(dr["Dname"])
                    };
                    Employees.Add(emp);
                }
                _connection.Close();
                return Employees;
            }
            return new List<EmployeeModel>();
        }
        public string InsertEmployee(EmployeeModel employee)
        {
            string query = $"insert into Employee values({employee.Eno},'{employee.Ename}','{employee.Job}',{employee.Salary},'{employee.Dname}')";
            _command = new SqlCommand(query, _connection);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            string msg = dr.RecordsAffected + " Record(s) Affected";
            _connection.Close();
            return msg;
        }
        public string UpdateEmployee(EmployeeModel employee)
        {
            _command = new SqlCommand("[dbo].[UpdateEmployee]", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            Console.WriteLine("Select which one to be update: 1.Ename 2.Job 3.Dname");
            int userinput = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the selected value:");
            string value = Console.ReadLine();
            try
            {
                _connection.Open();
                _command.Parameters.AddWithValue("@eno", employee.Eno);
                _command.Parameters.AddWithValue("@input", userinput);
                _command.Parameters.AddWithValue("@value", value);
                SqlDataReader dr = _command.ExecuteReader();
                string msg = dr.RecordsAffected + "Record(s) Affected";
                _connection.Close();
                return msg;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return "";
        }
        public string DeleteEmployee(EmployeeModel employee)
        {
            _command = new SqlCommand("[dbo].[DeleteEmployee]", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@eno", employee.Eno);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            _connection.Close();
            string msg = dr.RecordsAffected + "Record(s) Affected";
            return msg;
        }

        public List<EmployeeModel> SortEmployee(string column , string order)
        {

            _command = new SqlCommand("[dbo].[SortEmployee]", _connection);
            _command.CommandType = CommandType.StoredProcedure;
            _command.Parameters.AddWithValue("@column", column);
            _command.Parameters.AddWithValue("@order", order);
            _connection.Open();
            SqlDataReader dr = _command.ExecuteReader();
            SortEmployees.Clear();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    EmployeeModel emp = new EmployeeModel()
                    {
                        Eno = Convert.ToInt32(dr["Eno"]),
                        Ename = Convert.ToString(dr["Ename"]),
                        Job = Convert.ToString(dr["Job"]),
                        Salary = Convert.ToDouble(dr["Salary"]),
                        Dname = Convert.ToString(dr["Dname"])
                    };
                    SortEmployees.Add(emp);
                }
            }
            _connection.Close();
            return SortEmployees;
        }
    }
}