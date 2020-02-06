using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceManual;
using System.Collections.Generic;

namespace ServiceManualUnitTests
{
    [TestClass]
    public class DatabaseTests
    {
        public Database db = new Database();

        public DatabaseTests()
        {
            LoadConnectionStringManually();
        }

        /// <summary>
        /// Connection String has to be set manually because UnitTests can't get them from configuration
        /// </summary>
        public void LoadConnectionStringManually()
        {
            Database.Config = new ServiceManual.Config.DatabaseConfig
            {
                Server = "localhost",
                Database = "ServiceManual",
                UserID = "root"
            };
        }

        [TestMethod]
        public void MaintenanceTaskExist()
        {
            // Check true output
            bool actual = db.MaintenanceTaskExists(2);
            Assert.AreEqual(true, actual);

            // Check false output
            bool actual2 = db.MaintenanceTaskExists(9999);
            Assert.AreEqual(false, actual2);
        }

        [TestMethod]
        public void CheckAPIKey()
        {
            // Check true output
            bool actual = db.CheckAPIKey("askjlkjl1234895389fsdjsdfjhksdfjhk383notrealkey");
            Assert.AreEqual(true, actual);

            // Check false output
            bool actual2 = db.CheckAPIKey("asdasd");
            Assert.AreEqual(false, actual2);
        }

        [TestMethod]
        public void ExecuteCmd()
        {
            // Run Update command that changes 1 row. Check if it actually changes one row.
            int actual = db.ExecuteCmd("UPDATE Users SET userID=2 WHERE userID = 2");
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetDevices()
        {
            // Get list of devices
            List<Device> devices = db.GetDevices();

            // Check if first device data is correct
            Assert.AreEqual(3736, devices[0].DeviceID);
            Assert.AreEqual("Device 0", devices[0].Name);
            Assert.AreEqual(2004, devices[0].Year);
            Assert.AreEqual("Type19", devices[0].Type);
        }

        [TestMethod]
        public void GetSingleDevices()
        {
            // Get single devices
            List<Device> devices = db.GetDevices(3737);

            // Check if device data is correct
            Assert.AreEqual(3737, devices[0].DeviceID);
            Assert.AreEqual("Device 1", devices[0].Name);
            Assert.AreEqual(1987, devices[0].Year);
            Assert.AreEqual("Type2", devices[0].Type);
        }
    }
}
