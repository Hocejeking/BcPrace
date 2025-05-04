using NaiveBayes.src.Classifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes.src.Utils
{
    public class textDeserializer
    {
        string path;
        ConfusionMatrix confusionMatrix;
        public textDeserializer(string path, ConfusionMatrix confusionMatrix)
        {
            this.path = path;
            this.confusionMatrix = confusionMatrix;
        }

        public void Write(ConfusionMatrix confusionMatrix)
        {
            string directoryPath = "TrainedModel";
            string filePath = Path.Combine(directoryPath, "data.text");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Console.WriteLine($"Directory '{directoryPath}' created.");
            }

            string dataToAppend = $"True positive: {confusionMatrix.TruePositive}\n" +
                          $"False positive: {confusionMatrix.FalsePositive}\n" +
                          $"True negative: {confusionMatrix.TrueNegative}\n" +
                          $"False negative: {confusionMatrix.FalseNegative}\n" +
                          $"Sensitivity: {confusionMatrix.Sensitivity}\n" +
                          $"Specificity: {confusionMatrix.Specificity}\n" +
                          $"Precision: {confusionMatrix.Precision}\n" +
                          $"Negative predictive value: {confusionMatrix.NegativePredictiveValue}\n" +
                          $"Accuracy: {confusionMatrix.Accuracy}\n" +
                          "-------------------------------------------------\n";
            File.AppendAllText(filePath, dataToAppend);
            Console.WriteLine("Saved data");
        }
        
    }
}
