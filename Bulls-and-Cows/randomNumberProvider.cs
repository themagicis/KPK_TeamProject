namespace BullsAndCows
{
    using System;
    using System.Linq;

    public class RandomNumberProvider
    {
        //ToDo: Fix Modifier
        protected Random ran = new Random();
        private static RandomNumberProvider currentProvider;

        public static RandomNumberProvider CurrentProvider
        {
            get
            {
                if (currentProvider == null)
                {
                    currentProvider = new RandomNumberProvider();
                }

                return currentProvider;
            }

            set
            {
                currentProvider = value;
            }
        }

        public virtual string GetRandomNumber()
        {
            //ToDo: Implement
            return 4165.ToString();
        }
    }
}
