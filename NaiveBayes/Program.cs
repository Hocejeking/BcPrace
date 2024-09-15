using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using java.util;
internal class Program
{
    private static void Main(string[] args)
    {
        List<Email> emails;
        Dataset data;
        HamWordDictionary hamWordDictionary = new HamWordDictionary();
        SpamWordDictionary spamWordDictionary = new SpamWordDictionary();
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var filePath  = "/Users/tobiashocevar/Downloads/enron_spam_data.csv";
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            Delimiter = ";"
        };
        
        using(var reader = new StreamReader(filePath))
        using(var csv = new CsvReader(reader, config)){
            emails = csv.GetRecords<Email>().ToList();
            int hamCounter = 0;
            int spamCounter = 0;
            foreach (var email in emails)
            {
                email.PerformDeserializationLogic();
                if(email.Class == "ham"){
                    hamCounter++;
                    foreach(string token in email.TokenizedMessage){
                        hamWordDictionary.addWordOrUpdate(token);
                    }
                }
                else{
                    spamCounter++;
                    foreach(string token in email.TokenizedMessage){
                        spamWordDictionary.addWordOrUpdate(token);
                    }
                }
            }
            data = new Dataset(emails.LastOrDefault().Id+2, hamCounter,spamCounter);
        }
        stopwatch.Stop();
        Console.WriteLine("Done in: " + stopwatch.Elapsed.Seconds);
    }
}