using System.Data;

namespace Employee.Domain
{
    public class EmployeeModel
    {
        public int Eno { get; set; }
        public string Ename { get; set; }
        public string Job { get; set; }
        public double Salary { get; set; }
        public string Dname { get; set; }
        public string column { get; set; }
        public string order { get; set; }
    }
}