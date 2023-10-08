using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NASAProj.Controllers;
using NASAProj.Domain.Configurations;
using NASAProj.Service.DTOs.Quizzes;
using NASAProj.Service.Interfaces;
using ZaminEducation.Api.Helpers;

namespace ZaminEducation.Api.Controllers
{
    public class QuestionController : BaseApiController
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        /// <summary>
        /// Create new question {Admin)
        /// </summary>
        /// <param name="questionForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public async ValueTask<IActionResult> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
            => Ok(await questionService.CreateAsync(questionForCreationDTO));

        [HttpPut("{id}"), Authorize]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id, QuestionForCreationDTO questionForCreationDTO)
          => Ok(await questionService.UpdateAsync(id, questionForCreationDTO));

        /// <summary>
        /// Get all questions {Everyone}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await questionService.GetAllAsync(@params));


        /// <summary>
        /// Get a question {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await questionService.GetAsync(u => u.Id == id));


        /// <summary>
        /// Delete a question {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await questionService.DeleteAsync(id));
    }
}
