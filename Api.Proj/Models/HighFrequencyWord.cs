namespace WordFreqApi.Models
{
    public class HighFrequencyWord
    {
        public long HighFrequencyWordId {get; set;}
        public string Word { get; set;}
        public int Frequency { get; set;}
        public long SubmissionId {get; set;}
    }
}