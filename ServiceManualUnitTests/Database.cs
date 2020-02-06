using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceManual;

namespace ServiceManualUnitTests
{
    [TestClass]
    public class DatabaseTests
    {
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
            LoadConnectionStringManually();
            Database db = new Database();

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
            LoadConnectionStringManually();
            Database db = new Database();

            // Check true output
            bool actual = db.CheckAPIKey("askjlkjl1234895389fsdjsdfjhksdfjhk383notrealkey");
            Assert.AreEqual(true, actual);

            // Check false output
            bool actual2 = db.CheckAPIKey("asdasd");
            Assert.AreEqual(false, actual2);
        }
    }
}
