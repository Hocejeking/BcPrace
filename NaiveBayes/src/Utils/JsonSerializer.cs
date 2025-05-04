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
        public static void Serialization(Dictionary<string, double> hamProbs, Dictionary<string, double> spamProbs)
        {
            var data = new Dictionary<EmailClass, Dictionary<string, double>>
            {
                { EmailClass.HAM, hamProbs},
                { EmailClass.SPAM, spamProbs},
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

        public static Dictionary<EmailClass, Dictionary<string, double>> Deserialization()
        {
            string filePath = "TrainedModel/TrainedModel.json";
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File does not exist", filePath);
            }
            string jsonData = File.ReadAllText(filePath);
            var model = JsonConvert.DeserializeObject<Dictionary<EmailClass, Dictionary<string, double>>>(jsonData);
            return model;
        }
    }


}
