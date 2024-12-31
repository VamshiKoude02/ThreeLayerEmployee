using Employee.BAL;
using Employee.Domain;
using System;
using System.Collections.Generic;

namespace Employee.UI
{
    internal class Program
    {
        static EmployeeBAL objBAL;
        static Program()
        {
            objBAL = new EmployeeBAL();
        }
        static EmployeeModel GetInputEmp()
        {
            Console.WriteLine("Enter Employee Details: ");
            return new EmployeeModel()
            {
                Eno = Convert.ToInt32(Console.ReadLine()),
                Ename = Console.ReadLine(),
                Job = Console.ReadLine(),
                Salary = Convert.ToDouble(Console.ReadLine()),
                Dname = Console.ReadLine()
            };
        }
        static EmployeeModel GetUpdateEmp()
        {
            Console.WriteLine("enter employee number to be update");
            return new EmployeeModel()
            {
                Eno = Convert.ToInt32(Console.ReadLine()),
            };
        }
        static EmployeeModel GetDeleteEmp()
        {
            Console.WriteLine("Enter Employee number to Delete");
            return new EmployeeModel()
            {
                Eno = Convert.ToInt32(Console.ReadLine()),
            };
        }
        static EmployeeModel GetSortEmp()
        {
            Console.WriteLine("Enter the column name and sort order to be sort");
            string column = Console.ReadLine();
            Console.WriteLine("Enter sort order (ASC/DESC): ");
            string order = Console.ReadLine();
            return new EmployeeModel()
            {
            column = column,
            order = order
            };
        }
        static string InsertEmployee()
        {
            EmployeeModel emp = GetInputEmp();
            string res = objBAL.InsertEmployee(emp);
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            return res;
        }
        static string UpdateEmployee()
        {
            EmployeeModel emp = GetUpdateEmp();
            string res = objBAL.UpdateEmployee(emp);
            Console.WriteLine("Updated List");
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            return res;
        }
        static string DeleteEmployee()
        {
            EmployeeModel emp = GetDeleteEmp();
            string res = objBAL.DeleteEmployee(emp);
            Console.WriteLine("Updated List");
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            return res;
        }
        static void SortEmployee()
        {
            EmployeeModel emp = GetSortEmp();
            List<EmployeeModel> emps = objBAL.SortEmployee(emp.column, emp.order);
            Console.WriteLine("Sorted List:");
            Display(emps);
        }
        static void Display(List<EmployeeModel> employees)
        {
            foreach (EmployeeModel e in employees)
            {
                Console.WriteLine($"{e.Eno} -   {e.Ename}   -   {e.Job}     -   {e.Salary}      -   {e.Dname}");
            }
        }
        static void Main(string[] args)
        {
            List<EmployeeModel> emps = objBAL.GetEmployees();
            Display(emps);
            Console.WriteLine("Select operation: ");
            Console.WriteLine("1.Insert\n2.Update\n3.Delete\n4.Sort");
            int dbChoice = Convert.ToInt32(Console.ReadLine());
            switch (dbChoice)
            {
                case 1:
                    InsertEmployee();
                    break;
                case 2:
                    UpdateEmployee();
                    break;
                case 3:
                    DeleteEmployee();
                    break;
                case 4:
                    SortEmployee();
                    break;
                default:
                    break;
            }

            Console.Read();
        }
    }
}