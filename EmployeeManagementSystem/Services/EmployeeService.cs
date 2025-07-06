using EmployeeManagementSystem.Modals;
using Microsoft.Data.SqlClient;
public class EmployeeService
{
    private readonly string _connectionString;
    public EmployeeService(IConfiguration configuration) { _connectionString = configuration.GetConnectionString("DefaultConnection");  }
    public List<Employee> GetAll()
    {
        var list = new List<Employee>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Employees", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        { list.Add(new Employee
            {
                EmployeeID = (int)reader["EmployeeID"],
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Salary = (decimal)reader["Salary"],
                DepartmentID = (int)reader["DepartmentID"],
                LocationID = (int)reader["LocationID"],
                ManagerID = reader["ManagerID"] == DBNull.Value ? null : (int?)reader["ManagerID"]
            });
        }  return list;
    }
    public void Add(Employee emp)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand(@"INSERT INTO Employees 
            (FirstName, LastName, Salary, DepartmentID, LocationID, ManagerID) 
            VALUES (@FirstName, @LastName, @Salary, @DepartmentID, @LocationID, @ManagerID)", conn);
        cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
        cmd.Parameters.AddWithValue("@LastName", emp.LastName);
        cmd.Parameters.AddWithValue("@Salary", emp.Salary);
        cmd.Parameters.AddWithValue("@DepartmentID", emp.DepartmentID);
        cmd.Parameters.AddWithValue("@LocationID", emp.LocationID);
        cmd.Parameters.AddWithValue("@ManagerID", (object?)emp.ManagerID ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }

    public void Update(Employee emp)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand(@"UPDATE Employees SET 
            FirstName = @FirstName,
            LastName = @LastName,
            Salary = @Salary,
            DepartmentID = @DepartmentID,
            LocationID = @LocationID,
            ManagerID = @ManagerID
            WHERE EmployeeID = @EmployeeID", conn);
        cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
        cmd.Parameters.AddWithValue("@LastName", emp.LastName);
        cmd.Parameters.AddWithValue("@Salary", emp.Salary);
        cmd.Parameters.AddWithValue("@DepartmentID", emp.DepartmentID);
        cmd.Parameters.AddWithValue("@LocationID", emp.LocationID);
        cmd.Parameters.AddWithValue("@ManagerID", (object?)emp.ManagerID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@EmployeeID", emp.EmployeeID);
        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var updateManagers = new SqlCommand("UPDATE Employees SET ManagerID = NULL WHERE ManagerID = @id", conn);
        updateManagers.Parameters.AddWithValue("@id", id);
        updateManagers.ExecuteNonQuery();

        using var cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}
