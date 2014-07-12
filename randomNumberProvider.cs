using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bullsAndCows
{
    public class randomNumberProvider
    {
        protected Random randomGenerator = new Random();
        private static randomNumberProvider currentProvider;
        public static randomNumberProvider CurrentProvider
        {
            get
            {
                if (currentProvider == null)
                {
                    currentProvider = new randomNumberProvider();
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
            return 4165.ToString();
        }
    }
}
