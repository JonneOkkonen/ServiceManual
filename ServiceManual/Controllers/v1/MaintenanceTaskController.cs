using System;
using Microsoft.AspNetCore.Mvc;
using ServiceManual.Exceptions;

namespace ServiceManual.Controllers.v1
{
    public class MaintenanceTaskController : Controller
    {
        public MaintenanceTaskController()
        {
        }

        /// <summary>
        /// Get all Maintenance Tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoute.Tasks.GetAll)]
        public IActionResult GetAll()
        {
            try
            {
                // Create DB object
                Database db = new Database();

                // Return all tasks
                return Ok(db.GetMaintenanceTasks());
            }
            catch (Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        /// Get single Maintenance Task
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoute.Tasks.Get)]
        public IActionResult GetOne([FromRoute]int? taskID)
        {
            try
            {
                // Check if id was integer
                if (int.TryParse(taskID.ToString(), out int ID) == false)
                {
                    return Ok(new ErrorMessage("ID has to be integer!"));
                }

                // Create DB object
                Database db = new Database();

                // Return single task
                return Ok(db.GetMaintenanceTasks(ID));
            }
            catch(Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        /// Get all Maintenance Tasks for given deviceID
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoute.Tasks.GetAllForDevice)]
        public IActionResult GetAllForDevice([FromRoute]int? deviceID)
        {
            try
            {
                // Check if id was integer
                if (int.TryParse(deviceID.ToString(), out int ID) == false)
                {
                    return Ok(new ErrorMessage("ID has to be integer!"));
                }

                // Create DB object
                Database db = new Database();

                // Return all tasks for given deviceID
                return Ok(db.GetMaintenanceTasks(null, ID));
            }
            catch (Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        /// Add New Maintenance Task
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="deviceID"></param>
        /// <param name="created"></param>
        /// <param name="priority"></param>
        /// <param name="state"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost(APIRoute.Tasks.Create)]
        public IActionResult Create([FromBody] int taskID, int deviceID, string created, 
                                    int priority, int state, string description)
        {
            try
            {
                Database db = new Database();

                // Check that DeviceID is integer
                if (!int.TryParse(deviceID.ToString(), out int deviceIDOut))
                {
                    throw new IncorrectTypeException("DeviceID", IncorrectTypeException.Types.Int);
                }

                // Check if Created Date is in DateTime format
                if (!DateTime.TryParse(created, out DateTime createdOut))
                {
                    throw new IncorrectTypeException("Created", IncorrectTypeException.Types.DateTime);
                }

                // Check if Priority is integer
                if(!int.TryParse(priority.ToString(), out int priorityOut))
                {
                    throw new IncorrectTypeException("Priority", IncorrectTypeException.Types.Int);
                }

                // Check if State is integer
                if(!int.TryParse(state.ToString(), out int stateOut))
                {
                    throw new IncorrectTypeException("State", IncorrectTypeException.Types.Int);
                }

                // Create MaintenanceTask from Data
                MaintenanceTask task = new MaintenanceTask {
                    TaskID = taskID,
                    DeviceID = deviceIDOut,
                    Created = createdOut,
                    Priority = MaintenanceTask.PriorityList[priorityOut],
                    State = MaintenanceTask.StateList[stateOut],
                    Description = description
                };

                // Try saving maintenance task to database
                int result = db.AddMaintenanceTask(task);

                // Check if adding was successfull
                if (result == 0) throw new Exception("INSERT Failed");

                // Add MaintenanceTask URL to Location parameter to response header
                string baseURL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                string locationURL = baseURL + "/" + APIRoute.Tasks.Get.Replace("{taskID}", result.ToString());

                return Created(locationURL, task);
            }
            catch (IncorrectTypeException e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
            catch (Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        /// Update maintenance task data to database
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="id"></param>
        /// <param name="deviceID"></param>
        /// <param name="created"></param>
        /// <param name="priority"></param>
        /// <param name="state"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut(APIRoute.Tasks.Update)]
        public IActionResult Update([FromRoute]int taskID, [FromBody]int id, int deviceID, string created,
                                    int priority, int state, string description)
        {
            try
            {
                Database db = new Database();

                // Check that DeviceID is integer
                if (!int.TryParse(deviceID.ToString(), out int deviceIDOut))
                {
                    throw new IncorrectTypeException("DeviceID", IncorrectTypeException.Types.Int);
                }

                // Check if Created Date is in DateTime format
                if (!DateTime.TryParse(created, out DateTime createdOut))
                {
                    throw new IncorrectTypeException("Created", IncorrectTypeException.Types.DateTime);
                }

                // Check if Priority is integer
                if (!int.TryParse(priority.ToString(), out int priorityOut))
                {
                    throw new IncorrectTypeException("Priority", IncorrectTypeException.Types.Int);
                }

                // Check if State is integer
                if (!int.TryParse(state.ToString(), out int stateOut))
                {
                    throw new IncorrectTypeException("State", IncorrectTypeException.Types.Int);
                }

                // Create MaintenanceTask from Data
                MaintenanceTask task = new MaintenanceTask
                {
                    TaskID = taskID,
                    DeviceID = deviceIDOut,
                    Created = createdOut,
                    Priority = MaintenanceTask.PriorityList[priorityOut],
                    State = MaintenanceTask.StateList[stateOut],
                    Description = description
                };

                // Try updating maintenance task data to database
                bool result = db.UpdateMaintenanceTask(task);

                // Check if adding was successfull
                if (!result) throw new Exception("UPDATE Failed");

                return Ok(task);
            }
            catch (IncorrectTypeException e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
            catch (Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }
    }
}
