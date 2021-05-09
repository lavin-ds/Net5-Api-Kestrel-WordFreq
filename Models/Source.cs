using System;
namespace WordFreqApi.Models
{
    public class Source
    {
        public long SourceId {get; set;}
        public string Url { get; set;}
        public DateTime Date { get; set;}
        public long SubmissionId {get; set;}
    }
}