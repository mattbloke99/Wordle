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
            Assert.Equal(Flag.Wrong, result[1]);
            Assert.Equal(Flag.Wrong, result[2]);
            Assert.Equal(Flag.Wrong, result[3]);
            Assert.Equal(Flag.Wrong, result[4]);
            Assert.False(row.Correct);
        }

        [Fact]
        public void Test2()
        {
            Row row = new Row("PILOT");
            Flag[] result = row.Guess("TAAAA");

            Assert.Equal(Flag.Partial, result[0]);
            Assert.Equal(Flag.Wrong, result[1]);
            Assert.Equal(Flag.Wrong, result[2]);
            Assert.Equal(Flag.Wrong, result[3]);
            Assert.Equal(Flag.Wrong, result[4]);
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
            var ret = new Flag[Answer.Length];

            int i = 0;
            foreach (var letter in Answer)
            {

                ret[0] = Flag.Wrong;

                if (guess.Equals(letter))
                {
                    ret[0] = Flag.Right;
                } else if (guess.Contains(letter))
                {
                    ret[0] = Flag.Partial;
                }
                i++;

            }


            Correct = Answer.Equals(guess,StringComparison.InvariantCultureIgnoreCase);
            return ret;
        }
    }

    enum Flag
    {
        Wrong,
        Right,
        Partial
    }
}