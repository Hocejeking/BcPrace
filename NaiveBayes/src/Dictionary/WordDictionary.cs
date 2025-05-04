public abstract class WordDictionary : IWordDictionary
{
    private Dictionary<string, double> _wordDictionary = new();
    private Dictionary<string, double> _wordProbabilities = new();

    public void AddWordOrUpdate(string token)
    {
        if (_wordDictionary.ContainsKey(token))
        {
            _wordDictionary[token]++;
        }
        else
        {
            _wordDictionary.Add(token, 1);
        }
    }

    public double GetAmountOfWord(string token)
    {
        return _wordDictionary.TryGetValue(token, out var count) ? count : 0;
    }

    public double GetWordsCount()
    {
        return _wordDictionary.Values.Sum();
    }

    public void AddProbability(string token, double probability)
    {
        if (!_wordProbabilities.ContainsKey(token))
        {
            _wordProbabilities.Add(token, probability);
        }
    }

    public double GetProbability(string token)
    {
        return _wordProbabilities.TryGetValue(token, out var prob) ? prob : 0;
    }

    public Dictionary<string, double> GetSavedModel()
    {
        return _wordProbabilities;
    }

    public void CalculateProbabilities()
    {
        double wordsCount = GetWordsCount();
        foreach (KeyValuePair<string, double> word in _wordDictionary)
        {
            double probability = word.Value / wordsCount;
            AddProbability(word.Key, probability);
        }
    }
}
