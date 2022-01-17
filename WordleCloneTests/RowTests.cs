using Moq;
using WordleClone.Core;
using Xunit;

namespace WordleCloneTests
{
    public class RowTests
    {
        Mock<IWordDictionary> mockDictionaryObject = new Mock<IWordDictionary>();


        [Fact]
        public void MarkRowAllWrong()
        {
            Row row = new Row("PILOT");
            row.Mark("AAAAA");

            Assert.Equal(Mark.Wrong, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[4].Value);
            Assert.False(row.Correct);

            Assert.Equal("Wrong Wrong Wrong Wrong Wrong", row.Render());

        }

        [Fact]
        public void MarkRowRightLetterWrongPosition()
        {
            Row row = new Row("PILOT");
            row.Mark("TAAAA");

            Assert.Equal(Mark.Partial, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[4].Value);
            Assert.False(row.Correct);

            Assert.Equal("Partial Wrong Wrong Wrong Wrong", row.Render());
        }

        [Fact]
        public void MarkRowRightLetterWrongAndRightPosition()
        {
            Row row = new Row("PILOT");
            row.Mark("TAAAT");

            Assert.Equal(Mark.Partial, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[4].Value);
            Assert.False(row.Correct);

            Assert.Equal("Partial Wrong Wrong Wrong Right", row.Render());
        }

        [Fact]
        public void MarkRowAllRight()
        {
            Row row = new Row("PILOT");
            row.Mark("PILOT");

            Assert.Equal(Mark.Right, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Right, row.MarkedGuess[4].Value);
            Assert.True(row.Correct);

            Assert.Equal("Right Right Right Right Right", row.Render());
        }

        [Fact]
        public void HandleLowerCase()
        {
            Row row = new Row("PILOT");
            row.Mark("pilot");

            Assert.True(row.Correct);

            Assert.Equal("Right Right Right Right Right", row.Render());
        }

        [Fact]
        public void HandleDoubleLettersCase()
        {
            Row row = new Row("LOBBY");
            row.Mark("build");

            Assert.Equal(Mark.Partial, row.MarkedGuess[0].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[1].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[2].Value);
            Assert.Equal(Mark.Partial, row.MarkedGuess[3].Value);
            Assert.Equal(Mark.Wrong, row.MarkedGuess[4].Value);

            Assert.False(row.Correct);
        }
    }
}