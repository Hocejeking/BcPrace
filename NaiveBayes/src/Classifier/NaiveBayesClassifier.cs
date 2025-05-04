using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes.Classifier
{
    public class NaiveBayesClassifier
    {
        Dictionary<EmailClass, Dictionary<string, double>> data;
        Dataset dataset;
        HamWordDictionary hamWordDictionary;
        SpamWordDictionary spamWordDictionary;
        public NaiveBayesClassifier(Dictionary<EmailClass, Dictionary<string, double>> data, Dataset dataset, HamWordDictionary hamDict, SpamWordDictionary spamDict)
        {
            this.data = data;
            this.dataset = dataset;
            this.hamWordDictionary = hamDict;
            this.spamWordDictionary = spamDict;
        }

        public bool Predict(string[] message)
        {
            int vocabularySize = (int) (hamWordDictionary.GetWordsCount() + spamWordDictionary.GetWordsCount());
            double Spam = Math.Log(dataset.priorPropabilitySpam) * 12;
            double Ham = Math.Log(dataset.priorPropabilityHam) * -12;
            double smoothingFactor = 1.0 / (dataset.spamWordsCount + vocabularySize);
            foreach (var word in message)
            {
                double rawSpamProb = spamWordDictionary.GetProbability(word);
                double spamProb = rawSpamProb > 0
                    ? rawSpamProb
                    : smoothingFactor;

                double rawHamProb = hamWordDictionary.GetProbability(word);
                double hamProb = rawHamProb > 0
                    ? rawHamProb
                    : smoothingFactor;

                //Console.WriteLine($"Ham prob: {hamProb}, Spam prob: {spamProb}, RawHam: {rawHamProb}, RawSpam: {spamProb}");

                Spam += Math.Log(spamProb);
                Ham += Math.Log(hamProb);
                //Console.WriteLine($"Spam: {Spam}, Ham: {Ham},Ham prob: {hamProb}, Spam prob: {spamProb}");
            }

            double maxLog = Math.Max(Spam, Ham);
            double expSpam = Math.Exp(Spam - maxLog);
            double expHam = Math.Exp(Ham - maxLog);
            double total = expSpam + expHam;
            double spamProbability = expSpam / total;
            Console.WriteLine(spamProbability);
            return spamProbability > 0.99;
        }

    }
}
