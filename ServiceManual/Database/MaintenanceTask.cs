using System;

namespace ServiceManual
{
    public class MaintenanceTask : Device
    {
        public int TaskID { get; set; }
        public DateTime Created { get; set; }
        public string Priority { get; set; }
        public string State { get; set; }
        public string Description { get; set; }

        // Priority levels from 1-3
        // 1: Lievä, 2: Tärkeä, 3: Kriittinen
        public static readonly string[] PriorityList = new[]
        {
            "Undefined", "Lievä", "Tärkeä", "Kriittinen"
        };

        // State levels from 0-1
        // 0: Huollettu, 1: Avoin
        public static readonly string[] StateList = new[]
{
            "Huollettu", "Avoin"
        };
    }
}
