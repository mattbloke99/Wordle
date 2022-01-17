using Moq;
using WordleClone.Core;
using Xunit;

namespace WordleCloneTests
{
    public class WordDictionaryTests
    {
        [Fact]
        public void WordDictionaryLookupTest()
        {
            IWordDictionary dictionary = new WordDictionary(new string[] { "STORE", "BRAIN" });
            Assert.True(dictionary.Lookup("STORE"));
            Assert.True(dictionary.Lookup("store"));
            Assert.False(dictionary.Lookup("AAAAA"));
        }

        [Fact]
        public void WordDictionaryGenerateRandomWordTest()
        {
            IWordDictionary dictionary = new WordDictionary(new string[] { "STORE", "BRAIN","AARDVARK" });
            string randomWord = dictionary.GenerateRandomWord(5);
            Assert.Equal(5, randomWord.Length);
            //Assert.Collection(dictionary, o => o.Contains(randomWord));
        }
    }
}