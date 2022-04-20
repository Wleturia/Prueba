using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prueba.Common;
using System;

namespace Prueba.Tests.Common
{
    [TestClass]
    public class LogTest
    {
        private readonly ILog log;

        public LogTest()
        {
            log = new Log();
        }

        [TestMethod]
        public void TestDateTimeInMessageLog()
        {
            var time = DateTime.Now;
            DateTime logData = log.Message($"Test {time}");
            // logData.Ticks  Varia por milésimas
            Assert.AreEqual(time.ToString(), logData.ToString());
        }

        [TestMethod]
        public void TestDateTimeInErrorLog()
        {
            var time = DateTime.Now;
            DateTime logData = log.Error($"Test {time}");
            // logData.Ticks  Varia por milésimas
            Assert.AreEqual(time.ToString(), logData.ToString());
        }


        [TestMethod]
        public void TestDateTimeInSuccessLog()
        {
            var time = DateTime.Now;
            DateTime logData = log.Success($"Test {time}");
            // logData.Ticks  Varia por milésimas
            Assert.AreEqual(time.ToString(), logData.ToString());
        }
    }
}
