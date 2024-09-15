public interface IWordDictionary{
    void addWordOrUpdate(string token);
    decimal getAmountOfWord(string token);
    decimal getAmountOfWords();
    void addPropability(string token, decimal propability);
    decimal getPropability(string token);
    void calculatePropabilities();
}