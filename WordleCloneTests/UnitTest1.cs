using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WordleCloneTests
{
    public class UnitTest1
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
        }


        [Fact]
        public void Game1IncorrectGuess()
        {
            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game("PILOT", mockDictionaryObject.Object);
            game.Guess("AAAAA");
            Assert.Equal(1, game.Rows.Count);
            Assert.False(game.Won);
            Assert.Equal(5, game.RemainingGuesses);

        }

        [Fact]
        public void Game2IncorrectGuesses()
        {
            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game("PILOT", mockDictionaryObject.Object);
            game.Guess("AAAAA");
            game.Guess("BBBBB");
            Assert.Equal(2, game.Rows.Count);
            Assert.False(game.Won);
            Assert.Equal(4, game.RemainingGuesses);
        }

        [Fact]
        public void GameCorrectGuess()
        {
            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game("PILOT", mockDictionaryObject.Object);
            game.Guess("PILOT");
            Assert.True(game.Won);
        }

        [Fact]
        public void Game6IncorrectGuesses()
        {
            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game("PILOT", mockDictionaryObject.Object);
            game.Guess("AAAAA");
            game.Guess("BBBBB");
            game.Guess("CCCCC");
            game.Guess("DDDDD");
            game.Guess("EEEEE");
            game.Guess("FFFFF");
            Assert.Equal(6, game.Rows.Count);
            Assert.False(game.Won);
            Assert.Equal(0, game.RemainingGuesses);
        }

        [Fact]
        public void WordDictionaryLookupTest()
        {
            IWordDictionary dictionary = new WordDictionary(new string[] { "STORE", "RAIN" });
            Assert.True(dictionary.Lookup("STORE"));
            Assert.False(dictionary.Lookup("AAAAA"));
        }


        [Fact]
        public void GameValidWordGuess()
        {
            Mock<IWordDictionary> mockDictionaryObject = new Mock<IWordDictionary>();

            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game("PILOT", mockDictionaryObject.Object);
            Assert.Equal(GuessCode.OK, game.Guess("AAAAA"));
        }

        [Fact]
        public void GameInvalidWordGuess()
        {
            Mock<IWordDictionary> mockDictionaryObject = new Mock<IWordDictionary>();

            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(false);

            var game = new Game("PILOT", mockDictionaryObject.Object);
            Assert.Equal(GuessCode.WordNotInDictionary, game.Guess("AAAAA"));
        }

    }

    public interface IWordDictionary
    {
        bool Lookup(string word);
    }

    public class WordDictionary : IWordDictionary
    {
        public WordDictionary(string[] dictionary)
        {
            Dictionary = dictionary;
        }

        public string[] Dictionary { get; }

        public bool Lookup(string word)
        {
            return Dictionary.Contains(word);
        }

    }

    internal enum GuessCode
    {
        IncorrectSpelling,
        WordNotInDictionary,
        OK
    }

    internal class Game
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

        internal GuessCode Guess(string guess)
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