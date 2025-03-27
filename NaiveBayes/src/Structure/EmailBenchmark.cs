using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace NaiveBayes.src.Structure
{
    public class EmailBenchmark : IEmail
    {
        [Ignore]
        public String[]? TokenizedMessage;

        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        [EnumIgnoreCase]
        public EmailClass Class { get; set; }
        public string? Date { get; set; }
        public string? RewrittenMessage { get; set; }

        public void PerformDeserializationLogic()
        {
            TokenizedMessage = TextProcessing.Tokenize(RewrittenMessage);
            TokenizedMessage = TextProcessing.RemoveStopWords(TokenizedMessage);
            TokenizedMessage = TextProcessing.RemovePunctuation(TokenizedMessage);
            TokenizedMessage = TextProcessing.StemTokens(TokenizedMessage);
        }
    }


}
