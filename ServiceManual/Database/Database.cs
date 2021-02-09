using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ServiceManual.Config;
using ServiceManual.Exceptions;

namespace ServiceManual
{
    public class Database
    {
        /// <summary>
        /// DatabaseConfig class holds database connection details
        /// </summary>
        public static DatabaseConfig Config;

        /// <summary>
        /// Get single or list of devices from database
        /// </summary>
        /// <param name="id"></param>
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
                if (id != null) query += $" WHERE DeviceID=@id";
                using MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add parameters
                if (id != null) cmd.Parameters.AddWithValue("@id", id);

                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new Device(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetString(3)));
                    }
                }
                else throw new NoResultsFoundException();

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
        /// <param name="id"></param>
        /// <param name="deviceID"></param>
        /// <returns>
        /// Returns single or list of MaintenanceTasks
        /// </returns>
        public List<MaintenanceTask> GetMaintenanceTasks(int? id = null, int? deviceID = null)
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
                if (id != null) query += $" WHERE MaintenanceTask.TaskID=@id";
                if (deviceID != null) query += $" WHERE Device.DeviceID=@deviceID";

                // Order with priority and created
                query += " ORDER BY Priority DESC, Created DESC";
                using MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add parameters
                if (id != null) cmd.Parameters.AddWithValue("@id", id);
                if (deviceID != null) cmd.Parameters.AddWithValue("@deviceID", deviceID);

                    // Read the result of the query
                    using MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data.Add(new MaintenanceTask
                        {
                            TaskID = reader.GetInt32(0),
                            DeviceID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Year = reader.GetInt32(3),
                            Type = reader.GetString(4),
                            Created = reader.GetDateTime(5),
                            Priority = MaintenanceTask.PriorityList[reader.GetInt32(6)],
                            State = MaintenanceTask.StateList[reader.GetInt32(7)],
                            Description = reader.GetString(8)
                        });
                    }
                }
                else throw new NoResultsFoundException();

                // Return results
                return data;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add new maintenance task to database
        /// </summary>
        /// <param name="task"></param>
        /// <returns>True/false depending on result</returns>
        public int AddMaintenanceTask(MaintenanceTask task)
        {
            // Create parameters list
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@deviceID", task.DeviceID));
            parameters.Add(new MySqlParameter("@created", task.Created.ToString("yyyy-MM-dd HH:mm:ss")));
            parameters.Add(new MySqlParameter("@priority", Array.IndexOf(MaintenanceTask.PriorityList, task.Priority)));
            parameters.Add(new MySqlParameter("@state", Array.IndexOf(MaintenanceTask.StateList, task.State)));
            parameters.Add(new MySqlParameter("@description", task.Description));

            // Execute Command
            int result = ExecuteCmd($"INSERT INTO MaintenanceTask(DeviceID, Created, Priority, State, Description) " +
                                    $"VALUES(@deviceID, @created, @priority, @state, @description);", parameters);

            // If adding was successfull get id for that object
            if(result > 0)
            {
                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();

                // Run the SQL Query
                using MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", conn);

                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();

                // Read ID
                reader.Read();
                result = Convert.ToInt32(reader.GetValue(0));
            }

            return result;
        }

        /// <summary>
        /// Update maintenance task data to database
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public bool UpdateMaintenanceTask(MaintenanceTask task)
        {
            // Create parameters list
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@deviceID", task.DeviceID));
            parameters.Add(new MySqlParameter("@created", task.Created.ToString("yyyy-MM-dd HH:mm:ss")));
            parameters.Add(new MySqlParameter("@priority", Array.IndexOf(MaintenanceTask.PriorityList, task.Priority)));
            parameters.Add(new MySqlParameter("@state", Array.IndexOf(MaintenanceTask.StateList, task.State)));
            parameters.Add(new MySqlParameter("@description", task.Description));
            parameters.Add(new MySqlParameter("@taskID", task.TaskID));

            // Update Data to Database
            int result = ExecuteCmd($"UPDATE MaintenanceTask SET DeviceID=@deviceID, " +
                                    $"Created=@created, Priority=@priority, State=@state, Description=@description WHERE TaskID=@taskID;", parameters);

            return result > 0;
        }

        /// <summary>
        /// Delete maintenance task from database
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool DeleteMaintenanceTask(int taskID)
        {
            // Delete Data from Database
            int result = ExecuteCmd($"DELETE FROM MaintenanceTask WHERE taskID = {taskID}");

            return result > 0;
        }

        /// <summary>
        ///  Check if MaintenanceTask Exists
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public bool MaintenanceTaskExists(int taskID)
        {
            try
            {
                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();
                // Run the SQL Query
                using MySqlCommand cmd = new MySqlCommand($"SELECT * FROM MaintenanceTask WHERE taskID=@taskID", conn);

                // Add parameters
                cmd.Parameters.AddWithValue("@taskID", taskID);

                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();

                // Select Data from Database
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Check If API key exists
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        public bool CheckAPIKey(string apiKey)
        {
            try
            {
                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();
                // Run the SQL Query
                using MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Users WHERE API_key=@apikey", conn);

                // Add parameters
                cmd.Parameters.AddWithValue("@apikey", apiKey);

                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();

                // Select Data from Database
                return reader.HasRows;
            }
            catch (Exception e)
            {
                throw e;
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
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Execute MySQL Queries with Params
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteCmd(string query, List<MySqlParameter> parameters)
        {
            try
            {
                // Create Connection and open it
                using MySqlConnection conn = new MySqlConnection(GetConnectionString());
                conn.Open();
                // Run the SQL Query
                using MySqlCommand cmd = new MySqlCommand(query, conn);

                // Add Parameters
                foreach(MySqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }

                // Read the result of the query
                using MySqlDataReader reader = cmd.ExecuteReader();
                // Return results
                return reader.RecordsAffected;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Create and return Connection string
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", Config.Server, Config.Database, Config.UserID, Config.Password);
        }
    }
}
