using System;
using Microsoft.AspNetCore.Mvc;

namespace ServiceManual.Controllers.v1
{
    public class MaintenanceTaskController : Controller
    {
        public MaintenanceTaskController()
        {
        }

        [HttpGet(APIRoute.Tasks.GetAll)]
        public IActionResult GetAll()
        {
            try
            {
                Database db = new Database();
                return Ok(db.GetMaintenanceTasks());
            }catch(Exception e)
            {
                return Ok(e.Message);
            }
        }
    }
}
