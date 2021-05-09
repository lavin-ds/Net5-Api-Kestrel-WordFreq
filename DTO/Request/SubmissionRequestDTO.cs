using System.ComponentModel.DataAnnotations;
namespace WordFreqApi.DTO.Request
{
    public class SubmissionRequestDTO
    {
        [RequiredAttribute]
        public string Submitter { get; set;}
        [RequiredAttribute]
        public string Content {get; set;}
        [RequiredAttribute]
        public string Comment {get; set;}
        [RequiredAttribute]
        public SourceRequestDTO Source {get; set;}
    }
}