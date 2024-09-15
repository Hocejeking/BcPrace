using CsvHelper.Configuration.Attributes;
public class Email
{
    public int Id { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
    public string? Class {get; set; }
    public string? Date { get; set; }

    [Ignore]
    public String[]? TokenizedMessage;

    public void PerformDeserializationLogic(){
        TokenizedMessage = TextProcessing.Tokenize(Message);
        TokenizedMessage = TextProcessing.RemoveStopWords(TokenizedMessage);
        TokenizedMessage = TextProcessing.RemovePunctuation(TokenizedMessage);
        TokenizedMessage = TextProcessing.StemTokens(TokenizedMessage);
    }
}