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
    }
}
