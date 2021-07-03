using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spellmaker;
using log4net;

namespace UnitTestProject1
{
    [TestClass]
    public class Settingtest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ItThrows()
        {
            var Instance = Settings.Instance;
        }

        [TestMethod]
        public void HasDefault()
        {



        }
    }
}
