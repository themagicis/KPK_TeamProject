// <copyright file="NumberProccesser.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    using System;

    /// <summary>
    /// Class that holds a secret number and calculates if it is guessed
    /// </summary>
    public class NumberProccesser
    {
        /// <summary>
        /// The secret number that should be guessed
        /// </summary>
        private string secretNumber;

        /// <summary>
        /// Generator of a number
        /// </summary>
        private NumberGenerator numberGenerator;

        /// <summary>
        /// Holds the positions of the bulls
        /// </summary>
        private bool[] bulls;

        /// <summary>
        /// Holds the revealed digits of the secret number
        /// </summary>
        private char[] helpingNumber;

        /// <summary>
        /// The count of the revealed digits
        /// </summary>
        private int revealedDigits;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberProccesser"/> class.
        /// </summary>
        /// <param name="numberLength">Length of the secret number</param>
        /// <param name="numberGenerator">Number generator for the secret number</param>
        public NumberProccesser(int numberLength, NumberGenerator numberGenerator)
        {
            this.bulls = new bool[numberLength];
            this.helpingNumber = new char[numberLength];

            for (int i = 0; i < numberLength; i++)
            {
                this.helpingNumber[i] = 'X';
            }

            this.numberGenerator = numberGenerator;
            this.revealedDigits = 0;
            this.GenerateSecretNumber(numberLength);
        }

        /// <summary>
        /// Compares a number with the secret number and checks if it is guessed.
        /// </summary>
        /// <param name="number">Guessing number</param>
        /// <returns>True if the secret number is guessed</returns>
        public bool CheckIsGuessed(string number)
        {
            bool isGuessed = number == this.secretNumber;
            return isGuessed;
        }

        /// <summary>
        /// Compares the secret number with guessing number and
        /// counts how many cows(matching only digit without position) are found.
        /// </summary>
        /// <param name="number">Guessing number</param>
        /// <returns>How many cows are found</returns>
        public int CountCows(string number)
        {
            int count = 0;

            for (int i = 0; i < number.Length; i++)
            {
                char currentDigit = number[i];

                for (int j = 0; j < this.secretNumber.Length; j++)
                {
                    if (i == j || this.bulls[j])
                    {
                        continue;
                    }
                    else if (currentDigit == this.secretNumber[j])
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Compares the secret number with guessing number and
        /// counts how many bull(matching digit and position) are found.
        /// </summary>
        /// <param name="number">Number from the player</param>
        /// <returns>How many bulls are found</returns>
        public int CountBulls(string number)
        {
            int count = 0;

            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == this.secretNumber[i])
                {
                    count++;
                    this.bulls[i] = true;
                    continue;
                }

                this.bulls[i] = false;
            }

            return count;
        }

        /// <summary>
        /// Reveals digit from the secret number.
        /// </summary>
        /// <returns>Char array with secret number digits</returns>
        public char[] RevealDigit()
        {
            bool flag = false;

            if (this.revealedDigits >= this.secretNumber.Length)
            {
                return this.helpingNumber;
            }
            else
            {
                while (!flag)
                {
                    Random random = new Random();
                    int digitForReveal = random.Next(0, this.secretNumber.Length);
                    if (this.helpingNumber[digitForReveal] == 'X')
                    {
                        this.helpingNumber[digitForReveal] = this.secretNumber[digitForReveal];
                        this.revealedDigits++;
                        flag = true;
                    }
                }
            }
            
            return this.helpingNumber;
        }

        /// <summary>
        /// Generates the secret number as string with different digits 
        /// </summary>
        /// <param name="numberLength">The number of the digits in the number</param>
        private void GenerateSecretNumber(int numberLength)
        {
            this.secretNumber = this.numberGenerator.GenerateNumber(numberLength); 
        }
    }
}
