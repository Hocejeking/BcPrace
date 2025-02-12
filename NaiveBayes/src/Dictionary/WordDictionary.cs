public class WordDictionary : IWordDictionary
{
    private Dictionary<string, int> _wordDictionary = new();
    private Dictionary<string, decimal> _wordPropabilities = new();

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
        return _wordDictionary[token];
    }

    public decimal GetAmountOfWords()
    {
        return _wordDictionary.Count;
    }

    public void AddPropability(string token, decimal propability)
    {
        if (_wordPropabilities.ContainsKey(token))
        {
            return;
        }
        else
        {
            _wordPropabilities.Add(token, propability);
        }
    }

    public decimal GetPropability(string token)
    {
        return _wordPropabilities[token];
    }

    public Dictionary<string, decimal> GetSavedModel()
    {
        return _wordPropabilities;
    }

    public void CalculatePropabilities()
    {
        foreach (KeyValuePair<string, int> word in _wordDictionary)
        {
            _wordPropabilities.Add(word.Key, (decimal)(GetAmountOfWord(word.Key) / GetAmountOfWords()));
        }
    }
}