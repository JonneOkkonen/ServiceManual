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
            public const string GetAll = Base + "/task/all";
            public const string Create = Base + "/task";
            public const string Get = Base + "/task/{taskID}";
            public const string Update = Base + "/task/{taskID}";
            public const string Delete = Base + "/task/{taskID}";
            public const string GetAllForDevice = Base + "/task/{deviceID}";
        }

        public static class Devices
        {
            public const string GetAll = Base + "/device/all";
            public const string GetOne = Base + "/device/{deviceID}";
        }
    }
}
