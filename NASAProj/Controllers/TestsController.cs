using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NASAProj.Controllers;
using NASAProj.Domain.Configurations;
using NASAProj.Service.DTOs.Quizzes;
using NASAProj.Service.Interfaces;
using ZaminEducation.Api.Helpers;

namespace ZaminEducation.Api.Controllers
{
    public class TestsController : BaseApiController
    {
        private readonly IQuizService quizService;
        private readonly IQuizResultService quizResultService;

        public TestsController(IQuizService quizService, IQuizResultService quizResultService)
        {
            this.quizService = quizService;
            this.quizResultService = quizResultService;
        }

        /// <summary>
        /// Create test
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async ValueTask<IActionResult> CreateAsync(QuizForCreationDTO quizForCreationDto)
            => Ok(await quizService.CreateAsync(quizForCreationDto));

        /// <summary>
        /// Get test by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await quizService.GetAsync(id));

        [HttpGet, Authorize]
        public async ValueTask<IActionResult> GetAllAsync()
            => Ok(await quizService.GetAllAsync());
        /// <summary>
        /// Create answer
        /// </summary>
        /// <param name="questionAnswerForCreationDto"></param>
        /// <returns></returns>
        [HttpPost("answers"), Authorize]
        public async ValueTask<IActionResult> CreateAnswerAsync(QuestionAnswerForCreationDTO questionAnswerForCreationDto)
            => Ok(await quizService.CreateAnswerAsync(questionAnswerForCreationDto));

        /// <summary>
        /// Update test
        /// </summary>
        /// <returns></returns>
        [HttpPut("/api/tests/{testId}"), Authorize]
        public async ValueTask<IActionResult> UpdateAsync(int testId,QuizForCreationDTO quizForCreationDto)
                => Ok(await quizService.UpdateAsync(testId,quizForCreationDto));

        /// <summary>
        /// Update answer
        /// </summary>
        /// <param name="answerId"></param>
        /// <param name="questionAnswerForCreationDto"></param>
        /// <returns></returns>
        [HttpPut("/api/tests/answers/{answerId}"), Authorize]
        public async ValueTask<IActionResult> UpdateAnswerAsync(int answerId,
                                                QuestionAnswerForCreationDTO questionAnswerForCreationDto)
            => Ok(await quizService.UpdateAnswerAsync(answerId, questionAnswerForCreationDto));

        /// <summary>
        /// Remove test
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/tests/{testId}"), Authorize]
        public async ValueTask<IActionResult> RemoveAsync([FromRoute(Name = "testId")] int id)
            => Ok(await quizService.DeleteAsync(q => q.Id == id));

        /// <summary>
        /// Remove asnwer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/tests/answers/{answerId}"), Authorize]
        public async ValueTask<IActionResult> RemoveAnswerAsync([FromRoute(Name = "answerId")] int id)
            => Ok(await quizService.DeleteAnswerAsync(qa => qa.Id == id));

        /// <summary>
        /// Check quiz result
        /// </summary>
        /// <param name="userSelectionDtos"></param>
        /// <returns></returns>
        [HttpPost("results"), Authorize]
        public async ValueTask<IActionResult> CreateAsync(IEnumerable<UserSelectionDTO> userSelectionDtos)
            => Ok(await quizResultService.CreateAsync(userSelectionDtos));

        /// <summary>
        /// Get quiz result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("results/{userId}"), Authorize]
        public async ValueTask<IActionResult> GetResultAsync([FromRoute(Name = "userId")] int id)
            => Ok(await quizResultService.GetAsync(qr => qr.UserId == id));

        /// <summary>
        /// Get all quiz results
        /// </summary>
        /// <param name="id"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet("results/{userId}/collection"), Authorize]
        public async ValueTask<IActionResult> GetAllResultsAsync([FromRoute(Name = "userId")] int id, [FromQuery] PaginationParams @params)
            => Ok(await quizResultService.GetAllAsync(qr => qr.UserId == id, @params));
    }
}
