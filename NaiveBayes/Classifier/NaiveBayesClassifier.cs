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
            double priorSpam = Math.Log((double)dataset.priorPropabilitySpam);
            double priorHam = Math.Log((double)dataset.priorPropabilityHam);
            int vocabularySize = data[EmailClass.SPAM].Keys.Union(data[EmailClass.HAM].Keys).Count();
            foreach (var word in message)
            {
                decimal spamProb = 0;
                decimal hamProb = 0;
                spamProb = (decimal)((data[EmailClass.SPAM].ContainsKey(word)
                         ? (double)data[EmailClass.SPAM][word] + 1
                         : 1) / (dataset.spamWordsCount + vocabularySize));

                hamProb = (decimal)((data[EmailClass.HAM].ContainsKey(word)
                        ? (double)data[EmailClass.HAM][word] + 1
                        : 1) / (dataset.hamWordsCount + vocabularySize));

                priorSpam += Math.Log((double)Math.Max((double)spamProb, 1e-10));
                priorHam += Math.Log((double)Math.Max((double)hamProb, 1e-10));
            }

            return priorSpam > priorHam;
        }
    }
}
