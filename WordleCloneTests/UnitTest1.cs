using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WordleCloneTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Row row = new Row("PILOT");
            row.Mark("AAAAA");

            Assert.Equal(Mark.Wrong, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[4].Value);
            Assert.False(row.Correct);
        }

        [Fact]
        public void Test2()
        {
            Row row = new Row("PILOT");
            row.Mark("TAAAA");

            Assert.Equal(Mark.Partial, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[4].Value);
            Assert.False(row.Correct);
        }

        [Fact]
        public void Test3()
        {
            Row row = new Row("PILOT");
            row.Mark("TAAAT");

            Assert.Equal(Mark.Partial, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[4].Value);
            Assert.False(row.Correct);
        }

        [Fact]
        public void Test4()
        {
            Row row = new Row("PILOT");
            row.Mark("PILOT");

            Assert.Equal(Mark.Right, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[4].Value);
            Assert.True(row.Correct);
        }

    }

    public class Row
    {
        public Row(string answer)
        {
            Answer = answer;
        }

        public string Answer { get; }
        public bool Correct => MarkedGuess.All(o => o.Value == WordleCloneTests.Mark.Right);
        internal IList<KeyValuePair<char, Mark>> MarkedGuess { get;  } = new List<KeyValuePair<char, Mark>>();

        internal void Mark(string guess)
        {
            for (int i = 0; i < guess.Length; i++)
            {
                var letter = guess[i];
                Mark mark;

                if (Answer[i].Equals(letter))
                {
                    mark = WordleCloneTests.Mark.Right;
                }
                else if (Answer.Contains(letter))
                {
                    mark = WordleCloneTests.Mark.Partial;
                }
                else
                {
                    mark = WordleCloneTests.Mark.Wrong;
                }
                MarkedGuess.Add(new KeyValuePair<char, Mark>(letter, mark));
            }
        }
    }

    enum Mark
    {
        Wrong,
        Right,
        Partial
    }
}