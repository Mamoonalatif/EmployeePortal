using EmployeeManagementSystem.Modals;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
public class LocationService
{
    private readonly string _connectionString;
    public LocationService(IConfiguration configuration) { _connectionString = configuration.GetConnectionString("DefaultConnection");}
    public List<Location> GetAll()
    {
        var list = new List<Location>();
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = new SqlCommand("SELECT * FROM Locations", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Location
            {
                LocationID = (int)reader["LocationID"],
                City = reader["City"].ToString(),
                Country = reader["Country"].ToString()
            });
        }

        return list;
    }
    public void Add(Location loc)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = new SqlCommand("INSERT INTO Locations (City, Country) VALUES (@City, @Country)", conn);
        cmd.Parameters.AddWithValue("@City", loc.City);
        cmd.Parameters.AddWithValue("@Country", loc.Country);

        cmd.ExecuteNonQuery();
    }

    public void Update(Location loc)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();

        using var cmd = new SqlCommand("UPDATE Locations SET City = @City, Country = @Country WHERE LocationID = @ID", conn);
        cmd.Parameters.AddWithValue("@City", loc.City);
        cmd.Parameters.AddWithValue("@Country", loc.Country);
        cmd.Parameters.AddWithValue("@ID", loc.LocationID);

        cmd.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        using (var cmdUpdate = new SqlCommand("UPDATE Employees SET LocationID = NULL WHERE LocationID = @id", conn))
        {
            cmdUpdate.Parameters.AddWithValue("@id", id);
            cmdUpdate.ExecuteNonQuery();
        }
        using (var cmdDelete = new SqlCommand("DELETE FROM Locations WHERE LocationID = @id", conn))
        {
            cmdDelete.Parameters.AddWithValue("@id", id);
            cmdDelete.ExecuteNonQuery();
        }
    }
}
