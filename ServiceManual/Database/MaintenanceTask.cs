using System;
namespace ServiceManual
{
    public class MaintenanceTask
    {
        public int TaskID { get; set; }
        public int DeviceID { get; set; }
        public DateTime Created { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }
        public string Description { get; set; }

        public MaintenanceTask(int tID, int dID, DateTime created,
            int priority, int state, string description)
        {
            TaskID = tID;
            DeviceID = dID;
            Created = created;
            Priority = priority;
            State = state;
            Description = description;
        }
    }
}
