using CsvHelper;
using CsvHelper.Configuration;
using System.Diagnostics;
using System.Globalization;
using NaiveBayes.src.Utils;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Email> emails;
        Dataset data;
        HamWordDictionary hamWordDictionary = new();
        SpamWordDictionary spamWordDictionary = new();
        Stopwatch stopwatch = new();
        stopwatch.Start();
        var filePath = "../../../Dataset/enron_spam_data.csv";
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            Delimiter = ";"
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            emails = csv.GetRecords<Email>().ToList();
            int hamCounter = 0;
            int spamCounter = 0;
            int counter = 0;
            foreach (var email in emails)
            {
                email.PerformDeserializationLogic();
                counter++;
                if (email.Class == EmailTypes.HAM)
                {
                    hamCounter++;
                    foreach (string token in email.TokenizedMessage)
                    {
                        hamWordDictionary.AddWordOrUpdate(token);
                    }
                }
                else
                {
                    spamCounter++;
                    foreach (string token in email.TokenizedMessage)
                    {
                        spamWordDictionary.AddWordOrUpdate(token);
                    }
                }
            }
            hamWordDictionary.CalculatePropabilities();
            spamWordDictionary.CalculatePropabilities();
            data = new Dataset(counter, hamCounter, spamCounter);
        }
        JsonSerializer.Serialization(hamWordDictionary.GetSavedModel(), spamWordDictionary.GetSavedModel());
        stopwatch.Stop();
        Console.WriteLine("Done in " + stopwatch.Elapsed.Seconds + " seconds");
    }
}