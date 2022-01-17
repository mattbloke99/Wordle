namespace WordleClone.Core
{
    public class WordDictionary : IWordDictionary
    {
        public WordDictionary(string[] dictionary)
        {
            _dictionary = dictionary;
        }

        private string[] _dictionary { get; } = new string[0];

        public bool Lookup(string word)
        {
            return _dictionary.Contains(word, StringComparer.OrdinalIgnoreCase);
        }

        public string GenerateRandomWord(int wordLength)
        {
            return _dictionary.Where(x => x.Length == wordLength).OrderBy(x => Guid.NewGuid()).FirstOrDefault().ToUpper();
        }
    }
}