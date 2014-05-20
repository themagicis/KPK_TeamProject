namespace BullsAndCows
{
    using System;
    using System.Linq;

    public class MyProvider : RandomNumberProvider
    {
        public override string GetRandomNumber()
        {
            //ToDo: Implement Random Generator
            return "1234";
            //// ((int)(rand.NextDouble() * 9000 + 1000)).ToString();
        }
    }
}
