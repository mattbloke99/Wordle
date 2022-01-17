namespace WordleClone.Core
{
    public class Row
    {
        public Row(string answer)
        {
            Answer = answer;
        }

        public string Answer { get; }
        public bool Correct => MarkedGuess.All(o => o.Value == Core.Mark.Right);
        public IList<KeyValuePair<char, Mark>> MarkedGuess { get; } = new List<KeyValuePair<char, Mark>>();

        public void Mark(string guess)
        {
            guess = guess.ToUpper();

            for (int i = 0; i < guess.Length; i++)
            {
                var letter = guess[i];
                Mark mark;

                if (Answer[i].Equals(letter))
                {
                    mark = Core.Mark.Right;
                }
                else if (Answer.Contains(letter))
                {
                    mark = Core.Mark.Partial;
                }
                else
                {
                    mark = Core.Mark.Wrong;
                }
                MarkedGuess.Add(new KeyValuePair<char, Mark>(letter, mark));
            }
        }

        public string Render()
        {
            return string.Join(" ", MarkedGuess.Select(o => o.Value.ToString()));
        }
    }
}