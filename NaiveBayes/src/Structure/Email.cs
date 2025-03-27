using CsvHelper.Configuration.Attributes;
using NaiveBayes.src.Structure;

public class Email : IEmail
{
    [Ignore]
    public String[]? TokenizedMessage;

    public int Id { get; set; }
    public string? Subject { get; set; }
    public string? Message { get; set; }
    [EnumIgnoreCase]
    public EmailClass Class { get; set; }
    public string? Date { get; set; }

    public void PerformDeserializationLogic()
    {
        TokenizedMessage = TextProcessing.Tokenize(Message);
        TokenizedMessage = TextProcessing.RemoveStopWords(TokenizedMessage);
        TokenizedMessage = TextProcessing.RemovePunctuation(TokenizedMessage);
        TokenizedMessage = TextProcessing.StemTokens(TokenizedMessage);
    }
}