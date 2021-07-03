using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spellmaker;

namespace UnitTestProject1
{
    [TestClass]
    public class SheetTest
    {
        [TestMethod]
        public void CheckName()
        {
            Settings settings = Settings.Init();
            var sheet = new Sheet(settings);
            Assert.AreEqual("KOD quests, protips (Mellester)", Sheet.Title, $"Title of spreadsheet not eqaul");
        }
    }
}
