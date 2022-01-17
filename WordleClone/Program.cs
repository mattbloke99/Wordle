using WordleClone.Core;

namespace WordleClone
{
    public class ConsoleGame
    {

        static void Main()
        {
            DisplayIntroText();
            do
            {
                var file = File.ReadAllLines("words-english.txt");
                Game game = new Game(new WordDictionary(file));

                PlayGame(game);
            } while (TryAgain());
        }

        static private void PlayGame(IGame game)
        {
            do
            {
                string wordGuess = Console.ReadLine();

                if (game.Guess(wordGuess) == GuessCode.IncorrectSpelling)
                {
                    Console.WriteLine($"{wordGuess} not in dictionary");
                } else
                {
                    Row row = game.GetLastRow();

                    foreach (var item in row.MarkedGuess)
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                        switch (item.Value)
                        {
                            case Mark.Right:
                                Console.BackgroundColor = ConsoleColor.Green;
                                break;
                            case Mark.Partial:
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            default:
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                        }
                        Console.Write(item.Key);
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
            } while (game.RemainingGuesses > 0 && !game.Won);


            if(game.Won)
            {
                Console.WriteLine($"You have won in {game.GuessesMade} guess");
            } else
            {
                Console.WriteLine($"You have lost, the answer was {game.Answer}");
            }
        }

        private static bool TryAgain()
        {
            Console.WriteLine("ANOTHER WORDLE? (YES OR NO)? ");
            return IsInputYes(Console.ReadLine());
        }

        public static bool IsInputYes(string consoleInput)
        {
            var options = new string[] { "Y", "YES" };
            return options.Any(o => o.Equals(consoleInput, StringComparison.CurrentCultureIgnoreCase));
        }

        private static void DisplayIntroText()
        {
            Console.WriteLine("PLAY WORDLE");
        }
    }
}