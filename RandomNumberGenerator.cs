namespace BullsAndCowsCommandPattern
{
    using System;
    using System.Collections.Generic;

    public class RandomNumberGenerator : NumberGenerator
    {
        private readonly Random randomGenerator = new Random();

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
        /// <param name="number">Seeken number</param>
        /// <returns></returns>
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
