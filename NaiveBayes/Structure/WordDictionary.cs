public class WordDictionary : IWordDictionary{

    private Dictionary<string, int> _wordDictionary = new Dictionary<string, int>();

    public void addWordOrUpdate(string token){
        if(_wordDictionary.ContainsKey(token)){
            _wordDictionary[token]++;
        }
        else{
            _wordDictionary.Add(token, 1);
        }
    }

    public decimal getAmountOfWords(string token){
        return _wordDictionary[token];
    }
}