public interface IWordDictionary
{
    void AddWordOrUpdate(string token);  // Add or update the word's count in the dictionary
    decimal GetAmountOfWord(string token);  // Get the count of a specific word
    decimal GetUniqueWordsCount();  // Get the number of unique words in the dictionary
    void AddProbability(string token, decimal probability);  // Add or update the probability of a word
    decimal GetProbability(string token);  // Get the probability of a specific word
    void CalculateProbabilities();  // Calculate the probabilities for all words in the dictionary
}
