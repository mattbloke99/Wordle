using Moq;
using WordleClone.Core;
using Xunit;

namespace WordleCloneTests
{
    public class GameTests
    {
        Mock<IWordDictionary> mockDictionaryObject = new Mock<IWordDictionary>();

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
}