namespace BullsAndCows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Game
    {
        private static readonly bool ShouldContinue = true;
        private static readonly bool ShouldNotContinue = false;
        private string maxGuessNumber;
        private List<int> possition;
        private int countPossition = 0;
        private ScoreBoard myBoard;
        private TopScoresDelegate doTopScores;

        public Game(ScoreBoard scoreBoard, TopScoresDelegate doTopScores)
        {
            this.myBoard = scoreBoard;
            this.doTopScores = doTopScores;
        }

        //ToDo:Fix Modifier
        internal int Score { get; set; }

        private List<int> Positions
        {
            get
            {
                if (this.possition == null)
                {
                    this.possition = new List<int>();

                    for (int i = 0; i < this.maxGuessNumber.Length; ++i)
                    {
                        this.possition.Add(i);
                    }

                    for (int i = 0; i < this.maxGuessNumber.Length; i++)
                    {
                        int t = int.Parse(RandomNumberProvider.CurrentProvider.GetRandomNumber());
                        t = (int)((t - 1000.0) / 9000.0 * this.maxGuessNumber.Length);
                        int tmp = this.possition[t];
                        this.possition[t] = this.possition[i];
                        this.possition[i] = tmp;
                    }
                }

                return this.possition;
            }
        }

        public bool Run()
        {
            Console.WriteLine("A new game has begun!");

            this.InitialState();

            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "exit":
                        {
                            return ShouldNotContinue;
                        }

                    case "restart":
                        {
                            return ShouldContinue;
                        }

                    case "help":
                        {
                            this.Score = -1;
                            this.ShowRand();
                            break;
                        }

                    case "top":
                        {
                            this.myBoard.Output();
                            break;
                        }

                    default:
                        {
                            if (this.Score != -1)
                            {
                                this.Score++;
                            }

                            if (this.MatchCurrent(command))
                            {
                                this.doTopScores(this, this.myBoard);

                                if (this.Question())
                                {
                                    return ShouldContinue;
                                }
                                else
                                {
                                    return ShouldNotContinue;
                                }
                            }

                            break;
                        }
                }
            }
        }

        private bool Question()
        {
            Console.WriteLine("Another game ? (Y/N)");
            string s = Console.ReadLine();
            
            if (s.ToLower() == "y")
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        private bool MatchCurrent(string cmd)
        {
            if (cmd == this.maxGuessNumber)
            {
                Console.WriteLine("HOLYCOW, YOU HAVE WON!");
                return true;
            }

            bool[] found = new bool[this.maxGuessNumber.Length];

            int b = this.SecondCount(cmd, found);
            int c = this.FirstCount(cmd, found);
            
            //TODO:FIX CONCATINATION
            Console.WriteLine(b + " bull" + ((b != 1) ? "s" : string.Empty) + " and " + c + " cow" + ((c != 1) ? "s" : string.Empty));

            return false;
        }

        private int SecondCount(string cmd, bool[] found)
        {
            int count = 0;

            for (int i = 0; i < this.maxGuessNumber.Length; i++)
            {
                for (int j = 0; j < cmd.Length; j++)
                {
                    if (this.maxGuessNumber[i] == cmd[j] && i == j)
                    {
                        found[i] = true;
                        count++;
                    }
                }
            }
       
            return count;
        }

        private int FirstCount(string cmd, bool[] found)
        {
            int count = 0;

            for (int i = 0; i < this.maxGuessNumber.Length; i++)
            {
                if (!found[i])
                {
                    bool isFound = false;
            
                    for (int j = 0; j < cmd.Length; j++)
                    {
                        if (this.maxGuessNumber[i] == cmd[j])
                        {
                            if (i != j)
                            {
                                isFound = true;
                            }
                            else
                            {
                                Environment.Exit(-1);
                            }
                        }
                    }

                    if (isFound) 
                    { 
                        count++; 
                    }
                }
            }

            return count;
        }

        private void ShowRand()
        {
            //ToDo:Fix Concatination
            Console.WriteLine("Bull at position " + (this.Positions[++this.countPossition % this.maxGuessNumber.Length] + 1) + ": <" + this.maxGuessNumber[this.Positions[this.countPossition % this.maxGuessNumber.Length]] + ">");
        }

        private void InitialState()
        {
            RandomNumberProvider.CurrentProvider = new MyProvider();
            this.maxGuessNumber = RandomNumberProvider.CurrentProvider.GetRandomNumber();
            this.Score = 0;
        }
    }
}
