using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayes.src.Classifier
{
    public class ConfusionMatrix
    {
        private int truePositive, trueNegative, falsePositive, falseNegative;
        private double precision, negativePredictiveValue, accuracy, specificity, sensitivity;

        public ConfusionMatrix(int truePositive, int trueNegative, int falsePositive, int falseNegative)
        {
            this.TruePositive = truePositive;
            this.TrueNegative = trueNegative;
            this.FalsePositive = falsePositive;
            this.FalseNegative = falseNegative;
            calculateStats();
        }

        public int TruePositive { get => truePositive; set => truePositive = value; }
        public int TrueNegative { get => trueNegative; set => trueNegative = value; }
        public int FalsePositive { get => falsePositive; set => falsePositive = value; }
        public int FalseNegative { get => falseNegative; set => falseNegative = value; }
        public double Precision { get => precision; set => precision = value; }
        public double NegativePredictiveValue { get => negativePredictiveValue; set => negativePredictiveValue = value; }
        public double Accuracy { get => accuracy; set => accuracy = value; }
        public double Specificity { get => specificity; set => specificity = value; }
        public double Sensitivity { get => sensitivity; set => sensitivity = value; }

        private void calculateStats()
        {
            // Avoid dividing by zero
            double TP_FN = TruePositive + FalseNegative;
            double TN_FP = TrueNegative + FalsePositive;
            double TP_FP = TruePositive + FalsePositive;
            double TN_FN = TrueNegative + FalseNegative;
            double total = TruePositive + TrueNegative + FalsePositive + FalseNegative;

            // Sensitivity = TP / (TP + FN)
            Sensitivity = TP_FN == 0 ? Double.NaN : (TruePositive / TP_FN) * 100;

            // Specificity = TN / (TN + FP)
            Specificity = TN_FP == 0 ? Double.NaN : (TrueNegative / TN_FP) * 100;

            // Precision = TP / (TP + FP)
            Precision = TP_FP == 0 ? Double.NaN : (TruePositive / TP_FP) * 100;

            // Negative Predictive Value = TN / (TN + FN)
            NegativePredictiveValue = TN_FN == 0 ? Double.NaN : (TrueNegative / TN_FN) * 100;

            // Accuracy = (TP + TN) / (TP + TN + FP + FN)
            Accuracy = total == 0 ? Double.NaN : (TruePositive + TrueNegative) / total * 100;
        }
    }
}
