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

        [Fact]
        public void Test3()
        {
            Row row = new Row("PILOT");
            Flag[] result = row.Guess("TAAAT");

            Assert.Equal(Flag.Partial, result[0]);
            Assert.Equal(Flag.Wrong, result[1]);
            Assert.Equal(Flag.Wrong, result[2]);
            Assert.Equal(Flag.Wrong, result[3]);
            Assert.Equal(Flag.Right, result[4]);
            Assert.False(row.Correct);
        }

        [Fact]
        public void Test4()
        {
            Row row = new Row("PILOT");
            Flag[] result = row.Guess("PILOT");

            Assert.Equal(Flag.Right, result[0]);
            Assert.Equal(Flag.Right, result[1]);
            Assert.Equal(Flag.Right, result[2]);
            Assert.Equal(Flag.Right, result[3]);
            Assert.Equal(Flag.Right, result[4]);
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
        public bool Correct { get; private set; }

        internal Flag[] Guess(string guess)
        {
            var ret = Enumerable.Repeat(Flag.Wrong, Answer.Length).ToArray();

            int i = 0;
            foreach (var letter in guess)
            {

                if (Answer[i].Equals(letter))
                {
                    ret[i] = Flag.Right;
                } else if (Answer.Contains(letter))
                {
                    ret[i] = Flag.Partial;
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