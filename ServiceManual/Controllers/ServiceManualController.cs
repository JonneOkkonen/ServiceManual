using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceManual.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceManualController : ControllerBase
    {
        // Priority levels from 1-3
        // 1: Lievä, 2: Tärkeä, 3: Kriittinen
        private static readonly string[] Priority = new[]
        {
            "Lievä", "Tärkeä", "Kriittinen" 
        };

        // State levels from 0-1
        // 0: Huollettu, 1: Avoin
        private static readonly string[] State = new[]
{
            "Huollettu", "Avoin"
        };

        private readonly ILogger<ServiceManualController> _logger;

        public ServiceManualController(ILogger<ServiceManualController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MaintenanceTask> Get()
        {
            Database db = new Database();
            return db.GetMaintenanceTasks();
        }
    }
}
