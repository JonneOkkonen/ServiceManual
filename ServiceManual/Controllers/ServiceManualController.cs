using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Device> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Device
            {
                DeviceID = rng.Next(0,500),
                Name = "Device " + rng.Next(3849,5000),
                Year = rng.Next(1900, 2020),
                Type = "Type " + rng.Next(15, 35)
            })
            .ToArray();
        }
    }
}
