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
        public void TestDateTimeInLog()
        {
            throw new NotImplementedException();
        }
    }
}
