using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullsAndCowsCommandPattern;

namespace BullsAndCows.Tests
{
    [TestClass]
    public class TestNumberProcesser
    {
        [TestMethod]
        public void TestCheckIsGuessed()
        {
            NumberProccesser numberProcesser = new NumberProccesser(4, new MockNumberGenerator("1234"));

            Assert.IsTrue(numberProcesser.CheckIsGuessed("1234"), "Number is the corrent one");
        }

        [TestMethod]
        public void TestCountCows()
        {
            NumberProccesser numberProcesser = new NumberProccesser(4, new MockNumberGenerator("1234"));

            Assert.AreEqual(numberProcesser.CountCows("2561"), 2, "There must be 2 cows");
        }

        [TestMethod]
        public void TestCountBulls()
        {
            NumberProccesser numberProcesser = new NumberProccesser(4, new MockNumberGenerator("1234"));

            Assert.AreEqual(numberProcesser.CountBulls("5236"), 2, "There must be 2 bulls");
        }
    }
}
