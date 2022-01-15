using Moq;
using WordleClone.Core;
using Xunit;

namespace WordleCloneTests
{
    public class GameTests
    {
        Mock<IWordDictionary> _mockDictionaryObject = new Mock<IWordDictionary>();

        public GameTests()
        {
            _mockDictionaryObject.Setup(x => x.GenerateRandomWord(It.IsAny<int>())).Returns("PILOT");
        }

        [Fact]
        public void Game1IncorrectGuess()
        {
            _mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game(_mockDictionaryObject.Object);
            game.Guess("AAAAA");
            Assert.Equal(1, game.Rows.Count);
            Assert.False(game.Won);
            Assert.Equal(5, game.RemainingGuesses);

        }

        [Fact]
        public void Game2IncorrectGuesses()
        {
            _mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game(_mockDictionaryObject.Object);
            game.Guess("AAAAA");
            game.Guess("BBBBB");
            Assert.Equal(2, game.Rows.Count);
            Assert.False(game.Won);
            Assert.Equal(4, game.RemainingGuesses);
        }

        [Fact]
        public void GameCorrectGuess()
        {
            _mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game(_mockDictionaryObject.Object);
            game.Guess("PILOT");
            Assert.True(game.Won);
        }

        [Fact]
        public void Game6IncorrectGuesses()
        {
            _mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game(_mockDictionaryObject.Object);
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
        public void GameValidWordGuess()
        {
            Mock<IWordDictionary> mockDictionaryObject = new Mock<IWordDictionary>();
            mockDictionaryObject.Setup(x => x.GenerateRandomWord(It.IsAny<int>())).Returns("PILOT");
            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

            var game = new Game(mockDictionaryObject.Object);
            Assert.Equal(GuessCode.OK, game.Guess("AAAAA"));
        }

        [Fact]
        public void GameInvalidWordGuess()
        {
            Mock<IWordDictionary> mockDictionaryObject = new Mock<IWordDictionary>();

            mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(false);

            var game = new Game(mockDictionaryObject.Object);
            Assert.Equal(GuessCode.WordNotInDictionary, game.Guess("AAAAA"));
        }
    }
}