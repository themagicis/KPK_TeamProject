using BullsAndCows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Tests
{
    [TestClass]
    public class TestRecord
    {
        [TestMethod]
        public void TestName()
        {
            Record testRecord = new Record("Pesho", 20);
            Assert.IsTrue(testRecord.Name == "Pesho", "Incorrect name saved");
        }

        [TestMethod]
        public void TestScore()
        {
            Record testRecord = new Record("Pesho", 20);
            Assert.IsTrue(testRecord.Score == 20, "Incorrect name saved");
        }

        [TestMethod]
        public void TestCompare()
        {
            Record testRecord1 = new Record("Pesho", 20);
            Record testRecord2 = new Record("Gosho", 30);

            Assert.IsTrue(testRecord1.CompareTo(testRecord2) < 0, "Incorrect name saved");
        }
    }
}
