namespace WordleClone.Core
{
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
}