using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomoDataServ;
using System.Collections.Generic;

namespace DomoDataServTest
{
    [TestClass]
    public class DataLoggerTest
    {
        [TestMethod]
        public void AddDataTest()
        {
            DateTime date = DateTime.UtcNow;
            DataLogger logger = new DataLogger("mongodb://WHS", "Test");

            DataLog data = new DataLog() { Type = "TestData", Date = date, JsonData = "{ TestValue = Test}" };
            DataLog dataGet = logger.AddData(data);

            List<DataLog> logs = logger.GetData("TestData", null, null);
            Assert.IsNotNull(logs);
            Assert.IsTrue(logs.Count > 0);
        }

        [TestCleanup]
        public void CleanUp()
        {
            DataLogger logger = new DataLogger("mongodb://WHS", "Test");
            logger.RemoveAll("TestData");
        }
    }
}
