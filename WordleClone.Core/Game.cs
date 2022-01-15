namespace WordleClone.Core
{
    public class Game
    {
        const int GuessQuantity = 6;
        const int WordLength = 5;
        private readonly IWordDictionary dictionary;

        public Game(IWordDictionary dictionary)
        {
            _answer = dictionary.GenerateRandomWord(WordLength);
            this.dictionary = dictionary;
        }

        private string _answer { get; }
        public IList<Row> Rows { get; internal set; } = new List<Row>();
        public bool Won => Rows.Last().Correct;

        public int RemainingGuesses => GuessQuantity - Rows.Count;

        public GuessCode Guess(string guess)
        {
            if (!dictionary.Lookup(guess))
            {
                return GuessCode.WordNotInDictionary;
            }

            var row = new Row(_answer);
            row.Mark(guess);
            Rows.Add(row);
            return GuessCode.OK;
        }
    }
}