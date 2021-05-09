using System.Collections.Generic;

namespace WordFreqApi.DTO.Response
{
    public class SubmissionResponseDTO
    {
        public long SubmissionID{get;set;}
        public string Submitter { get; set;}
        public string Content {get; set;}
        public List<HighFrequencyWordDTO> HfWords{get; set;}
        public string Comment {get; set;}
        public SourceResponseDTO Source {get; set;}
    }
}