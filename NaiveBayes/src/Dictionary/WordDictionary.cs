using org.glassfish.grizzly.http.server.util;

public class WordDictionary : IWordDictionary{
    private Dictionary<string, int> _wordDictionary = new Dictionary<string, int>();
    private Dictionary<string, decimal> _wordPropabilities = new Dictionary<string, decimal>();

    public void addWordOrUpdate(string token){
        if(_wordDictionary.ContainsKey(token)){
            _wordDictionary[token]++;
        }
        else{
            _wordDictionary.Add(token, 1);
        }
    }

    public decimal getAmountOfWord(string token){
        return _wordDictionary[token];
    }

    public decimal getAmountOfWords(){
        return _wordDictionary.Count;
    }

    public void addPropability(string token, decimal propability){
        if(_wordPropabilities.ContainsKey(token)){
            return;
        }
        else{
            _wordPropabilities.Add(token,propability);
        }
    }

    public decimal getPropability(string token){
        return _wordPropabilities[token];
    }

    public void calculatePropabilities(){
        foreach(KeyValuePair<string, int> word in _wordDictionary)
        {
            _wordPropabilities.Add(word.Key, (decimal) (getAmountOfWord(word.Key) / getAmountOfWords()));
        }
    }
}