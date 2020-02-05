using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceManual;

namespace ServiceManualUnitTests
{
    [TestClass]
    public class DatabaseTests
    {
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
