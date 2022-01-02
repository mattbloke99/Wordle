using System;
using System.Collections.Generic;
using Xunit;

namespace WordleCloneTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Row row = new Row("PILOT");
            Flag[] result =  row.Guess("AAAAA");

            Assert.Equal(Flag.Wrong, result[0]);
            Assert.False(row.Correct);
        }
    }

    public class Row
    {
        public Row(string answer)
        {
            Answer = answer;
        }

        public string Answer { get; }
        public bool Correct { get; private set; }

        internal Flag[] Guess(string guess)
        {
            Correct = false;
            return new[] { Flag.Wrong, Flag.Wrong, Flag.Wrong, Flag.Wrong, Flag.Wrong };
        }
    }

    enum Flag
    {
        Wrong,
        Right,
        Partial
    }
}