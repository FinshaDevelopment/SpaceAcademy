using AutoMapper;
using NASAProj.Domain.Entities.Quizzes;
using NASAProj.Domain.Models;
using NASAProj.Service.DTOs.Users;
using ZaminEducation.Service.DTOs.Quizzes;

namespace NASAProj.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForCreationDTO>().ReverseMap();
            CreateMap<User, UserForUpdateDTO>().ReverseMap();
            CreateMap<Question, QuestionAnswerForCreationDTO>().ReverseMap();
            CreateMap<Question, QuestionForCreationDTO>().ReverseMap();
            CreateMap<QuizResult, QuizResultForCreationDTO>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerForCreationDTO>().ReverseMap();
        }
    }
}
