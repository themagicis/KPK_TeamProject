using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BullsAndCows.Tests
{
    [TestClass]
    public class TestDigitCommand
    {
        [TestMethod]
        public void TestNormalCreationOfADigitCommand()
        {
            var digitCommand = new DigitCommand(Engine.GetInstance(), 4);

            Assert.AreEqual(digitCommand.NumberLength, 4, "The length must be 4 digits");
        }

        [TestMethod]
        public void TestNormalSetOfCommand()
        {
            var digitCommand = new DigitCommand(Engine.GetInstance(), 4);
            digitCommand.CommandValue = "1234";

            Assert.AreEqual(digitCommand.CommandValue, "1234", "The number must be 1234s");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Length of the number must be 1-10 digits long")]
        public void TestDigitCommandWithInvalidNumberLength()
        {
            var digitCommand = new DigitCommand(Engine.GetInstance(), 11);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Invalid number!")]
        public void TestDigitCommandWithInvalidNumber()
        {
            var digitCommand = new DigitCommand(Engine.GetInstance(), 11);
            digitCommand.CommandValue = "12s4";
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Number length must be 5 digits")]
        public void TestDigitCommandWithInvalidCountOfDigits()
        {
            var digitCommand = new DigitCommand(Engine.GetInstance(), 5);
            digitCommand.CommandValue = "1234";
        }
    }
}
