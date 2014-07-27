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
    public class TestRandomNumberGenerator
    {
        [TestMethod]
        public void TestGeneratedLength()
        {
            RandomNumberGenerator generator = new RandomNumberGenerator();

            Assert.AreEqual(generator.GenerateNumber(4).Length, 4, "The generated number must have length of 4");
        }

        [TestMethod]
        public void TestDuplicateDigits()
        {
            RandomNumberGenerator generator = new RandomNumberGenerator();

            string generatedNumber = generator.GenerateNumber(4);
            bool foundDuplicates = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                {
                    if (generatedNumber[i] == generatedNumber[j])
                    {
                        foundDuplicates = true;
                        break;
                    }
                }
            }

            Assert.AreEqual(foundDuplicates, false, "There musn't be duplicate digits");
        }

        [TestMethod]
        public void TestValidDigits()
        {
            RandomNumberGenerator generator = new RandomNumberGenerator();

            string generatedNumber = generator.GenerateNumber(4);
            bool invalidDigit = false;
            for (int i = 0; i < 3; i++)
            {
                if (i == 0 && generatedNumber[0] == 0)
                {
                    invalidDigit = true;
                    break;
                }

                if (generatedNumber[i] < '0' || generatedNumber[i] > '9')
                {
                    invalidDigit = true;
                    break;
                }
            }

            Assert.AreEqual(invalidDigit, false, "Digits must be between 0 and 9");
        }

        [TestMethod]
        public void TestZeroInStart()
        {
            RandomNumberGenerator generator = new RandomNumberGenerator();

            string generatedNumber = generator.GenerateNumber(4);
            bool invalidDigit = false;
            for (int i = 0; i < 3; i++)
            {
                if (i == 0 && generatedNumber[0] == 0)
                {
                    invalidDigit = true;
                    break;
                }
            }

            Assert.AreEqual(invalidDigit, false, "Zero not alloed in start of number");
        }
    }
}
