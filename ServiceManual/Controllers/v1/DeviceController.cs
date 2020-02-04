using System;
using Microsoft.AspNetCore.Mvc;
using ServiceManual.Exceptions;

namespace ServiceManual.Controllers.v1
{
    public class DeviceController : Controller
    {
        public DeviceController()
        {
        }

        /// <summary>
        /// Get All Devices
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoute.Devices.GetAll)]
        public IActionResult GetAll()
        {
            try
            {
                // Create DB object
                Database db = new Database();

                // Return all tasks
                return Ok(db.GetDevices());
            }
            catch (Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }

        /// <summary>
        /// Get single Device
        /// </summary>
        /// <returns></returns>
        [HttpGet(APIRoute.Devices.GetOne)]
        public IActionResult GetOne([FromRoute]int? deviceID)
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

                // Return single task
                return Ok(db.GetDevices(ID));
            }
            catch (Exception e)
            {
                return Ok(new ErrorMessage(e.Message));
            }
        }
    }
}
