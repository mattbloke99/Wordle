namespace WordleClone.Core
{
    public class Game : IGame
    {
        const int GuessQuantity = 6;
        const int WordLength = 5;
        private readonly IWordDictionary dictionary;

        public Game(IWordDictionary dictionary)
        {
            Answer = dictionary.GenerateRandomWord(WordLength);
            this.dictionary = dictionary;
        }

        public string Answer { get; }
        private IList<Row> Rows { get; set; } = new List<Row>();
        public bool Won =>  GetLastRow()?.Correct ?? false;

        public int GuessesMade => Rows.Count;
        public int RemainingGuesses => GuessQuantity - GuessesMade;

        public GuessCode Guess(string guess)
        {
            if (guess.Length != Answer.Length)
            {
                return GuessCode.WrongLength;
            }

            if (!dictionary.Lookup(guess))
            {
                return GuessCode.IncorrectSpelling;
            }

            var row = new Row(Answer);
            row.Mark(guess);
            Rows.Add(row);
            return GuessCode.OK;
        }

        public Row? GetLastRow() => Rows?.LastOrDefault();
    }
}