using System;
namespace ServiceManual.Controllers.v1
{
    public static class APIRoute
    {
        private const string Root = "api";
        private const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Tasks
        {
            public const string GetAll = Base + "/tasks";
            public const string GetOne = Base + "/task/{taskID}";
            public const string GetAllForDevice = Base + "/tasks/{deviceID}";
        }

        public static class Devices
        {
            public const string GetAll = Base + "/devices";
            public const string GetOne = Base + "/device/{deviceID}";
        }
    }
}
