using System.Data.SqlClient;

namespace DataBase
{
    public class DB
    {
        static public bool ExistsDataBase()
        {
            bool existsDataBase = false;

            using (SqlConnection connection = new SqlConnection(DbConnectionString.connectionStringMaster))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Sys.Databases WHERE name = 'dbCentralServices'", connection);
                try
                {
                    connection.Open();
                    comando.ExecuteNonQuery();
                    SqlDataReader dr = comando.ExecuteReader();
                    if (dr.Read())
                    {
                        existsDataBase = true;
                    }

                }
                catch
                {
                    throw;
                }
            }

            return existsDataBase;
        }

        static public void CreateTables()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.connectionString))
            {
                string sql = "CREATE TABLE [dbo].[Users] (" +
                "    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                "    [name] VARCHAR(200) NULL, " +
                "    [CPF] VARCHAR(MAX) NULL, " +              
                "    [address] VARCHAR(200) NULL," +
                "    [phone] VARCHAR(20) NULL);" +
                            
               "CREATE TABLE [dbo].[Services] (" +
                "    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), " +
                "    [description] VARCHAR(MAX) NULL, " +
                "    [date_service] Date NULL," +
                "    [time_of_service] VARCHAR(10) NULL," +
                "    [departure_time] VARCHAR(10) NULL," +
                "    [user_id] INT NOT NULL," +
                "    FOREIGN KEY ([user_id]) REFERENCES [dbo].[users](id) ON DELETE CASCADE );";

                SqlCommand command = new SqlCommand(sql, connection);
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

        static public void CreateDatabase()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionString.connectionStringMaster))
            {
                string sql = "CREATE DATABASE dbCentralServices";
                SqlCommand command = new SqlCommand(sql, connection);
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