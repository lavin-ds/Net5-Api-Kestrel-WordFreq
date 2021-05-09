using System;
using System.ComponentModel.DataAnnotations;

namespace WordFreqApi.DTO.Request
{
    public class SourceRequestDTO
    {
        [RequiredAttribute]
        public string Url { get; set;}
        [RequiredAttribute]
        public DateTime Date { get; set;}
    }
}