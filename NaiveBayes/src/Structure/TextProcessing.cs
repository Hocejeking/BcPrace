using opennlp.tools.stemmer;
using opennlp.tools.tokenize;

public static class TextProcessing
{
    public static PorterStemmer porterStemmer = new PorterStemmer();
    public static WhitespaceTokenizer whitespaceTokenizer = WhitespaceTokenizer.INSTANCE;

    public static HashSet<string> stopWords = new HashSet<string>{
        "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours", "yourself", "yourselves", "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their", "theirs", "themselves", "what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are", "was", "were", "be", "been", "being", "have", "has", "had", "having", "do", "does", "did", "doing", "a", "an", "the", "and", "but", "if", "or", "because", "as", "until", "while", "of", "at", "by", "for", "with", "about", "against", "between", "into", "through", "during", "before", "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off", "over", "under", "again", "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both", "each", "few", "more", "most", "other", "some", "such", "only", "own", "same", "so", "than", "too", "very", "s", "t", "can", "will", "just", "don", "should", "now", "1","2","3","4","5","6","7","8","9","0"
    };

    public static HashSet<string> punctuation = new HashSet<string>{
        "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "=", "+", "[", "]", "{", "}", "\\", "|", ";", ":", "'", "\"", ",", ".", "<", ">", "/", "?", " ", "\n"
    };

    public static string[] RemoveStopWords(string[] tokens)
    {
        return tokens.Where(token => !stopWords.Contains(token.ToLower())).ToArray();
    }

    public static string[] RemovePunctuation(string[] tokens)
    {
        return tokens.Where(token => !punctuation.Contains(token.ToLower())).ToArray();
    }

    public static string[] Tokenize(string message)
    {
        return whitespaceTokenizer.tokenize(message.ToLower());
    }

    public static string[] StemTokens(string[] tokens)
    {
        var stemmer = new PorterStemmer();
        return tokens.Select(token => stemmer.stem(token)).ToArray();
    }
}