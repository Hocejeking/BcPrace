public interface IWordDictionary
{
    void AddWordOrUpdate(string token);
    decimal GetAmountOfWord(string token);
    decimal GetUniqueWordsCount();
    void AddProbability(string token, decimal probability);
    decimal GetProbability(string token);
    void CalculateProbabilities();
}
