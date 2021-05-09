using System.Collections.Generic;
namespace WordFreqApi.Models
{
    public class Submission
    {
        public long SubmissionId { get; set;}
        public string Submitter { get; set;}
        public string Content {get; set;}
        public List<HighFrequencyWord> ContentFreq {get; set;}
        public string Comment {get; set;}
        public Source Source {get; set;}
    }
}