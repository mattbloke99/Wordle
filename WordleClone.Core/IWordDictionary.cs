namespace WordleClone.Core
{
    public interface IWordDictionary
    {
        bool Lookup(string word);
        string GenerateRandomWord(int wordLength);
    }
}