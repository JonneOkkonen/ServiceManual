using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceManual;
using System.Configuration;

namespace ServiceManualUnitTests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void GetConnectionString()
        {
            // Generate Connection String
            string server = "localhost";
            string database = "servicemanual";
            string uid = "root";
            string password = "";
            string expected = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", server, database, uid, password);

            // Get Connection String
            string actual = Database.GetConnectionString();

            // Check if they are equal
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MaintenanceTaskExist()
        {
            Database db = new Database();

            // Check true output
            bool actual = db.MaintenanceTaskExists(1);
            Assert.AreEqual(true, actual);

            // Check false output
            bool actual2 = db.MaintenanceTaskExists(9999);
            Assert.AreEqual(false, actual2);
        }
    }
}
