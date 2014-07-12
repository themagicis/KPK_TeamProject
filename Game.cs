namespace bullsAndCows
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Game
    {
        private string generatedNumberToWhatToGuess;
        private List<int> poss;
        int cposs = 0;
        internal int score; 
        private static readonly bool shouldContinue = true;
        private static readonly bool shouldNotContinue = false;
        private ScoreBoard myBoard;
        private TopScoresDelegate doTopScores;

        public Game(ScoreBoard bb, TopScoresDelegate doTopScores)
        {
            this.myBoard = bb;
            this.doTopScores = doTopScores; 
        }

        private List<int> Positions
        {
            get
            {
                if (poss == null)
                {
                    poss = new List<int>();
                    for(int i=0;i<generatedNumberToWhatToGuess.Length;++i)
                    {
                        poss.Add(i);
                    }
                    for (int i = 0; i < generatedNumberToWhatToGuess.Length; i++)
                    {
                        int t = int.Parse(randomNumberProvider.CurrentProvider.GetRandomNumber());
                        t = (int)((t - 1000.0) / 9000.0 * generatedNumberToWhatToGuess.Length);
                        int tmp = poss[t];
                        poss[t] = poss[i];
                        poss[i] = tmp;
                    }
                }

                return poss;
            }
        }

        public bool Run()
        {
            Console.WriteLine("A new game has begun!");
            Init();

            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "exit":
                        return shouldNotContinue;
                    case "restart":
                        return shouldContinue;
                    case "help":
                        score = -1;
                        ShowRand();
                        break;
                    case "top":
                        myBoard.Output();
                        break;
                    default:
                        if (score != -1)
                        {
                            score++;
                        }

                        if (MatchCurrent(command))
                        {
                            this.doTopScores(this, this.myBoard);

                            if (Question())
                            {
                                return shouldContinue;
                            }
                            else 
                            {
                                return shouldNotContinue;
                            }
                        }
                        break;
                }
            } 
        }

        private bool Question()
        {
            Console.WriteLine("Another game ? (Y/N)");
            string s = Console.ReadLine();
            if (s.ToLower() == "y") return true;
            else return false;
        }

        private bool MatchCurrent(string cmd)
        {
            if (cmd == generatedNumberToWhatToGuess)
            {
                Console.WriteLine("HOLYCOW, YOU HAVE WON!");
                return true;
            }

            bool[] found = new bool[generatedNumberToWhatToGuess.Length];

            int bulls = CountBulls(cmd, found);
            int cows = CountCows(cmd, found);

            Console.WriteLine(bulls + " bull" + ((bulls != 1) ? "s" : "") + " and " + cows + " cow" + ((cows != 1) ? "s" : ""));
            return false;
        }

        private int CountBulls(string cmd, bool[] found)
        {
            int bulls = 0;
            for (int i = 0; i < generatedNumberToWhatToGuess.Length; i++)
            {
                for (int j = 0; j < cmd.Length; j++)
                {
                    if (generatedNumberToWhatToGuess[i] == cmd[j])
                    {
                        if (i == j)
                        {
                            found[i] = true;
                            bulls++;
                        }
                    }
                }
            }

            return bulls;
        }

        private int CountCows(string cmd, bool[] found)
        {
            int cows  = 0;
            for (int i = 0; i < generatedNumberToWhatToGuess.Length; i++)
            {
                if (!found[i])
                {
                    bool found2 = false;

                    for (int j = 0; j < cmd.Length; j++)
                    {
                        if (generatedNumberToWhatToGuess[i] == cmd[j])
                        {
                            if (i != j)
                            {
                                found2 = true;
                            }
                            else
                            {
                                Environment.Exit(-1);
                            }
                        }
                    }
                    if (found2)
                    {
                        cows++;
                    }
                }
            }

            return cows;
        }

        private void ShowRand()
        {
            Console.WriteLine("Bull at position "+(Positions[++cposs % generatedNumberToWhatToGuess.Length]+1)+ ": <" + generatedNumberToWhatToGuess[Positions[cposs % generatedNumberToWhatToGuess.Length]] + ">");
        }

        private void Init()
        {
            randomNumberProvider.CurrentProvider = new MyProvider();
            generatedNumberToWhatToGuess = randomNumberProvider.CurrentProvider.GetRandomNumber();
            score = 0;
        }
    }
}
