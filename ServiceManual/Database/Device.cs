using System;

namespace ServiceManual
{
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }

        public Device(int id, string name, int year, string type)
        {
            DeviceID = id;
            Name = name;
            Year = year;
            Type = type;
        }
    }
}
