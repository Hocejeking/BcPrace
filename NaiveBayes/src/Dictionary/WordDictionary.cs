public abstract class WordDictionary : IWordDictionary
{
    private Dictionary<string, decimal> _wordDictionary = new();
    private Dictionary<string, decimal> _wordProbabilities = new();

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

    public decimal GetAmountOfWord(string token)
    {
        return _wordDictionary.TryGetValue(token, out var count) ? count : 0;
    }

    public decimal GetUniqueWordsCount()
    {
        return _wordDictionary.Count;
    }

    public void AddProbability(string token, decimal probability)
    {
        if (!_wordProbabilities.ContainsKey(token))
        {
            _wordProbabilities.Add(token, probability);
        }
    }

    public decimal GetProbability(string token)
    {
        return _wordProbabilities.TryGetValue(token, out var prob) ? prob : 0;
    }

    public Dictionary<string, decimal> GetSavedModel()
    {
        return _wordProbabilities;
    }

    public void CalculateProbabilities()
    {
        foreach (KeyValuePair<string, decimal> word in _wordDictionary)
        {
            decimal probability = word.Value / GetUniqueWordsCount();
            AddProbability(word.Key, probability);
        }
    }
}
