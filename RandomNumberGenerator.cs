// <copyright file="RandomNumberGenerator.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCows
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class that generates a random number with unique digits by given
    /// digit count.
    /// </summary>
    public class RandomNumberGenerator : NumberGenerator
    {
        /// <summary>
        /// Generator for random numbers;
        /// </summary>
        private readonly Random randomGenerator = new Random();

        /// <summary>
        /// Generates a number with defined length. All digits of the number
        /// are unique
        /// </summary>
        /// <param name="numberLength">The length of the number</param>
        /// <returns>Generated number as string</returns>
        public override string GenerateNumber(int numberLength)
        {
            if (numberLength < 1 || numberLength > 10)
            {
                throw new ArgumentException("Length of the number cannot be smaller than 1 or bigger than 10!");
            }

            List<char> secretNumberDigits = new List<char>(numberLength);
            int insertedNumbers = 0;

            while (insertedNumbers < numberLength)
            {
                int randomNumber = this.randomGenerator.Next(0, 10);

                if (insertedNumbers == 0 && randomNumber == 0)
                {
                    continue;
                } 

                if (!this.CheckIfDigitIsUsed(secretNumberDigits, randomNumber))
                {
                    secretNumberDigits.Add(randomNumber.ToString()[0]);
                    insertedNumbers++;
                }
            }

            return new string(secretNumberDigits.ToArray());
        }

        /// <summary>
        /// Checks if a digit is met in given list of chars
        /// </summary>
        /// <param name="numberDigits">Digits of a number as a list of chars</param>
        /// <param name="number">Sought number</param>
        /// <returns>Returns true if digit is already used</returns>
        private bool CheckIfDigitIsUsed(List<char> numberDigits, int number)
        {
            var isDigitUsed = false;

            for (int i = 0; i < numberDigits.Count; i++)
            {
                if (numberDigits[i] == number.ToString()[0])
                {
                    isDigitUsed = true;
                    break;
                }
            }

            return isDigitUsed;
        }
    }
}
