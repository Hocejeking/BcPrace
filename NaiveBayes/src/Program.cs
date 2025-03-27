using CsvHelper;
using CsvHelper.Configuration;
using NaiveBayes.Classifier;
using NaiveBayes.src.Structure;
using NaiveBayes.src.Utils;
using System.Diagnostics;
using System.Globalization;

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

        Console.Clear();

        var filePath = "../../../Dataset/enron_spam_data.csv";
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            Delimiter = ";"
        };

        Console.WriteLine("Starting the training process\n");

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            emails = csv.GetRecords<Email>().ToList();
            int hamCounter = 0;
            int spamCounter = 0;
            int counter = 0;
            int totalEmails = emails.Count;
            int spamWordsCount = 0;
            int hamWordsCount = 0;

            foreach (var email in emails)
            {
                email.PerformDeserializationLogic();
                counter++;
                int percentage = (int)((double)counter / totalEmails * 100);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\rProgress: {0}%", percentage);
                Console.ForegroundColor = ConsoleColor.White;

                if (email.Class == EmailClass.HAM)
                {
                    hamCounter++;
                    foreach (string token in email.TokenizedMessage)
                    {
                        hamWordsCount++;
                        hamWordDictionary.AddWordOrUpdate(token);
                    }
                }
                else
                {
                    spamCounter++;
                    foreach (string token in email.TokenizedMessage)
                    {
                        spamWordsCount++;
                        spamWordDictionary.AddWordOrUpdate(token);
                    }
                }
            }

            hamWordDictionary.CalculatePropabilities();
            spamWordDictionary.CalculatePropabilities();

            data = new Dataset(counter, hamCounter, spamCounter, hamWordsCount, spamWordsCount);
        }

        JsonSerializer.Serialization(hamWordDictionary.GetSavedModel(), spamWordDictionary.GetSavedModel());
        stopwatch.Stop();

        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nDone in " + stopwatch.Elapsed.Seconds + " seconds");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();
        stopwatch.Restart();
        Console.WriteLine("Classifying...");
        List<EmailBenchmark> rewrittenEmails;
        var filePathBenchmark = "../../../Dataset/rewrittenDataset.csv";
        var configBenchmark = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            Delimiter = ";"
        };
        using (var reader = new StreamReader(filePathBenchmark))
        using (var csv = new CsvReader(reader, configBenchmark))
        {
            rewrittenEmails = csv.GetRecords<EmailBenchmark>().ToList();
            var trainedData = JsonSerializer.Deserialization();
            NaiveBayesClassifier naiveBayes = new NaiveBayesClassifier(trainedData, data);
            int correctCounter = 0, falseCounter = 0;
            foreach (var email in rewrittenEmails)
            {
                email.PerformDeserializationLogic();
                bool isSpam = naiveBayes.Predict(email.TokenizedMessage);
                if (isSpam)
                {
                    correctCounter++;
                }
                else
                {
                    falseCounter++;
                }
            }

            Console.WriteLine("The number of correctly classified emails is: " + correctCounter);
            Console.WriteLine("The number of incorrectly classified emails is: " + falseCounter);
            int totalEmails = correctCounter + falseCounter;
            double accuracy = (double)correctCounter / totalEmails * 100;
            stopwatch.Stop();
            Console.WriteLine($"Accuracy: {accuracy:F2}%");
            Console.WriteLine($"Done in: {stopwatch.Elapsed.Seconds}s");
        }
    }

}