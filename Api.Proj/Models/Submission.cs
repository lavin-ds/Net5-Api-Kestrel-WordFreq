using System.Collections.Generic;
namespace WordFreqApi.Models
{
    public class Submission
    {
        public long SubmissionId { get; set;}
        public string Submitter { get; set;}
        public string Content {get; set;}
        public virtual List<HighFrequencyWord> ContentFreq {get; set;}
        public string Comment {get; set;}
        public virtual Source Source {get; set;}
    }
}