using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NASAProj.Data.IRepositories;
using NASAProj.Domain.Configurations;
using NASAProj.Domain.Entities.Quizzes;
using NASAProj.Service.Exceptions;
using NASAProj.Service.Extensions;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Quizzes;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IMapper mapper;
        public QuestionService(IGenericRepository<Quiz> quizRepository, IGenericRepository<Question> questionRepository, IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
        }

        public async ValueTask<Question> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
        {
            var question = await questionRepository.AddAsync(mapper.Map<Question>(questionForCreationDTO));
            await questionRepository.SaveChangesAsync();
            return question;
        }

        public async ValueTask<bool> DeleteAsync(int id)
        {
            var question = (await questionRepository.GetAsync(q => q.Id == id));

            if (question is null)
                throw new HttpStatusCodeException(404, "Question not found");
            questionRepository.Delete(question);

            await questionRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<Question>> GetAllAsync(PaginationParams @params, Expression<Func<Question, bool>> expression = null)
        {
            var questions = questionRepository.GetAll(expression: expression, isTracking: false, includes: "Answers, Assets");
            return await questions.ToPagedList(@params).ToListAsync();
        }

        public async ValueTask<Question> GetAsync(Expression<Func<Question, bool>> expression)
        {
            var question = await questionRepository.GetAsync(expression, "Answers, Attachments");
            if (question is null)
                throw new HttpStatusCodeException(404, "Question Not Found");

            return mapper.Map<Question>(question);
        }

        public async ValueTask<Question> UpdateAsync(int id, QuestionForCreationDTO questionForCreationDTO)
        {
            var existQuestion = await questionRepository.GetAsync(q => q.Id == id);
            if (existQuestion is null)
                throw new HttpStatusCodeException(404, "Question not found");

            var quiz = await quizRepository.GetAsync(q => q.Id == questionForCreationDTO.QuizId);
            if (quiz is null)
                throw new HttpStatusCodeException(404, "Quiz not found");

            existQuestion.UpdatedAt = DateTime.UtcNow;
            existQuestion = questionRepository.Update(mapper.Map(questionForCreationDTO, existQuestion));
            await questionRepository.SaveChangesAsync();

            return existQuestion;
        }
    }
}
