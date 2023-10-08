using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NASAProj.Data.IRepositories;
using NASAProj.Domain.Configurations;
using NASAProj.Domain.Entities.Quizzes;
using NASAProj.Service.DTOs;
using NASAProj.Service.Exceptions;
using NASAProj.Service.Extensions;
using NASAProj.Service.Helpers;
using System.Linq.Expressions;
using ZaminEducation.Service.DTOs.Quizzes;
using ZaminEducation.Service.DTOs.UserCourses;
using ZaminEducation.Service.Interfaces;
using ZaminEducation.Service.ViewModels.Quizzes;

namespace ZaminEducation.Service.Services;

public class QuizResultService : IQuizResultService
{
    private readonly IGenericRepository<QuestionAnswer> _questionAnswerRepository;
    private readonly IGenericRepository<QuizResult> _quizResultRepository;
    private readonly IGenericRepository<Quiz> _quizRepository;
    private readonly IConfiguration _configuration;
    private readonly ICertificateService _certificateService;

    private QuestionAnswer questionAnswer;
    private int countOfCorrectAnswers = 0;
    private readonly IMapper _mapper;
    private long courseId;

    public QuizResultService(IGenericRepository<Quiz> quizRepository,
        IGenericRepository<QuestionAnswer> questionAnswerRepository,
        IMapper mapper, IConfiguration configuration,
        IGenericRepository<QuizResult> quizResultRepository,
        ICertificateService certificateService)
    {
        _questionAnswerRepository = questionAnswerRepository;
        _quizResultRepository = quizResultRepository;
        _certificateService = certificateService;
        _quizRepository = quizRepository;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async ValueTask<IEnumerable<QuizResult>> GetAllAsync(
        Expression<Func<QuizResult, bool>> expression,
        PaginationParams @params)
    {
        var pagedList =
            _quizResultRepository.GetAll(expression, "User, Course", false).ToPagedList(@params);

        return await pagedList.ToListAsync();
    }
    public async ValueTask<QuizResult> GetAsync(Expression<Func<QuizResult, bool>> expression)
    {
        var existQuizResult = await _quizResultRepository.GetAsync(
            expression, "User, Course");

        if (existQuizResult is null)
            throw new HttpStatusCodeException(404, "QuizResult not found.");

        return existQuizResult;
    }

    public async ValueTask<UserQuizzesResultForViewDTO> CreateAsync(IEnumerable<UserSelectionDTO> dto)
    {
        var results = await CheckAsync(dto);

        double allowCertificatePersentage = double.Parse(_configuration["AllowCertificatePersentage"]);
        double userResult = GetTotalPersentage(dto.Count(), countOfCorrectAnswers);

        // certificate generation
        if (allowCertificatePersentage <= userResult)
            await _certificateService.CreateAsync(new CertificateForCreationDTO()
            {
                CourseId = courseId,
                UserId = HttpContextHelper.UserId,
                Result = new CertificateResultDTO()
                {
                    PassedPoint = $"{countOfCorrectAnswers}/{dto.Count()}",
                    Percentage = userResult
                }
            });

        // add result to database
        var quizResult = new QuizResultForCreationDTO()
        {
            Percentage = userResult,
            UserId = HttpContextHelper.UserId
        };

        await _quizResultRepository.AddAsync(_mapper.Map<QuizResult>(quizResult));
        await _quizResultRepository.SaveChangesAsync();

        return new()
        {
            Persentage = quizResult.Percentage,
            AcceptedQuizzes = countOfCorrectAnswers,
            quizResultViewModels = results,
            QuizzesCount = dto.Count()
        };
    }

    private async ValueTask<IEnumerable<QuizForViewDTO>> CheckAsync(IEnumerable<UserSelectionDTO> dto)
    {
        if (dto is null)
            throw new HttpStatusCodeException(400, "Quiz must not be empty.");

        ICollection<QuizForViewDTO> results = new List<QuizForViewDTO>();

        var quiz = await _quizRepository.GetAsync(q => q.Id == dto.First().QuestionId);

        var quizzes = _quizRepository.GetAll(c => c.Id == courseId);

        if (quizzes.All(q => q.Id == quiz.Id))
            throw new HttpStatusCodeException(400, "Quiz must belong to this course.");

        foreach (var UserSelectionDTO in dto)
        {
            bool isCorrect = false;

            questionAnswer =
                await _questionAnswerRepository.GetAsync(a => a.Id == UserSelectionDTO.AnswerId);

            if (questionAnswer is not null
                && questionAnswer.QuestionId == UserSelectionDTO.QuestionId
                && questionAnswer.IsCorrect)
            {
                countOfCorrectAnswers++;
                isCorrect = true;
            }
            else if (questionAnswer is not null && questionAnswer.QuestionId != UserSelectionDTO.QuestionId)
                throw new HttpStatusCodeException(400, "Answers are incorrect.");

            results.Add(new QuizForViewDTO()
            {
                IsCorrect = isCorrect,
                Choice = questionAnswer,
                Quiz = quizzes.FirstOrDefault(q => q.Id == UserSelectionDTO.QuestionId)
            });
        }
        return results;
    }

    private double GetTotalPersentage(long total, int value)
        => Math.Round((double)value * 100 / (double)total, 2);
}
