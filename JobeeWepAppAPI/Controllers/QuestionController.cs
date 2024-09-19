using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace JobeeWepAppAPI.Controllers
{
    [ApiController]
    [Route("api/Interview")]
    public class QuestionController : Controller
    {
        private readonly IQuestionServices _services;

        public QuestionController(IQuestionServices services)
        {
            _services = services;
        }

        [HttpGet("question")]
        public async Task<IActionResult> GetQuestionByID(int id)
        {
            if (id == 0)
            {
                var allQuestions = await _services.getAllQuestion();
                if (allQuestions == null || !allQuestions.Any())
                {
                    return NotFound("Không tìm thấy bản ghi nào của question");
                }
                return Ok(allQuestions);
            }
            else if (id > 0)
            {
                var allQuestions = await _services.getQuestionByID(id);
                if (allQuestions == null || !allQuestions.Any())
                {
                    return NotFound($"Không tìm thấy question với ID {id}");
                }
                return Ok(allQuestions);
            }
            else
            {
                return BadRequest("ID không hợp lệ");
            }
        }
        [HttpPost("create-question")]
        public async Task AddQuestion([FromBody] InterviewQuestion question)
        {
            
        }
    }
}
