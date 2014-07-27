using BullsAndCowsCommandPattern;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Tests
{
    [TestClass]
    public class TestScoreBoard
    {
        [TestMethod]
        public void TestCorrectScoreOrder()
        {
            ScoreBoard testScoreBoard = new ScoreBoard(5);
            testScoreBoard.AddScore(20, "Pesho");
            testScoreBoard.AddScore(30, "Gosho");

            Record[] records = testScoreBoard.GetTopScores();
            Record firstRecord = records[0];
            Record secondRecord = records[1];
            Assert.IsTrue(firstRecord.Name == "Gosho", "Incorrect score order");
            Assert.IsTrue(firstRecord.Score > secondRecord.Score, "Incorrect score order");
        }

        [TestMethod]
        public void TestScoresUpdate()
        {
            ScoreBoard testScoreBoard = new ScoreBoard(2);
            testScoreBoard.AddScore(20, "Pesho");
            testScoreBoard.AddScore(30, "Gosho");
            testScoreBoard.AddScore(40, "Pesho");

            Record firstRecord = testScoreBoard.GetTopScores()[0];
            Assert.IsTrue(firstRecord.Name == "Pesho", "Incorrect score update");
            Assert.IsTrue(firstRecord.Score == 40, "Incorrect score update");
        }

        [TestMethod]
        public void TestScoresCount()
        {
            ScoreBoard testScoreBoard = new ScoreBoard(3);
            testScoreBoard.AddScore(40, "Pesho");
            testScoreBoard.AddScore(20, "Gosho");
            testScoreBoard.AddScore(30, "Sasho");
            testScoreBoard.AddScore(50, "Pencho");

            Assert.IsTrue(testScoreBoard.GetTopScores().Length == 3, "Incorrect score length");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "A too long name was inappropriately allowed.")]
        public void TestTooLongName()
        {
            ScoreBoard testScoreBoard = new ScoreBoard(3);
            testScoreBoard.AddScore(40, "Peshoooooooooooooooooooooooooooooooooooooooooooo");

            Assert.IsTrue(testScoreBoard.GetTopScores().Length == 3, "Incorrect score length");
        }
    }
}
