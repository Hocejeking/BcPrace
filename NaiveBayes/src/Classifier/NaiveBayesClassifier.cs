using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes.Classifier
{
    public class NaiveBayesClassifier
    {
        Dictionary<EmailClass, Dictionary<string, decimal>> data;
        Dataset dataset;
        public NaiveBayesClassifier(Dictionary<EmailClass, Dictionary<string, decimal>> data, Dataset dataset)
        {
            this.data = data;
            this.dataset = dataset;
        }

        public bool Predict(string[] message)
        {
            int vocabularySize = data[EmailClass.SPAM].Keys.Union(data[EmailClass.HAM].Keys).Count();
            double Spam = Math.Log((double)dataset.priorPropabilitySpam);
            double Ham = Math.Log((double)dataset.priorPropabilityHam);

            foreach (var word in message)
            {
                double spamProb = (data[EmailClass.SPAM].ContainsKey(word)
                    ? (double)data[EmailClass.SPAM][word]
                    : 0) + 1.0 / (dataset.spamWordsCount + vocabularySize);

                double hamProb = (data[EmailClass.HAM].ContainsKey(word)
                    ? (double)data[EmailClass.HAM][word]
                    : 0) + 1.0 / (dataset.hamWordsCount + vocabularySize);

                Spam += Math.Log(spamProb);
                Ham += Math.Log(hamProb);
            }

            Console.WriteLine("PriorSpam: " + Spam + " PriorHam: " + Ham);
            return Spam > Ham;
        }
    }
}
