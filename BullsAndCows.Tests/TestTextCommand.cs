using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullsAndCows.Exceptions;

namespace BullsAndCows.Tests
{
    [TestClass]
    public class TestTextCommand
    {
        [TestMethod]
        public void TestTextCommandWithValidSetCommand()
        {
            TextCommand txtComm = new TextCommand(Engine.GetInstance());
            txtComm.CommandValue = "help";

            Assert.AreEqual(txtComm.CommandValue, "help", "The command value must be help");
        }

        [TestMethod]
        public void TestTextCommandWithValidSetCommandWithBigAndSmallLetters()
        {
            TextCommand txtComm = new TextCommand(Engine.GetInstance());
            txtComm.CommandValue = "hElP";

            Assert.AreEqual(txtComm.CommandValue, "help", "The command value must be help");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException), "Invalid text command!")]
        public void TestDigitCommandWithInvalidNumberLength()
        {

            TextCommand txtComm = new TextCommand(Engine.GetInstance());
            txtComm.CommandValue = "HelPs";
        }
    }
}
