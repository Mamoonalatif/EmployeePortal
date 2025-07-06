using EmployeeManagementSystem.Modals;
using Microsoft.Data.SqlClient;
public class DepartmentService
{
    private readonly string _connectionString;
    public DepartmentService(IConfiguration configuration) { _connectionString = configuration.GetConnectionString("DefaultConnection");  }
    public List<Department> GetAll() {
        var list = new List<Department>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("SELECT * FROM Departments", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Department
            {
                DepartmentID = (int)reader["DepartmentID"],
                DepartmentName = reader["DepartmentName"].ToString()
            });
        }
        return list; }
    public void Add(Department dept){
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("INSERT INTO Departments (DepartmentName) VALUES (@name)", conn);
        cmd.Parameters.AddWithValue("@name", dept.DepartmentName);
        cmd.ExecuteNonQuery(); }
    public void Update(Department dept) {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("UPDATE Departments SET DepartmentName = @name WHERE DepartmentID = @id", conn);
        cmd.Parameters.AddWithValue("@name", dept.DepartmentName);
        cmd.Parameters.AddWithValue("@id", dept.DepartmentID);
        cmd.ExecuteNonQuery(); }
    public void Delete(int id) {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using var cmd = new SqlCommand("DELETE FROM Departments WHERE DepartmentID = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();  }
}
