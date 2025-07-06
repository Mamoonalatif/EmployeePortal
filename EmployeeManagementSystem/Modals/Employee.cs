namespace EmployeeManagementSystem.Modals
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentID { get; set; }
        public int LocationID { get; set; }
        public int? ManagerID { get; set; }
    }
}
