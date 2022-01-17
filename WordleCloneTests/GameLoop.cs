using Moq;
using System;
using System.Collections.Generic;
using WordleClone.Core;
using Xunit;

namespace WordleCloneTests
{
    //public class GameLoopTests
    //{

    //    [Fact]
    //    public void GameLoopTest()
    //    {
    //        var gameLoop = new GameLoop();

    //        Assert.Equal(Messages.Start, gameLoop.Start());


    //        var game = new Mock<Game>();

    //        game.Setup(x => x.Guess(It.IsAny<string>())).Returns(GuessCode.IncorrectSpelling);

    //        //gameLoop.Guess();

    //        //game.Setup

    //        //gameLoop.PlayGame(game.Object);
    //        //game.Gu





    //        //IWordDictionary wordDictionary = new WordDictionary(new[] { "PILOT" });

    //        //var game = new Game(wordDictionary);






    //        //mockDictionaryObject.Setup(x => x.Lookup(It.IsAny<string>())).Returns(true);

    //        //var game = new Game("PILOT", mockDictionaryObject.Object);
    //        //game.Guess("AAAAA");
    //        //Assert.Equal(1, game.Rows.Count);
    //        //Assert.False(game.Won);
    //        //Assert.Equal(5, game.RemainingGuesses);

    //    }
    //}

    //public class GameLoop
    //{
    //    public string Start()
    //    {
    //        return Messages.Start;
    //    }

    //    public void PlayGame(IGame game)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //internal class Messages
    //{
    //    public const string Start = "PLAY WORDLE";

    //}
}