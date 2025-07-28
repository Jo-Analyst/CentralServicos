﻿using System;
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
        public int person_id { get; set; }

        public void Save()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.connectionString))
            {
                connection.Open();
                string sql = id == 0
                ? "INSERT INTO Services (description, date_service, person_id,  time_of_service, departure_time) VALUES (@description, @date_service, @person_id, @time_of_service, @departure_time); SELECT @@identity"
                : "UPDATE Services SET description = @description, date_service = @date_service, person_id = @person_id, time_of_service = @time_of_service, departure_time = @departure_time  WHERE id = @id";
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@time_of_service", timeOfService);
                command.Parameters.AddWithValue("@departure_time", departureTime);
                command.Parameters.AddWithValue("@date_service", dateService);
                command.Parameters.AddWithValue("@person_id", person_id);
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

        static public DataTable FindByPersonId(int person_id, int page, double quantRows)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    string sql = $"SELECT id, description, CONVERT(VARCHAR, date_service, 103) AS date_service, time_of_service, departure_time, person_id FROM Services WHERE person_id = {person_id} ORDER BY id DESC OFFSET {page} ROWS FETCH  NEXT {quantRows} ROWS ONLY";
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

        static public int CountQuantityServices(int personId)
        {
            try
            {
                using (var connection = new SqlConnection(DbConnectionString.connectionString))
                {
                    connection.Open();
                    string sql = $"SELECT COUNT(id) AS quantity FROM Services WHERE person_id = {personId}";
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
