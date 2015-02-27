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

            DataLog data = new DataLog() { Type = "TestData", Date = date, JsonData = "{ TestValue = Test}" };
            DataLog dataGet = DataLogger.Instance.AddData(data);

            List<DataLog> logs = DataLogger.Instance.GetData("TestData", null, null);
            Assert.IsNotNull(logs);
            Assert.IsTrue(logs.Count > 0);
        }

        [TestCleanup]
        public void CleanUp()
        {
            DataLogger.Instance.RemoveAll("TestData");
        }
    }
}
