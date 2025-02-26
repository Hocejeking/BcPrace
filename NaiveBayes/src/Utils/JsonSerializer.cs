using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NaiveBayes.src.Utils
{
    public static class JsonSerializer
    {
      public static void Serialization(Dictionary<string, decimal> hamProbs, Dictionary<string, decimal> spamProbs)
      {
            var data = new Dictionary<EmailTypes, Dictionary<string, decimal>>
            {
                { EmailTypes.HAM, hamProbs},
                { EmailTypes.SPAM, spamProbs},
            };
            Console.WriteLine("\nSaving model");
            string directoryPath = "TrainedModel";
            string filePath = Path.Combine(directoryPath, "TrainedModel.json");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory '{directoryPath}' created.");
            }

            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, jsonData);

            Console.WriteLine($"\nData has been saved to {filePath}");
        }

    }
}
