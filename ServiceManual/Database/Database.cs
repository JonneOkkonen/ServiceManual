using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;

namespace ServiceManual
{
    public class Database
    {
        // Priority levels from 1-3
        // 1: Lievä, 2: Tärkeä, 3: Kriittinen
        private static readonly string[] Priority = new[]
        {
            "Undefined", "Lievä", "Tärkeä", "Kriittinen"
        };

        // State levels from 0-1
        // 0: Huollettu, 1: Avoin
        private static readonly string[] State = new[]
{
            "Huollettu", "Avoin"
        };

        /// <summary>
        /// Get single or list of devices from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// Returns single or list of Devices
        /// </returns>
        public List<Device> GetDevices(int? id = null)
        {
            try
            {
                // List for data
                List<Device> data = new List<Device>();

                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();

                // Run the SQL Query
                string query = "SELECT * FROM Device";
                if (id != null) query += $" WHERE DeviceID = {id}";
                using MySqlCommand cmd = new MySqlCommand(query, conn);

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
        /// Get single or list of maintenance tasks from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>
        /// Returns single or list of MaintenanceTasks
        /// </returns>
        public List<MaintenanceTask> GetMaintenanceTasks(int? id = null)
        {
            try
            {
                // List for data
                List<MaintenanceTask> data = new List<MaintenanceTask>();

                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();

                // Run the SQL Query
                string query = "SELECT " +
                                "TaskID, " +
                                "Device.DeviceID, " +
                                "Device.Name, " +
                                "Device.Year, " +
                                "Device.Type, " +
                                "Created, " +
                                "Priority, " +
                                "State, " +
                                "Description " +
                                "FROM `MaintenanceTask` " +
                                "INNER JOIN Device " +
                                "ON MaintenanceTask.DeviceID = Device.DeviceID";
                if (id != null) query += $" WHERE MaintenanceTask.TaskID = {id}";
                using MySqlCommand cmd = new MySqlCommand(query, conn);

                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new MaintenanceTask {
                        TaskID = reader.GetInt32(0),
                        DeviceID = reader.GetInt32(1),
                        Name = reader.GetString(2),
                        Year = reader.GetInt32(3),
                        Type = reader.GetString(4),
                        Created = reader.GetDateTime(5),
                        Priority = Priority[reader.GetInt32(6)],
                        State = State[reader.GetInt32(7)],
                        Description = reader.GetString(8)
                    });
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
