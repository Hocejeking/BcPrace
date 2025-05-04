public interface IWordDictionary
{
    void AddWordOrUpdate(string token);
    double GetAmountOfWord(string token);
    double GetWordsCount();
    void AddProbability(string token, double probability);
    double GetProbability(string token);
    void CalculateProbabilities();
}
