using WordleClone.Core;

namespace WordleClone
{
    public class ConsoleGame
    {

        static void Main()
        {
            ConsoleGame consoleGame = new ConsoleGame();
            consoleGame.GameLoop();
        }

        public void GameLoop()
        {
            DisplayIntroText();

            do
            {
                PlayGame();
            } while (TryAgain());


     

        }

        private void PlayGame()
        {
            Game game = new Game("PILOT", new WordDictionary(new[] {"AAAAA"}));
            Row row;

            do
            {
                string wordGuess = Console.ReadLine();

                if (game.Guess(wordGuess) == GuessCode.WordNotInDictionary)
                {
                    Console.WriteLine($"{wordGuess} not in dictionary");
                }

                row = game.Rows.Last();


            } while (game.RemainingGuesses > 0 || !row.Correct);


            if(game.Rows.Last().Correct)
            {
                Console.WriteLine("You have won");
                return;
            }
                Console.WriteLine("You have lost");

        }

        private bool TryAgain()
        {
            Console.WriteLine("ANOTHER WORDLE? (YES OR NO)? ");
            return IsInputYes(Console.ReadLine());
        }

        public static bool IsInputYes(string consoleInput)
        {
            var options = new string[] { "Y", "YES" };
            return options.Any(o => o.Equals(consoleInput, StringComparison.CurrentCultureIgnoreCase));
        }

        private void DisplayIntroText()
        {
            Console.WriteLine("PLAY WORDLE");
        }
    }
}