using CsvHelper;
using CsvHelper.Configuration;
using NaiveBayes.Classifier;
using NaiveBayes.src.Classifier;
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
            Console.WriteLine("Calculating propabilities");
            hamWordDictionary.CalculateProbabilities();
            spamWordDictionary.CalculateProbabilities();

            data = new Dataset(counter, spamCounter, hamCounter, hamWordsCount, spamWordsCount);
        }

        JsonSerializer.Serialization(hamWordDictionary.GetSavedModel(), spamWordDictionary.GetSavedModel());
        stopwatch.Stop();

        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nDone in " + stopwatch.Elapsed.Seconds + " seconds");
        Console.ForegroundColor = ConsoleColor.White;
        stopwatch.Restart();
        Console.WriteLine("Classifying... (This can take a long time depending on the data size) ");
        List<EmailBenchmark> rewrittenEmails;
        var filePathBenchmark = "../../../Dataset/trecHam+enronSpam.csv";
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
            int totalEmailsBenchmark = rewrittenEmails.Count;
            Console.WriteLine("Total emails to classify: {0}", totalEmailsBenchmark);
            int counter = 0;
            int percentage = 0;
            var trainedData = JsonSerializer.Deserialization();
            NaiveBayesClassifier naiveBayes = new NaiveBayesClassifier(trainedData, data, hamWordDictionary, spamWordDictionary);
            Console.WriteLine(data.priorPropabilityHam);
            Console.WriteLine(data.priorPropabilitySpam);
            int truePositive = 0, trueNegative = 0, falsePositive = 0, falseNegative = 0, totalSpam = 0, totalHam = 0;
            foreach (var email in rewrittenEmails)
            {
                email.PerformDeserializationLogic();
                counter++;
                
                percentage = (int)((counter / (double)totalEmailsBenchmark) * 100); 
                Console.Write($"\rProgress: {percentage}%");
                bool isSpam = naiveBayes.Predict(email.TokenizedMessage);
                if(isSpam == true && email.Class == EmailClass.SPAM)
                {
                    truePositive++;
                }
                else if(isSpam == false && email.Class == EmailClass.SPAM)
                {
                    falseNegative++;
                }
                else if(isSpam == false && email.Class == EmailClass.HAM)
                {
                    trueNegative++;
                }
                else if(isSpam == true &&email.Class == EmailClass.HAM)
                {
                    falsePositive++;
                }

                if(email.Class == EmailClass.SPAM)
                {
                    totalSpam++;
                }
                else if(email.Class == EmailClass.HAM)
                {
                    totalHam++;
                }
            }

            counter = 0;
            percentage = 0;

            ConfusionMatrix confusionMatrix = new ConfusionMatrix(truePositive, trueNegative, falsePositive, falseNegative);
            Console.WriteLine($"Total spam e-mails classified: {totalSpam}");
            Console.WriteLine($"Total ham e-mails classified: {totalHam}");
            Console.WriteLine("");
            Console.WriteLine("True Positive (TP): Spam correctly predicted as Spam");
            Console.WriteLine("False Positive (FP): Not Spam incorrectly predicted as Spam");
            Console.WriteLine("False Negative (FN): Spam incorrectly predicted as Not Spam");
            Console.WriteLine("True Negative (TN): Not Spam correctly predicted as Not Spam");
            Console.WriteLine($"True positive: {confusionMatrix.TruePositive}");
            Console.WriteLine($"False positive: {confusionMatrix.FalsePositive}");
            Console.WriteLine($"True negative: {confusionMatrix.TrueNegative}");
            Console.WriteLine($"False negative: {confusionMatrix.FalseNegative}");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Sensitivity: {confusionMatrix.Sensitivity}");
            Console.WriteLine($"Specificity: {confusionMatrix.Specificity}");
            Console.WriteLine($"Precision: {confusionMatrix.Precision}");
            Console.WriteLine($"Negative predictive value: {confusionMatrix.NegativePredictiveValue}");
            Console.WriteLine($"Accuracy: {confusionMatrix.Accuracy}");
            stopwatch.Stop();
            Console.WriteLine($"Done in: {stopwatch.Elapsed.Seconds}s");
            textDeserializer textDeserializer = new("TrainedModel/vystupniData.txt", confusionMatrix);
        }
    }

}