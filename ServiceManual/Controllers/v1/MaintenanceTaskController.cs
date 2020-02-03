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
        [HttpGet(APIRoute.Tasks.GetOne)]
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
    }
}
