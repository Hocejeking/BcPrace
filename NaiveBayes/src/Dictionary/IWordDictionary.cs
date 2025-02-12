public interface IWordDictionary
{
    void AddWordOrUpdate(string token);
    decimal GetAmountOfWord(string token);
    decimal GetAmountOfWords();
    void AddPropability(string token, decimal propability);
    decimal GetPropability(string token);
    void CalculatePropabilities();
}