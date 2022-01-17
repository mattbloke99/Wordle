namespace WordleClone.Core
{
    public interface IGame
    {
        bool Won { get; }
        int GuessesMade { get;}
        int RemainingGuesses { get; }
        string Answer { get; }

        GuessCode Guess(string guess);
        Row? GetLastRow();

    }
}