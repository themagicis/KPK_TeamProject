// <copyright file="NumberGenerator.cs" company="Bulls-and-Cows-3">
//     Bulls-and-Cows-3 Team. All rights reserved.
// </copyright>
// <author></author>
namespace BullsAndCowsCommandPattern
{
    /// <summary>
    /// Abstract class for creating a secret number.
    /// </summary>
    public abstract class NumberGenerator
    {
        /// <summary>
        /// Abstract method that generates a secret number with all unique digits
        /// bu given length of the number
        /// </summary>
        /// <param name="numberLength">How many digits number to have</param>
        /// <returns>Generated number as string</returns>
        public abstract string GenerateNumber(int numberLength);
    }
}
