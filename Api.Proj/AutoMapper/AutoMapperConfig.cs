using System.Reflection.PortableExecutable;
using AutoMapper;
using WordFreqApi.DTO.Response;
using WordFreqApi.Models;
namespace WordFreqApi.AutoMapperConfig
{
    public static class AutoMapperConfig
    {
        private static MapperConfiguration _config;
        public static MapperConfiguration Configuration { get => _config; set => _config = value; }

        public static void Register() 
        {
            _config = new MapperConfiguration(cfg=>
            {
                cfg.AddProfile<SubmissionProfile>();
            });
        }
    }

    public class SubmissionProfile : Profile
    {

    }
}