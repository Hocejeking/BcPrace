public class Dataset
{
    public Dataset(int numOfEmails, int numOfSpamEmails, int numOfHamEmails, int hamWordsCount, int spamWordsCount)
    {
        this.numOfEmails = numOfEmails;
        this.numOfSpamEmails = numOfSpamEmails;
        this.numOfHamEmails = numOfHamEmails;
        this.priorPropabilitySpam = (double) numOfSpamEmails / (double) numOfEmails;
        this.priorPropabilityHam = (double) numOfHamEmails / (double)numOfEmails;
        this.hamWordsCount = hamWordsCount;
        this.spamWordsCount = spamWordsCount;
    }

    public readonly int hamWordsCount;
    public readonly int spamWordsCount;
    public readonly int numOfEmails;
    public readonly int numOfSpamEmails;
    public readonly int numOfHamEmails;
    public double priorPropabilitySpam;
    public double priorPropabilityHam;
}