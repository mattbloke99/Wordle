namespace WordleClone.Core
{
    public class Game
    {
        const int GuessQuantity = 6;
        private readonly IWordDictionary dictionary;

        public Game(string answer, IWordDictionary dictionary)
        {
            Answer = answer;
            this.dictionary = dictionary;
        }

        public string Answer { get; }
        public IList<Row> Rows { get; internal set; } = new List<Row>();
        public bool Won => Rows.Last().Correct;

        public int RemainingGuesses => GuessQuantity - Rows.Count;

        public GuessCode Guess(string guess)
        {
            if (!dictionary.Lookup(guess))
            {
                return GuessCode.WordNotInDictionary;
            }

            var row = new Row(Answer);
            row.Mark(guess);
            Rows.Add(row);
            return GuessCode.OK;
        }
    }
}