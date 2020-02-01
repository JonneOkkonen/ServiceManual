using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;

namespace ServiceManual
{
    public class Database
    {

        public Database()
        {
        }

        /// <summary>
        /// Get list of devices from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// Returns List of Devices
        /// </returns>
        public List<Device> GetDevices()
        {
            try
            {
                // List for data
                List<Device> data = new List<Device>();
                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();
                // Run the SQL Query
                using MySqlCommand cmd = new MySqlCommand("SELECT * FROM Device", conn);
                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new Device(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(2),
                        reader.GetString(3)));
                }
                // Return results
                return data;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Execute MySQL Queries.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// Returns number of rows affected.
        /// </returns>
        public int ExecuteCmd(string query)
        {
            try
            {
                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();
                // Run the SQL Query
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();
                // Return results
                return reader.RecordsAffected;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Create and return Connection string
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string server = "localhost";
            string database = "ServiceManual";
            string uid = "root";
            string password = "";
            return string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", server, database, uid, password);
        }
    }
}
