using System;
using System.Data;
using System.Data.SqlClient;

namespace DataBase
{
    public class Service
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime dateService { get; set; }
        public string timeOfService { get; set; }
        public string departureTime { get; set; }
        public string sector { get; set; }
        public int userId { get; set; }

        public void Save()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.connectionString))
            {
                connection.Open();
                string sql = id == 0
                ? "INSERT INTO Services (description, date_service, user_id,  time_of_service, departure_time, sector) VALUES (@description, @date_service, @user_id, @time_of_service, @departure_time, @sector); SELECT @@identity"
                : "UPDATE Services SET description = @description, date_service = @date_service, user_id = @user_id, time_of_service = @time_of_service, departure_time = @departure_time, sector = @sector WHERE id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@time_of_service", timeOfService);
                command.Parameters.AddWithValue("@departure_time", departureTime);
                command.Parameters.AddWithValue("@date_service", dateService);
                command.Parameters.AddWithValue("@sector", sector);
                command.Parameters.AddWithValue("@user_id", userId);
                command.CommandText = sql;
                try
                {
                    if (id == 0)
                        id = Convert.ToInt32(command.ExecuteScalar());
                    else
                        command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }

        static public DataTable FindByUserId(int user_id, int page, double quantRows)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    string sql = $"SELECT id, description, CONVERT(VARCHAR, date_service, 103) AS date_service, time_of_service, departure_time, Services.sector, user_id FROM Services WHERE user_id = {user_id} ORDER BY CONVERT(DATE, date_service, 103), CONVERT(Time, time_of_service, 103) DESC OFFSET {page} ROWS FETCH  NEXT {quantRows} ROWS ONLY";
                    var adapter = new SqlDataAdapter(sql, connection);
                    adapter.SelectCommand.CommandText = sql;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch
            {
                throw;
            }
        }
       
        static public DataTable FindByYearAndMonth(int year, string month, int page = 1, double quantRows = 1)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    string sql = $"SELECT Services.description, CONVERT(VARCHAR, Services.date_service, 103) AS date_service, Services.time_of_service, Services.departure_time, Services.sector, Users.name FROM Services INNER JOIN Users ON Users.id = Services.user_id WHERE CONVERT(VARCHAR, date_service, 103) LIKE '%{month}/{year}%' ORDER BY CONVERT(DATE, Services.date_service, 103), CONVERT(Time, Services.time_of_service, 103) DESC OFFSET {page} ROWS FETCH  NEXT {quantRows} ROWS ONLY";
                    var adapter = new SqlDataAdapter(sql, connection);
                    adapter.SelectCommand.CommandText = sql;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch
            {
                throw;
            }
        }
        
        static public DataTable FindByYear(int year , int page = 1, double quantRows = 1)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    string sql = $"SELECT Services.description, CONVERT(VARCHAR, Services.date_service, 103) AS date_service, Services.time_of_service, Services.departure_time, Services.sector, Users.name FROM Services INNER JOIN Users ON Users.id = Services.user_id WHERE date_service LIKE '%{year}%' ORDER BY CONVERT(DATE, Services.date_service, 103), CONVERT(Time, Services.time_of_service, 103) DESC OFFSET {page} ROWS FETCH  NEXT {quantRows} ROWS ONLY";
                    var adapter = new SqlDataAdapter(sql, connection);
                    adapter.SelectCommand.CommandText = sql;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch
            {
                throw;
            }
        }
       
        static public DataTable GetSectors()
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    string sql = $"SELECT DISTINCT sector FROM Services;";
                    var adapter = new SqlDataAdapter(sql, connection);
                    adapter.SelectCommand.CommandText = sql;
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch
            {
                throw;
            }
        }

        static public int CountQuantityServicesByUserId(int userId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    connection.Open();
                    string sql = $"SELECT COUNT(id) AS quantity FROM Services WHERE user_id = {userId}";
                    var command = new SqlCommand(sql, connection);
                    command.CommandText = sql;

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }

        static public int CountQuantityServicesByYear(string year)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    connection.Open();
                    string sql = $"SELECT COUNT(id) AS quantity FROM Services WHERE CONVERT(VARCHAR, date_service, 103) LIKE '%{year}%'";
                    var command = new SqlCommand(sql, connection);
                    command.CommandText = sql;

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch
            {
                throw;
            }
        }

        static public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.connectionString))
            {
                string sql = "DELETE FROM Services WHERE id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                command.CommandText = sql;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
