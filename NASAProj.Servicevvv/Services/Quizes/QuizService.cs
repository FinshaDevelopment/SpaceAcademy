using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NASAProj.Data.IRepositories;
using NASAProj.Domain.Entities.Quizzes;
using NASAProj.Service.Exceptions;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Quizzes;
using ZaminEducation.Service.Interfaces;

namespace ZaminEducation.Service.Services
{
    public class QuizService : IQuizService
    {
        private readonly IGenericRepository<Quiz> quizRepository;
        private readonly IGenericRepository<Question> questionRepository;
        private readonly IGenericRepository<QuestionAnswer> answerRepository;
        private readonly IMapper mapper;

        public QuizService(IGenericRepository<Quiz> quizRepository,
            IGenericRepository<Question> quizContentRepository,
            IGenericRepository<QuestionAnswer> answerRepository,
            IMapper mapper)
        {
            this.quizRepository = quizRepository;
            this.questionRepository = quizContentRepository;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
        }

        public async ValueTask<Quiz> CreateAsync(QuizForCreationDTO quizDto)
        {
            var quiz = mapper.Map<Quiz>(quizDto);

            await quizRepository.AddAsync(quiz);
            await quizRepository.SaveChangesAsync();

            return quiz;
        }

        public async ValueTask<QuestionAnswer> CreateAnswerAsync(
            QuestionAnswerForCreationDTO dto)
        {
            var content = await questionRepository.GetAsync(c => c.Id == dto.QuestionId);

            if (content is null)
                throw new HttpStatusCodeException(404, "Question not found");

            var answer = mapper.Map<QuestionAnswer>(dto);

            await answerRepository.AddAsync(answer);
            await answerRepository.SaveChangesAsync();

            return answer;
        }

        public async ValueTask<IEnumerable<Quiz>> GetAllAsync(
            Expression<Func<Quiz, bool>> expression = null)
        {
            return await quizRepository.GetAll(expression, isTracking: false).ToListAsync();
        }

        public async ValueTask<Quiz> GetAsync(long quizId)
        {
            var quiz = await quizRepository.GetAsync(q => q.Id == quizId, "Questions");

            if (quiz is null)
                throw new HttpStatusCodeException(404, "Quiz not found");

            return quiz;
        }

        public async ValueTask<Quiz> UpdateAsync(
            long quizId,
            QuizForCreationDTO quizForCreationDto)
        {
            var quizexists =
                await quizRepository.GetAsync(q => q.Id.Equals(quizId));

            if (quizexists is null)
                throw new HttpStatusCodeException(404, "Quiz not found");


            var quiz = mapper.Map<Quiz>(quizForCreationDto);
            quiz.Update();

            quizRepository.Update(quiz);
            await quizRepository.SaveChangesAsync();

            return quiz;
        }

        public async ValueTask<QuestionAnswer> UpdateAnswerAsync(
            long answerId,
            QuestionAnswerForCreationDTO answerForCreationDto)
        {
            var quizContent = await questionRepository.GetAsync(qc => qc.Id.Equals(answerForCreationDto.QuestionId));
            if (quizContent is null)
                throw new HttpStatusCodeException(404, "Quiz content not found");

            var answer = await answerRepository.GetAsync(a => a.Id.Equals(answerId));
            if (answer is null)
                throw new HttpStatusCodeException(404, "Answer not found");

            answer = mapper.Map<QuestionAnswer>(answerForCreationDto);
            answer.Update();

            answerRepository.Update(answer);
            await answerRepository.SaveChangesAsync();

            return answer;
        }

        public async ValueTask<bool> DeleteAnswerAsync(Expression<Func<QuestionAnswer, bool>> expression)
        {
            var answer = await answerRepository.GetAsync(expression);
            if (answer is null)
                throw new HttpStatusCodeException(404, "Answer not found");

            answerRepository.Delete(answer);
            await answerRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<bool> DeleteAsync(Expression<Func<Quiz, bool>> expression)
        {
            var quiz = await quizRepository.GetAsync(expression);
            if (quiz is null)
                throw new HttpStatusCodeException(404, "Quiz not found");

            quizRepository.Delete(quiz);
            await quizRepository.SaveChangesAsync();

            return true;
        }
    }
}
